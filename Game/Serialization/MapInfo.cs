using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bomberman.Game.Serialization
{
    class MapInfo : GameElementInfo
    {
        public MapElementInfo[,] Squares;
        public Tuple<int, int> StartPosition;

        public MapInfo(MapElementInfo[,] squares) { this.Squares = squares; }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
