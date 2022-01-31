using Monogame_Top_Down_Tile_Based.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class Tile
    {
        /// <summary>
        /// The Palette The Tile Is From
        /// </summary>
        public TilePalette Palette;

        public int Index;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool Solid;
    }
}