using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game.Movable
{
    public interface IMovable
    {
        void Move(int elapsedTime, Moves move = Moves.None);
        bool IsMoving { get; }
        Moves Direction { get; }
    }
}
