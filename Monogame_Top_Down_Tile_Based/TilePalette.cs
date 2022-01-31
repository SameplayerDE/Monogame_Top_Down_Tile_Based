using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Monogame_Top_Down_Tile_Based.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class TilePalette
    {
        public GameTexture2D GameTexture2D;
        public List<Tile> Tiles;
        
        public int Count => Tiles.Count;
        public int CountX;
        public int CountY;

        public Tile Get(int index)
        {
            index = Math.Clamp(index, 0, Count - 1);
            return Tiles[index];
        }

        public Tile Get(int x, int y)
        {
            x = Math.Clamp(x, 0, CountX - 1);
            y = Math.Clamp(y, 0, CountY - 1);
            return Get(x + CountX * y);
        }
        
        public static TilePalette Generate(int x, int y, GameTexture2D gameTexture2D)
        {
            var result = new TilePalette();

            result.Tiles = new List<Tile>();
            result.GameTexture2D = gameTexture2D;

            var tilesX = gameTexture2D.Texture2D.Bounds.Size.X / x;
            var tilesY = gameTexture2D.Texture2D.Bounds.Size.Y / y;

            result.CountX = tilesX;
            result.CountY = tilesY;
            
            for (int i = 0; i < tilesY; i++)
            {
                for (int j = 0; j < tilesX; j++)
                {
                    var tile = new Tile();
                    tile.X = j * x;
                    tile.Y = i * y;
                    tile.Width = x;
                    tile.Height = y;
                    tile.Palette = result;
                    result.Tiles.Add(tile);
                }
            }

            return result;
        }
    }
}