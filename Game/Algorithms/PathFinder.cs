using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable;

namespace Bomberman.Game.Algorithms
{
    static class PathFinder
    {
        public static Moves GetMoveTo(Map.Map map, MovableElement movableFrom, MovableElement movableTo)
        {
            var g = map.ToGraph(movableFrom);
            int from = map.GetSquareIndex(movableFrom.X, movableFrom.Y);
            int to = map.GetSquareIndex(movableTo.X, movableTo.Y);

            int next = BreadthFirstSearch.ComputePath(g, from, to);
            Moves move = map.IndexToMove(next, movableFrom.X, movableFrom.Y);
            return move;
        }
    }
}
