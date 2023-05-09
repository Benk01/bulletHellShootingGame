using Microsoft.Xna.Framework;
using System;

namespace TohoGame.Pathing
{
    internal class PathingFactory
    {
        // Takes a pathing type and creates the correct pathing child class (e.g. GruntPathing1) according to that type
        public EntityPathing CreatePathing(String pathingType, double speed, Vector2 startingPosition)
        {
            switch (pathingType)
            {
                case "BulletPathing":
                    return new BulletPathing(speed, startingPosition);
                case "BulletPathing2":
                    return new BulletPathing2(speed, startingPosition);
                case "GruntPathing1":
                    return new HorizontalPathing(speed, startingPosition);
                case "SpeedChangePathing":
                    return new SpeedChangePathing(speed, startingPosition);
                case "BouncePathing":
                    return new BouncePathing(speed, startingPosition);
                default:
                    return null;
            }
        }
    }
}
