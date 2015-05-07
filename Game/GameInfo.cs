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
    public partial class GameInfo : IXmlSerializable
    {
        public int Score;
        public double Time;
        public int Lifes;
        public string PlayerName;
        public GameLevels Level;

        int [] mapFragmentsFound;
        public int foundFragments { get { return mapFragmentsFound[0]; } private set { return; } }
        public int fragmentsToFind { get { return mapFragmentsFound[1]; } private set { return; } }

        public GameInfo(string playerName, int fragmentsToFind, GameLevels level, double time)
        {
            Score = 0;
            Time = time;
            Lifes = 3;
            Level = level;
 
            mapFragmentsFound = new int[2] { 0, fragmentsToFind };
        }
                                                                                  
        public GameInfo(string playerName, int fragmentsToFind, GameLevels level, double time, int score, int lifes)
        {
            Score = score;
            Time = time;
            Lifes = lifes;
            Level = level;

            mapFragmentsFound = new int[2] { 0, fragmentsToFind };
        }

        #region Serialization
        public GameInfo() { }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            this.Score = Int32.Parse(reader.ReadElementString());
            this.Time = Double.Parse(reader.ReadElementString());
            this.Lifes = Int32.Parse(reader.ReadElementString());
            this.Level = (GameLevels)Enum.Parse(typeof(GameLevels), reader.ReadElementString());
            this.mapFragmentsFound = new int[2];
            this.mapFragmentsFound[0] = Int32.Parse(reader.ReadElementString());
            this.mapFragmentsFound[1] = Int32.Parse(reader.ReadElementString());
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteElementString("Score", Score.ToString());
            writer.WriteElementString("Time", Time.ToString());
            writer.WriteElementString("Lifes", Lifes.ToString());
            writer.WriteElementString("Level", Level.ToString());
            writer.WriteElementString("MapFragmentsFound", mapFragmentsFound[0].ToString());
            writer.WriteElementString("MapFragmentsToFind", mapFragmentsFound[1].ToString());
        }
        #endregion

        #region Setters
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

        public bool ElapseTime(int elapsedTime)
        {
            this.Time -= (double)elapsedTime / 1000;
            return this.Time < 1;
        }

        public void NextLevel()
        {
            this.Level = (GameLevels)((int)Level + 1);
        }
        #endregion
    }
}
