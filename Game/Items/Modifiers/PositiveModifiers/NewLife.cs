using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers.PositiveModifiers
{
    class NewLife : Modifier
    {
        protected override void OnApply(GameInfo gameInfo, List<Movable.Enemy> enemies, Movable.Adventurer adventurer)
        {
            gameInfo.AddLife();
        }

        protected override void OnTimeEnded(GameInfo gameInfo, List<Movable.Enemy> enemies, Movable.Adventurer adventurer)
        {
            return;
        }

        protected override void SetTime()
        {
            Time = 0;
        }

        public NewLife(Vector2 position) : base(position) { }
    }
}
