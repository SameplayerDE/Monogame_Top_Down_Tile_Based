using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class AnimationFile
    {
        [JsonProperty("duration")] public int Duration;
        [JsonProperty("tileid")] public int TileId;
    }
}