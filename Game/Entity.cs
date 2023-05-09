using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace TohoGame
{
    public class Entity
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        //this is the Radius of the Texture
        public float Radius { get; set; }
        public List<(int, int)> HitBox;
        public int Damage;
        //move this lol
        public Texture2D BulletTexture { get; set; }
        public Texture2D RewardTexture { get; set; }
        public double BulletSpeed {get; set;}
        public int BulletRadius { get; set; }
        private List<IObserver> observers = new List<IObserver>();

        public Entity(Texture2D texture, Vector2 position, float radius)
        {
            Texture = texture;
            Position = position;
            Radius = radius;
            Damage = 0;
        }

        public Entity(Texture2D texture, Vector2 position, float radius, int damage)
        {
            Texture = texture;
            Position = position;
            Radius = radius;
            this.Damage = damage;
        }

        public void die()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }
        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public virtual void Update(GameTime gameTime)
        {
            if(this.Position.X < -500)
            {
                this.die();
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }


        public virtual bool IntersectsWith(Entity other)
        {
            float distance = Vector2.Distance(Position, other.Position);
            float combinedRadius = Radius + other.Radius;

            return distance <= combinedRadius;
        }

        public virtual void UpdatePosition(Vector2 delta)
        {
            Position += delta;
        }

        public virtual int GetHealth()
        {
            return 1;
        }
        public virtual void DoDamage(int damage)
        {
        }
        public virtual void TakeDamage(int damage)
        {
        }
    }

}