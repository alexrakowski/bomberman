using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;
using Bomberman.Game.Items;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items
{
    abstract class Modifier : CollectableElement
    {
        public int Time;
        public abstract void Apply(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer);
        public abstract void Update(int elapsedTime, GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer);

        public override void Collect(GameManager manager)
        {
            throw new NotImplementedException();
        }

        public Modifier(Vector2 position) : base(position) { }
    }
}
