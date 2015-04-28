using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;
using Bomberman.Game.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Items
{
    partial class MapFragment : CollectableElement
    {
        public const string ASSET_NAME = "textures/items/map_fragment";
        private static Texture2D TEXTURE;

        public override void LoadContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(MapFragment.ASSET_NAME);
        }
        public static void LoadClassContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(MapFragment.ASSET_NAME);
        }
        public override Texture2D GetTexture()
        {
            return MapFragment.TEXTURE;
        }

        public MapFragment(Vector2 position) : base(position) { }
    }
    partial class MapFragment
    {
        public override System.Xml.Serialization.IXmlSerializable GetInfo()
        {
            var info = new MapFragmentInfo(X, Y, Position, GetType().Name);

            return info;
        }

        public override void Collect(ICollector collector)
        {
            collector.AddMapFragment();
        }
    }
}
