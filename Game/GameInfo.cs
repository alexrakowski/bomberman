using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items.Modifiers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Game
{
    partial class GameInfo : IXmlSerializable
    {
        public int Score;
        public int Time;
        public int Lifes;
        string PlayerName;
        public GameLevels Level;

        int [] mapFragmentsFound;
        public int foundFragments { get { return mapFragmentsFound[0]; } private set { return; } }
        public int fragmentsToFind { get { return mapFragmentsFound[1]; } private set { return; } }

        public GameInfo(string playerName, int fragmentsToFind, GameLevels level)
        {
            Score = 0;
            Time = 1000;
            Lifes = 3;
            PlayerName = playerName;
            Level = level;
 
            mapFragmentsFound = new int[2] { 0, fragmentsToFind };
        }

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
            throw new NotImplementedException();
        }
    }
    [Serializable]
    class Car : ISerializable
    {
        int value = 1;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", value, typeof(int));
        }
    }

    //setters
    partial class GameInfo
    {
        /// <summary>
        /// Increments found map fragments
        /// </summary>
        /// <returns>
        /// True if all fragments found.
        /// </returns>
        public bool AddMapFragment()
        {
            mapFragmentsFound[0]++;
            return (mapFragmentsFound[0] == mapFragmentsFound[1]);
        }
        /// <summary>
        /// Decrements lifes left.
        /// </summary>
        /// <returns>
        /// True if the last life was lost.</returns>
        public bool LoseLife()
        {
            this.Lifes -= 1;
            Lifes = Lifes < 1 ? 0 : Lifes;
            return Lifes < 1;
        }

        public void AddLife()
        {
            this.Lifes++;
        }

        public void AddPoints(int pointsToAdd)
        {
            this.Score += pointsToAdd;
        }
    }
}
