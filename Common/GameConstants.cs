using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Map;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    static class GameConstants
    {
        public static int SQUARE_WIDTH { get { return MapElement.WIDTH; } }
        public static int SQUARE_HEIGTH { get { return MapElement.HEIGHT; } }

        public static int BOARD_WIDTH = 15;
        public static int BOARD_HEIGTH = 9;

        public static int SQUARE_RADIUS = SQUARE_WIDTH / 2;

        public static int DisplayWidth 
        {
            get
            {
                if (_graphics == null)
                    return 0;
                else
                    return _graphics.PreferredBackBufferWidth;
            }        
        }
        public static int DisplayHeigth
        {
            get
            {
                if (_graphics == null)
                    return 0;
                else
                    return _graphics.PreferredBackBufferHeight;
            }
        }
        private static GraphicsDeviceManager _graphics;

        public static void SetDisplaySize(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            //MapElement.UpdateSquareSize(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }
    }
}
