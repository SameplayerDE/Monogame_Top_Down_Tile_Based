using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Hx
{
    public static class HxKeyboard
    {
        private static KeyboardState _curr;
        private static KeyboardState _prev;
        
        public static void Update(GameTime gameTime)
        {
            _prev = _curr;
            _curr = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return _curr.IsKeyDown(key);
        }
        
        public static bool WasKeyDown(Keys key)
        {
            return _prev.IsKeyDown(key);
        }

        public static bool IsKeyDownOnce(Keys key)
        {
            return !WasKeyDown(key) && IsKeyDown(key);
        }
        
    }
}