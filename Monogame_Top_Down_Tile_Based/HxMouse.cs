using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hx
{

    public enum HxMouseButton
    {
        Left,
        Right,
        Middle,
        X1,
        X2
    }
    
    public static class HxMouse
    {
        public static Point Position => _curr.Position;
        public static Point MouseDelta => _curr.Position - _prev.Position;
        
        private static MouseState _curr;
        private static MouseState _prev;

        public static ButtonState LeftButton => _curr.LeftButton;
        public static ButtonState RightButton => _curr.RightButton;
        public static ButtonState MiddleButton => _curr.MiddleButton;
        public static ButtonState XButton1 => _curr.XButton1;
        public static ButtonState XButton2 => _curr.XButton2;
        
        public static int ScrollWheelValue => _curr.ScrollWheelValue;
        public static int HorizontalScrollWheelValue => _curr.HorizontalScrollWheelValue;
        
        public static int DeltaScrollWheelValue => _curr.ScrollWheelValue - _prev.ScrollWheelValue;
        public static int DeltaHorizontalScrollWheelValue => _curr.HorizontalScrollWheelValue - _prev.HorizontalScrollWheelValue;

        public static void Update(GameTime gameTime)
        {
            _prev = _curr;
            _curr = Mouse.GetState();
        }

        public static bool WasButtonDown(HxMouseButton button)
        {
            return button switch
            {
                HxMouseButton.Left => _prev.LeftButton == ButtonState.Pressed,
                HxMouseButton.Right => _prev.RightButton == ButtonState.Pressed,
                HxMouseButton.Middle => _prev.MiddleButton == ButtonState.Pressed,
                HxMouseButton.X1 => _prev.XButton1 == ButtonState.Pressed,
                HxMouseButton.X2 => _prev.XButton2 == ButtonState.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }
        
        public static bool IsButtonDown(HxMouseButton button)
        {
            return button switch
            {
                HxMouseButton.Left => _curr.LeftButton == ButtonState.Pressed,
                HxMouseButton.Right => _curr.RightButton == ButtonState.Pressed,
                HxMouseButton.Middle => _curr.MiddleButton == ButtonState.Pressed,
                HxMouseButton.X1 => _curr.XButton1 == ButtonState.Pressed,
                HxMouseButton.X2 => _curr.XButton2 == ButtonState.Pressed,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button, null)
            };
        }
        
        public static bool IsButtonDownOnce(HxMouseButton button)
        {
            return !WasButtonDown(button) && IsButtonDown(button);
        }
    }
}