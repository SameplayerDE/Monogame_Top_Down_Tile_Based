using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileSetFile
    {
        [JsonProperty("columns")]
        public int Columns { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imageheight")]
        public int ImageHeight { get; set; }

        [JsonProperty("imagewidth")]
        public int ImageWidth { get; set; }

        [JsonProperty("margin")]
        public int Margin { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("objectalignment")]
        public string ObjectAlignment { get; set; }

        [JsonProperty("spacing")]
        public int Spacing { get; set; }

        [JsonProperty("tilecount")]
        public int TileCount { get; set; }

        [JsonProperty("tiledversion")]
        public string TiledVersion { get; set; }

        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }

        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public double Version { get; set; }
        
        [JsonProperty("tiles")]
        public List<TileSetTileFile> Tiles { get; set; }

        public string Path { get; set; }
    }
}