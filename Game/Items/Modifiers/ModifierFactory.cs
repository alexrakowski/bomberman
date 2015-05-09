using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bomberman.Game.Items.Modifiers.Positive;
using Bomberman.Game.Items.Modifiers.Negative;
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
                default:
                    return new NewLife();
            }            
        }
        public static Modifier GetRandomNegativeModifier()
        {
            InitRandom();
            int r = rand.Next() % PositivesCount;
            NegativeModifiers m = (NegativeModifiers)r;

            switch (m)
            {
                case NegativeModifiers.LessBombs:
                    return new LessBombs();
                case NegativeModifiers.LessTime:
                    return new LessTime();
                case NegativeModifiers.ShorterBombsRange:
                    return new ShorterBombsRange();
                case NegativeModifiers.SpeedDown:
                    return new SpeedDown();
                default:
                    return new LessTime();
            }  
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
                case "LessBombs":
                    return new LessBombs();
                case "LessTime":
                    return new LessTime();
                case "ShorterBombsRange":
                    return new ShorterBombsRange();
                case "SpeedDown":
                    return new SpeedDown();
                default:
                    throw new BombermanException("Unknown modifier type: " + modifierInfo.Type); 
            }
        }
    }
}
