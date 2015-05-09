using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Serialization;

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
            public static MapElement ConstructSquare(IXmlSerializable info)
            {
                MapElement element = null;
                MapElementInfo squareInfo = (MapElementInfo)info;
                int x = squareInfo.X;
                int y = squareInfo.Y;

                switch (squareInfo.Type)
                {
                    case "Ground":
                        element = new Ground(x, y);
                        break;
                    case "Woods":
                        element = new Woods(x, y);
                        break;
                    case "Rock":
                        element = new Rock(x, y);
                        break;
                    default:
                        throw new BombermanException("Could not construct square.");
                }
                if (squareInfo.Collectable != null)
                {
                    var collectableInfo = (CollectableInfo)squareInfo.Collectable;
                    var collectable = CollectableFactory.Construct(collectableInfo);
                    element.AddCollectable(collectable);
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
                List<MapElement> containers = new List<MapElement>();

                foreach (var square in map._mapElements)
                {
                    if (square.IsContainerTerrain)
                    {
                        containers.Add(square);
                    }
                }

                int mapFragmentsCount = FragmentsPerLevel(level);
                int modifiersCount = ModifiersPerLevel(level);

                Utils.Shuffler.Shuffle(containers);
                AddMapFragments(containers, mapFragmentsCount);
                AddModifiers(containers, modifiersCount);
            }

            private static void AddMapFragments(List<MapElement> containers, int mapCount)
            {
                if (containers.Count < mapCount)
                {
                    throw new BombermanException("Not enough space to hide all the Map Fragments.");
                }
                for (int i = 0; i < mapCount; ++i)
                {
                    var square = containers[i];
                    var mapFragment = new MapFragment(square.Position);
                    square.AddCollectable(mapFragment);
                    containers.Remove(square);
                }
            }
            private static void AddModifiers(List<MapElement> containers, int modifiersCount)
            {
                for (int i = 0; i < modifiersCount; ++i)
                {
                    if (containers.Count == 0)
                    {
                        break;
                    }
                    var square = containers[i];
                    var pos = square.Position;
                    var modifier = i % 3 != 0 ? ModifierFactory.GetRandomPositiveModifier() : ModifierFactory.GetRandomNegativeModifier();
                    var bonus = new Bonus(pos, modifier);
                    square.AddCollectable(bonus);
                    containers.Remove(square);
                }
            }
            private static int FragmentsPerLevel(GameLevels level)
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
            private static int ModifiersPerLevel(GameLevels level)
            {
                switch (level)
                {
                    case GameLevels.First:
                        return 4;
                    case GameLevels.Second:
                        return 5;
                    case GameLevels.Third:
                        return 6;
                    default:
                        return 4;
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
