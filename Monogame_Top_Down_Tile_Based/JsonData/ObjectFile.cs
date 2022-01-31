using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class ObjectFile
    {
        [JsonProperty("height")]
        public float Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("properties")]
        public List<PropertyFile> Properties { get; set; }

        [JsonProperty("rotation")]
        public float Rotation { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; }
        
        [JsonProperty("point")]
        public bool Point { get; set; }
        
        [JsonProperty("ellipse")]
        public bool Ellipse { get; set; }
        
        [JsonProperty("polygon")]
        public List<PolygonPositionFile> Polygon { get; set; }

        [JsonProperty("width")]
        public float Width { get; set; }

        [JsonProperty("x")]
        public float X { get; set; }

        [JsonProperty("y")]
        public float Y { get; set; }
    }
}