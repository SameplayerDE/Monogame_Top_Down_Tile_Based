using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class PolygonPositionFile
    {
        [JsonProperty("x")] public float X;
        [JsonProperty("y")] public float Y;
    }
}