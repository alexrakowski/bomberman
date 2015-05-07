using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Movable.Enemies;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers
{
    class ParalyzeEnemies : Modifier
    {
        protected override void OnApply(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            foreach (var enemy in enemies)
            {
                enemy.Paralyze();
            }
        }

        protected override void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer)
        {
            foreach (var enemy in enemies)
            {
                enemy.UnParalyze();
            }
        }

        protected override void SetTime()
        {
            Time = 150;
        }

        public override bool EndsOnPlayerDeath
        {
            get { return true; }
        }
    }
}
