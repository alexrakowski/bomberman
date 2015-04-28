using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Game.Items.Modifiers.PositiveModifiers;
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
    }
}
