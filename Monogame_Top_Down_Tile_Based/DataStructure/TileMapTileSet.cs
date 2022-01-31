using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monogame_Top_Down_Tile_Based.Graphics;
using Monogame_Top_Down_Tile_Based.JsonData;

namespace Monogame_Top_Down_Tile_Based.DataStructure
{
    public class TileMapTileSet
    {
        public string Source;
        public int FirstGid;
        public TileSet TileSet;

        public List<TileSetTileFile> TileFiles => TileSet.Tiles;

        public Rectangle GetTileSection(int index)
        {
            return TileSet.GetTileSection(index - FirstGid);
        }
        
        public List<AnimationFile> GetTileAnimationFile(int index)
        {
            return TileSet.GetTileAnimationFile(index - FirstGid);
        }
        
    }
}