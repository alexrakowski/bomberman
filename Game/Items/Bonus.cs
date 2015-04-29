using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
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

        public override System.Xml.Serialization.IXmlSerializable ToInfo()
        {
            var info = new BonusInfo(X, Y, Position, GetType().Name);
            info.Modifier = (ModifierInfo)Modifier.ToInfo();
            return info;
        }
        public void Construct(System.Xml.Serialization.IXmlSerializable info)
        {
            base.Construct(info);
            var bonusInfo = (BonusInfo)info;
            this.Modifier = ModifierFactory.Construct(bonusInfo.Modifier);
        }

        public Bonus(IXmlSerializable info)
        {
            this.Construct(info);
        }
        private Bonus() { }
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
