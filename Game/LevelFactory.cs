using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Items;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Map;
using Bomberman.Game.Movable;
using Bomberman.Game.Movable.Enemies;
using Bomberman.Game.Serialization;

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
            int totalCount = wolvesCount + owlsCount + foxesCount;

            var adventurerSquare = map.GetStartSquare();
            var freeSquares = map.GetUnoccupiedSquares();
            // put enemies in a distance from adventurer's starting point
            freeSquares.RemoveAll(s => (Map.Map.GetSquaresDistance(s, adventurerSquare) < 5));
            if (freeSquares.Count < totalCount)
            {
                throw new BombermanException("Not enough space for the enemies");
            }

            Utils.Shuffler.Shuffle(freeSquares);

            MapElement square;
            for (int i = 0; i < wolvesCount; ++i)
            {
                square = freeSquares.First();
                var wolf = new Wolf(map, square);
                enemies.Add(wolf);
                freeSquares.Remove(square);
            }
            for (int i = 0; i < owlsCount; ++i)
            {
                square = freeSquares.First();
                var owl = new Owl(map, square);
                enemies.Add(owl);
                freeSquares.Remove(square);
            }
            for (int i = 0; i < foxesCount; ++i)
            {
                square = freeSquares.First();
                var fox = new Fox(map, square);
                enemies.Add(fox);
                freeSquares.Remove(square);
            }

            return enemies;
        }

        public static void LoadLevel(GameState gameState, out GameInfo gameInfo, out Map.Map map, out Adventurer adventurer, 
            out List<Enemy> enemies, out List<Bomb> bombs, out List<Modifier> modifiers)
        {
            gameInfo = (GameInfo)gameState.GameInfo;
            map = new Map.Map(gameState.Map);

            enemies = new List<Enemy>();
            if (gameState.Enemies != null)
            {
                foreach (var info in gameState.Enemies)
                {
                    var enemy = EnemyFactory.Construct(info, map);
                    enemies.Add(enemy);
                }
            }

            bombs = new List<Bomb>();
            if (gameState.Bombs != null)
            {
                foreach (var info in gameState.Bombs)
                {
                    var bomb = new Bomb(info);
                    bombs.Add(bomb);
                    map.OccupySquare(bomb);
                }
            }

            adventurer = Adventurer.ConstructInstance(gameState.Player, map, bombs);
            map.OccupySquare(adventurer);

            modifiers = new List<Modifier>();
            if (gameState.Modifiers != null)
            {
                foreach (var info in gameState.Modifiers)
                {
                    var modifier = ModifierFactory.Construct(info);
                    modifiers.Add(modifier);
                    modifier.Apply(gameInfo, enemies, adventurer);
                }
            }
        }
    }
}
