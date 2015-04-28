using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game.Items
{
    interface ICollector
    {
        void AddMapFragment();
        void AddModifier(Modifiers.Modifier mod);
    }
}
