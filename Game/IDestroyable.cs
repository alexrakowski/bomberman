using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game
{
    interface IDestroyable
    {
        int Destroy();
        int GetValue();
        bool IsDead { get; }
        bool IsIndestructible { get; set; }
    }
}
