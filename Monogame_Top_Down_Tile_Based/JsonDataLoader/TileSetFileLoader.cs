using System.IO;
using Monogame_Top_Down_Tile_Based.JsonData;
using Newtonsoft.Json;

namespace Monogame_Top_Down_Tile_Based.JsonDataLoader
{
    public static class TileSetFileLoader
    {
        public static TileSetFile LoadTileset(string path)
        {
            var result = JsonConvert.DeserializeObject<TileSetFile>(File.ReadAllText(path));
            if (result == null) return null;
            result.Path = path;
            return result;
        }
    }
}