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
    internal class FinalBossEnemy : Enemy
    {
        float radius;
        int health;
        private int specialBullets;
        bool startSpecial1, startSpecial2, endSpecial1, endSpecial2, isSpecialActive;
        private List<String> bulletPathing;
        private EntityPathing originalPathing;
        public FinalBossEnemy(
            EntityPathing pathing,
            List<String> bulletPathing, List<int> damage, 
            List<double> bulletSpeed, 
            List<Texture2D> bulletTexture, 
            List<int> bulletRadius,
            Texture2D texture, 
            float radius, 
            int health,
            EntityController entities
        ) : base(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, 500, entities)
        {
            this.pathing = pathing;
            base.Texture = texture;
            this.radius = radius;
            this.health = health;
            base.CountdownTime = 50.0f;
            this.bulletPathing = bulletPathing;
            this.startSpecial1 = true;
            this.startSpecial2 = true;
            this.endSpecial1 = true;
            this.endSpecial2 = true;
            this.isSpecialActive = false;
            base.WeaponCoolDown = 0.3;
            this.specialBullets = 28;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //switching Bulletpathing -> erase this if you want to set all Bullet pathing from Game/Jsom 
            if (base.CountdownTime <= 40)
            {
                this.BulletPathingType = bulletPathing[1];
            }

            if (base.CountdownTime <= 35 && startSpecial1)
            {
                this.specialBullets = 28;
                base.Position = new Vector2(500, 50);
                this.originalPathing = base.pathing;
                base.pathing = new SpecialJigglePathing(originalPathing.speed, new Vector2(0, 0));
                startSpecial1 = false;
                this.isSpecialActive = true;
            }

            if (base.CountdownTime <= 28 && endSpecial1)
            {
                base.pathing = this.originalPathing;
                this.BulletPathingType = bulletPathing[2];
                endSpecial1 = false;
                this.isSpecialActive = false;
            }


            if (base.CountdownTime <= 23)
            {
                this.BulletPathingType = bulletPathing[3];

            }

            if (base.CountdownTime <= 17 && startSpecial2)
            {
                this.specialBullets = 28;
                base.Position = new Vector2(800, 50);
                this.originalPathing = base.pathing;
                base.pathing = new SpecialJigglePathing(originalPathing.speed, new Vector2(0, 0));
                startSpecial2 = false;
                this.isSpecialActive = true;
            }

            if (base.CountdownTime <= 10 && endSpecial2)
            {
                base.pathing = this.originalPathing;
                this.BulletPathingType = bulletPathing[4];
                endSpecial2 = false;
                this.isSpecialActive = false;
            }

            //switching Bulletpathing code Ends

            if (WeaponDelay < 0)
            {
                if (WeaponCoolDown <= 0)
                {
                    if (this.isSpecialActive)
                    {
                        if (this.specialBullets > 0)
                        {

                            entities.BossSpecialFireBullet(this);
                            WeaponCoolDown = 0.1;
                            this.specialBullets--;
                        }
                    }
                    else
                    {
                        entities.EnemyFireBullet(this);
                        WeaponCoolDown = 0.3;
                    }
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
