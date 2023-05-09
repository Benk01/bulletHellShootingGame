using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame.Pathing;

//<summary>
//You can create a MidBossEnemy instance with a countdown time like this:
//MidBossEnemy midBoss = new MidBossEnemy(pathing, midBossTexture, midBossRadius, midBossHealth); // 27 seconds countdown time

//after 27 seconds the midBoss will move to position Vector2(-1000, -1000) and disappear from screen
//However, it is not actually removed
//The Game/UI class must remove MidBossEnemy instance when countdownTime < 0
//</summary>


namespace TohoGame.EnemyNamespace
{

    internal class MidBossEnemy : Enemy
    {
        
        private List<String> bulletPathing;
        //Game1.cs can use this to get the countdownTime and display it on screen
        public MidBossEnemy(EntityPathing pathing, List<String> bulletPathing, List<int> damage, List<double> bulletSpeed, List<Texture2D> bulletTexture, List<int> bulletRadius, Texture2D texture, float radius, int health, EntityController entities) : base(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, health, entities)
        {

            base.CountdownTime = 26.0f;
            this.bulletPathing = bulletPathing;
            base.WeaponCoolDown = 0.3;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //switching Bulletpathing -> erase this if you want to set all Bullet pathing from Game/Jsom 
            if (base.CountdownTime <= 21)
            {
                this.BulletPathingType = bulletPathing[1];

            }
            if (base.CountdownTime <= 17)
            {
                this.BulletPathingType = bulletPathing[2];

            }
            if (base.CountdownTime <= 12)
            {
                this.BulletPathingType = bulletPathing[3];

            }
            if (base.CountdownTime <= 8)
            {
                this.BulletPathingType = bulletPathing[4];

            }
            //switching Bulletpathing code Ends

            if (base.WeaponDelay < 0)
            {
                if (WeaponCoolDown <= 0)
                {
                    entities.EnemyFireBullet(this);
                    WeaponCoolDown = 0.3;
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
