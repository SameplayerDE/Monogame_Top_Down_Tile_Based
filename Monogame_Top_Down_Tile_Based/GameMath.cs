using System;
using Microsoft.Xna.Framework;

namespace Monogame_Top_Down_Tile_Based
{
    public static class GameMath
    {
        public static Vector2 PixelPerfectClamp(Vector2 vector2, int pixelsPerUnit)
        {
            var vectorInPixels = new Vector2(
                MathF.Round(vector2.X * pixelsPerUnit),
                MathF.Round(vector2.Y * pixelsPerUnit)
            );
            
            Console.WriteLine("X: " + vector2.X + ", ClampX: " + vectorInPixels.X);
            Console.WriteLine("Y: " + vector2.Y + ", ClampY: " + vectorInPixels.Y);
            
            return vectorInPixels / pixelsPerUnit;
        }
    }
}