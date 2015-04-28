using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items
{
    abstract class CollectableElement : GameElement, ICollectable, IToInfo
    {
        public abstract void Collect(ICollector collector);

        public CollectableElement(Vector2 position)
        {
            this.Position = position;
        }
    }
}
