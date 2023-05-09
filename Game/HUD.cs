using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame;

namespace TohoGame
{
    public class HUD
    {
        private SpriteFont _font;
        public HUD(SpriteFont font)
        {
            _font = font;
        }

        public void DrawHealth(SpriteBatch spriteBatch, int health, int lives)
        {
            //SpriteFont font = Content.Load()
            spriteBatch.DrawString(_font, "Health: " + health + " Lives: " + lives, new Vector2(0, 0), Color.Red);
        }
        public void DrawBomb(SpriteBatch spriteBatch, int bomb)
        {
            //SpriteFont font = Content.Load()
            spriteBatch.DrawString(_font, " Bomb(B): " + bomb, new Vector2(0, 50), Color.Red);
        }

    }
}
