using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class SceneRenderer
    {
        private readonly Scene _scene;

        public SceneRenderer(Scene scene)
        {
            _scene = scene;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = _scene.GameObjects.Length - 1; i >= 0; i--)
            {
                var gameObject = _scene.GameObjects[i];
                if (gameObject is Sprite sprite)
                {
                    sprite.Draw(spriteBatch, gameTime);
                }
            }
        }
    }
}