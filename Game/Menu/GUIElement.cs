using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame.Menu
{
    internal class GUIElement
    {
        private Texture2D menuTexture;
        private Rectangle menuRect;
        private string assetName;

        public string AssetName { get => assetName; set => assetName = value; }

        public delegate void ElementClicked(string element);
        public event ElementClicked clickEvent;
        public GUIElement(string assetName)
        {
            this.AssetName = assetName;
        }

        public void LoadContent(ContentManager content)
        {
            menuTexture = content.Load<Texture2D>(AssetName);
            menuRect = new Rectangle(0, 0, menuTexture.Width, menuTexture.Height);
        }

        public void Update(GameTime gameTime)
        {
            // check if we clicked any gui element
            if (menuRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(AssetName);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuTexture, menuRect, Color.White);
        }

        public void CenterElement(int height, int width)
        {
            menuRect = new Rectangle(
                width / 2 - menuTexture.Width / 2,
                height / 2 - menuTexture.Height / 2,
                menuTexture.Width, menuTexture.Height
            );
        }

        public void MoveElement(int height, int width)
        {
            menuRect = new Rectangle(
                menuRect.X += height,
                menuRect.Y += width,
                menuRect.Width,
                menuRect.Height
            );
        }
    }
}
