using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;
using Bomberman.Game.Algorithms;

namespace Bomberman.Game.Movable.Enemies
{
    partial class Fox : Enemy
    {
        public override bool CanFly
        {
            get
            {
                return false;
            }
            protected set { }
        }
        private static int POINTS_VALUE = 700;

        public override Moves GetNextStep()
        {
            var adventurer = Adventurer.GetInstance();
            int distFromTarget = (int)Map.Map.GetSquaresDistance(X, Y, adventurer.X, adventurer.Y);

            if (distFromTarget > 5)
            {
                return GetRandomMove();
            }
            else
            {
                Moves move = PathFinder.GetMoveToAdventuer(_map, this, adventurer);
                if (move == Moves.None)
                {
                    return GetRandomMove();
                }
                return move;
            }
        }
        public Fox(Map.Map map, MapElement startSquare)
            : base(map, startSquare)
        {
            InitialSpeed = Adventurer.INITIAL_SPEED * 1.5f;
        }

        public override int GetValue()
        {
            return Fox.POINTS_VALUE;
        }
    }
    partial class Fox
    {
        public const string ASSET_NAME = "textures/movable_objects/fox";
        private static Texture2D TEXTURE;

        public override Texture2D GetTexture()
        {
            return Fox.TEXTURE;
        }

        public static void LoadClassContent(ContentManager content)
        {
            Fox.TEXTURE = content.Load<Texture2D>(Fox.ASSET_NAME);
        }
        public override void LoadContent(ContentManager content)
        {
            Fox.TEXTURE = content.Load<Texture2D>(Fox.ASSET_NAME);
        }
    }
}
