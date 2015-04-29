using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    class MapElementInfo : GameElementInfo
    {
        public CollectableInfo Collectable;

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.ReadStartElement();
            var type = reader.MoveToContent();
            if (reader.LocalName == "Collectable")
            {
                if (!reader.IsEmptyElement)
                {
                    reader.Read();
                    if (reader.LocalName == "Modifier")
                    {
                        //Bonus
                        this.Collectable = new BonusInfo(reader);
                    }
                    else
                    {
                        //MapFragment
                        this.Collectable = new MapFragmentInfo(reader);
                    }
                }
                else
                {
                    reader.Read();
                }
            }
            base.ReadXml(reader);
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("Collectable");
            if (Collectable != null)
            {
                Collectable.WriteXml(writer);
            }
            writer.WriteEndElement();
            base.WriteXml(writer);
        }

        public MapElementInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public MapElementInfo() { }
        public MapElementInfo(int X, int Y, Vector2 Position, string Type) : base(X, Y, Position, Type) { }
    }
}
