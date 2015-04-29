using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public class BombInfo : GameElementInfo
    {
        public int Time;
        public int ExplosionTime;
        public bool HasExploded;

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.HasExploded = bool.Parse(reader.GetAttribute("HasExploded"));
            this.Time = Int32.Parse(reader.ReadElementString());
            this.ExplosionTime = Int32.Parse(reader.ReadElementString());
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("HasExploded", HasExploded.ToString());
            writer.WriteElementString("Time", Time.ToString());
            writer.WriteElementString("ExplosionTime", ExplosionTime.ToString());
            (this as GameElementInfo).WriteXml(writer);
        }

        public BombInfo(XmlReader reader)
        {
            this.ReadXml(reader);
            (this as GameElementInfo).ReadXml(reader);
        }
        public BombInfo(int X, int Y, Vector2 Position, string Type, int time, int explosionTime, bool hasExploded)
            : base(X, Y, Position, Type) { this.Time = time; this.ExplosionTime = explosionTime; this.HasExploded = hasExploded; }
    }
}
