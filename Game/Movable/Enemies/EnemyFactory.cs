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
            var enemyInfo = (EnemyInfo)info;
            var startSquare = map.GetSquare(enemyInfo.X, enemyInfo.Y);

            switch (enemyInfo.Type)
            {
                case "Wolf":
                    return new Wolf(map, startSquare);
                case "Owl":
                    return new Owl(map, startSquare);
                case "Fox":
                    return new Fox(map, startSquare);
                default:
                    throw new BombermanException("Unknown Enemy type: " + enemyInfo.Type);
            }
        }
    }
}
