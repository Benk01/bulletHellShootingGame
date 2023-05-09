using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TohoGame;
using TohoGame.EnemyNamespace;
using TohoGame.Pathing;

namespace TohoGame
{
    using KeybordKey = Microsoft.Xna.Framework.Input.Keys;
    public class FireBulletCommand : ICommand
    {
        protected Entity source;
        protected Texture2D BulletTexture;
        protected int Damage;
        protected double BulletSpeed;
        protected int BulletRadius;
        protected List<Entity> ShootingBuffer;
        protected event EventHandler CanExecuteChanged;

        public FireBulletCommand(Entity source, List<Entity> shootingBuffer)
        {
            this.source = source;
            this.BulletTexture = source.BulletTexture;
            this.BulletRadius = source.BulletRadius;
            this.Damage = source.Damage;
            this.BulletSpeed = source.BulletSpeed;
            ShootingBuffer = shootingBuffer;
            this.ShootingBuffer = shootingBuffer;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            //TypedReference tr = __makeref(source);
            //unsafe
            //{
            //    IntPtr ptr = **(IntPtr**)(&tr);
            //    Debug.WriteLine($"fired bullet: source adress: {ptr}");
            //}
            PathingFactory pathingFactory = new PathingFactory(); ;
            EntityPathing pathing = pathingFactory.CreatePathing("BulletPathing", 1.0, new Vector2(0, 0));
            Bullet bullet = new Bullet(
                pathing,
                BulletTexture,
                10,
                45
            );
            bullet.Position = new Vector2(source.Position.X + source.Texture.Width / 2 - bullet.Texture.Width / 2, source.Position.Y - bullet.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet);
        }
    }
    public class EnemyFireBulletCommand : FireBulletCommand
    {
        public EnemyFireBulletCommand(Entity source, List<Entity> shootingBuffer) : base(source, shootingBuffer)
        {

        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            //TypedReference tr = __makeref(source);
            //unsafe
            //{
            //    IntPtr ptr = **(IntPtr**)(&tr);
            //    Debug.WriteLine($"fired bullet: source adress: {ptr}");
            //}

            BulletPathingFactory _pathingFactory = new BulletPathingFactory();
            Enemy enemy = base.source as Enemy;
            if (enemy != null)
            {
                String bulletPathingType = enemy.BulletPathingType;
                // use bulletPathingType as needed
                EntityPathing pathing2 = _pathingFactory.CreateBulletPathing(bulletPathingType, BulletSpeed, new Vector2(source.Position.X, source.Position.Y + source.HitBox[1].Item2 + 2));
                Bullet bullet = new Bullet(
                    pathing2,
                    BulletTexture,
                    BulletRadius,
                    Damage
                );
                bullet.FiredByEnemy = true;
                ShootingBuffer.Add(bullet);

            }
        }
    }

    public class BossSpecialFireBulletCommand : FireBulletCommand
    {
        public BossSpecialFireBulletCommand(Entity source, List<Entity> shootingBuffer) : base(source, shootingBuffer)
        {

        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            //TypedReference tr = __makeref(source);
            //unsafe
            //{
            //    IntPtr ptr = **(IntPtr**)(&tr);
            //    Debug.WriteLine($"fired bullet: source adress: {ptr}");
            //}

            BulletPathingFactory _pathingFactory = new BulletPathingFactory();
            Enemy enemy = base.source as Enemy;
            if (enemy != null)
            {
                EntityPathing pathing1 = _pathingFactory.CreateBulletPathing("BulletZigzagPathing", BulletSpeed, new Vector2(source.Position.X, source.Position.Y + source.HitBox[1].Item2 + 2));
                ShootingBuffer.Add(new Bullet(
                    pathing1,
                    BulletTexture,
                    BulletRadius,
                    Damage
                ));

                EntityPathing pathing2 = _pathingFactory.CreateBulletPathing("BulletZigzagPathing", BulletSpeed, new Vector2(source.Position.X + 100, source.Position.Y + source.HitBox[1].Item2 + 2));
                ShootingBuffer.Add(new Bullet(
                    pathing2,
                    BulletTexture,
                    BulletRadius,
                    Damage
                ));

                EntityPathing pathing3 = _pathingFactory.CreateBulletPathing("BulletZigzagPathing", BulletSpeed, new Vector2(source.Position.X - 100, source.Position.Y + source.HitBox[1].Item2 + 2));
                ShootingBuffer.Add(new Bullet(
                    pathing3,
                    BulletTexture,
                    BulletRadius,
                    Damage
                ));

                EntityPathing pathing4 = _pathingFactory.CreateBulletPathing("BulletZigzagPathing", BulletSpeed, new Vector2(source.Position.X + 200, source.Position.Y + source.HitBox[1].Item2 + 2));
                ShootingBuffer.Add(new Bullet(
                    pathing4,
                    BulletTexture,
                    BulletRadius,
                    Damage
                ));

            }
        }
    }


    public class FireBombCommand : ICommand
    {
        protected Entity source;
        protected Texture2D BulletTexture;
        protected List<Entity> ShootingBuffer;
        protected event EventHandler CanExecuteChanged;

        public FireBombCommand(Entity source, List<Entity> shootingBuffer)
        {
            this.source = source;
            ShootingBuffer = shootingBuffer;
            this.ShootingBuffer = shootingBuffer;
            this.BulletTexture = source.BulletTexture;
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }


        public void Execute(object parameter)
        {
           PathingFactory pathingFactory = new PathingFactory(); ;
            EntityPathing pathing = pathingFactory.CreatePathing("BulletPathing", 1.0, new Vector2(0, 0));
            //////////
            Bullet bullet = new Bullet(pathing, BulletTexture, 10, 45);
            bullet.Position = new Vector2(source.Position.X, source.Position.Y - bullet.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet);
            ///////////
            Bullet bullet2 = new Bullet(pathing, BulletTexture, 10, 45);
            bullet2.Position = new Vector2(source.Position.X + 100, source.Position.Y - bullet2.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet2);
            ///////////
            Bullet bullet3 = new Bullet(pathing, BulletTexture, 10, 45);
            bullet3.Position = new Vector2(source.Position.X - 100, source.Position.Y - bullet3.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet3);

            /////////////
            Bullet bullet4 = new Bullet(pathing, BulletTexture, 10, 45);
            bullet4.Position = new Vector2(source.Position.X + 200, source.Position.Y - bullet4.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet4);
            /////////////
            Bullet bullet5 = new Bullet(pathing, BulletTexture, 10, 45);
            bullet5.Position = new Vector2(source.Position.X - 200, source.Position.Y - bullet5.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet5);
            /////////////
            Bullet bullet6 = new Bullet(pathing, BulletTexture, 10, 45);
            bullet6.Position = new Vector2(source.Position.X + 300, source.Position.Y - bullet6.HitBox[1].Item2 - 1);
            ShootingBuffer.Add(bullet6);
        }
    }



public class UpdateEntitiesCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private List<Entity> _activeEntities, _shootingBuffer;
        private Player _player;
        private GameTime _gameTime;
        private bool _instakill, _noDamage;
        private Action<Entity> _shoot;
        private Action<Entity> _addEntity;
        private Action<Entity> _shootbomb;


        public UpdateEntitiesCommand(GameTime gameTime, List<Entity> activeEntities, Player player, List<Entity> shootingBuffer, bool instakill, bool nodamage, Action<Entity> shoot, Action<Entity> addEntity, Action<Entity> shootbomb)
        {
            _gameTime = gameTime;
            _activeEntities = activeEntities;
            _player = player;
            _shootingBuffer = shootingBuffer;
            _instakill = instakill;
            _noDamage = nodamage;
            _shoot = shoot;
            _addEntity = addEntity;
            _shootbomb = shootbomb;
        }


        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            HandlePlayerMovement();
            List<Entity> deletedEntities = new List<Entity>();
            for (int i = _activeEntities.Count - 1; i >= 0; i--)
            {
                _activeEntities[i].Update(_gameTime);
            }
            for (int i = _activeEntities.Count - 1; i >= 0; i--)
            {
                // realy bad lmao
                if (_activeEntities[i].HitBox != null)
                {
                    collide(_activeEntities[i], deletedEntities);
                }
                if (_activeEntities[i].Position.Y < -1000 || _activeEntities[i].Position.Y > 1000)
                {

                    _activeEntities.RemoveAt(i);
                }
                else if (_activeEntities[i].Position.X < -1000)
                {

                    _activeEntities.RemoveAt(i);
                }




            }
            foreach (var bullet in _shootingBuffer)
            {
                _addEntity(bullet);
            }
            foreach (var element in deletedEntities)
            {
                element.die();
            }


            _shootingBuffer.Clear();
        }
        public void HandlePlayerMovement()
        {
            KeybordKey[] keys = Keyboard.GetState().GetPressedKeys();
            List<KeybordKey> listKeys = new List<KeybordKey>(keys);
            if (listKeys.Contains(Keys.LeftShift))
            {
                _player.currentSpeed = _player.baseSpeed * 2;
            }

            if (listKeys.Contains(Keys.W))
            {
                if (!_player.reachedTop())
                {
                    _player.UpdatePosition(new Vector2(0, 0 - _player.currentSpeed));
                }

                //Debug.WriteLine("W pressed");
            }

            if (listKeys.Contains(Keys.S))
            {
                if (!_player.reachedBottom())
                {
                    _player.UpdatePosition(new Vector2(0, _player.currentSpeed));
                }
                //Debug.WriteLine("S pressed");
            }

            if (listKeys.Contains(Keys.D))
            {
                if (!_player.reachedRightSide())
                {
                    _player.UpdatePosition(new Vector2(_player.currentSpeed, 0));
                }
                //Debug.WriteLine("D pressed");
            }

            if (listKeys.Contains(Keys.A))
            {
                if (!_player.reachedLeftSide())
                {
                    _player.UpdatePosition(new Vector2(0 - _player.currentSpeed, 0));
                }
                //Debug.WriteLine("S pressed");
            }

            if (listKeys.Contains(Keys.Space))
            {
                if (_player.WeaponCoolDown <= 0)
                {
                    _shoot(_player);
                    _player.WeaponCoolDown = 0.3;
                }
            }

            //bomb system
            if (listKeys.Contains(Keys.B) && (_player.GetBombCount() > 0))
            {

                if (_player.WeaponCoolDown <= 0)
                {
                    //foreach (var entity in _activeEntities)
                    //{
                    //    if (entity is Bullet)
                    //    {
                    //        entity.Position = new Vector2(0, -3001);
                    //    }
                    //}
                    _player.DecreaseBombCount();
                    _shootbomb(_player);
                    _player.WeaponCoolDown = 1;
                }



            }

            else if (listKeys.Contains(Keys.LeftControl))
            {
                _player.currentSpeed = _player.baseSpeed / 2;
            }
            else
            {
                _player.currentSpeed = _player.baseSpeed;
            }
        }
        private void collide(Entity entity1, List<Entity> deleted)
        {
            foreach (var entity2 in _activeEntities)
            {
                if (entity1 == entity2)
                {
                    continue;
                }
                if (detectCollision(entity1, entity2, deleted))
                {
                    onColision(entity1, entity2, deleted);
                }
            }
        }

        private void onColision(Entity entity1, Entity entity2, List<Entity> deleted)
        {
            if (entity1.GetType() == entity2.GetType() || 
                (entity1 is Enemy && entity2 is Enemy) ||
                ((entity1 is Enemy && entity2 is Bullet) && ((Bullet)entity2).FiredByEnemy) ||
                ((entity2 is Enemy && entity1 is Bullet) && ((Bullet)entity1).FiredByEnemy))
            {
                return;
            }
            // Handle collision detection here.
            if (_instakill)
            {
                if (entity2.Damage > 0)
                {
                    entity1.TakeDamage(entity1.GetHealth());

                }
                if (entity1.Damage > 0)
                {
                    entity2.TakeDamage(entity2.GetHealth());

                }
            }
            else if (!_noDamage && (entity1.GetHealth() > 0 && entity2.GetHealth() > 0))
            {
                //TypedReference tr = __makeref(entity1);
                //TypedReference tr2 = __makeref(entity2);
                //unsafe
                //{
                //    IntPtr ptr = **(IntPtr**)(&tr);
                //    IntPtr ptr2 = **(IntPtr**)(&tr2);
                //Debug.WriteLine($"Entity1({ptr}): Took {entity2.Damage}");
                //Debug.WriteLine($"Entity2({ptr2}): Took {entity1.Damage}");
                //}
                if (entity1 is Reward && entity2 is Player)
                {
                    if (!deleted.Contains(entity1))
                    {
                        _player._bombCount++;
                        deleted.Add(entity1);
                    }
                }
                else if (entity2 is Reward && entity1 is Player)
                {
                    if (!deleted.Contains(entity2))
                    {
                        _player._bombCount++;
                        deleted.Add(entity2);
                    }
                }

                entity1.TakeDamage(entity2.Damage);
                entity2.TakeDamage(entity1.Damage);

            }

        }
        private bool detectCollision(Entity entity1, Entity entity2, List<Entity> deleted)
        {
            if (entity1 == null || entity2 == null || entity1.HitBox == null || entity2.HitBox == null)
            {
                return false;
            }

            Rectangle rect = new Rectangle((int)entity1.Position.X, (int)entity1.Position.Y, entity1.HitBox[2].Item1, entity1.HitBox[1].Item2);
            foreach ((int, int) point in entity2.HitBox)
            {
                Point p1 = new Point(point.Item1 + (int)entity2.Position.X, point.Item2 + (int)entity2.Position.Y);
                if (rect.Contains(p1))
                {
                    return true;
                }
            }

            return false;
        }





    }
    public class EntityControllerCommands
    {
    }
}
