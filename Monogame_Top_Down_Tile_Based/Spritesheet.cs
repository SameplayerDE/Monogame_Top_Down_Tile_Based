using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monogame_Top_Down_Tile_Based.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class Spritesheet
    {
        public GameTexture2D GameTexture2D;
        public List<Rectangle> Sprites;
        
        public int Count => Sprites.Count;
        public int CountX;
        public int CountY;
        
        public static Spritesheet Generate(int x, int y, GameTexture2D gameTexture2D)
        {
            var result = new Spritesheet();

            result.Sprites = new List<Rectangle>();
            result.GameTexture2D = gameTexture2D;

            var tilesX = gameTexture2D.Texture2D.Bounds.Size.X / x;
            var tilesY = gameTexture2D.Texture2D.Bounds.Size.Y / y;

            result.CountX = tilesX;
            result.CountY = tilesY;
            
            for (int i = 0; i < tilesY; i++)
            {
                for (int j = 0; j < tilesX; j++)
                {
                    var tile = new Rectangle();
                    tile.X = j * x;
                    tile.Y = i * y;
                    tile.Width = x;
                    tile.Height = y;
                    result.Sprites.Add(tile);
                }
            }

            return result;
        }
    }
}