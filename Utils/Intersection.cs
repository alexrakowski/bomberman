using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Bomberman.Game.Map;

namespace Bomberman.Utils
{
    static class Intersection
    {
        public static bool CheckElementsCollision(Element elem1, Element elem2)
        {
            int radius = MapElement.WIDTH / 2;
            return Utils.Intersection.CheckCircleIntersection(elem1.Position, elem2.Position, radius);
        }

        public static bool CheckCircleIntersection(Vector2 pos1, Vector2 pos2, int radius)
        {
            //(R0-R1)^2 <= (x0-x1)^2+(y0-y1)^2 <= (R0+R1)^2
            double val = Math.Pow((pos1.X - pos2.X), 2) + Math.Pow((pos1.Y - pos2.Y), 2);

            return (0 <= val) && (val <= Math.Pow(2 * radius, 2));
        }
    }
}
