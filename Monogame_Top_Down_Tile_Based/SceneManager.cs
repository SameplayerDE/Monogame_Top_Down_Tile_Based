namespace Monogame_Top_Down_Tile_Based
{
    public class SceneManager
    {
        public Scene Current;

        public void SetScene(Scene scene)
        {
            if (Current != scene)
            {
                GameObject.ClearGameObjects();
                Current = scene;
            }
        }
    }
}