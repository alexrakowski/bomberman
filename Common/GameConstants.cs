using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Map;

namespace Bomberman
{
    static class GameConstants
    {
        public static int SQUARE_WIDTH { get { return MapElement.WIDTH; } }
        public static int SQUARE_HEIGTH { get { return MapElement.HEIGHT; } }

        public static int BOARD_WIDTH = 15;
        public static int BOARD_HEIGTH = 9;
    }
}
