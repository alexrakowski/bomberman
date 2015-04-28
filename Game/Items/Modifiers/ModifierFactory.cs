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
        public static Modifier GetRandomPositiveModifier(Vector2 position)
        {
            return new NewLife(position);
        }
        public static Modifier GetRandomNegativeModifier(Vector2 position)
        {
            return new PositiveModifiers.NewLife(position);
        }
    }
}
