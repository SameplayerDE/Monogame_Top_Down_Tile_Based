using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Top_Down_Tile_Based.DataStructure;
using SharpDX.Direct3D11;
using BlendState = Microsoft.Xna.Framework.Graphics.BlendState;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;

namespace Monogame_Top_Down_Tile_Based
{
    public class Tilemap
    {
        /// <summary>
        /// Name Of The Tilemap
        /// </summary>
        public string Name;
        
        /// <summary>
        /// If The Tilemap Should Be Rendered
        /// </summary>
        public bool Visible = true;
        
        /// <summary>
        /// Parent Grid
        /// </summary>
        public Grid Grid;

        /// <summary>
        /// Tiles On The Map
        /// </summary>
        public Dictionary<Point, Tile> TilemapData;

        public RenderTarget2D RenderTarget2D;

        public Tilemap(GraphicsDevice graphicsDevice)
        {
            RenderTarget2D = new RenderTarget2D(graphicsDevice, 100, 100);
            Name = Guid.NewGuid().ToString();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, GraphicsDevice graphicsDevice, Camera camera)
        {
            graphicsDevice.SetRenderTarget(RenderTarget2D);

            graphicsDevice.Clear(Color.White);

            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                transformMatrix: camera.TranslationMatrix
            );
            foreach (var (point, tile) in TilemapData)
            {
                spriteBatch.Draw(
                    tile.Palette.GameTexture2D.Texture2D,
                    point.ToVector2() * new Vector2(8, 8),
                    new Rectangle(tile.X, tile.Y, tile.Width, tile.Height),
                    Color.White
                );
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            TilemapData[new Point(x, y)] = tile;
        }

        public void SetTile(int x, int y, TilePalette palette, int index)
        {
            var tile = palette.Get(index);
            SetTile(x, y, tile);
        }

        public void SetTile(int x, int y, TilePalette palette, int tx, int ty)
        {
            var tile = palette.Get(tx, ty);
            SetTile(x, y, tile);
        }

        public void SetTile(int x, int y, TilePalette palette, int tx, int ty, int tw, int th)
        {
            for (int i = 0; i <= th; i++)
            {
                for (int j = 0; j <= tw; j++)
                {
                    var tile = palette.Get(tx + j, ty + i);
                    SetTile(x + j, y + i, tile);
                }
            }
        }

        public void SetTile(int x, int y, params Tile[] tiles)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                var tile = tiles[i];
                SetTile(x + i, y, tile);
            }
        }
    }
}