using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Items;
using Bomberman.Game.Map;
using Bomberman.Game.Movable;
using Bomberman.Game.Items.Modifiers;
using Bomberman.Game.Serialization;

namespace Bomberman.Game
{
    partial class GameManager : IDrawable
    {
        private Map.Map _map;
        private List<Movable.Enemy> _enemies;
        private Movable.Adventurer _adventurer;
        private List<Items.Bomb> _bombs;
        private List<Modifier> _modifiers;

        public GameInfo GameInfo { get; private set; }
        private GamePanel _gamePanel;

        public bool HasPlayerLost() { throw new NotImplementedException(); }

        public void Update(int elapsedTime, Moves move)
        {
            //move movables
            _adventurer.Move(elapsedTime, move);
            var collectable = _adventurer.PickCollectable();
            if (collectable != null)
            {
                collectable.Collect(this);
            }

            foreach (var enemy in _enemies)
                enemy.Move(elapsedTime);
            //items
            UpdateModifiers(elapsedTime);
            UpdateBombs(elapsedTime);
            //cleanup of destroyed or killed elements
            CleanupTheDead();
        }
        private void UpdateModifiers(int elapsedTime)
        {
            foreach (var modifier in _modifiers)
            {
                modifier.Update(elapsedTime, GameInfo, _enemies, _adventurer);
            }
        }
        private void UpdateBombs(int elapsedTime)
        {
            int pointsScored = 0;
            foreach (var bomb in _bombs)
            {
                if (bomb.Tick(elapsedTime))
                {
                    pointsScored += bomb.Explode(_map);
                }
            }

            GameInfo.AddPoints(pointsScored);
        }
        private void CleanupTheDead()
        {
            _modifiers.RemoveAll(modifier => modifier.Time < 0);
            _bombs.RemoveAll(bomb => bomb.IsDead);
            _enemies.RemoveAll(enemy => enemy.IsDead);
            if (_adventurer.IsDead)
            {
                OnLifeLoss();
            }
        }
        private void OnLifeLoss()
        {
            if (GameInfo.LoseLife())
            {
                throw new NotImplementedException();
            }
            else
            {
                var square = _map.GetStartSquare();
                _adventurer.PutOnAnotherSquare(square);
                _adventurer.MakeAlive();
            }
        }
    }
    partial class GameManager : ICollector
    {
        public void AddMapFragment()
        {
            if (GameInfo.AddMapFragment())
            {
                // found all map fragments
                throw new NotImplementedException();
            }
        }
        public void AddModifier(Modifier modifier)
        {
            modifier.Apply(GameInfo, _enemies, _adventurer);
            if (_modifiers == null)
            {
                _modifiers = new List<Modifier>();
            }
            _modifiers.Add(modifier);
        }
    }
    partial class GameManager
    {
        private IGame _iGame;
        private GameManager(IGame iGame) { this._iGame = iGame; _gamePanel = GamePanel.GetInstance(); }
        private static GameManager instance;
        public static GameManager GetInstance(IGame iGame)
        {
            if (instance == null)
            {
                return instance = new GameManager(iGame);
            }
            else
            {
                return instance;
            }
        }
    }
    partial class GameManager
    {
        public void NewGame(string playerName)
        {
            var level = GameLevels.First;
            int fragmentsToFind = GetMapFragmentsToFind(level);
            this.GameInfo = new Game.GameInfo(playerName, fragmentsToFind, level);
            _map = LoadMap(level);
            _bombs = new List<Items.Bomb>();
            _adventurer = Game.Movable.Adventurer.GetNewInstance(_map, _bombs);

            _enemies = new List<Movable.Enemy>();
            AddEnemies(level, _map);

            _modifiers = new List<Modifier>();
        }
        public GameState GetGameState()
        {
            GameState gameState = ConstructGameState();
            return gameState;
        }
        private GameState ConstructGameState()
        {
            GameState gameState = new GameState();

            gameState.GameInfo = GameInfo;
            gameState.Player = _adventurer.GetInfo();
            gameState.Enemies = _enemies.Select(enemy => enemy.GetInfo()).ToList();
            gameState.Map = _map.GetInfo();
            gameState.Modifiers = _modifiers.Select(modifier => modifier.GetInfo()).ToList();
            gameState.Bombs = _bombs.Select(bomb => bomb.GetInfo()).ToList();

            return gameState;
        }

        private Map.Map LoadMap(GameLevels level)
        {
            var mapFile = _iGame.LoadMapFile(((int)level + 1).ToString());
            var map = Map.Map.MapFactory.MakeMap(mapFile, level);

            return map;
        }
        private int GetMapFragmentsToFind(GameLevels level)
        {
            switch (level)
            {
                case GameLevels.First:
                    return 3;
                case GameLevels.Second:
                    return 4;
                case GameLevels.Third:
                    return 5;
                default:
                    return 3;
            }
        }
        private void AddEnemies(GameLevels level, Map.Map map)
        {
            int wolvesCount = 0;
            int owlsCount = 0;
            int foxesCount = 0;
            int bearsCount = 0;

            switch (level)
            {
                case GameLevels.First:
                    wolvesCount = 4;
                    owlsCount = 2;
                    break;
                case GameLevels.Second:
                    wolvesCount = 3;
                    owlsCount = 3;
                    foxesCount = 1;
                    bearsCount = 1;
                    break;
                case GameLevels.Third:
                    wolvesCount = 2;
                    owlsCount = 2;
                    foxesCount = 4;
                    bearsCount = 3;
                    break;
                default:
                    wolvesCount = 4;
                    owlsCount = 2;
                    break;
            }

            var freeSquares = map.GetUnoccupiedSquares();
            var adventurerSquare = map.GetStartSquare();

            Utils.Shuffler.Shuffle(freeSquares);

            MapElement square;
            for (int i = 0; i < wolvesCount; ++i)
            {
                // put enemies in a distance from adventurer's starting point
                do
                {
                    if (freeSquares.Count < 1)
                    {
                        throw new BombermanException("Not enough space for the enemies");
                    }
                    square = freeSquares.First();
                    freeSquares.Remove(square);
                } while (Map.Map.GetSquaresDistance(square, adventurerSquare) < 5);

                var wolf = new Wolf(map, square);
                _enemies.Add(wolf);
            }
        }
    }
    //drawing
    partial class GameManager
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            _gamePanel.Draw(spriteBatch, GameInfo);

            _map.Draw(spriteBatch);
            _adventurer.Draw(spriteBatch);

            foreach (var enemy in _enemies)
                enemy.Draw(spriteBatch);
            foreach (var bomb in _bombs)
                bomb.Draw(spriteBatch);
        }

        public void LoadContent(ContentManager content)
        {
            _gamePanel.LoadContent(content);

            _map.LoadContent(content);
            _adventurer.LoadContent(content);

            foreach (var enemy in _enemies)
                enemy.LoadContent(content);
            LoadItemsContent(content);
        }
        private void LoadItemsContent(ContentManager content)
        {
            Bomb.LoadClassContent(content);
            MapFragment.LoadClassContent(content);
            Modifier.LoadClassContent(content);
        }
    }
}
