using System.Collections.Generic;
using Monogame_Top_Down_Tile_Based.JsonData;

namespace Monogame_Top_Down_Tile_Based.DataStructure
{
    public class TileMapObjectLayer
    {
        public List<ObjectFile> Objects;
        public int X;
        public int Y;
        public int Height;
        public int Width;
        public int Id;
        public string Name;
        public float Opacity;
        public bool Visible;
        public List<PropertyFile> Properties;
        
        public static TileMapObjectLayer FromTileMapLayerFile(TileMapLayerFile tileMapLayerFile)
        {
            var result = new TileMapObjectLayer
            {
                Objects = tileMapLayerFile.Objects,
                X = tileMapLayerFile.X,
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