using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Movable;

namespace Bomberman.Game
{
    class GameState : IXmlSerializable
    {
        GameInfo gameInfo;
        Adventurer adventurer;
        List<Enemy> enemies;
        List<Bomb> bombs;  
        List<Modifier> modifiers;
        Map.Map map;

        public GameState(GameInfo gameInfo, Adventurer adventurer, List<Enemy> enemies, List<Bomb> bombs,  
            List<Modifier> modifiers, Map.Map map)
        {
            this.gameInfo = gameInfo;
            this.adventurer = adventurer;
            this.enemies = enemies;
            this.bombs = bombs;
            this.modifiers = modifiers;
            this.map = map;
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
            writer.WriteElementString("Value", 3.ToString());
        }
    }
}
