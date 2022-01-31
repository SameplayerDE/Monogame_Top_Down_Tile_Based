using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame_Top_Down_Tile_Based
{
    public abstract class GameObject
    {
        protected Scene Scene;
        public string Tag;

        private static List<GameObject> _gameObjects;

        protected GameObject()
        {
            Tag = Guid.NewGuid().ToString();
        }
        
        static GameObject()
        {
            _gameObjects = new List<GameObject>();
        }

        public static void RegisterGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }
        
        public static void UnregisterGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }
        
        public void SetScene(Scene scene)
        {
            Scene = scene;
        }
        
        public Scene GetScene()
        {
            return Scene;
        }

        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            var result = new List<GameObject>();
            for (var index = _gameObjects.Count - 1; index >= 0; index--)
            {
                var gameObject = _gameObjects[index];
                if (gameObject.Tag.Equals(tag))
                {
                    result.Add(gameObject);
                }
            }
            return result.Count != 0 ? result.ToArray() : null;
        }

        public static void ClearGameObjects()
        {
            _gameObjects.Clear();
        }
    }
}