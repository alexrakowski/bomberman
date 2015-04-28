using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    class MapElementInfo : GameElementInfo
    {
        public CollectableInfo Collectable;

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            if (Collectable != null)
            {
                writer.WriteStartElement("Collectable");
                Collectable.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        public MapElementInfo(int X, int Y, Vector2 Position, string Type) : base(X,Y, Position, Type) {}
    }
}
