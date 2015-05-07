﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable.Enemies;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers.Positive
{
    class MoreTime : Modifier
    {
        protected override void OnApply(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            gameInfo.Time += 30;
        }

        protected override void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            return;
        }

        protected override void SetTime()
        {
            Time = 0;
        }

        public override bool EndsOnPlayerDeath
        {
            get { return false; }
        }
    }
}
