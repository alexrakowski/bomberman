using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items.Modifiers.PositiveModifiers;
using Bomberman.Game.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers
{
    static class ModifierFactory
    {
        public static Modifier GetRandomPositiveModifier()
        {
            return new NewLife();
        }
        public static Modifier GetRandomNegativeModifier()
        {
            return new PositiveModifiers.NewLife();
        }
        public static Modifier Construct(IXmlSerializable info)
        {
            var modifierInfo = (ModifierInfo)info;
            Modifier modifier = null;

            switch (modifierInfo.Type)
            {
                case "NewLife":
                    modifier = new NewLife();
                    break;
            }

            return modifier;
        }
    }
}
