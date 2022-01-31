using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class PropertyFile
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}