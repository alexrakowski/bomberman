using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game.Items;

namespace Bomberman.Game.Map
{
    partial class Map
    {
        public static class MapFactory
        {
            const string MAP_WIDTH_ERR_MSG = "Map file width is not correct.";
            const string MAP_HEIGTH_ERR_MSG = "Map file height is not correct.";

            public static Map MakeMap(string[][] mapFile, GameLevels level)
            {
                var map = ParseMapFile(mapFile);
                AddCollectables(map, level);
                return map;
            }
            private static Map ParseMapFile(string[][] mapFile)
            {
                ValidateMapFile(mapFile);

                bool isStartingPoint = false;
                Tuple<int, int> startPoint = new Tuple<int,int>(0,0);
                MapElement[,] elements = new MapElement[Map.MAP_WIDTH, Map.MAP_HEIGHT];
                for (int i = 0; i < Map.MAP_WIDTH; ++i)
                    for (int j = 0; j < Map.MAP_HEIGHT; ++j)
                    {
                        elements[i, j] = ParseSquare(mapFile[j][i], i, j, out isStartingPoint);
                        if (isStartingPoint) startPoint = new Tuple<int, int>(i, j);
                    }

                var map = new Map(elements, startPoint.Item1, startPoint.Item2);
                return map;
            }
            private static MapElement ParseSquare(string squareText, int x, int y, out bool isStartingPoint)
            {
                MapElement element = null;
                var squareVal = Int16.Parse(squareText);
                isStartingPoint = false;

                switch (squareVal)
                {
                    case 0:
                        isStartingPoint = true;
                        element = new Ground(x, y);
                        break;
                    case 1:
                        element = new Ground(x, y);
                        break;
                    case 2:
                        element = new Woods(x, y);
                        break;
                    case 3:
                        element = new Rock(x, y);
                        break;
                    default:
                        throw new BombermanException("Could not parse square " + squareText);
                }
                return element;
            }
            private static void ValidateMapFile(string[][] mapFile)
            {
                if (mapFile.Length != Map.MAP_HEIGHT)
                {
                    throw new BombermanException(MAP_HEIGTH_ERR_MSG);
                }
                for (int i = 0; i < mapFile.Rank; ++i)
                {
                    if (mapFile[i].Length != Map.MAP_WIDTH)
                    {
                        throw new BombermanException(MAP_WIDTH_ERR_MSG);
                    }
                }
            }

            private static void AddCollectables(Map map, GameLevels level)
            {
                List<MapElement> destroyables = new List<MapElement>();

                foreach (var square in map._mapElements)
                {
                    if (square.CanBeDestroyed)
                    {
                        destroyables.Add(square);
                    }
                }

                int mapFragmentsCount = 0;
                switch (level)
                {
                    case GameLevels.First:
                        mapFragmentsCount = 3;
                        break;
                    case GameLevels.Second:
                        mapFragmentsCount = 4;
                        break;
                    case GameLevels.Third:
                        mapFragmentsCount = 5;
                        break;
                }

                var rand = new Random();
                for (int i = 0; i < mapFragmentsCount; ++i)
                {
                    var square = destroyables[rand.Next() % destroyables.Count];
                    var mapFragment = new MapFragment(square.Position);
                    square.AddCollectable(mapFragment);
                    destroyables.Remove(square);
                }
            }


            /// <summary>
            /// For testing etc.
            /// </summary>
            /// <returns>
            /// Map consisting only of Earth cells
            /// </returns>
            public static Map GetBasicMap()
            {
                MapElement[,] objects = new MapElement[Map.MAP_WIDTH, Map.MAP_HEIGHT];
                for (int i = 0; i < Map.MAP_WIDTH; ++i)
                    for (int j = 0; j < Map.MAP_HEIGHT; ++j)
                        objects[i, j] = new Ground(i, j);

                return new Map(objects, 0, 0);
            }
        }
    }
}
