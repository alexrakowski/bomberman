using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    /// <summary>
    /// Represents exceptions related to Bomberman's game logic.
    /// Eg.: incorrect file format, incorrect game values etc.
    /// </summary>
    public class BombermanException : Exception
    {
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public BombermanException() { }

        /// <summary>
        /// Constructor with description.
        /// </summary>
        /// <param name="msg">
        /// Exception description
        /// </param>
        public BombermanException(string msg) : base(msg) { }
    }
}
