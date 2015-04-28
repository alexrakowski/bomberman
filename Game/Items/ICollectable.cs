using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;

namespace Bomberman.Game.Items
{
    interface ICollectable
    {
        void Collect(ICollector collector);
    }   
}
