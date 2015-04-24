using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public class BombermanException : Exception
    {
        public BombermanException() { }
        public BombermanException(string msg) : base(msg) { }
    }
}
