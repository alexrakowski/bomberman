using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable.Enemies;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers.Negative
{
    class LessBombs : Modifier
    {
        protected override void OnApply(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            if (adventurer.BombsLimit > 1)
                adventurer.BombsLimit--;
        }

        protected override void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
        }

        protected override void SetTime()
        {
            Time = 0;
        }
        public LessBombs() : base() { }

        public override bool EndsOnPlayerDeath
        {
            get { return false; }
        }
    }
}
