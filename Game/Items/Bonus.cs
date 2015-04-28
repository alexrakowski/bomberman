using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Game.Items
{
    /// <summary>
    /// Container class for Modifiers
    /// </summary>
    partial class Bonus : CollectableElement
    {
        public Modifier Modifier { get; protected set; }

        public override void Collect(ICollector collector)
        {
            collector.AddModifier(this.Modifier);
        }

        public override System.Xml.Serialization.IXmlSerializable GetInfo()
        {
            var info = new BonusInfo(X, Y, Position, GetType().Name);
            info.Modifier = (ModifierInfo)Modifier.GetInfo();
            return info;
        }

        public Bonus(Vector2 position) : base(position) { }
        public Bonus(Vector2 position, Modifier modifier) : base(position) { this.Modifier = modifier; }
    }
    partial class Bonus
    {
        public const string ASSET_NAME = "textures/items/bonus";
        private static Texture2D TEXTURE;

        public override void LoadContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(Bonus.ASSET_NAME);
        }
        public static void LoadClassContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(Bonus.ASSET_NAME);
        }
        public override Texture2D GetTexture()
        {
            return Bonus.TEXTURE;
        }
    }
}
