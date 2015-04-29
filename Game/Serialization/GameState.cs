using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Bomberman.Game.Items;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Movable;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Serialization
{
    public class GameState : IXmlSerializable
    {
        public IXmlSerializable GameInfo;
        public AdventurerInfo Player;
        public List<IXmlSerializable> Enemies;
        public List<BombInfo> Bombs;
        public List<IXmlSerializable> Modifiers;
        public IXmlSerializable Map;

        public GameState(GameInfo gameInfo)
        {
            this.GameInfo = gameInfo;
        }
        public GameState() { }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            reader.MoveToContent();
            reader.Read(); // Skip ahead to next node

            //GameInfo
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "GameInfo")
            {
                this.GameInfo = new GameInfo();
                this.GameInfo.ReadXml(reader);
            }
            //Adventurer
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Adventurer")
            {
                this.Player = new AdventurerInfo(reader);
            }
            //Enemies
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Enemies")
            {
                if (!reader.IsEmptyElement)
                {
                    reader.Read();
                    this.Enemies = new List<IXmlSerializable>();

                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Enemy")
                    {
                        var enemy = new EnemyInfo(reader);
                        this.Enemies.Add(enemy);
                    }
                    reader.ReadEndElement();
                }
                else
                {
                    reader.Read();
                }
            }
            //Bombs
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Bombs")
            {
                if (!reader.IsEmptyElement)
                {
                    reader.Read();
                    this.Bombs = new List<BombInfo>();

                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Bomb")
                    {
                        var bomb = new BombInfo(reader);
                        this.Bombs.Add(bomb);
                    }
                    reader.ReadEndElement();
                }
                else
                {
                    reader.Read();
                }
            }
            //Modifiers
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Modifiers")
            {
                if (!reader.IsEmptyElement)
                {
                    reader.Read();
                    this.Modifiers = new List<IXmlSerializable>();

                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Modifier")
                    {
                        var modifier = new ModifierInfo(reader);
                        this.Modifiers.Add(modifier);
                    }
                    reader.ReadEndElement();
                }
                else
                {
                    reader.Read();
                }                
            }
            //Map
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Map")
            {
                this.Map = new MapInfo(reader);
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("GameInfo");
            this.GameInfo.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Adventurer");
            this.Player.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Enemies");
            foreach (var enemyInfo in Enemies)
            {
                writer.WriteStartElement("Enemy");
                enemyInfo.WriteXml(writer);
                writer.WriteEndElement();                
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Bombs");
            foreach (var bomb in Bombs)
            {
                writer.WriteStartElement("Bomb");
                bomb.WriteXml(writer);
                writer.WriteEndElement();                
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Modifiers");
            foreach (var modifier in Modifiers)
            {
                writer.WriteStartElement("Modifier");
                modifier.WriteXml(writer);
                writer.WriteEndElement();                
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Map");
            this.Map.WriteXml(writer);
            writer.WriteEndElement();
        }
    }
    
}
