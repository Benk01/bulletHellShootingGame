using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//ZigzagBulletPathing
namespace TohoGame.Pathing
{
    internal class BulletZigzagPathing : EntityPathing
    {
        enum Direction
        {
            Left,
            Right
        }

        private Direction direction;
        private int iteration;
        private float speed;

        override public Vector2 Move(Vector2 position)
        {
            // move diagonally
            if (direction == Direction.Left)
            {
                position.X -= 10 * this.speed;
                position.Y += 5 * this.speed;
            }
            else
            {
                position.X += 10 * this.speed;
                position.Y += 5 * this.speed;
            }

            // change direction every 10 iterations
            if (++iteration % 10 == 0)
            {
                direction = direction == Direction.Left ? Direction.Right : Direction.Left;
            }

            return position;
        }

        public BulletZigzagPathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
            this.speed = (float)speed;
        }
    }
}
