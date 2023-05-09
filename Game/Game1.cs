using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TohoGame.Pathing;
using TohoGame.EnemyNamespace;
using TohoGame.Menu;
using System.Collections.Generic;
using System.Diagnostics;

namespace TohoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player _player1;
        //private Enemy _enemy1;
        //private Enemy _enemy2;
        private StageManager _stageManager;
        private PathingFactory _pathingFactory;
        private double _totalTime;
        //private EntityController _entities;
        private MainMenu _mainMenu = new MainMenu();
        //private GamePhases _gamePhases;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_gamePhases = new GamePhases();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_stageManager = new StageManager();
            //_entities = new EntityController();
            _totalTime = 0;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1500;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mainMenu.LoadContent(Content, this._graphics.PreferredBackBufferHeight, this._graphics.PreferredBackBufferWidth);
            _stageManager = new StageManager(GraphicsDevice);
            _stageManager.Initialize(this.Content, this._graphics);
        }

        protected override void UnloadContent()
        {
            //texture.Dispose(); <-- Only directly loaded
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            _totalTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //this.CheckAndUpdatePhase();
            if (_mainMenu.State == MainMenu.GameState.inGame)
            {
                _stageManager.UpdatePhase(gameTime);
            }
            else if (_mainMenu.State == MainMenu.GameState.cheating)
            {
                _stageManager.activateCheats(gameTime);
            }
            base.Update(gameTime);
            _mainMenu.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _mainMenu.Draw(_spriteBatch);
            _spriteBatch.End();

            if (_mainMenu.State == MainMenu.GameState.inGame || _mainMenu.State == MainMenu.GameState.cheating)
            {
                _spriteBatch.Begin();
                _stageManager.DrawPhase(_spriteBatch);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
