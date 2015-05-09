using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable.Enemies;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers.Positive
{
    class Indestructible : Modifier
    {
        private int _wholeTime = 200;
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
            Time = _wholeTime;
        }
        public Indestructible(int time)
        {
            _wholeTime = time;
        }
        public Indestructible() : base() { }

        public override bool EndsOnPlayerDeath
        {
            get { return true; }
        }
    }
}
