using Microsoft.Xna.Framework;
using System;

namespace TohoGame.Pathing
{
    internal class BulletPathingFactory
    {
        public EntityPathing CreateBulletPathing(String pathingType, double speed, Vector2 startingPosition)
        {
            switch (pathingType)
            {
                case "BulletPathing":
                    return new BulletPathing(speed, startingPosition);
                case "BulletPathing2":
                    return new BulletPathing2(speed, startingPosition);
                case "BulletZigzagPathing":
                    return new BulletZigzagPathing(speed, startingPosition);
                case "BulletCircularPathing":
                    return new BulletCircularPathing(speed, startingPosition);
                case "BulletSpiralPathing":
                    return new BulletSpiralPathing(speed, startingPosition);
                default:
                    return null;
            }
        }
    }
}
