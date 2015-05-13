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
using Bomberman.Game.Movable.Enemies;

namespace Bomberman.Game
{
    class GameManager : IDrawable, ICollector
    {
        #region Fields
        public const int RESPAWN_SHIELD_TIME_MILISECS = 50;

        private Map.Map _map;
        private List<Enemy> _enemies;
        private Adventurer _adventurer;
        private List<Bomb> _bombs;
        private List<Modifier> _modifiers;

        private GameInfo _gameInfo;
        private GamePanel _gamePanel;
        private List<ModifierDisplay> _modifierDisplays;

        public bool HasPlayerLost { get; protected set; }
        public bool HasPlayerWon { get; protected set; }
        #endregion

        #region Game Management
        public void Update(int elapsedTime, Moves move)
        {
            if (_gameInfo.ElapseTime(elapsedTime))
            {
                this.HasPlayerLost = true;
            }
            //move movables
            _adventurer.Move(elapsedTime, move);
            var collectable = _adventurer.PickCollectable();
            if (collectable != null)
            {
                collectable.Collect(this);
            }

            foreach (var enemy in _enemies.Where(e => !e.IsParalyzed))
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
            if (_modifierDisplays != null)
            {
                foreach (var display in _modifierDisplays)
                {
                    display.Update(elapsedTime);
                }
                _modifierDisplays.RemoveAll(d => d.IsFinished);
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
                this.HasPlayerLost = true;
            }
            else
            {
                var square = _map.GetStartSquare();
                _adventurer.PutOnAnotherSquare(square);
                _adventurer.MakeAlive();

                var modifiersEndingOnDeath = _modifiers.Where(m => m.EndsOnPlayerDeath);
                foreach (var m in modifiersEndingOnDeath)
                    m.EndNow(_gameInfo, _enemies, _adventurer);

                AddRespawnShield();
            }
        }
        private void AddRespawnShield()
        {
            var indestructibleModifier = new Items.Modifiers.Positive.Indestructible(RESPAWN_SHIELD_TIME_MILISECS);
            _modifiers.Add(indestructibleModifier);
            indestructibleModifier.Apply(_gameInfo, _enemies, _adventurer);
        }
        #endregion

        #region ICollector
        public void AddMapFragment()
        {
            if (_gameInfo.AddMapFragment())
            {
                // found all map fragments
                OnLevelWon();
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
            AddModifierDisplay(new ModifierDisplay(modifier));
        }
        private void AddModifierDisplay(ModifierDisplay display)
        {
            if (_modifierDisplays == null)
            {
                _modifierDisplays = new List<ModifierDisplay>();
            }
            _modifierDisplays.Add(display);
        }
        #endregion

        #region Levels' management
        private void ResetWonLost()
        {
            HasPlayerLost = false;
            HasPlayerWon = false;
        }
        public void NewGame(string playerName)
        {
            ResetWonLost();

            var level = GameLevels.First; 
            _gameInfo = null;

            StartLevel(level, playerName);
        }
        public void LoadGame(object gameStateObj)
        {
            ResetWonLost();

            GameState gameState;
            try
            {
                gameState = (GameState)gameStateObj;
            }
            catch (InvalidCastException exception)
            {
                throw new BombermanException("Could not parse Game State. \n" + exception.Message);
            }

            LevelFactory.LoadLevel(gameState, out _gameInfo, out _map, out _adventurer, out _enemies, out _bombs, out _modifiers);
        }

        private void StartLevel(GameLevels level, string playerName)
        {
            _modifiers = new List<Modifier>();
            _bombs = new List<Items.Bomb>();

            _map = LoadMap(level);

            LevelFactory.CreateLevel(level, playerName, _map, ref _gameInfo, out _enemies, _bombs);
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
                OnGameWon();
                return;
            }
            Bomb.ResetRange();
            _gameInfo.NextLevel();
            StartLevel(_gameInfo.Level, _gameInfo.PlayerName);
        }
        private void OnLevelWon()
        {
            // calculate points for time left
            int pointsForTime = (int)_gameInfo.Time;
            int timeForLevel = LevelFactory.GetTimeForLevel(_gameInfo.Level);

            double timePercentage = _gameInfo.Time / timeForLevel;
            if (timePercentage < 0.25)
            {
                pointsForTime *= 10;
            }
            else if (timePercentage < 0.5)
            {
                pointsForTime *= 20;
            }
            else
            {
                pointsForTime *= 50;
            }

            _gameInfo.AddPoints(pointsForTime);
        }
        private void OnGameWon()
        {
            this.HasPlayerWon = true;
            this._iGame.UpdateHighScores(_gameInfo.PlayerName, _gameInfo.Score);
        }
        private GameState ConstructGameState()
        {
            GameState gameState = new GameState();

            gameState.GameInfo = _gameInfo;
            gameState.Player = (AdventurerInfo) _adventurer.ToInfo();
            gameState.Enemies = _enemies.Select(enemy => enemy.ToInfo()).ToList();
            gameState.Map = _map.ToInfo();
            gameState.Modifiers = _modifiers.Select(modifier => modifier.ToInfo()).ToList();
            gameState.Bombs = _bombs.Select(bomb => (BombInfo)bomb.ToInfo()).ToList();

            return gameState;
        }
        private Map.Map LoadMap(GameLevels level)
        {
            var mapFile = _iGame.LoadMapFile(((int)level + 1).ToString());
            var map = Map.Map.MapFactory.MakeMap(mapFile, level);

            return map;
        }
        #endregion

        #region Singleton implementation
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
        #endregion

        #region Drawing
        public void Draw(SpriteBatch spriteBatch)
        {
            _gamePanel.Draw(spriteBatch, _gameInfo);

            _map.Draw(spriteBatch);
            _adventurer.Draw(spriteBatch);

            foreach (var enemy in _enemies)
                enemy.Draw(spriteBatch);
            foreach (var bomb in _bombs)
                bomb.Draw(spriteBatch);

            //ModifierDisplays
            if (_modifierDisplays != null)
            {
                foreach (var display in _modifierDisplays)
                {
                    display.Draw(spriteBatch);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            _gamePanel.LoadContent(content);

            _map.LoadContent(content);
            _adventurer.LoadContent(content);

            foreach (var enemy in _enemies)
                enemy.LoadContent(content);
            Fox.LoadClassContent(content);
            Bear.LoadClassContent(content);
            
            LoadItemsContent(content);
        }
        private void LoadItemsContent(ContentManager content)
        {
            Bomb.LoadClassContent(content);
            MapFragment.LoadClassContent(content);
            Bonus.LoadClassContent(content);
            ModifierDisplay.LoadClassContent(content);
        }
        #endregion
    }
}
