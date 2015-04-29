using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Bomberman.Game.Serialization
{
    public interface IToInfo
    {
        IXmlSerializable ToInfo();
        void Construct(IXmlSerializable info);
    }
}
