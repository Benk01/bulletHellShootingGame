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
    internal class MainMenu
    {
        internal enum GameState { mainMenu, enterName, inGame, pause, cheating };
        internal GameState state;
        List<GUIElement> elements = new List<GUIElement>();
        List<GUIElement> enterName = new List<GUIElement>();
        private Keys[] lastPressedKeys = new Keys[5];
        private string playerName = "";
        private SpriteFont sf;
        private bool isShift = false;
        private bool isCaps = false;

        internal GameState State { get => state; set => state = value; }

        public MainMenu()
        {
            elements.Add(new GUIElement("menu"));
            elements.Add(new GUIElement("PlayButton"));
            elements.Add(new GUIElement("NameButton"));
            elements.Add(new GUIElement("CheatButton"));

            enterName.Add(new GUIElement("NameForm"));
            enterName.Add(new GUIElement("DoneButton"));

        }

        public void LoadContent(ContentManager content, int window_width, int window_height)
        {
            //sf = content.Load<SpriteFont>("MenuFont");
            // load content for every element involved in menu
            foreach (GUIElement element in elements)
            {
                element.LoadContent(content);
                element.CenterElement(window_width, window_height);
                element.clickEvent += OnClick;
            }
            elements.Find(x => x.AssetName == "CheatButton").MoveElement(0, 100);
            elements.Find(x => x.AssetName == "PlayButton").MoveElement(0, -200);
            elements.Find(x => x.AssetName == "NameButton").MoveElement(0, -50);

            foreach (GUIElement element in enterName)
            {
                element.LoadContent(content);
                element.CenterElement(window_width, window_height);
                element.clickEvent += OnClick;
            }
            enterName.Find(x => x.AssetName == "DoneButton").MoveElement(0, 115);
        }

        public void Update(GameTime gameTime)
        {
            switch (State)
            {
                case GameState.mainMenu:
                    foreach (GUIElement element in elements)
                    {
                        element.Update(gameTime);
                    }
                    break;
                case GameState.enterName:
                    foreach (GUIElement element in enterName)
                    {
                        element.Update(gameTime);
                    }
                    GetKeys();
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case GameState.mainMenu:
                    foreach (GUIElement element in elements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.enterName:
                    foreach (GUIElement element in enterName)
                    {
                        element.Draw(spriteBatch);
                    }
                    spriteBatch.DrawString(sf, playerName, new Vector2(400, 495), Color.Black);
                    break;
                default:
                    break;
            }
        }

        public void OnClick(string element)
        {
            if (element == "PlayButton")
            {
                State = GameState.inGame;
            }
            else if (element == "NameButton")
            {
                State = GameState.enterName;
            }
            else if (element == "DoneButton")
            {
                State = GameState.mainMenu;
            }
            else if (element == "CheatButton")
            {
                State = GameState.cheating;
            }
        }

        public void GetKeys()
        {
            KeyboardState kb_state = Keyboard.GetState();
            Keys[] pressedKeys = kb_state.GetPressedKeys();
            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    // key is no longer pressed
                    OnKeyReleased(key);
                }
            }
            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyPressed(key);
                }
            }
            lastPressedKeys = pressedKeys;
        }

        public void OnKeyReleased(Keys key)
        {
            //if (key == Keys)
            //{

            //}
        }

        public void OnKeyPressed(Keys key)
        {
            if (key == Keys.Back && playerName.Length > 0)
            {
                playerName = playerName.Remove(playerName.Length - 1);
            }
            else if (playerName.Length < 80)
            {
                if (key == Keys.LeftShift || key == Keys.RightShift) { isShift = true; return; }
                if (key == Keys.CapsLock && !isCaps) { isCaps = true; return; }
                else if(key == Keys.CapsLock && isCaps) { isCaps = false; return; }
                if (key == Keys.Space) { playerName += " "; return; }

                if (isShift || isCaps)
                {
                    playerName += key.ToString().ToUpper();
                    isShift = false;
                }
                else
                {
                    playerName += key.ToString().ToLower();
                }
            }
        }

    }
}

