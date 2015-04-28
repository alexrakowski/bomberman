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
        public IXmlSerializable Player;
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

            writer.WriteStartElement("GameInfo");
            this.Player.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("Enemies");
            foreach (var enemyInfo in Enemies)
            {
                enemyInfo.WriteXml(writer);
            }
            writer.WriteEndElement();
        }
    }
    
}
