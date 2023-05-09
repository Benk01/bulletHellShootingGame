using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame.Pathing
{
    internal class BulletSpiralPathing : BulletPathing
    {
        private float radius;
        private float angularSpeed;
        private float initialAngle;
        private int iteration;
        private float speed;

        public BulletSpiralPathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            this.radius = 100; // default value for radius
            this.angularSpeed = 0.1f; // default value for angular speed
            this.initialAngle = 0; // default value for initial angle
            this.iteration = 0;
            this.speed = (float)speed;
        }

        public void SetSpiralParams(float radius, float angularSpeed, float initialAngle)
        {
            this.radius = radius;
            this.angularSpeed = angularSpeed;
            this.initialAngle = initialAngle;
        }

        override public Vector2 Move(Vector2 position)
        {
            iteration++;

            float angle = initialAngle + angularSpeed * iteration;
            position.X = (float)(startingPosition.X + radius * Math.Cos(angle)) * this.speed;
            position.Y = (float)(startingPosition.Y + radius * Math.Sin(angle)) - (float)(iteration * -3.0) * this.speed; // subtract a decreasing value to make it move up over time



            return position;
        }



    }
}
