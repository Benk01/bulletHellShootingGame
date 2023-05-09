using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using TohoGame.Pathing;


namespace TohoGame
{
    internal class Bullet : Entity
    {
        public EntityPathing pathing;
        public bool FiredByEnemy;
        public new readonly int Damage;
        private int _health = 1;
        public Bullet(EntityPathing pathing, Texture2D texture, float radius, int damage) : base(texture, pathing.startingPosition, radius, damage)
        {
            this.Damage = damage;
            this.pathing = pathing;
            HitBox = new List<(int, int)>() { (10, 0), (10, texture.Height - 10), (texture.Width - 10, texture.Height - 10), (texture.Width - 10, 0) };
        }

        public override void Update(GameTime gameTime)
        {
            if (GetHealth() <= 0)
            {
                die();
            }
            // Add enemy-specific update logic here
            base.Update(gameTime);
            Vector2 move = this.pathing.Move(base.Position);
            base.Position = move;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Add enemy-specific draw logic here
            base.Draw(spriteBatch);
        }

        public override void TakeDamage(int damage)
        {
            //Debug.WriteLine($"Bullet helth Reduced by: {damage}");
            this._health = 0;
        }
        public override int GetHealth()
        {
            return _health;
        }
    }
}
