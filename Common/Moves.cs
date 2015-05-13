using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    /// <summary>
    /// Enumeration of all moves that the user can input during the game.
    /// </summary>
    public enum Moves
    {
        /// <summary>
        /// Direction up (in the menu, or in the game)
        /// Represents the up direction during the game.
        /// Keys: Up or W.
        /// </summary>
        Up,
        /// <summary>
        /// Direction down (in the menu, or in the game)
        /// Represents the down direction during the game.
        /// Keys: Down or S.
        /// </summary>
        Down, 
        /// <summary>
        /// Direction right - only during the game.
        /// Represents the rigth direction during the game.
        /// Keys: Rigth or D.
        /// </summary>
        Right,
        /// <summary>
        /// Direction left - only during the game.
        /// Represents the left direction during the game.
        /// Keys: Left or A
        /// </summary>
        Left, 
        /// <summary>
        /// Move to put the bomb by the adventurer - only during the game.
        /// Default key - Shift.
        /// </summary>
        Fire, 
        /// <summary>
        /// To choose a menu position - only in a menu.
        /// Default key - Enter.
        /// </summary>
        Enter, 
        /// <summary>
        /// To pause the game and show the pause menu.
        /// Default key - Esc
        /// </summary>
        Pause, 
        /// <summary>
        /// Represents a 'null' move - either in menus, or in the game.
        /// </summary>
        None
    }
}
