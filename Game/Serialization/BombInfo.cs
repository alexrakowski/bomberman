using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    class BombInfo : GameElementInfo
    {
        public int Time;
        public int ExplosionTime;
        public bool HasExploded;

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }
        public BombInfo(int X, int Y, Vector2 Position, string Type, int time, int explosionTime, bool hasExploded)
            : base(X, Y, Position, Type) { this.Time = time; this.ExplosionTime = explosionTime; this.HasExploded = hasExploded; }
    }
}
