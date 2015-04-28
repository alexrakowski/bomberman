using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public abstract class GameElementInfo : IXmlSerializable
    {
        public int X;
        public int Y;
        public Vector2 Position;
        public string Type;

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public abstract void ReadXml(System.Xml.XmlReader reader);

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Type", Type);
            writer.WriteElementString("X", X.ToString());
            writer.WriteElementString("Y", Y.ToString());
            writer.WriteElementString("Position", Position.ToString());
        }

        public GameElementInfo(int X, int Y, Vector2 Position, string Type)
        {
            this.X = X;
            this.Y = Y;
            this.Position = Position;
            this.Type = Type;
        }
    }
}
