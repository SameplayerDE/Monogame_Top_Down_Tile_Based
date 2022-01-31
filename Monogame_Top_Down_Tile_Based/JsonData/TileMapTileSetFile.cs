using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileMapTileSetFile
    {
        [JsonProperty("firstgid")]
        public int FirstGid { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}