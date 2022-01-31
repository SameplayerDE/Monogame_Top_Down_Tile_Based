using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileSetTileFile
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("properties")]
        public List<PropertyFile> Properties { get; set; }
        
        [JsonProperty("animation")]
        public List<AnimationFile> Animations { get; set; }
    }
}