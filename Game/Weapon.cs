using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame
{
    public class Weapon //we can probably implement different subclasses for patterns
    {
        public int FireRate { get; set; }
        public int BulletSpeed { get; set; }
        public int Damage { get; set; }

        public Weapon(int fireRate, int bulletSpeed, int damage)
        {
            FireRate = fireRate;
            BulletSpeed = bulletSpeed;
            Damage = damage;
        }
    }
}

