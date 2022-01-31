using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileMapObjectLayerFile
    {
        [JsonProperty("objects")]
        public List<ObjectFile> Data { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("opacity")]
        public float Opacity { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}