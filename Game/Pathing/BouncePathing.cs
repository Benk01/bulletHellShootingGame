using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TohoGame.Pathing
{
    internal class BouncePathing : EntityPathing
    {
        enum Direction
        {
            Left,
            Right
        }

        private Direction direction;
        private int iteration;
        private float speed;

        override public Vector2 Move(Vector2 position) {
            if (position.X< 0)
            {
                this.direction = Direction.Right;
            }

            // vice-versa
            if (position.X > 1400)
            {
                this.direction = Direction.Left;
    }

            // if direction is left, move left. else if direction is right move right
            if (this.direction.Equals(Direction.Left))
            {
        if (iteration % 300 < 50)
        {
            position.X -= 10 * this.speed;
                    position.Y += 2 * this.speed;
                }
        else if (iteration % 300 < 150)
        {
            position.X -= 10 * this.speed;
                }
        else if (iteration % 300 < 250)
        {
            position.X -= 10 * this.speed;
                }
        else
        {
            position.X -= 10 * this.speed;
                    position.Y -= 2 * this.speed;
                }
    }
            else
            {
        if (iteration % 300 < 50)
        {
            position.X += 10 * this.speed;
                    position.Y += 2 * this.speed;
                }
        else if (iteration % 300 < 150)
        {
            position.X += 10 * this.speed;
                }
        else if (iteration % 300 < 250)
        {
            position.X += 10 * this.speed;
                }
        else
        {
            position.X += 10 * this.speed;
            position.Y -= 2 * this.speed;
        }
    }

    iteration++;
            return position;
    }

    public BouncePathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
            this.speed = (float)speed;
        }
    }
}
