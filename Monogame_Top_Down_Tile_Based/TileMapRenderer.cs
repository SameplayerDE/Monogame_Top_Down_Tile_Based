using System;
using System.Linq;
using System.Security.Permissions;
using Hx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Top_Down_Tile_Based.DataStructure;
using PrimitiveExpander;

namespace Monogame_Top_Down_Tile_Based
{
    public static class TileMapRenderer
    {

        private static double _animationTimer;

        public static void UpdateAnimationTimer()
        {
            _animationTimer += HxTime.DeltaTimeF * 1000f;
        }

        public static void Draw(this TileMap tileMap, SpriteBatch spriteBatch, GameTime gameTime, Vector2 offset)
        {
            /*var right = tileMap.Right;
            var up = tileMap.Up;
            var left = tileMap.Left;
            var down = tileMap.Down;

            if (right != null)
            {
                foreach (var tileMapTileLayer in right.TileMapTileLayers)
                {
                    if (!tileMapTileLayer.Visible) continue;
                    for (var y = 0; y < tileMapTileLayer.Height; y++)
                    {
                        for (var x = 0; x < tileMapTileLayer.Width; x++)
                        {
                            var index = tileMapTileLayer.Data[x + tileMapTileLayer.Width * y];
                            var tileSet = right.GetTileSetByTileIndex(index);
                            if (tileSet == null) continue;
                            var tileArea = tileSet.GetTileSection(index);
                            spriteBatch.Draw(
                                tileSet.TileSet.Texture2D,
                                new Vector2(x + tileMap.Width, y) * new Vector2(right.TileWidth, right.TileHeight),
                                tileArea,
                                Color.White * tileMapTileLayer.Opacity
                            );
                        }
                    }
                }
            }*/

            foreach (var tileMapTileLayer in tileMap.TileMapTileLayers)
            {
                if (!tileMapTileLayer.Visible) continue;
                
                var tileSize = new Vector2(tileMap.TileWidth, tileMap.TileHeight);
                var parallaxOffset = tileMapTileLayer.Parallax;
                //var parallaxOffset = (tileMapTileLayer.Parallax - Vector2.One) / tileSize;

                for (var y = 0; y < tileMapTileLayer.Height; y++)
                {
                    for (var x = 0; x < tileMapTileLayer.Width; x++)
                    {
                        var index = tileMapTileLayer.Data[x + tileMapTileLayer.Width * y];
                        var tileSet = tileMap.GetTileSetByTileIndex(index);
                        if (tileSet == null) continue;
                        var tileArea = tileSet.GetTileSection(index);

                        var animations = tileSet.GetTileAnimationFile(index);

                        var gridPosition = new Vector2(x, y);
                        var worldPosition = gridPosition * tileSize;

                        /*if (parallaxOffset != Vector2.Zero)
                        {
                            parallaxOffset += new Vector2((float)Math.Sin(_animationTimer), 0) * tileSize;
                        }*/

                        //worldPosition = Vector2.Transform(worldPosition, Matrix.CreateTranslation(new Vector3(parallaxOffset * -offset, 0)));

                        if (animations != null)
                        {
                            var animationIndex = 0;
                            animationIndex = (int)_animationTimer / animations[animationIndex].Duration % animations.Count;
                            tileArea = tileSet.TileSet.GetTileSection(animations[animationIndex].TileId);
                            spriteBatch.Draw(
                                tileSet.TileSet.Texture2D,
                                worldPosition,
                                tileArea,
                                Color.White * tileMapTileLayer.Opacity
                            );
                        }
                        else
                        {
                            spriteBatch.Draw(
                                tileSet.TileSet.Texture2D,
                                worldPosition,
                                tileArea,
                                Color.White * tileMapTileLayer.Opacity
                            );
                        }
                    }
                }
            }
        }
    }
}