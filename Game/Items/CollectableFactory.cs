using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Items
{
    static class CollectableFactory
    {
        public static CollectableElement Construct(CollectableInfo info)
        {
            CollectableElement element = null;
            if (info.Type == "Bonus")
            {
                element = new Bonus(info);
            }
            else
            {
                element = new MapFragment(info);
            }
            return element;
        }
    }
}
