using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TohoGame.Pathing;

namespace TohoGame.EnemyNamespace
{
    public static class EnemyFactory
    {
        public static Enemy CreateEnemy(String type, List<String> bulletPathing, List<int> damage, List<double> bulletSpeed, List<Texture2D> bulletTexture, List<int> bulletRadius, EntityPathing pathing, Texture2D texture, float radius, int health, EntityController entities)
        {
            switch (type)
            {
                case "Regular":
                    return new RegularEnemy(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, health, entities);
                case "MidBoss":
                    return new MidBossEnemy(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, health, entities);
                case "FinalBoss":
                    return new FinalBossEnemy(pathing, bulletPathing, damage, bulletSpeed, bulletTexture, bulletRadius, texture, radius, health, entities);
                default:
                    return null;
            }
        }
    }
}
