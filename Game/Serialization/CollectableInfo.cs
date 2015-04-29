using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public abstract class CollectableInfo : GameElementInfo
    {
        public abstract void WriteXml(System.Xml.XmlWriter writer);

        public CollectableInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public CollectableInfo() { }
        public CollectableInfo(int X, int Y, Vector2 Position, string Type) : base(X, Y, Position, Type) { }
    }
}
