using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public abstract class MovableElementInfo : GameElementInfo
    {
        public abstract void WriteXml(System.Xml.XmlWriter writer);

        public MovableElementInfo() { }
        public MovableElementInfo(int X, int Y, Vector2 Position, string Type) : base(X,Y, Position, Type) {}
    }
}
