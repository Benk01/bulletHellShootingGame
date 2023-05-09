using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame.Pathing;

namespace TohoGame.EnemyNamespace
{
    internal class RegularEnemy : Enemy //grunt or butterfly enemies
    {

        public RegularEnemy(EntityPathing pathing, List<String> bulletPathing, List<int> damage, List<double> bulletSpeed, List<Texture2D> bulletTexture, List<int> bulletRadius, Texture2D texture, float radius, int health, EntityController entities) : base(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, 100, entities)
        {
            base.CountdownTime = 11f;
            base.WeaponCoolDown = 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (base.WeaponDelay < 0)
            {
                if (WeaponCoolDown <= 0)
                {
                    entities.EnemyFireBullet(this);
                    WeaponCoolDown = 1;
                }
                //repeated
                if (WeaponCoolDown > 0)
                {
                    WeaponCoolDown -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else if (WeaponCoolDown < 0)
                {
                    WeaponCoolDown = 0;
                }
            } else
            {
                base.WeaponDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
