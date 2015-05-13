using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Bomberman.Game
{
    /// <summary>
    /// Enumeration of all Game Levels.
    /// </summary>
    public enum GameLevels
    {
        /// <summary>
        /// First level
        /// </summary>
        [Description("1")]
        First,
        /// <summary>
        /// Second level
        /// </summary>
        [Description("2")]
        Second,
        /// <summary>
        /// Third (last) level
        /// </summary>
        [Description("3")]
        Third
    }
}
