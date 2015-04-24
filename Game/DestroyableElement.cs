using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game
{
    abstract class DestroyableElement : GameElement, IDestroyable
    {
        public abstract int Destroy();

        public abstract int GetValue();

        public bool IsDead { get; protected set; }
    }
}
