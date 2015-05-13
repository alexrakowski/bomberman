using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;
using Bomberman.Game.Algorithms;
using Bomberman.Game.Items;

namespace Bomberman.Game.Movable.Enemies
{
    partial class Bear : Enemy
    {
        public override bool CanFly
        {
            get
            {
                return false;
            }
        }
        private static int POINTS_VALUE = 1000;
        private List<Bomb> _bombs;

        #region Moves
        public override Moves GetNextStep()
        {
            var moveFromBomb = GetMoveAwayFromBomb();
            if (moveFromBomb != Moves.None)
                return moveFromBomb;

            // follow adventurer
            var adventurer = Adventurer.GetInstance();
            Moves move = PathFinder.GetMoveTo(_map, this, adventurer);
            if (move == Moves.None)
            {
                return GetRandomMove();
            }
            return move;
        }
        private Moves GetMoveAwayFromBomb()
        {
            foreach (var bomb in _bombs)
            {
                int distFromTarget = (int)Map.Map.GetSquaresDistance(X, Y, bomb.X, bomb.Y);
                if (distFromTarget <= Bomb.GetRange() + 1)
                {
                    //get away from bomb
                    if (X < bomb.X && IsMoveValid(Moves.Left))
                    {
                        return Moves.Left;
                    }
                    if (X > bomb.X && IsMoveValid(Moves.Right))
                    {
                        return Moves.Right;
                    }
                    if (Y < bomb.Y && IsMoveValid(Moves.Up))
                    {
                        return Moves.Up;
                    }
                    if (Y > bomb.Y && IsMoveValid(Moves.Down))
                    {
                        return Moves.Down;
                    }
                }
            }
            return Moves.None;            
        }
        #endregion

        public Bear(Map.Map map, MapElement startSquare, List<Bomb> bombs)
            : base(map, startSquare)
        {
            InitialSpeed = Adventurer.INITIAL_SPEED;
            _bombs = bombs;
        }

        public override int GetValue()
        {
            return Bear.POINTS_VALUE;
        }


        #region Drawing
        public const string ASSET_NAME = "textures/movable_objects/bear";
        private static Texture2D TEXTURE;

        public override Texture2D GetTexture()
        {
            return Bear.TEXTURE;
        }

        public static void LoadClassContent(ContentManager content)
        {
            Bear.TEXTURE = content.Load<Texture2D>(Bear.ASSET_NAME);
        }
        public override void LoadContent(ContentManager content)
        {
            Bear.TEXTURE = content.Load<Texture2D>(Bear.ASSET_NAME);
        }
        #endregion
    }
}
