using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;

namespace Bomberman.Game.Movable
{
    partial class Wolf : Enemy
    {
        public override bool CanFly
        {
            get
            {
                return false;
            }
            protected set {}
        }
        private static int POINTS_VALUE;

        public override Moves GetNextStep()
        {
            var legalMoves = GetLegalMoves();
            if (legalMoves.Count < 1) return Moves.None;
            Utils.Shuffler.Shuffle(legalMoves);
            return legalMoves.First();
        }
        public Wolf(Map.Map map, MapElement startSquare)
            : base(map, startSquare)
        {
            this.InitialSpeed = 0.1f;
        }

        public override int GetValue()
        {
            return Wolf.POINTS_VALUE;
        }
    }
    partial class Wolf
    {
        public const string ASSET_NAME = "textures/movable_objects/wolf";
        private static Texture2D TEXTURE;

        public override Texture2D GetTexture()
        {
            return Wolf.TEXTURE;
        }

        public override void LoadContent(ContentManager content)
        {
            Wolf.TEXTURE = content.Load<Texture2D>(Wolf.ASSET_NAME);
        }
    }
}
