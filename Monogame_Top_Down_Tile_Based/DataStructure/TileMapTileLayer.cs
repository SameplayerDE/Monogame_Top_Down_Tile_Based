using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monogame_Top_Down_Tile_Based.JsonData;

namespace Monogame_Top_Down_Tile_Based.DataStructure
{
    public class TileMapTileLayer
    {
        public List<int> Data;
        public int X;
        public int Y;
        public int Height;
        public int Width;
        public float ParallaxX;
        public float ParallaxY;
        public int Id;
        public string Name;
        public float Opacity;
        public bool Visible;
        public List<PropertyFile> Properties;
        public Vector2 Parallax => new Vector2(ParallaxX, ParallaxY);

        public static TileMapTileLayer FromTileMapLayerFile(TileMapLayerFile tileMapLayerFile)
        {
            var result = new TileMapTileLayer
            {
                Data = tileMapLayerFile.Data,
                Height = tileMapLayerFile.Height,
                Width = tileMapLayerFile.Width,
                X = tileMapLayerFile.X,
                ParallaxX = tileMapLayerFile.ParallaxX,
                ParallaxY = tileMapLayerFile.ParallaxY,
                Y = tileMapLayerFile.Y,
                Id = tileMapLayerFile.Id,
                Name = tileMapLayerFile.Name,
                Opacity = tileMapLayerFile.Opacity,
                Visible = tileMapLayerFile.Visible,
                Properties = tileMapLayerFile.Properties
            };
            return result;
        }
    }
}