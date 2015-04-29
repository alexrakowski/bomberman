using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Bomberman.Game.Serialization
{
    public class ModifierInfo : IXmlSerializable
    {
        public int Time;
        public string Type;

        public ModifierInfo(int time, string type) { this.Time = time; this.Type = type; }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.Time = Int32.Parse(reader.ReadElementString());
            this.Type = reader.ReadElementString();
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Time", Time.ToString());
            writer.WriteElementString("Type", Type);
        }

        public ModifierInfo(XmlReader reader)
        {
            this.ReadXml(reader);
        }
        public ModifierInfo() { }
    }
}
