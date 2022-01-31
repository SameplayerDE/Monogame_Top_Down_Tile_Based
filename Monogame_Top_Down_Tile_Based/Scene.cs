using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public abstract class Scene
    {
        protected SceneRenderer _renderer;
        protected List<GameObject> _gameObjects;

        public GameObject[] GameObjects => _gameObjects.ToArray();

        protected Scene()
        {
            _gameObjects = new List<GameObject>();
            _renderer = new SceneRenderer(this);
        }

        protected void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }
        
        protected void RegisterGameObjects()
        {
            for (int i = _gameObjects.Count - 1; i >= 0; i--)
            {
                var gameObject = _gameObjects[i];
                GameObject.RegisterGameObject(gameObject);
            }
        }
        
        protected void UnregisterGameObjects()
        {
            for (int i = _gameObjects.Count - 1; i >= 0; i--)
            {
                var gameObject = _gameObjects[i];
                GameObject.UnregisterGameObject(gameObject);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _renderer.Draw(spriteBatch, gameTime);
        }
    }
}