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

        public GameInfo _gameInfo;
        private GamePanel _gamePanel;

        public bool HasPlayerLost() { throw new NotImplementedException(); }

        public void Update(int elapsedTime, Moves move)
        {
            if (_gameInfo.ElapseTime(elapsedTime))
            {
                throw new NotImplementedException();
            }
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
                modifier.Update(elapsedTime, _gameInfo, _enemies, _adventurer);
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

            _gameInfo.AddPoints(pointsScored);
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
            if (_gameInfo.LoseLife())
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
            if (_gameInfo.AddMapFragment())
            {
                // found all map fragments
                StartNextLevel();
            }
        }
        public void AddModifier(Modifier modifier)
        {
            modifier.Apply(_gameInfo, _enemies, _adventurer);
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
            _gameInfo = null;

            StartLevel(level);
        }
        private void StartLevel(GameLevels level)
        {
            _modifiers = new List<Modifier>();
            _bombs = new List<Items.Bomb>();

            _map = LoadMap(level);

            LevelFactory.CreateLevel(level, _map, ref _gameInfo, out _enemies);
            _adventurer = Game.Movable.Adventurer.GetNewInstance(_map, _bombs);
        }
        public GameState GetGameState()
        {
            GameState gameState = ConstructGameState();
            return gameState;
        }

        private void StartNextLevel()
        {
            if (_gameInfo.Level == GameLevels.Third)
            {
                // somebody has won the game, nice!
                throw new NotImplementedException();
            }
            _gameInfo.NextLevel();
            StartLevel(_gameInfo.Level);
        }
        private GameState ConstructGameState()
        {
            GameState gameState = new GameState();

            gameState.GameInfo = _gameInfo;
            gameState.Player = (AdventurerInfo) _adventurer.GetInfo();
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
        
    }
    //drawing
    partial class GameManager
    {
        public void Draw(SpriteBatch spriteBatch)
        {
            _gamePanel.Draw(spriteBatch, _gameInfo);

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
            Bonus.LoadClassContent(content);
        }
    }
}
