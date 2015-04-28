using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
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
        public IXmlSerializable Map;
        public List<IXmlSerializable> Modifiers;
        public List<IXmlSerializable> Bombs;

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
            throw new NotImplementedException();
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
