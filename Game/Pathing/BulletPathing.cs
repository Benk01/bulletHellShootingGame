﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame.Pathing
{
    internal class BulletPathing : EntityPathing
    {
        enum Direction
        {
            Left,
            Right
        }

        private Direction direction;
        private int iteration;

        override public Vector2 Move(Vector2 position)
        {

            position.Y -= 20;
            return position;
        }

        public BulletPathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
        }
    }
}
