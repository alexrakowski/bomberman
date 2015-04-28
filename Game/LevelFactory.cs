using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Map;
using Bomberman.Game.Movable;

namespace Bomberman.Game
{
    static class LevelFactory
    {
        public static void CreateLevel(GameLevels level, Map.Map map, ref GameInfo gameInfo, out List<Enemy> enemies)
        {
            int mapFragmentsToFind = GetMapFragmentsToFind(level);
            if (gameInfo == null)
            {
                gameInfo = new Game.GameInfo("Alek", mapFragmentsToFind, level);
            }
            else
            {
                var newInfo = new GameInfo("Alek", mapFragmentsToFind, level, gameInfo.Score, gameInfo.Lifes);
                gameInfo = newInfo;
            }
            enemies = GetEnemies(level, map);
        }

        private static int GetMapFragmentsToFind(GameLevels level)
        {
            switch (level)
            {
                case GameLevels.First:
                    return 3;
                case GameLevels.Second:
                    return 4;
                case GameLevels.Third:
                    return 5;
                default:
                    return 3;
            }
        }
        private static List<Enemy> GetEnemies(GameLevels level, Map.Map map)
        {
            List<Enemy> enemies = new List<Enemy>();
            int wolvesCount = 0;
            int owlsCount = 0;
            int foxesCount = 0;
            int bearsCount = 0;

            switch (level)
            {
                case GameLevels.First:
                    wolvesCount = 4;
                    owlsCount = 2;
                    break;
                case GameLevels.Second:
                    wolvesCount = 3;
                    owlsCount = 3;
                    foxesCount = 1;
                    bearsCount = 1;
                    break;
                case GameLevels.Third:
                    wolvesCount = 2;
                    owlsCount = 2;
                    foxesCount = 4;
                    bearsCount = 3;
                    break;
                default:
                    wolvesCount = 4;
                    owlsCount = 2;
                    break;
            }

            var freeSquares = map.GetUnoccupiedSquares();
            var adventurerSquare = map.GetStartSquare();

            Utils.Shuffler.Shuffle(freeSquares);

            MapElement square;
            for (int i = 0; i < wolvesCount; ++i)
            {
                // put enemies in a distance from adventurer's starting point
                do
                {
                    if (freeSquares.Count < 1)
                    {
                        throw new BombermanException("Not enough space for the enemies");
                    }
                    square = freeSquares.First();
                    freeSquares.Remove(square);
                } while (Map.Map.GetSquaresDistance(square, adventurerSquare) < 5);

                var wolf = new Wolf(map, square);
                enemies.Add(wolf);
            }
            return enemies;
        }
    }
}
