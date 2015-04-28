using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Time", Time.ToString());
            writer.WriteElementString("Type", Type);
        }
    }
}
