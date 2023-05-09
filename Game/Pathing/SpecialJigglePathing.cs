using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TohoGame.Pathing
{
    internal class SpecialJigglePathing : EntityPathing
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


            if (this.direction.Equals(Direction.Left))
            {
                position.X -= 3;
                if (this.iteration % 2 == 0)
                {
                    this.direction = Direction.Right;
                }
            } 
            else
            {
                position.X += 3;
                if (this.iteration % 2 == 0)
                { 
                    this.direction = Direction.Left;
                }
                   
            }
            iteration++;
            return position;
        }

        public SpecialJigglePathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            // starting direction
            this.direction = Direction.Left;
            this.iteration = 0;
        }
    }
}
