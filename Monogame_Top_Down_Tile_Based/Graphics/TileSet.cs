using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Top_Down_Tile_Based.DataStructure;
using Monogame_Top_Down_Tile_Based.JsonData;

namespace Monogame_Top_Down_Tile_Based.Graphics
{
    public class TileSet
    {
        public string Name;
        public string ImagePath;
        public int Columns;
        public int TileWidth;
        public int TileHeight;
        public int TileCount;

        public List<TileSetTileFile> Tiles;

        public Texture2D Texture2D;

        public static TileSet FromTileSetFile(TileSetFile tileSetFile)
        {
            var result = new TileSet
            {
                Name = tileSetFile.Name,
                ImagePath = Path.Combine(Path.GetDirectoryName(tileSetFile.Path) ?? string.Empty, tileSetFile.Image),
                Columns = tileSetFile.Columns,
                TileWidth = tileSetFile.TileWidth,
                TileHeight = tileSetFile.TileHeight,
                TileCount = tileSetFile.TileCount,
                Tiles = tileSetFile.Tiles
            };
            return result;
        }

        public Rectangle GetTileSection(int x, int y)
        {
            return GetTileSection(x + Columns * y);
        }
        
        public Rectangle GetTileSection(int index)
        {
            var x = index % Columns;
            var y = index / Columns;
            var result = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
            return result;
        }
        
        public List<AnimationFile> GetTileAnimationFile(int index)
        {
            foreach (var tileFile in Tiles)
            {
                if (tileFile.Id == index)
                {
                    if (tileFile.Animations != null)
                    {
                        return tileFile.Animations;
                    }
                }
            }

            return null;
        }

        public void LoadImage(GraphicsDevice graphicsDevice)
        {
            if (!File.Exists(ImagePath)) throw new FileNotFoundException();
            var filestream = File.Open(ImagePath, FileMode.Open);
            Texture2D = Texture2D.FromStream(graphicsDevice, filestream);
            filestream.Close();
            filestream.Dispose();
        }

        public void Dispose()
        {
            Texture2D.Dispose();
        }
    }
}