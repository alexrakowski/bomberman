using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable.Enemies;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers
{
    class Indestructible : Modifier
    {
        protected override void OnApply(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            adventurer.IsIndestructible = true;
        }

        protected override void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            adventurer.IsIndestructible = false;
        }

        protected override void SetTime()
        {
            Time = 200;
        }
        public Indestructible(int time)
        {
            Time = time;
        }
        public Indestructible() : base() { }

        public override bool EndsOnPlayerDeath
        {
            get { return true; }
        }
    }
}
