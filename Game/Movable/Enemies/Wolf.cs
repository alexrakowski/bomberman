using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;

namespace Bomberman.Game.Movable.Enemies
{
    partial class Wolf : Enemy
    {
        public override bool CanFly
        {
            get
            {
                return false;
            }
        }
        private static int POINTS_VALUE = 200;

        public override Moves GetNextStep()
        {
            return GetRandomMove();
        }
        public Wolf(Map.Map map, MapElement startSquare)
            : base(map, startSquare)
        {
            InitialSpeed = Adventurer.INITIAL_SPEED / 2;
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
