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
    /// <summary>
    /// This class has two main purposes:
    /// It holds neccessary information about the game's state,
    /// such as player's score, time left, etc.
    /// Apart from that,it is used as a container for all deserialized object when saving the game.
    /// </summary>
    public class GameInfo : IXmlSerializable
    {
        /// <summary>
        /// Player's Score.
        /// </summary>
        public int Score { get; private set; }
        /// <summary>
        /// Time left.
        /// </summary>
        public double Time { get; set; }
        /// <summary>
        /// Lifes left.
        /// </summary>
        public int Lifes { get; private set; }
        /// <summary>
        /// Player's name.
        /// </summary>
        public string PlayerName { get; private set; }
        /// <summary>
        /// Current level of the game.
        /// </summary>
        public GameLevels Level { get; private set; }

        int [] mapFragmentsFound;
        /// <summary>
        /// The number of map fragments that have been found by the player.
        /// </summary>
        public int FoundFragments { get { return mapFragmentsFound[0]; } private set { return; } }
        /// <summary>
        /// The overall number of map fragments the player has to find in order to finish the level.
        /// </summary>
        public int FragmentsToFind { get { return mapFragmentsFound[1]; } private set { return; } }

        /// <summary>
        /// Basic constructor.
        /// Use this to start a new game.
        /// It will initialize neccessary fields with their starting values.
        /// </summary>
        /// <param name="playerName">Player's name</param>
        /// <param name="fragmentsToFind">Number of map fragments to find</param>
        /// <param name="level">Game level</param>
        /// <param name="time">Time left</param>
        public GameInfo(string playerName, int fragmentsToFind, GameLevels level, double time)
        {
            Score = 0;
            Time = time;
            Lifes = 3;
            Level = level;
 
            mapFragmentsFound = new int[2] { 0, fragmentsToFind };
        }
        /// <summary>
        /// Use this constructor when you are loading the game,
        /// and want to provide another number of lifes left, and score.
        /// </summary>
        /// <param name="playerName">Player's name</param>
        /// <param name="fragmentsToFind">Number of map fragments to find</param>
        /// <param name="level">Game level</param>
        /// <param name="time">Time left</param>
        /// <param name="score">Player's score</param>
        /// <param name="lifes">Lifes left</param>                                                   
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

        /// <summary>
        /// Increments number of player's lifes.
        /// </summary>
        public void AddLife()
        {
            this.Lifes++;
        }

        /// <summary>
        /// Increments player's score.
        /// </summary>
        /// <param name="pointsToAdd">
        /// Amount of points to increment the score.
        /// </param>
        public void AddPoints(int pointsToAdd)
        {
            this.Score += pointsToAdd;
        }

        /// <summary>
        /// Decrements the time that is left in the game.
        /// </summary>
        /// <param name="elapsedTime">
        /// Amount of time that has passed.</param>
        /// <returns>
        /// True, if there is no time left.
        /// </returns>
        public bool ElapseTime(int elapsedTime)
        {
            this.Time -= (double)elapsedTime / 1000;
            return this.Time < 1;
        }

        /// <summary>
        /// Sets the current level to the next value.
        /// </summary>
        public void NextLevel()
        {
            this.Level = (GameLevels)((int)Level + 1);
            if (this.Level == null)
            {
                throw new BombermanException("Unknown level number");
            }
        }
        #endregion
    }
}
