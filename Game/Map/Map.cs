using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Game.Map
{
    partial class Map : IDrawable
    {
        public const short MAP_WIDTH = 15;
        public const short MAP_HEIGHT = 9;

        private MapElement[,] _mapElements;
        public Tuple<int, int> startPosition { get; private set; }
        public MapElement GetStartSquare() { return _mapElements[startPosition.Item1, startPosition.Item2]; }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var mapObject in _mapElements)
            {
                mapObject.Draw(spriteBatch);
            }
        }
        public void LoadContent(ContentManager content)
        {
            foreach (var mapObject in _mapElements)
            {
                mapObject.LoadContent(content);
            }
        }

        public MapElement GetSquare(int x, int y)
        {
            if (x >= 0 && x <= _mapElements.GetUpperBound(0) && y>=0 && y <= _mapElements.GetUpperBound(1))
            {
                return _mapElements[x, y];
            }
            else
            {
                return null;
                //throw new BombermanException("Square " + x + ", " + y + "does not exist.");
            }
        }
        public List<MapElement> GetUnoccupiedSquares()
        {
            List<MapElement> freeSquares = new List<MapElement>();

            foreach (var square in _mapElements)
            {
                if (square.IsWalkingTerrain && !square.IsOccupied)
                {
                    freeSquares.Add(square);
                }
            }
            
            return freeSquares;
        }
        public int DestroySquare(int x, int y)
        {
            int value = 0;
            MapElement square = this.GetSquare(x, y);
            if (square != null)
            {
                if (square.CanBeAffected)
                {
                    value += square.DestroyOccupiables();
                }
                if (square.CanBeDestroyed)
                {
                    value += square.Destroy();
                    this._mapElements[x, y] = new Ground(x, y);
                    if ( square.Collectable != null)
                        this._mapElements[x, y].AddCollectable(square.Collectable);
                }
            }
            return value;
        }
        public int DestroySquare(MapElement square)
        {
            return DestroySquare(square.X, square.Y);
        }
        public MapElement OccupySquare(DestroyableElement element)
        {
            var square = GetSquare(element.X, element.Y);
            square.Occupy(element);

            return square;
        }
        public void OccupySquare(int x, int y, DestroyableElement element)
        {
            var square = GetSquare(x, y);
            square.Occupy(element);
        }
        public void LeaveSquare(DestroyableElement element)
        {
            var square = GetSquare(element.X, element.Y);
            square.Leave(element);
        }

        public static double GetSquaresDistance(MapElement square1, MapElement square2)
        {
            double distance = 0;
            double xDistance, yDistance;

            xDistance = Math.Abs(square1.X - square2.X);
            yDistance = Math.Abs(square1.Y - square2.Y);
            distance = Math.Max(xDistance, yDistance);

            return distance;
        }

        private Map(MapElement[,] elements, int x, int y) { 
            _mapElements = elements;
            startPosition = new Tuple<int, int>(x, y);
        }
    }
}
