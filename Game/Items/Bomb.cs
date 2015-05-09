using System;
using System.Collections.Generic;
using System.Text;
using Bomberman.Game;
using Bomberman.Game.Movable;
using Bomberman.Game.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Serialization;
using System.Xml.Serialization;


namespace Bomberman.Game.Items
{
    class Bomb : DestroyableElement
    {
        #region Drawing
        public const string ASSET_NAME = "textures/items/bomb";
        public const string FIRE_ASSET_NAME = "textures/items/fire";
        public const string EXPLODED_ASSET_NAME = "textures/items/explosion";
        private static Texture2D TEXTURE;
        private static Texture2D FIRE_TEXTURE;
        private static Texture2D EXPLODED_TEXTURE;

        public override void LoadContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(Bomb.ASSET_NAME);
        }
        public static void LoadClassContent(ContentManager content)
        {
            TEXTURE = content.Load<Texture2D>(Bomb.ASSET_NAME);
            FIRE_TEXTURE = content.Load<Texture2D>(Bomb.FIRE_ASSET_NAME);
            EXPLODED_TEXTURE = content.Load<Texture2D>(Bomb.EXPLODED_ASSET_NAME);
        }
        public override Texture2D GetTexture()
        {
            return this.HasExploded ? Bomb.EXPLODED_TEXTURE : Bomb.TEXTURE;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.HasExploded && AffectedPositions != null)
            {
                var texture = Bomb.FIRE_TEXTURE;
                foreach (var firePos in AffectedPositions)
                {
                    if (firePos == this.Position) continue;
                    var rectangle = new Rectangle((int)firePos.X, (int)firePos.Y, MapElement.WIDTH, MapElement.HEIGHT);
                    spriteBatch.Draw(texture, rectangle, Color.White);
                }
            }
            base.Draw(spriteBatch);
        }
        #endregion

        #region BombLogic
        const int BOMB_TIME_IN_MSECONDS = 3000;
        const int EXPLOSION_TIME_IN_MSECONDS = 500;
        private int _time = BOMB_TIME_IN_MSECONDS;
        private int _explosionTime = EXPLOSION_TIME_IN_MSECONDS;

        public static int GetRange() { return RANGE; }
        private static int RANGE = 1;
        public static void ResetRange()
        {
            RANGE = 1;
        }
        public static void IncrementRange()
        {
            RANGE++;
        }
        public static void DecrementRange()
        {
            if (RANGE > 1)
                RANGE--;
        }

        public bool HasExploded { get; private set; }
        List<Vector2> AffectedPositions;

        public bool Tick(int elapsedTime)
        {
            if (!this.HasExploded)
            {
                _time -= elapsedTime;
                return _time < 1;
            }
            else
            {
                _explosionTime -= elapsedTime;
                this.IsDead = _explosionTime < 1;
                return true;
            }
        }
        public int Explode(Map.Map map)
        {
            this.HasExploded = true;
            int pointsScored = 0;
            List<MapElement> squares = new List<MapElement>();
            AffectedPositions = new List<Vector2>();

            for (int x = this.X; x <= this.X + RANGE; ++x)
            {
                var square = map.GetSquare(x, Y);
                if (square == null)
                    break;
                if (!square.CanBeAffected && !square.CanBeDestroyed)
                {
                    //ROCK
                    break;
                }
                squares.Add(map.GetSquare(x, this.Y));
            }
            for (int x = this.X; x >= this.X - RANGE; --x)
            {
                var square = map.GetSquare(x, Y);
                if (square == null)
                    break;
                if (!square.CanBeAffected && !square.CanBeDestroyed)
                {
                    //ROCK
                    break;
                }
                squares.Add(map.GetSquare(x, this.Y));
            }
            for (int y = this.Y; y <= this.Y + RANGE; ++y)
            {
                var square = map.GetSquare(X, y);
                if (square == null)
                    break;
                if (!square.CanBeAffected && !square.CanBeDestroyed)
                {
                    //ROCK
                    break;
                }
                squares.Add(map.GetSquare(X, y));
            }
            for (int y = this.Y; y >= this.Y - RANGE; --y)
            {
                var square = map.GetSquare(X, y);
                if (square == null)
                    break;
                if (!square.CanBeAffected && !square.CanBeDestroyed)
                {
                    //ROCK
                    break;
                }
                squares.Add(map.GetSquare(X, y));
            }
            foreach (var square in squares)
            {
                if (square != null)
                {
                    pointsScored += map.DestroySquare(square);
                    if (square.CanBeAffected)
                    {
                        AffectedPositions.Add(square.Position);
                    }
                }
            }

            return pointsScored;
        }

        public override int Destroy()
        {
            this._time = 0;
            return GetValue();
        }
        public override int GetValue()
        {
            return 0;
        }

        public Bomb(int x, int y, Vector2 position)
        {
            this.X = x;
            this.Y = y;
            this.Position = position;
        }
        #endregion

        #region Serialization
        public void Construct(System.Xml.Serialization.IXmlSerializable info)
        {
            base.Construct(info);
            var bombInfo = (BombInfo)info;
            this._time = bombInfo.Time;
            this._explosionTime = bombInfo.ExplosionTime;
            this.HasExploded = bombInfo.HasExploded;
        }
        public override System.Xml.Serialization.IXmlSerializable ToInfo()
        {
            var info = new BombInfo(X, Y, Position, GetType().Name, _time, _explosionTime, HasExploded);

            return info;
        }

        public Bomb(IXmlSerializable info)
        {
            this.Construct(info);
        }
        private Bomb() { }
        #endregion
    }
}
