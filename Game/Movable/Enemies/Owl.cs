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
    partial class Owl : Enemy
    {
        public override bool CanFly
        {
            get
            {
                return true;
            }
            protected set { }
        }
        private static int POINTS_VALUE = 400;

        public override Moves GetNextStep()
        {
            return GetRandomMove();
        }
        public Owl(Map.Map map, MapElement startSquare)
            : base(map, startSquare)
        {
            InitialSpeed = Adventurer.INITIAL_SPEED;
        }

        public override int GetValue()
        {
            return Owl.POINTS_VALUE;
        }
    }
    partial class Owl
    {
        public const string ASSET_NAME = "textures/movable_objects/owl";
        private static Texture2D TEXTURE;

        public override Texture2D GetTexture()
        {
            return Owl.TEXTURE;
        }

        public override void LoadContent(ContentManager content)
        {
            Owl.TEXTURE = content.Load<Texture2D>(Owl.ASSET_NAME);
        }
    }
}
