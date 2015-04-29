using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Movable.Enemies
{
    static class EnemyFactory
    {
        public static Enemy Construct(IXmlSerializable info, Map.Map map)
        {
            Enemy enemy = null;
            var enemyInfo = (EnemyInfo)info;
            var startSquare = map.GetSquare(enemyInfo.X, enemyInfo.Y);

            switch (enemyInfo.Type)
            {
                case "Wolf":
                    enemy = new Wolf(map, startSquare);
                    break;
                case "Owl":
                    enemy = new Owl(map, startSquare);
                    break;
            }

            return enemy;
        }
    }
}
