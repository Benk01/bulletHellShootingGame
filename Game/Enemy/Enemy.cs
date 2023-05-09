using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TohoGame;
using TohoGame.Pathing;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace TohoGame.EnemyNamespace
{
    // needs to change to abstract class later
    // also needs to have EnemyFactory to create child classes
    public abstract class Enemy : Entity
    {
        public EntityPathing pathing;
        public double WeaponCoolDown;
        public double WeaponDelay;
        public int Health { get; set; }
        public Weapon EnemyWeapon { get; set; }
        public EntityController entities;
        public String BulletPathingType { get; set; }
        public float CountdownTime { get; set; }



        public Enemy(EntityPathing pathing, List<String>bulletPathing, List<int> damage, List<double> bulletSpeed, List<Texture2D> bulletTexture, List<int> bulletRadius, Texture2D texture, float radius, int health, EntityController entities) : base(texture, pathing.startingPosition, radius)
        {
            Health = health;
            this.pathing = pathing;
            this.entities = entities;
            this.WeaponDelay = 5;
            this.BulletPathingType = bulletPathing[0];
            base.Damage = damage[0];
            base.BulletSpeed = bulletSpeed[0];
            base.BulletTexture = bulletTexture[0];
            base.BulletRadius = bulletRadius[0];

            HitBox = new List<(int, int)>() { (0, 0), (0, texture.Height), (texture.Width, texture.Height), (texture.Width, 0) };
        }

        public override void Update(GameTime gameTime)
        {
            // Add enemy-specific update logic here
            base.Update(gameTime);
            float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CountdownTime -= elapsedSeconds;
            // Handles exiting the screen
            if (CountdownTime > 0)
            {
                Position = pathing.Move(Position);
            } else
            {
                Vector2 pos = Position;
                pos.X -= 10;
                Position = pos;
            }
            if (GetHealth() <= 0)
            {
                die();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Add enemy-specific draw logic here
            base.Draw(spriteBatch);
        }

        public override int GetHealth()
        {
            return this.Health;
        }
        public override void TakeDamage(int damage)
        {
            this.Health -= damage;
        }

    }
}
