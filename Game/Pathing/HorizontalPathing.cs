using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TohoGame.Pathing
{
    internal class HorizontalPathing : EntityPathing
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

            // if on the left side of screen change direction to right
            if (position.X < 0)
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
                position.X -= 10 * this.speed;
            } 
            else
            {
                position.X += 10 * this.speed;
            }

            iteration++;
            return position;
        }

        public HorizontalPathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
            this.speed = (float)speed;
        }
    }
}
