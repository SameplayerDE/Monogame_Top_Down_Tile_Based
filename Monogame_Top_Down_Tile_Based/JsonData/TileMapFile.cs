using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonData
{
    public class TileMapFile
    {
        [JsonProperty("compressionlevel")]
        public int CompressionLevel { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("infinite")]
        public bool Infinite { get; set; }

        [JsonProperty("layers")]
        public List<TileMapLayerFile> Layers { get; set; }

        [JsonProperty("nextlayerid")]
        public int NextLayerId { get; set; }

        [JsonProperty("nextobjectid")]
        public int NextObjectId { get; set; }

        [JsonProperty("orientation")]
        public string Orientation { get; set; }

        [JsonProperty("renderorder")]
        public string RenderOrder { get; set; }

        [JsonProperty("tiledversion")]
        public string TiledVersion { get; set; }

        [JsonProperty("tileheight")]
        public int TileHeight { get; set; }

        [JsonProperty("tilesets")]
        public List<TileMapTileSetFile> Tilesets { get; set; }
        
        [JsonProperty("properties")]
        public List<PropertyFile> Properties { get; set; }

        [JsonProperty("tilewidth")]
        public int TileWidth { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public double Version { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
        
        public string Path { get; set; }
    }
}