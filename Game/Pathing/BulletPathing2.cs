using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame.Pathing
{
    internal class BulletPathing2 : EntityPathing
    {
        enum Direction
        {
            Left,
            Right
        }

        private Direction direction;
        private int iteration;
        private double speed;

        override public Vector2 Move(Vector2 position)
        {

            position.Y += (float)(15 * this.speed);
            return position;
        }

        public BulletPathing2(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
            this.speed = speed;
        }
    }
}
