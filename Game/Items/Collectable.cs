using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;

namespace Bomberman.Game.Items
{
    interface ICollectable
    {
        void Collect(GameManager manager);
    }
    abstract class CollectableItem : GameElement, ICollectable
    {
        public void Collect(GameManager manager)
        {
            throw new NotImplementedException();
        }
    }
    
}
