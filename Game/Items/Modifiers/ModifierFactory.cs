using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items.Modifiers.Positive;
using Bomberman.Game.Serialization;
using Microsoft.Xna.Framework;

namespace Bomberman.Game.Items.Modifiers
{
    static class ModifierFactory
    {
        private static int PositivesCount = Enum.GetNames(typeof(PositiveModifiers)).Length;
        private static Random rand;

        private static void InitRandom()
        {
            if (rand == null)
            {
                rand = new Random();
            }
        }
        public static Modifier GetRandomPositiveModifier()
        {
            InitRandom();
            int r = rand.Next() % PositivesCount;
            PositiveModifiers m = (PositiveModifiers)r;

            switch (m)
            {
                case PositiveModifiers.Indestructible:
                    return new Indestructible();
                case PositiveModifiers.NewLife:
                    return new NewLife();
                case PositiveModifiers.MoreBombs:
                    return new MoreBombs();
                case PositiveModifiers.SpeedUp:
                    return new SpeedUp();
                case PositiveModifiers.MoreTime:
                    return new MoreTime();
                case PositiveModifiers.ParalyzeEnemies:
                    return new ParalyzeEnemies();
                case PositiveModifiers.Fly:
                    return new Fly();
                case PositiveModifiers.FartherBombsRange:
                    return new FartherBombsRange();
            }
            return new NewLife();
        }
        public static Modifier GetRandomNegativeModifier()
        {
            InitRandom();
            return GetRandomPositiveModifier();
        }
        public static Modifier Construct(IXmlSerializable info)
        {
            var modifierInfo = (ModifierInfo)info;

            switch (modifierInfo.Type)
            {
                case "NewLife":
                    return new NewLife();
                case "Indestructible":
                    return new Indestructible();
                case "MoreBombs":
                    return new MoreBombs();
                case "FartherBombsRange":
                    return new FartherBombsRange();
                case "MoreTime":
                    return new MoreTime();
                case "SpeedUp":
                    return new SpeedUp();
                case "ParalyzeEnemies":
                    return new ParalyzeEnemies();
                case "Fly":
                    return new Fly();
                default:
                    throw new BombermanException("Unknown modifier type: " + modifierInfo.Type); 
            }
        }
    }
}
