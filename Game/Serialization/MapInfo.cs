using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Bomberman.Game.Serialization
{
    class MapInfo : IXmlSerializable
    {
        public MapElementInfo[,] Squares;
        public Tuple<int, int> StartPosition;
        public int Width;
        public int Height;

        public MapInfo(MapElementInfo[,] squares) { this.Squares = squares; }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.StartPosition = Utils.Parser.ParseIntTuple(reader.GetAttribute("StartPosition"));
            this.Width = Int32.Parse(reader.GetAttribute("Width"));
            this.Height = Int32.Parse(reader.GetAttribute("Heigth"));
            reader.Read();

            this.Squares = new MapElementInfo[this.Width, this.Height];
            //Squares
            int squaresParsed = 0;
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Squares" && !reader.IsEmptyElement)
            {
                reader.Read();

                while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MapElement")
                {
                    var square = new MapElementInfo(reader);
                    this.Squares[square.X, square.Y] = square;
                    squaresParsed++;
                }
            }
            else
            {
                throw new BombermanException("Could not parse Map's Squares");
            }
            if (squaresParsed != Width * Height)
            {
                throw new BombermanException("Not enough Map Squares parsed. Parsed " + squaresParsed.ToString()
                    + ", should be: " + (Height * Width).ToString());
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("StartPosition", StartPosition.ToString());
            writer.WriteAttributeString("Width", Width.ToString());
            writer.WriteAttributeString("Heigth", Height.ToString());

            writer.WriteStartElement("Squares");
            foreach (var squareInfo in Squares)
            {
                writer.WriteStartElement("MapElement");
                squareInfo.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public MapInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public MapInfo() { }
    }
}
