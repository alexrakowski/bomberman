using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;
using Bomberman.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Game.Items
{
    abstract partial class Modifier : CollectableElement
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

        public override void Collect(GameManager manager)
        {
            throw new NotImplementedException();
        }

        public Modifier(Vector2 position) : base(position) { }
    }
    abstract partial class Modifier
    {
        public const string ASSET_NAME = "textures/items/bonus";
        private static Texture2D TEXTURE;

        public override void LoadContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(MapFragment.ASSET_NAME);
        }
        public static void LoadClassContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(MapFragment.ASSET_NAME);
        }
        public override Texture2D GetTexture()
        {
            return Modifier.TEXTURE;
        }
    }
}
