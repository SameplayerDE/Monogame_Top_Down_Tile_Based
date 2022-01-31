using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileMapLayerFile
    {
        [JsonProperty("data")] public List<int> Data { get; set; }

        [JsonProperty("height")] public int Height { get; set; }

        [JsonProperty("width")] public int Width { get; set; }

        [JsonProperty("parallaxx")] public float ParallaxX { get; set; } = 1f;

        [JsonProperty("parallaxy")] public float ParallaxY { get; set; } = 1f;

        [JsonProperty("objects")] public List<ObjectFile> Objects { get; set; }

        [JsonProperty("properties")] public List<PropertyFile> Properties { get; set; }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("opacity")] public float Opacity { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("visible")] public bool Visible { get; set; }

        [JsonProperty("x")] public int X { get; set; }

        [JsonProperty("y")] public int Y { get; set; }
    }
}