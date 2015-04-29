using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bomberman.Game.Items;
using Bomberman.Game.Serialization;
using System.Xml.Serialization;

namespace Bomberman.Game.Movable
{
    partial class Adventurer : MovableElement
    {
        public const string ASSET_NAME = "textures/movable_objects/MockAdventurer";
        private static Texture2D TEXTURE;

        public override bool CanFly
        {
            get
            {
                return false;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }
        public override bool CanAttack
        {
            get
            {
                return false;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        private List<Bomb> _bombs;
        public int BombsLimit { get; set; }

        private void PutBomb()
        {
            var bomb = new Bomb(X, Y, Position);
            _bombs.Add(bomb);
            var square = _map.OccupySquare(bomb);
            bomb.Position = square.Position;
        }
        public ICollectable PickCollectable()
        {
            ICollectable collectable = null;
            if (!this.IsMoving)
            {
                var square = _map.GetSquare(X, Y);
                collectable = square.PickCollectable();
            }
            return collectable;
        }
        public override void Move(int elapsedTime, Moves move)
        {
            if (move == Moves.Fire && _bombs.Count < BombsLimit)
            {
                PutBomb();
                return;
            }
            if (this.IsMoving)
                ContinueMoving(elapsedTime);
            else if (IsMoveValid(move))
            {
                //new move
                StartMoving(move);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            Adventurer.TEXTURE = content.Load<Texture2D>(Adventurer.ASSET_NAME);
        }
        public override Texture2D GetTexture()
        {
            return Adventurer.TEXTURE;
        }

        public void MakeAlive()
        {
            this.IsDead = false;
        }
        public override int Destroy()
        {
            this.IsDead = true;
            return GetValue();
        }
        public override int GetValue()
        {
            return 0;
        }
    }
    partial class Adventurer
    {
        private static Adventurer instance;
        /// <summary>
        /// Creates new Adventurer instance
        /// </summary>
        /// <param name="map"></param>
        /// <param name="bombs"></param>
        /// <returns></returns>
        public static Adventurer GetNewInstance(Map.Map map, List<Items.Bomb> bombs)
        {
            instance = new Adventurer(map, bombs);
            var startSquare = map.GetStartSquare();
            instance.X = startSquare.X;
            instance.Y = startSquare.Y;
            instance.Position = startSquare.Position;
            startSquare.Occupy(instance);

            return instance;
        }
        /// <summary>
        /// Returns current global instance of Adventurer class.
        /// Returns null, unless GetNewInstance has been invoked earlier.
        /// </summary>
        /// <returns>
        /// Current instance, or null if has not been created yet.
        /// </returns>
        public static Adventurer GetInstance()
        {
            return instance;
        }

        public static Map.MapElement GetOccupiedSquare()
        {
            if (instance == null) return null;
            else
            {
                var sqaure = instance._map.GetSquare(instance.X, instance.Y);
                return sqaure;
            }
        }
        private Adventurer(Map.Map map, List<Items.Bomb> bombs)
            : base(map)
        {
            this._bombs = bombs;
            this.BombsLimit = 2;

            this.InitialSpeed = 0.2f;
            this.IsMoving = false;
        }
    }

    partial class Adventurer
    {
        public static Adventurer ConstructInstance(IXmlSerializable info, Map.Map map, List<Items.Bomb> bombs)
        {
            var adventurerInfo = (AdventurerInfo)info;
            Adventurer adventurer = new Adventurer(map, bombs);
            adventurer.BombsLimit = adventurerInfo.BombsLimit;
            adventurer.X = adventurerInfo.X;
            adventurer.Y = adventurerInfo.Y;
            adventurer.Position = adventurerInfo.Position;

            Adventurer.instance = adventurer;
            return adventurer;
        }
        public void Construct(System.Xml.Serialization.IXmlSerializable info)
        {
            throw new NotImplementedException();
        }
        public override System.Xml.Serialization.IXmlSerializable ToInfo()
        {
            var info = new AdventurerInfo(X, Y, Position, GetType().Name, BombsLimit);
            return info;
        }
    }
}
