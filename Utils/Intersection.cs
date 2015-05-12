using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Bomberman.Game.Map;

namespace Bomberman.Utils
{
    /// <summary>
    /// Class for elements intersection checking
    /// </summary>
    public static class Intersection
    {
        /// <summary>
        /// Checks collision of two elements using Circle Intersection.
        /// The radius of the circle that is used is of a circle bounded into a game square - 
        /// half of the sqaure's width.
        /// </summary>
        /// <param name="elem1">First element to check</param>
        /// <param name="elem2">Second element to check</param>
        /// <returns>
        /// True if elements collide.
        /// </returns>
        public static bool CheckElementsCollision(Element elem1, Element elem2)
        {
            int radius = GameConstants.SQUARE_RADIUS;
            return Utils.Intersection.CheckCircleIntersection(elem1.Position, elem2.Position, radius);
        }

        /// <summary>
        /// Checks intersection of two circles with the same radius.
        /// </summary>
        /// <param name="pos1">Center of first circle</param>
        /// <param name="pos2">Center of second circle</param>
        /// <param name="radius">Radius of both circles</param>
        /// <returns>
        /// True if circles collide.
        /// </returns>
        public static bool CheckCircleIntersection(Vector2 pos1, Vector2 pos2, int radius)
        {
            //(R0-R1)^2 <= (x0-x1)^2+(y0-y1)^2 <= (R0+R1)^2
            double val = Math.Pow((pos1.X - pos2.X), 2) + Math.Pow((pos1.Y - pos2.Y), 2);

            return (0 <= val) && (val <= Math.Pow(2 * radius, 2));
        }
    }
}
