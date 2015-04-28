using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Game
{
    

    abstract class GameElement : Element, IPlaysSound, IToInfo
    {
        /* X, Y - indexes of the square occupied by the Element. Discrete values.*/
        public int X;
        public int Y;

        public void PutOnSquare(Map.MapElement square)
        {
            this.Position = square.Position;
            this.X = square.X;
            this.Y = square.Y;
        }
        public void PlaySound() { throw new NotImplementedException(); }

        public abstract IXmlSerializable GetInfo();

    }
}