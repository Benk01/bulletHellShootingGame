using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame.Pathing;

//for testing remove later
using TohoGame.EnemyNamespace;
using System.Diagnostics;

namespace TohoGame
{
    using KeybordKey = Microsoft.Xna.Framework.Input.Keys;
    public class EntityController : IObserver
    {
        public List<Entity> ActiveEntities { get; private set; }
        public List<Entity> ActiveEnemies { get; private set; }
        public List<Entity> ShootingBuffer { get; set; }
        public Texture2D BombTexture { get; set; }
        public bool GameOver { get; private set; }

        private Player _player;
        private GraphicsDevice _graphicsDevice;
        private Texture2D hitboxTexture;
        private bool debug = false;
        private bool instakill = false;
        private bool noDamage = false;

        public EntityController(GraphicsDevice graphicsDevice)
        {
            ActiveEntities = new List<Entity>();
            ShootingBuffer = new List<Entity>();
            ActiveEnemies = new List<Entity>();
            _graphicsDevice = graphicsDevice;
            hitboxTexture = new Texture2D(_graphicsDevice, 1, 1);
            hitboxTexture.SetData(new[] { Color.White });
            GameOver = false;
        }

        public void Update(Entity entity)
        {
            if (debug)
            {
                Debug.WriteLine("Active Enemies: " + this.ActiveEnemies.Count());
            }
            ActiveEntities.Remove(entity);
            if (entity is Enemy)
            {
                ActiveEnemies.Remove(entity);
                //reward system
                rewardSystem(entity);
            }
        }

        public void AddPlayer(Player player)
        {
            _player = player;
            ActiveEntities.Add(player);
        }
        public void AddEntity(Entity entity)
        {
            entity.Attach(this);
            ActiveEntities.Add(entity);
            if(entity is Enemy)
            {
                ActiveEnemies.Add(entity);
            }
        }

        public void UpdateEntities(GameTime gameTime)
        {
            if (_player.GetHealth() <= 0)
            {
                ActiveEntities.Clear();
                GameOver = true;
            }
            UpdateEntitiesCommand update = new UpdateEntitiesCommand(gameTime, ActiveEntities, _player, ShootingBuffer, instakill, noDamage, FireBullet, AddEntity, FireBomb);
            update.Execute(null);
        }

        public void cheat()
        {
            debug = true;
            instakill = true;
            _player.godMode = true;
        }

        private void drawHitbox(SpriteBatch spriteBatch, Entity entity)
        {
            spriteBatch.Draw(hitboxTexture, new Vector2(entity.Position.X + entity.HitBox[0].Item1, entity.Position.Y), null,
                Color.Black, 0, Vector2.Zero, new Vector2(1, entity.HitBox[1].Item2),
                SpriteEffects.None, 0);
            spriteBatch.Draw(hitboxTexture, new Vector2(entity.Position.X + entity.HitBox[0].Item1, entity.Position.Y + entity.HitBox[2].Item2), null,
                Color.Black, 0, Vector2.Zero, new Vector2(entity.HitBox[2].Item1 - entity.HitBox[0].Item1, 1),
                SpriteEffects.None, 0);
            spriteBatch.Draw(hitboxTexture, new Vector2(entity.Position.X + entity.HitBox[2].Item1, entity.Position.Y), null,
                Color.Black, 0, Vector2.Zero, new Vector2(1, entity.HitBox[1].Item2),
                SpriteEffects.None, 0);
            spriteBatch.Draw(hitboxTexture, new Vector2(entity.Position.X + entity.HitBox[0].Item1, entity.Position.Y), null,
                Color.Black, 0, Vector2.Zero, new Vector2(entity.HitBox[2].Item1 - entity.HitBox[0].Item1, 1),
                SpriteEffects.None, 0);
        }
        public void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach (var entity in ActiveEntities)
            {

                if (entity.HitBox != null)
                {
                    if (debug)
                    {
                        drawHitbox(spriteBatch, entity);
                    }
                }
                entity.Draw(spriteBatch);
            }
        }

        public void FireBullet(Entity source)
        {
            FireBulletCommand fire = new FireBulletCommand(source, ShootingBuffer);
            fire.Execute(null);
        }
        public void FireBomb(Entity source)
        {
            FireBombCommand fire = new FireBombCommand(source, ShootingBuffer);
            fire.Execute(null);
        }

        public void EnemyFireBullet(Entity source)
        {
            EnemyFireBulletCommand fire = new EnemyFireBulletCommand(source, ShootingBuffer);
            fire.Execute(null);
        }

        public void BossSpecialFireBullet(Entity source)
        {
            BossSpecialFireBulletCommand fire = new BossSpecialFireBulletCommand(source, ShootingBuffer);
            fire.Execute(null);
        }

        public void rewardSystem(Entity entity)
        {
            //get entity radius
            float myRadius = entity.Radius;

            //get position
            Vector2 myposition = entity.Position;

            //use these and create reward class
            //_bullettexture.SetData(new Color[] { Color.Blue });
            //Texture2D aaa = Content.Load<Texture2D>("spaceship");
            Reward reward = new Reward(BombTexture, myposition, myRadius);

            //add new entity to active entities
            reward.Attach(this);
            ActiveEntities.Add(reward);



        }

    }
}
