using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame.Pathing
{
    internal class BulletCircularPathing : BulletPathing
    {
        private float radius;
        private float angularSpeed;
        private float initialAngle;

        private int iteration;
        private float speed;

        public BulletCircularPathing(double speed, Vector2 startingPosition) : base(speed, startingPosition)
        {
            this.radius = 100;
            this.angularSpeed = MathHelper.Pi / 30;
            this.initialAngle = MathHelper.PiOver2;
            this.speed = (float)speed;
        }

        public bool IsCompleted { get; private set; }

        override public Vector2 Move(Vector2 position)
        {
            iteration++;

            float angle = initialAngle + angularSpeed * iteration;
            position.X = (float)(startingPosition.X + radius * Math.Cos(angle)) * this.speed;
            position.Y = (float)(startingPosition.Y + radius * Math.Sin(angle)) * this.speed;

            // Adjust position based on entity position
            //position += EntityPosition;

            if (iteration >= 60)
            {
                IsCompleted = true;
                // Disappear when countdown reaches zero
                return new Vector2(-1000, -1000);
            }

            return position;
        }
    }

}


