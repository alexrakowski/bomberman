using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bomberman.Game.Serialization
{
    class MapInfo : IXmlSerializable
    {
        public MapElementInfo[,] Squares;
        public Tuple<int, int> StartPosition;

        public MapInfo(MapElementInfo[,] squares) { this.Squares = squares; }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("StartPosition", StartPosition.ToString());
            foreach (var squareInfo in Squares)
            {
                writer.WriteStartElement("MapElement");
                squareInfo.WriteXml(writer);
                writer.WriteEndElement();
            }
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }
    }
}
