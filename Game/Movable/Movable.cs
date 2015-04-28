using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Game.Movable
{
    public interface IMovable
    {
        void Move(int elapsedTime, Moves move = Moves.None);
        bool IsMoving { get; }
        Moves Direction { get; }
    }

    abstract partial class MovableElement : DestroyableElement, IMovable
    {
        public float InitialSpeed = 1;
        public float Speed { get { return (float)(InitialSpeed * SpeedModifier); } }
        [System.ComponentModel.DefaultValue(1.0f)]
        public float SpeedModifier { get; set; }
        protected float _distanceLeft;
        protected Map.Map _map;

        public abstract bool CanFly { get; protected set; }
        public abstract bool CanAttack { get; protected set; }

        public abstract void Move(int elapsedTime, Moves moveDirection = Moves.None);
        protected void StartMoving(Moves moveDirection)
        {
            if (!this.IsMoving && moveDirection != Moves.None)
            {
                this.IsMoving = true;
                this.Direction = moveDirection;
                this._distanceLeft = Map.MapElement.WIDTH;

                _map.LeaveSquare(this);
                switch (moveDirection)
                {
                    case Moves.Up:
                        _map.OccupySquare(X, Y - 1, this);
                        break;
                    case Moves.Down:
                        _map.OccupySquare(X, Y + 1, this);
                        break;
                    case Moves.Right:
                        _map.OccupySquare(X + 1, Y, this);
                        break;
                    case Moves.Left:
                        _map.OccupySquare(X - 1, Y, this);
                        break;
                }
                return;
            }
        }
        protected void ContinueMoving(int elapsedTime)
        {      
            var distanceMoved = elapsedTime * this.Speed;
            this._distanceLeft -= distanceMoved;

            switch (this.Direction)
            {
                case Moves.Up:
                    this.Position.Y -= distanceMoved;
                    break;
                case Moves.Down:
                    this.Position.Y += distanceMoved;
                    break;
                case Moves.Right:
                    this.Position.X += distanceMoved;
                    break;
                case Moves.Left:
                    this.Position.X -= distanceMoved;
                    break;
            }
            if (this.IsMoving && this._distanceLeft < Map.MapElement.WIDTH /2)
            {
                
            }
            if (this.IsMoving && this._distanceLeft < 0.001)
            // finished moving to another square
            {           
                switch (this.Direction)
                {
                    case Moves.Up:
                        this.Y -= 1;
                        break;
                    case Moves.Down:
                        this.Y += 1;
                        break;
                    case Moves.Right:
                        this.X += 1;
                        break;
                    case Moves.Left:
                        this.X -= 1;
                        break;
                }
                var square = _map.GetSquare(X, Y);
                this.Position = square.Position;
                //_map.OccupySquare(this);
                this.IsMoving = false;
                this.Direction = Moves.None;
            }
        }
        protected bool IsMoveValid(Moves move)
        {
            Map.MapElement square = null;
            switch (move)
            {
                case Moves.Up:
                    square = _map.GetSquare(X, Y - 1);
                    break;
                case Moves.Down:
                    square = _map.GetSquare(X, Y + 1);
                    break;
                case Moves.Right:
                    square = _map.GetSquare(X + 1, Y);
                    break;
                case Moves.Left:
                    square = _map.GetSquare(X - 1, Y);
                    break;
            }
            if (square == null) return false;
            //check if move is legal
            if (!square.IsFlyingTerrain)
                return false;
            if (square.IsOccupied)
            {
                if (!this.CanAttack) return false;
                else
                {
                    var adventurerSquare = Adventurer.GetOccupiedSquare();
                    if (adventurerSquare != square) return false;
                }
            }
            return this.CanFly || square.IsWalkingTerrain;
        }
        [System.ComponentModel.DefaultValue(false)]
        public bool IsMoving { get; protected set; }
        public Moves Direction { get; protected set; }
        private void StopMove()
        {
            if (this.IsMoving)
            {
                this.IsMoving = false;
                this.Direction = Moves.None;
            }
        }

        public void PutOnAnotherSquare(Map.MapElement square)
        {
            var prevSquare = _map.GetSquare(X, Y);
            prevSquare.Leave(this);
            PutOnSquare(square);
            square.Occupy(this);
            this.StopMove();
        }

        public MovableElement(Map.Map map) { _map = map; SpeedModifier = 1; }
    }
}
