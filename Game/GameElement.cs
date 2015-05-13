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
    
    /// <summary>
    /// Abstract class representing an element that will be on a square of the game,
    /// thus having discrete X,Y coordinates.
    /// </summary>
    public abstract class GameElement : Element, IPlaysSound, IToInfo
    {
        /// <summary>
        /// X index of the square occupied by the Element. Discrete value.
        /// </summary>
        public int X;
        /// <summary>
        /// Y index of the square occupied by the Element. Discrete value.
        /// </summary>
        public int Y;

        /// <summary>
        /// IPlaysSound implementation
        /// </summary>
        public void PlaySound() { throw new NotImplementedException(); }

        public abstract IXmlSerializable ToInfo();
        public void Construct(IXmlSerializable info)
        {
            var gameElementInfo = (GameElementInfo)info;
            this.X = gameElementInfo.X;
            this.Y = gameElementInfo.Y;
            this.Position = gameElementInfo.Position;
        }
    }
}