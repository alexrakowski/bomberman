using System;
using System.Collections.Generic;
using Bomberman.Game.Map;
using System.Text;
using System.Linq;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Movable.Enemies
{
    abstract class Enemy : MovableElement
    {
        public abstract Moves GetNextStep();
        protected List<Moves> GetLegalMoves()
        {
            /*Dictionary<Moves, int[]> coordinates = new Dictionary<Moves, int[]>()
            {
                {Moves.Left, new int [2] {X - 1, Y}},
                {Moves.Right, new int [2] {X + 1, Y}},
                {Moves.Down, new int [2] {X, Y + 1}},
                {Moves.Up, new int [2] {X, Y - 1}},
            };
            

            var adventurerSquare = Adventurer.GetOccupiedSquare();
            foreach (var option in coordinates)
            {
                var square = _map.GetSquare(option.Value[0], option.Value[1]);
                if (square == null) continue;
                if (square.IsOccupied && square != adventurerSquare) 
                    continue;
                if (square.IsWalkingTerrain)
                {
                    legalMoves.Add(option.Key);
                }
                else if (square.IsFlyingTerrain && this.CanFly)
                {
                    legalMoves.Add(option.Key);
                }
            }*/
            List<Moves> legalMoves = new List<Moves>();
            List<Moves> possibleMoves = new List<Moves>() {Moves.Down, Moves.Up, Moves.Left, Moves.Right};

            foreach (var move in possibleMoves)
            {
                if (IsMoveValid(move))
                {
                    legalMoves.Add(move);
                }
            }

            return legalMoves;
        }
        protected Moves GetRandomMove()
        {
            var legalMoves = GetLegalMoves();
            if (legalMoves.Count < 1) return Moves.None;
            Utils.Shuffler.Shuffle(legalMoves);
            return legalMoves.First();
        }

        public override void Move(int elapsedTime, Moves move = Moves.None)
        {
            if (!this.IsMoving)
            {
                move = GetNextStep();
                StartMoving(move);
            }
            else
            {
                ContinueMoving(elapsedTime);
            }
            Attack();
        }
        private void Attack()
        {
            var adventurer = Adventurer.GetInstance();
            if (Utils.Intersection.CheckElementsCollision(this, adventurer))
            {
                adventurer.Destroy();
            }
        }

        public override bool CanAttack
        {
            get
            {
                return true;
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override int Destroy()
        {
            if (!this.IsDead)
            {
                this.IsDead = true;
                return GetValue();
            }
            else
                return 0;
        }

        protected Enemy(Map.Map map, MapElement startSquare)
            : base(map)
        {
            this.Position = startSquare.Position;
            this.X = startSquare.X;
            this.Y = startSquare.Y;
            startSquare.Occupy(this);
        }

        public override System.Xml.Serialization.IXmlSerializable ToInfo()
        {
            var info = new EnemyInfo(X, Y, Position, GetType().Name);

            return info;
        }
        public void Construct(System.Xml.Serialization.IXmlSerializable info)
        {
            throw new NotImplementedException();
        }
    }
}
