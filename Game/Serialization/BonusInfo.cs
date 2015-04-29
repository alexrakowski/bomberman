using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public class BonusInfo : CollectableInfo
    {
        public ModifierInfo Modifier;
        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.Modifier = new ModifierInfo(reader);
            (this as GameElementInfo).ReadXml(reader);
        }
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("Modifier");
            Modifier.WriteXml(writer);
            writer.WriteEndElement();
            (this as GameElementInfo).WriteXml(writer);
        }

        public BonusInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public BonusInfo() { }
        public BonusInfo(int X, int Y, Vector2 Position, string Type) : base(X, Y, Position, Type) { }
    }
}
