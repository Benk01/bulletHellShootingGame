using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TohoGame
{
    using KeybordKey = Microsoft.Xna.Framework.Input.Keys;
    public class Player : Entity
    {
        public int baseSpeed;
        public int currentSpeed;
        private PlayArea playArea; // player movement restrictions {X, Y, width, height}
        public double WeaponCoolDown;
        private HUD _hud;
        private int _health { get; set; }
        private int _maxHealth;
        private int _lives;
        public bool godMode = false;
        public int _bombCount;

        //Should not be here

        private class PlayArea
        {
            public int X;
            public int Y;
            public int Height;
            public int Width;

            public PlayArea(int x, int y, int width, int height)
            {
                X = x;
                Y = y;
                Height = height;
                Width = width;
            }
        }

        public Player(Texture2D texture, Texture2D BulletTexture, Vector2 position, float radius, int health, int window_width, int window_height, HUD hud) : base(texture, position, radius)
        {
            _health = health;
            _maxHealth = 100;
            _lives = 3;
            baseSpeed = 3;
            currentSpeed = baseSpeed;
            playArea = new PlayArea(0, 0, window_width, window_height);
            base.HitBox = new List<(int, int)>() { (50, 0), (50, 200), (150, 200), (150, 0) };
            WeaponCoolDown = 0;
            base.BulletTexture = BulletTexture;
            base.BulletSpeed = 1.0;
            base.BulletRadius = 10;
            base.Damage = 10;
            _hud = hud;
            _bombCount = 3;
        }

        public override void Update(GameTime gameTime)
        {
            if (WeaponCoolDown > 0)
            {
                WeaponCoolDown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (WeaponCoolDown < 0)
            {
                WeaponCoolDown = 0;
            }
            if (GetHealth() <= 0)
            {
                die();
            }
            // Add player-specific update logic here
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Add player-specific draw logic here
            base.Draw(spriteBatch);
            _hud.DrawHealth(spriteBatch, _health, _lives);
            _hud.DrawBomb(spriteBatch, _bombCount);
        }
        public bool reachedBottom()
        {
            foreach ((int, int) point in HitBox)
            {
                if (this.Position.Y + point.Item2 >= this.playArea.Y + this.playArea.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public bool reachedTop()
        {
            foreach ((int, int) point in HitBox)
            {
                if (this.Position.Y + point.Item2 <= this.playArea.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public bool reachedRightSide()
        {
            foreach ((int, int) point in HitBox)
            {
                if (this.Position.X + point.Item1 >= this.playArea.X + this.playArea.Width)
                {
                    //Debug.WriteLine(this.Position.X + point.Item1);
                    return true;
                }
            }
            return false;
        }
        public bool reachedLeftSide()
        {
            foreach ((int, int) point in HitBox)
            {
                if (this.Position.X + point.Item1 <= this.playArea.X)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHealth()
        {
            return _health;
        }

        public override void TakeDamage(int damage)
        {
            if (godMode)
            {
                return;
            }
            if (damage <= 0)
            {
                return;
            }
            else
            {
                _health -= damage;
                if (_health <= 0 && _lives > 0)
                {
                    _lives--;
                    int leftoverDamage = Math.Abs(_health);
                    _health = _maxHealth;
                    TakeDamage(leftoverDamage);
                }
            }
        }

        public void DecreaseBombCount()
        {
            _bombCount--;
        }
        public void IncreaseBombCount()
        {
            _bombCount++;
        }

        public int GetBombCount()
        {
            return _bombCount;
        }




    }

}
