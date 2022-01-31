using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Top_Down_Tile_Based.Enums;
using Monogame_Top_Down_Tile_Based.Graphics;
using Monogame_Top_Down_Tile_Based.JsonData;
using Monogame_Top_Down_Tile_Based.JsonDataLoader;

namespace Monogame_Top_Down_Tile_Based.DataStructure
{
    public class TileMap
    {
        public int Height;
        public int Width;
        public int TileWidth;
        public int TileHeight;
        public bool Infinite;
        public RenderOrder RenderOrder;

        public List<TileMapTileSet> TileMapTileSets;
        public List<TileMapTileLayer> TileMapTileLayers;
        public List<TileMapObjectLayer> TileMapObjectLayers;
        public List<PropertyFile> Properties;

        public TileMap Right;
        public TileMap Up;
        public TileMap Left;
        public TileMap Down;

        public int NextLayerId;

        public bool HasProperty(string value)
        {
            return Properties.Any(propertyFile => propertyFile.Name == value);
        }
        
        public T GetValue<T>(string value)
        {
            for (var index = Properties.Count - 1; index >= 0; index--)
            {
                var propertyFile = Properties[index];
                if (propertyFile.Name == value)
                {
                    return (T)propertyFile.Value;
                }
            }

            return default;
        }


        public static TileMap FromTileMapFile(TileMapFile tileMapFile, bool neighbours = true)
        {
            var result = new TileMap
            {
                Height = tileMapFile.Height,
                Width = tileMapFile.Width,
                Infinite = tileMapFile.Infinite,
                RenderOrder = RenderOrderHelper.FromString(tileMapFile.RenderOrder),
                TileHeight = tileMapFile.TileHeight,
                TileWidth = tileMapFile.TileWidth,
                TileMapTileLayers = new List<TileMapTileLayer>(),
                TileMapTileSets = new List<TileMapTileSet>(),
                TileMapObjectLayers = new List<TileMapObjectLayer>(),
                Properties = tileMapFile.Properties
            };

            if (neighbours)
            {
                var right = string.Empty;
                var up = string.Empty;
                var left = string.Empty;
                var down = string.Empty;

                if (tileMapFile.Properties != null)
                {
                    foreach (var property in tileMapFile.Properties)
                    {
                        if (property.Type == "file")
                        {
                            switch (property.Name.ToLower())
                            {
                                case "right":
                                    right = (string)property.Value;
                                    break;
                                case "left":
                                    left = (string)property.Value;
                                    break;
                                case "up":
                                    up = (string)property.Value;
                                    break;
                                case "down":
                                    down = (string)property.Value;
                                    break;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(right))
                {
                    result.Right = FromTileMapFile(
                        MapLoader.LoadTileMapFile($@"Content\Regions\{right}"),
                        false
                    );
                }

                if (!string.IsNullOrEmpty(left))
                {
                    result.Left = FromTileMapFile(
                        MapLoader.LoadTileMapFile($@"Content\Regions\{left}"),
                        false
                    );
                }

                if (!string.IsNullOrEmpty(up))
                {
                    result.Up = FromTileMapFile(
                        MapLoader.LoadTileMapFile($@"Content\Regions\{up}"),
                        false
                    );
                }

                if (!string.IsNullOrEmpty(down))
                {
                    result.Down = FromTileMapFile(
                        MapLoader.LoadTileMapFile($@"Content\Regions\{down}"),
                        false
                    );
                }
            }

            foreach (var mapTileSetFile in tileMapFile.Tilesets)
            {
                var tileMapTileSet = new TileMapTileSet();
                tileMapTileSet.Source = Path.GetFullPath(
                    Path.Join(
                        Path.GetDirectoryName(tileMapFile.Path) ?? string.Empty,
                        mapTileSetFile.Source
                    )
                );
                tileMapTileSet.FirstGid = mapTileSetFile.FirstGid;
                result.TileMapTileSets.Add(tileMapTileSet);
            }

            foreach (var tileMapLayerFile in tileMapFile.Layers)
            {
                switch (tileMapLayerFile.Type)
                {
                    case "tilelayer":
                    {
                        var tileMapTileLayer = TileMapTileLayer.FromTileMapLayerFile(tileMapLayerFile);
                        result.TileMapTileLayers.Add(tileMapTileLayer);
                        break;
                    }
                    case "objectgroup":
                    {
                        var tileMapObjectLayer = TileMapObjectLayer.FromTileMapLayerFile(tileMapLayerFile);
                        result.TileMapObjectLayers.Add(tileMapObjectLayer);
                        break;
                    }
                }
            }

            return result;
        }

        public void LoadTileSets(GraphicsDevice graphicsDevice)
        {
            Right?.LoadTileSets(graphicsDevice);
            Left?.LoadTileSets(graphicsDevice);
            Up?.LoadTileSets(graphicsDevice);
            Down?.LoadTileSets(graphicsDevice);
            foreach (var tileMapTileSet in TileMapTileSets)
            {
                var tileSetFile = TileSetFileLoader.LoadTileset(tileMapTileSet.Source);
                var tileSet = TileSet.FromTileSetFile(tileSetFile);
                tileSet.LoadImage(graphicsDevice);
                tileMapTileSet.TileSet = tileSet;
            }
        }

        public TileMapTileSet GetTileSetByTileIndex(int index)
        {
            foreach (var tileMapTileSet in TileMapTileSets)
            {
                if (index >= tileMapTileSet.FirstGid &&
                    index < tileMapTileSet.TileSet.TileCount + tileMapTileSet.FirstGid)
                {
                    return tileMapTileSet;
                }
            }

            return null;
        }
    }
}