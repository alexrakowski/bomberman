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
using Bomberman.Game.Movable.Enemies;

namespace Bomberman.Game.Items.Modifiers
{
    abstract partial class Modifier
    {
        public double Time { get; protected set; }
        public void Apply(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer)
        {
            SetTime();
            OnApply(gameInfo, enemies, adventurer);
        }
        public void Update(int elapsedTime, GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer)
        {
            Time -= (double)elapsedTime / 100;
            if (Time < 0)
            {
                OnTimeEnded(gameInfo, enemies, adventurer);
            }
        }
        public void EndNow(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer)
        {
            OnTimeEnded(gameInfo, enemies, adventurer);
        }
        protected abstract void OnApply(GameInfo gameInfo, List<Enemy> enemies, Movable.Adventurer adventurer);
        protected abstract void OnTimeEnded(GameInfo gameInfo, List<Enemy> enemies, Adventurer adventurer);
        protected abstract void SetTime();
        public abstract bool EndsOnPlayerDeath { get; }

        public Modifier()
        {
            SetTime();
        }
    }

    abstract partial class Modifier : IToInfo
    {
        public System.Xml.Serialization.IXmlSerializable ToInfo()
        {
            var info = new ModifierInfo((int)Time, GetType().Name);

            return info;
        }

        public void Construct(System.Xml.Serialization.IXmlSerializable info)
        {
            throw new NotImplementedException();
        }
    }
}
