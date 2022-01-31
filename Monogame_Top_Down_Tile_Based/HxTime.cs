using Microsoft.Xna.Framework;

namespace Hx
{
    public static class HxTime
    {
        public static float DeltaTimeF;
        public static double DeltaTimeD;

        public static void Update(GameTime gameTime)
        {
            DeltaTimeF = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTimeD = gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}