using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame_Top_Down_Tile_Based.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class Sprite : GameObject
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Origin = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public Color Tint = Color.White;
        
        public GameTexture2D Texture = null;
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Texture == null) return;

            var (width, height) = Texture.Texture2D.Bounds.Size;
            
            var unitsX = width / Texture.PixelsPerUnit;
            var unitsY = height / Texture.PixelsPerUnit;
            
            var scaleX = unitsX * GameSettings.PixelsPerUnits;
            var scaleY = unitsY * GameSettings.PixelsPerUnits;

            var factorX = width / (float)scaleX;
            var factorY = height / (float)scaleY;
            
            spriteBatch.Draw(
                Texture.Texture2D,
                Position, null,
                Tint, 0f,
                Origin, 
                Scale,
                SpriteEffects.None, 0f
            );
        }
    }
}