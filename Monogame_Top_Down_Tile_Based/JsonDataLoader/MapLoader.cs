using System.IO;
using System.Linq;
using Monogame_Top_Down_Tile_Based.JsonData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monogame_Top_Down_Tile_Based.JsonDataLoader
{
    public static class MapLoader
    {
        public static void LoadRegion(string path, out int[] data, out int width, out int height)
        {
            data = null;
            width = 0;
            height = 0;
            if (!File.Exists(path)) return;
            using var file = File.OpenText(path);
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            using var reader = new JsonTextReader(file);

            var jsonObject000 = (JObject)JToken.ReadFrom(reader);

            var jsonLayers = jsonObject000["layers"];
            if (jsonLayers != null)
            {
                for (var layerIndex = 0; layerIndex < jsonLayers.Count(); layerIndex++)
                {
                    var jsonLayer = jsonLayers[layerIndex];
                    var layerWidth = (int)jsonLayer?["width"];
                    var layerHeight = (int)jsonLayer?["height"];
                    var layerData = jsonLayer?["data"];

                    width = layerWidth;
                    height = layerHeight;

                    if (layerData == null) return;

                    var layerDataArray = layerData.Values<int>().ToArray();
                    data = layerDataArray;
                }
            }

            reader.Close();
            file.Dispose();
        }

        public static TileMapFile LoadTileMapFile(string path)
        {
            var result = JsonConvert.DeserializeObject<TileMapFile>(File.ReadAllText(path));
            if (result == null) return null;
            result.Path = path;
            return result;
        }
    }
}