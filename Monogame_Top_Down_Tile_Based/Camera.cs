using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public class Camera : GameObject
    {
        public GraphicsDevice GraphicsDevice;
        public new string Tag = "Camera";
        public Vector2 Position;
        public float Scale = 1f;

        public Camera(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }
        
        public Matrix ScaleMatrix => Matrix.CreateScale(Scale);
        public Matrix TranslationMatrix => Matrix.CreateTranslation(new Vector3(-Position, 0));
        public Matrix TranslationCenterMatrix => Matrix.CreateTranslation(new Vector3(GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0));
        public Matrix Matrix =>  TranslationMatrix * ScaleMatrix * TranslationCenterMatrix;
    }
}