using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TohoGame.Pathing
{
    public abstract class EntityPathing
    {
        public Vector2 startingPosition;
        public double speed;

        abstract public Vector2 Move(Vector2 position);

        public EntityPathing(double speed, Vector2 startingPosition)
        {
            this.startingPosition = startingPosition;
            this.speed = speed;
        }
    }
}
