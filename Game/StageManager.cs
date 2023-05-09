using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame.Pathing;
using Microsoft.Xna.Framework;
using TohoGame.EnemyNamespace;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TohoGame
{
    internal class StageManager
    {
        private bool _gameOver = false;
        private int _currentPhase;
        private int _totalPhases;
        private double _totalTime;
        private double _phaseCountdown;
        private double _phaseTime;
        private List<double> _phaseDuration;
        //private JSONParser _jsonParser;
        private EntityController _entities;
        private Entity _player;
        private PathingFactory _pathingFactory;
        private ContentManager Content;
        private SpriteFont _font;
        private dynamic _stages;
        private bool _isPhaseOver;

        public StageManager(GraphicsDevice graphicsDevice)
        {
            _entities = new EntityController(graphicsDevice);
            _totalTime = 0;
            _phaseTime = 0;
            _pathingFactory = new PathingFactory();
            _currentPhase = 1;
            _phaseDuration = new List<double> { 15, 15, 15, 15, 15 };
            _phaseCountdown = _phaseDuration[_currentPhase];
            _isPhaseOver = true;
            _totalPhases = 0;

        }
        public void activateCheats(GameTime time)
        {
            _entities.cheat();
            this.UpdatePhase(time);
        }

        internal void Initialize(ContentManager Content, GraphicsDeviceManager graphicsDevice)
        {
            //ParseJSON();
            // use json parser to initialize _gamePhases somehow
            this.Content = Content;
            _font = Content.Load<SpriteFont>("File");
            _player = new Player(
               Content.Load<Texture2D>("spaceship"),
               Content.Load<Texture2D>("missile"),
               new Vector2(300, 300),
               10,
               100,
               graphicsDevice.PreferredBackBufferWidth,
               graphicsDevice.PreferredBackBufferHeight,
               new HUD(_font)
               );
            //EntityPathing pathing = _pathingFactory.CreatePathing(PathingTypes.GruntPathing1, 1.0, new Vector2(1900, 0));
            //EntityPathing pathing2 = _pathingFactory.CreatePathing(PathingTypes.GruntPathing1, 1.0, new Vector2(-400, 0));
            _entities.AddPlayer((Player)_player);
            _entities.BombTexture = Content.Load<Texture2D>("smallbomb");

            //_entities.AddEntity(EnemyFactory.CreateEnemy(
            //    EnemyType.Regular,
            //    pathing,
            //    Content.Load<Texture2D>("ufo"),
            //    5, 100, _entities
            //));

            //_entities.AddEntity(EnemyFactory.CreateEnemy(
            //    EnemyType.Regular,
            //    pathing2,
            //    Content.Load<Texture2D>("ufo"),
            //    5, 100, _entities
            //));

            String path = "../../../Content/";
            String data = File.ReadAllText(path + "stages.json");
            _stages = JsonConvert.DeserializeObject<dynamic>(data);
            _stages = _stages["stages"];
            foreach (var stage in _stages)
            {
                _totalPhases++;
            }


        }
        internal void AdvancePhase()
        {

        }

        internal async void createWave(dynamic wave)
        {
            int enemyCount = Convert.ToInt32(wave["enemyCount"].Value);
            int startTimeMS = Convert.ToInt32(wave["startTime"].Value * 1000);

            String enemyType = wave["enemy"]["type"].Value;
            int size = Convert.ToInt32(wave["enemy"]["size"].Value);
            int health = Convert.ToInt32(wave["enemy"]["health"].Value);
            String texture = wave["enemy"]["texture"].Value;

            String pathingType = wave["pathing"]["type"].Value;
            double speed = Convert.ToDouble(wave["pathing"]["speed"].Value);
            Vector2 startingPos = new Vector2(Convert.ToInt32(wave["pathing"]["startingPosition"]["x"].Value), Convert.ToInt32(wave["pathing"]["startingPosition"]["y"].Value));

            List<String> bulletPathingTypes = new List<String>();
            List<double> bulletSpeeds = new List<double>();
            List<int> bulletRadiuses = new List<int>();
            List<int> bulletDamages = new List<int>();
            List<Texture2D> bulletTextures = new List<Texture2D>();

            foreach (dynamic b in wave["bullets"])
            {
                dynamic bullet = b.Value;
                bulletPathingTypes.Add(bullet["pathing"]["type"].Value);
                bulletSpeeds.Add(Convert.ToDouble(bullet["pathing"]["speed"].Value));
                bulletRadiuses.Add(Convert.ToInt32(bullet["radius"].Value));
                bulletDamages.Add(Convert.ToInt32(bullet["damage"].Value));
                bulletTextures.Add(this.Content.Load<Texture2D>(bullet["texture"].Value));
            }


            await Task.Delay(startTimeMS);
            for (int i = 0; i < enemyCount; i++)
            {
                _entities.AddEntity(
                    EnemyFactory.CreateEnemy(
                        enemyType,
                        bulletPathingTypes, bulletDamages, bulletSpeeds, bulletTextures, bulletRadiuses, _pathingFactory.CreatePathing(pathingType, speed, startingPos),
                        this.Content.Load<Texture2D>(texture),
                        size, health, _entities
                ));
                if (enemyCount > 1)
                {
                    int spawnDelayMS = Convert.ToInt32(wave["spawnDelay"].Value * 1000);
                    await Task.Delay(spawnDelayMS);
                }
            }
        }


        internal void UpdatePhase(GameTime gameTime)
        {

            if (_entities.GameOver || _gameOver)
            {
                return;
            }
            /*            if (_phaseCountdown <= 0)
                        {
                            itteratePhase();
                        }*/

            if (_isPhaseOver)
            {
                dynamic stage = _stages[_currentPhase.ToString()];
                dynamic waves = stage["waves"];
                foreach (dynamic wave in waves)
                {
                    createWave(wave.Value);
                }
                _isPhaseOver = false;
            }
            if (_entities.ActiveEnemies.Count == 0)
            {
                Debug.WriteLine("no more enemies");
                itteratePhase();
            }
            _entities.UpdateEntities(gameTime);
            /*            _totalTime += gameTime.ElapsedGameTime.TotalSeconds;
                        _phaseCountdown -= gameTime.ElapsedGameTime.TotalSeconds;
                        _phaseTime += gameTime.ElapsedGameTime.TotalSeconds;*/
        }
        internal void DrawPhase(SpriteBatch spriteBatch)
        {
            if (_entities.GameOver == true)
            {
                spriteBatch.DrawString(_font, "GameOver. You Lose", new Vector2(0, 0), Color.Red);
            }
            else if (_gameOver == true)
            {
                _entities.ActiveEntities.Clear();
                spriteBatch.DrawString(_font, "You Win!", new Vector2(0, 0), Color.Green);
            }
            _entities.DrawEntities(spriteBatch);
        }

        private void itteratePhase()
        {
            _currentPhase++;
            if (_currentPhase > _totalPhases)
            {
                _gameOver = true;
                return;
            }
            _isPhaseOver = true;
            /*            _phaseCountdown = _phaseDuration[_currentPhase];
                        _phaseTime = 0;*/
        }


        //internal void ParseJSON();

    }
}
