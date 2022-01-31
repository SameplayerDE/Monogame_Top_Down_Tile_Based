using System;
using Hx;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame_Top_Down_Tile_Based.DataStructure;
using Monogame_Top_Down_Tile_Based.Graphics;
using Monogame_Top_Down_Tile_Based.JsonDataLoader;
using PrimitiveExpander;

namespace Monogame_Top_Down_Tile_Based
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _tiles; //renders all tiles
        private RenderTarget2D _scene; //renders the scene with effects
        private RenderTarget2D _entities; //renders entities
        private RenderTarget2D _ui; //renders the ui

        private RenderTarget2D _complete; //renders the complete all targets to one

        private Texture2D _texture_000;
        private Texture2D _texture_001;
        private Texture2D _texture_002;

        private Texture2D _overlay_000;
        private Texture2D _overlay_001;

        private Camera _camera;
        private int _spriteIndex = 0;

        private Grid _grid;
        private Tilemap _tilemap;
        private Tilemap _colliders;
        private TilePalette _tilePalette;
        private TilePalette _colliderPalette;

        private Spritesheet _spritesheet;

        private Player _player;

        private Point _position;
        private Vector2 _positionf;

        private Rectangle _regionBounds;
        private Vector2 _regionLocation;

        private TileMap _tileMap_000;

        private bool wasInside = false;
        private Effect _effect;
        private Effect _earth_shake;

        public Game1()
        {
            Content.RootDirectory = "Content";

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _camera = new Camera(GraphicsDevice);
            _player = new Player();

            _tiles = new RenderTarget2D(
                GraphicsDevice,
                160, 128,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24
            );
            _scene = new RenderTarget2D(
                GraphicsDevice,
                160, 128,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24
            );
            _entities = new RenderTarget2D(
                GraphicsDevice,
                160, 128,
                false,
                GraphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24
            );
            _ui = new RenderTarget2D(GraphicsDevice, 160, 16);
            _complete = new RenderTarget2D(GraphicsDevice, 160, 144);

            _regionBounds = new Rectangle(0, 0, 160, 128);
            _player.Position = new Vector2(9 * 8, 8 * 8);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tileMap_000 = TileMap.FromTileMapFile(
                MapLoader.LoadTileMapFile(@"Content\Regions\map_001.json")
            );
            _tileMap_000.LoadTileSets(GraphicsDevice);

            _effect = Content.Load<Effect>("effect");
            _earth_shake = Content.Load<Effect>("earth_shake");

            /*_tileSet_000 = TileSet.FromTileSetFile(
                TileSetFileLoader.LoadTileset(@"Content\Tilesets\tileset_000.json")
            );
            _tileSet_000.LoadImage(GraphicsDevice);

            int[] data;
            int width;
            int height;

            MapLoader.LoadRegion(
                @"Content/Regions/map_001.json",
                out data,
                out width, out height
            );*/

            var texture_000 = new GameTexture2D();

            texture_000.Texture2D = Content.Load<Texture2D>("tileset_000");
            texture_000.Name = "texture_000";
            texture_000.Path = "Content/tileset_000.png";
            texture_000.PixelsPerUnit = 8;

            var texture_001 = new GameTexture2D();

            texture_001.Texture2D = Content.Load<Texture2D>("spritesheet_000");
            texture_001.Name = "spritesheet_000";
            texture_001.Path = "Content/spritesheet_000.png";
            texture_001.PixelsPerUnit = 8;

            var texture_002 = new GameTexture2D();

            texture_002.Texture2D = Content.Load<Texture2D>("sprite_000");
            texture_002.Name = "sprite_000";
            texture_002.Path = "Content/sprite_000.png";
            texture_002.PixelsPerUnit = 8;

            var texture_003 = new GameTexture2D();

            texture_003.Texture2D = Content.Load<Texture2D>("tileset_001");
            texture_003.Name = "tileset_001";
            texture_003.Path = "Content/tileset_001.png";
            texture_003.PixelsPerUnit = 8;

            _texture_000 = Content.Load<Texture2D>("texture_000");
            _texture_001 = Content.Load<Texture2D>("texture_001");
            _texture_002 = Content.Load<Texture2D>("texture_002");

            _overlay_000 = Content.Load<Texture2D>("overlay_000");
            _overlay_001 = Content.Load<Texture2D>("overlay_001");

            _spritesheet = Spritesheet.Generate(16, 16, texture_001);

            /*_player.Texture = texture_002;

            /*_grid = new Grid();
            _grid.Tilemaps = new List<Tilemap>();

            _tilemap = new Tilemap(GraphicsDevice);
            _tilemap.TilemapData = new Dictionary<Point, Tile>();

            _colliders = new Tilemap(GraphicsDevice);
            _colliders.TilemapData = new Dictionary<Point, Tile>();

            _tilePalette = TilePalette.Generate(8, 8, texture_000);
            _colliderPalette = TilePalette.Generate(8, 8, texture_003);

            _tilemap.Grid = _grid;
            _colliders.Grid = _grid;

            _grid.Tilemaps.Add(_colliders);
            _grid.Tilemaps.Add(_tilemap);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var index = data[x + width * y];
                    if (index <= 0) continue;
                    index -= 1;
                    _tilemap.SetTile(x, y, _tilePalette, index);
                }
            }*/

            PrimitiveRenderer.Initialise(GraphicsDevice);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            const float speed = 32f;
            var updateGameplay = true;

            //if (!IsActive) return;

            HxTime.Update(gameTime);
            HxMouse.Update(gameTime);
            HxKeyboard.Update(gameTime);
            TileMapRenderer.UpdateAnimationTimer();

            if (HxKeyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (HxMouse.IsButtonDown(HxMouseButton.Middle))
            {
                _camera.Position -= HxMouse.MouseDelta.ToVector2() / _camera.Scale;
            }
            
            if (HxMouse.DeltaScrollWheelValue != 0)
            {
                var value = HxMouse.DeltaScrollWheelValue / 120;
                value *= 10;
                _camera.Scale += value * HxTime.DeltaTimeF;
            }

            var direction = Vector2.Zero;
            
            if (HxKeyboard.IsKeyDown(Keys.Up))
            {
                direction.Y -= 1;
                _position.Y -= 1;
            }
            if (HxKeyboard.IsKeyDown(Keys.Right))
            {
                direction.X += 1;
                _position.X += 1;
            }
            if (HxKeyboard.IsKeyDown(Keys.Down))
            {
                direction.Y += 1;
                _position.Y += 1;
            }
            if (HxKeyboard.IsKeyDown(Keys.Left))
            {
                direction.X -= 1;
                _position.X -= 1;
            }

            if (direction.Length() != 0)
            {
                direction.Normalize();
                _positionf += direction * HxTime.DeltaTimeF * 60f;
            }

            Console.WriteLine(_position);
            Console.WriteLine(_positionf);
            Console.WriteLine("------------");

            _position.X = Math.Clamp(_position.X, 0, 160 - 16);
            _position.Y = Math.Clamp(_position.Y, 0, 128 - 16);
            
            _positionf.X = Math.Clamp(_positionf.X, 0, 160 - 16);
            _positionf.Y = Math.Clamp(_positionf.Y, 0, 128 - 16);
            
            /*var playerRectangle = new Rectangle(_player.Position.ToPoint(), new Point(8, 8));
            var playerRegionRectangle = new Rectangle(_player.Position.ToPoint(), new Point(8, 8));

            if (_tileMap_000.GetValue<bool>("clamp_cam"))
            {
                if (!wasInside)
                {
                    wasInside = _regionBounds.Contains(playerRegionRectangle);
                }

                var rightOut = playerRegionRectangle.Right > _regionBounds.Right;
                var leftOut = playerRegionRectangle.Left < _regionBounds.Left;
                var topOut = playerRegionRectangle.Top < _regionBounds.Top;
                var bottomOut = playerRegionRectangle.Bottom > _regionBounds.Bottom;

                if ((rightOut || leftOut || bottomOut || topOut) && wasInside)
                {
                    if (rightOut)
                    {
                        MoveBounds(1, 0);
                    }
                    else if (leftOut)
                    {
                        MoveBounds(-1, 0);
                    }
                    else if (topOut)
                    {
                        MoveBounds(0, -1);
                    }
                    else if (bottomOut)
                    {
                        MoveBounds(0, 1);
                    }

                    wasInside = false;
                }

                var regionDiff = Vector2.Subtract(_regionBounds.Location.ToVector2(), _regionLocation).ToPoint()
                    .ToVector2();

                if (regionDiff.Length() != 0)
                {
                    regionDiff.Normalize();

                    if (regionDiff.Y != 0)
                    {
                        _regionLocation += regionDiff * HxTime.DeltaTimeF * 128;
                        _player.Position += regionDiff * HxTime.DeltaTimeF * 8;
                    }
                    else
                    {
                        _regionLocation += regionDiff * HxTime.DeltaTimeF * 160;
                        _player.Position += regionDiff * HxTime.DeltaTimeF * 8;
                    }

                    updateGameplay = false;
                }
            }


            


            if (updateGameplay)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    velocity.Y -= 1f;
                    _spriteIndex = 2;
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    velocity.Y += 1f;
                    _spriteIndex = 6;
                }

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    velocity.X -= 1f;
                    _spriteIndex = 4;
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    velocity.X += 1f;
                    _spriteIndex = 0;
                }

                if (velocity.Length() != 0)
                {
                    velocity.Normalize();
                    _player.Position += velocity;
                }

                _player.Position.Round();

                playerRectangle = new Rectangle(_player.Position.ToPoint(), new Point(8, 8));
            }

            if (!_tileMap_000.GetValue<bool>("clamp_cam"))
            {
                _regionLocation = _player.Position - _regionBounds.Center.ToVector2();
                
            }*/

            _effect.Parameters["DeltaTime"].SetValue(HxTime.DeltaTimeF);
            _effect.Parameters["TotalTime"].SetValue(HxTime.DeltaTimeF);

            _earth_shake.Parameters["DeltaTime"].SetValue(HxTime.DeltaTimeF);
            _earth_shake.Parameters["TotalTime"].SetValue(HxTime.DeltaTimeF);

            var fTime = _earth_shake.Parameters["fTime"].GetValueSingle();
            var fShakeAmount = _earth_shake.Parameters["fShakeAmount"].GetValueSingle();
            var fPeriod = _earth_shake.Parameters["fPeriod"].GetValueSingle();

            fTime = Math.Max(fTime - 1f * HxTime.DeltaTimeF, 0);
            fShakeAmount = fTime > 0 ? fShakeAmount : 0f;

            _earth_shake.Parameters["fTime"].SetValue(fTime);
            _earth_shake.Parameters["fShakeAmount"].SetValue(fShakeAmount);
            _earth_shake.Parameters["fPeriod"].SetValue(fPeriod);
            
            PrimitiveRenderer.ViewOffset = new Vector3(_camera.Position + _regionLocation, 0);
            PrimitiveRenderer.Scale = 1f / _camera.Scale;
            PrimitiveRenderer.UpdateDefaultCamera();
            base.Update(gameTime);
        }

        private void PlayWobble(float duration, float strength)
        {
            _effect.Parameters["Curves"].SetValue(strength);
            _effect.Parameters["Speed"].SetValue(strength);
            _effect.Parameters["Wraps"].SetValue(duration);
        }

        private void PlayShake(bool horizontal = true, bool vertical = false, bool sync = true, float duration = 1f,
            float strength = 0.016f, float period = 0.1f)
        {
            _earth_shake.Parameters["Sync"].SetValue(sync);
            _earth_shake.Parameters["Vertical"].SetValue(vertical);
            _earth_shake.Parameters["Horizontal"].SetValue(horizontal);
            _earth_shake.Parameters["fTime"].SetValue(duration);
            _earth_shake.Parameters["fShakeAmount"].SetValue(strength);
            _earth_shake.Parameters["fPeriod"].SetValue(period);
        }

        /*private void checkCollision(TileMap tilemap, Vector2 position, Vector2 size)
        {
            var playerRectangle = new Rectangle(position.ToPoint(), size.ToPoint());
            for (int i = tilemap.TileMapTileLayers.Count - 1; i >= 0; i--)
                {
                    var layer = tilemap.TileMapTileLayers[i];
                    for (int y = 0; y < layer.Height; y++)
                    {
                        for (int x = 0; x < layer.Width; x++)
                        {
                            var index = x + layer.Width * y;
                            var tileId = layer.Data[index];
                            var tileSet = tilemap.GetTileSetByTileIndex(tileId);
                            var tiles = tileSet?.TileFiles;
                            if (tiles == null) continue;
                            foreach (var tile in tiles)
                            {
                                if (tile.Properties != null)
                                {
                                    foreach (var pro in tile.Properties)
                                    {
                                        if (pro.Type == "bool")
                                        {
                                            if (pro.Name == "solid")
                                            {
                                                if ((bool)pro.Value)
                                                {
                                                    var tileRectangle = new Rectangle((new Vector2(x, y) * 8).ToPoint(),
                                                        new Point(8, 8));
                                                    if (tileRectangle.Intersects(playerRectangle))
                                                    {
                                                        //Todo Fix Collision
                                                        _player.Position.X = playerPosition.X;
                                                        if (_spriteIndex == 0)
                                                        {
                                                            _spriteIndex = 30;
                                                        }

                                                        if (_spriteIndex == 2)
                                                        {
                                                            _spriteIndex = 33;
                                                        }

                                                        if (_spriteIndex == 4)
                                                        {
                                                            _spriteIndex = 34;
                                                        }

                                                        if (_spriteIndex == 6)
                                                        {
                                                            _spriteIndex = 37;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
        }*/

        private void MoveBounds(int i, int i1)
        {
            _regionBounds.Location += new Point(i, i1) * new Point(160, 128);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_ui);
            GraphicsDevice.Clear(new Color(248, 248, 176));
            DrawUI(_spriteBatch, gameTime);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(_tiles);
            GraphicsDevice.Clear(new Color(248, 248, 176));
            DrawScene(_spriteBatch, gameTime);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(_scene);
            GraphicsDevice.Clear(new Color(248, 248, 176));
            DrawSceneEffect(_spriteBatch, gameTime);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(_entities);
            GraphicsDevice.Clear(Color.Transparent);
            DrawEntities(_spriteBatch, gameTime);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.SetRenderTarget(_complete);
            DrawComplete(_spriteBatch, gameTime);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(new Color(248, 248, 176));

            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                transformMatrix: _camera.Matrix
            );
            _spriteBatch.Draw(
                _overlay_001,
                new Vector2(0, 0),
                Color.White
            );
            _spriteBatch.Draw(_complete, new Rectangle(221, 229, 160 * 4, 144 * 4), Color.White);
            
            _spriteBatch.End();
            

            foreach (var tileMapObjectLayer in _tileMap_000.TileMapObjectLayers)
            {
                if (!tileMapObjectLayer.Visible) continue;
                foreach (var objectFile in tileMapObjectLayer.Objects)
                {
                    if (!objectFile.Ellipse && !objectFile.Point && objectFile.Polygon == null)
                    {
                        PrimitiveRenderer.DrawQuadH(
                            null,
                            Color.Red,
                            (new Vector2(objectFile.X, objectFile.Y) + new Vector2(0, 16)) * 4 + new Vector2(221, 229) -
                            _regionLocation * 3,
                            new Vector2(objectFile.Width, objectFile.Height) * 4
                        );
                    }

                    if (objectFile.Ellipse)
                    {
                        PrimitiveRenderer.DrawCircleH(
                            null,
                            Color.Red,
                            (new Vector2(objectFile.X, objectFile.Y) + new Vector2(0, 16)) * 4 + new Vector2(221, 229) -
                            _regionLocation * 3,
                            objectFile.Height * 4
                        );
                    }

                    if (objectFile.Point)
                    {
                        PrimitiveRenderer.DrawCircleF(
                            null,
                            Color.Red,
                            (new Vector2(objectFile.X, objectFile.Y) + new Vector2(0, 16)) * 4 + new Vector2(221, 229) -
                            _regionLocation * 3,
                            4f
                        );
                    }
                }
            }
        }

        private void DrawComplete(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp
            );
            _spriteBatch.Draw(_ui, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_scene, new Vector2(0, 16), Color.White);
            _spriteBatch.Draw(_entities, new Vector2(0, 16), Color.White);
            spriteBatch.End();
        }

        private void DrawEntities(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: Matrix.CreateTranslation(new Vector3(-_regionLocation.ToPoint().ToVector2(), 0))
            );
            spriteBatch.Draw(
                _spritesheet.GameTexture2D.Texture2D,
                _position.ToVector2(),
                _spritesheet.Sprites[_spriteIndex],
                Color.White
            );
            
            spriteBatch.Draw(
                _spritesheet.GameTexture2D.Texture2D,
                _positionf + new Vector2(0, 16),
                _spritesheet.Sprites[_spriteIndex],
                Color.White
            );

            spriteBatch.End();
        }

        private void DrawUI(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp
            );
            spriteBatch.Draw(
                _texture_000,
                Vector2.Zero,
                Color.White
            );
            spriteBatch.End();
        }

        private void DrawScene(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                transformMatrix: Matrix.CreateTranslation(new Vector3(-_regionLocation.ToPoint().ToVector2(), 0))
            );

            /*for (var i = _grid.Tilemaps.Count - 1; i >= 0; i--)
            {
                var tilemap = _grid.Tilemaps[i];
                if (!tilemap.Visible) continue;
                foreach (var (point, tile) in tilemap.TilemapData)
                {
                    spriteBatch.Draw(
                        tile.Palette.GameTexture2D.Texture2D,
                        point.ToVector2() * new Vector2(8, 8),
                        new Rectangle(tile.X, tile.Y, tile.Width, tile.Height),
                        Color.White
                    );
                }
            }*/

            _tileMap_000.Draw(_spriteBatch, gameTime, _camera.Position);

            //spriteBatch.Draw(
            //    _texture_002,
            //    Vector2.Zero -
            //    (Vector2.One * (_regionBounds.Location.ToVector2() / new Vector2(160, 128)) + Vector2.One),
            //    Color.White
            //);


            //_player.Draw(spriteBatch, gameTime);

            spriteBatch.End();
        }

        private void DrawSceneEffect(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                effect: _earth_shake
            );
            spriteBatch.Draw(_tiles, Vector2.Zero, Color.White);
            spriteBatch.End();
        }
    }
}