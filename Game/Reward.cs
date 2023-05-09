using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace TohoGame
{
    public class Reward : Entity
    {
        //Base On Entity implement Reward
        public Reward(Texture2D texture, Vector2 position, float radius) : base(texture, position, radius)
        {
            HitBox = new List<(int, int)>() { (0, 0), (0, texture.Height), (texture.Width, texture.Height), (texture.Width, 0) };
        }
    }
}
