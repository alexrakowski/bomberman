using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public class AdventurerInfo : MovableElementInfo
    {
        public int BombsLimit;

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("BombsLimit", BombsLimit.ToString());
            (this as GameElementInfo).WriteXml(writer);
        }

        public AdventurerInfo(XmlReader reader)
        {
            this.BombsLimit = Int32.Parse(reader.GetAttribute("BombsLimit"));
            (this as GameElementInfo).ReadXml(reader);
        }
        public AdventurerInfo(int X, int Y, Vector2 Position, string Type, int bombsLimit) 
            : base(X, Y, Position, Type)
        { this.BombsLimit = bombsLimit; }
    }
}
