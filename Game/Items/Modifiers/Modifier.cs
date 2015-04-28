using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;
using Bomberman.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Items.Modifiers
{
    abstract partial class Modifier
    {
        public int Time { get; protected set; }
        public void Apply(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer)
        {
            SetTime();
            OnApply(gameInfo, enemies, adventurer);
        }
        public void Update(int elapsedTime, GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer)
        {
            Time -= elapsedTime;
            if (Time < 0)
            {
                OnTimeEnded(gameInfo, enemies, adventurer);
            }
        }

        protected abstract void OnApply(GameInfo gameInfo, List<Movable.Enemy> enemies, Movable.Adventurer adventurer);
        protected abstract void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer);
        protected abstract void SetTime();       
    }

    abstract partial class Modifier : IToInfo
    {

        public System.Xml.Serialization.IXmlSerializable GetInfo()
        {
            var info = new ModifierInfo(Time, GetType().Name);

            return info;
        }
    }
}
