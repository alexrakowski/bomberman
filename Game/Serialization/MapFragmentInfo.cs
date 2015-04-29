using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    class MapFragmentInfo : CollectableInfo
    {
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            (this as GameElementInfo).WriteXml(writer);
        }

        public MapFragmentInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public MapFragmentInfo() { }
        public MapFragmentInfo(int X, int Y, Vector2 Position, string Type) : base(X, Y, Position, Type) { }
    }
}
