using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Bomberman.Utils;
using Bomberman.Game.Movable;
using Bomberman.IO;

namespace Bomberman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class BombermanGame : Microsoft.Xna.Framework.Game, IGame
    {
        #region Fields
        //Graphics
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix spriteScale;
        //Input
        InputHelper inputHelper;

        //Managers
        private UI.UIManager _UIManager;
        private Game.GameManager _GameManager;
        
        // Error Logger
        ILoggable logger;

        //settings
        private OptionsSettings _settings;
        public bool IsGameRunning { get; private set; }
        public string PlayerName { get; private set; }
        bool IsPlayerLoggedIn = false;
        #endregion

        #region XNAGame methods
        public BombermanGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 600;
            GameConstants.SetDisplaySize(graphics);

            Content.RootDirectory = "Content";

            logger = new BoxLogger();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            inputHelper = new InputHelper();
            _settings = new OptionsSettings();

            //Managers
            _UIManager = new UI.UIManager(this, this.graphics);
            _GameManager = Game.GameManager.GetInstance((IGame)this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            float xScale =
                (float)graphics.GraphicsDevice.Viewport.Width / 900f;
            float yScale =
                (float)graphics.GraphicsDevice.Viewport.Height / 600f;
            spriteScale = Matrix.CreateScale(xScale, yScale, 1);

            _UIManager.LoadContent(this.Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update();
            var move = InputToMove(Keyboard.GetState());

            if (IsGameRunning)
            {
                if (move == Moves.Pause)
                {
                    IsGameRunning = false;
                    _UIManager.ShowPauseMenu();
                }
                else
                {
                    var elapsedTime = (int)gameTime.ElapsedGameTime.Milliseconds;
                    try
                    {
                        _GameManager.Update(elapsedTime, move);
                    }
                    catch (BombermanException bombermanException)
                    {
                        logger.Log(bombermanException);
                    }
                    if (_GameManager.HasPlayerLost)
                    {
                        IsGameRunning = false;
                        _UIManager.ShowGameOverMenu();
                    }
                    if (_GameManager.HasPlayerWon)
                    {
                        IsGameRunning = false;
                        _UIManager.ShowVictoryMenu();
                    }
                }
            }
            else
            {
                //display menu
                try
                {
                    if (IsPlayerLoggedIn)
                    {
                        _UIManager.ShowMainMenu();
                        _UIManager.Update(move);
                    }
                    else
                    {
                        _UIManager.ShowLoginMenu();

                        char input;
                        bool delete;
                        if (InputToChar(Keyboard.GetState(), out input, out delete))
                            _UIManager.Update(move, input, delete);
                        else
                            _UIManager.Update(move);
                    }
                }
                catch (BombermanException bombermanException)
                {
                    logger.Log(bombermanException);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(0, null, null, null, null, null, spriteScale);

            if (IsGameRunning)
            {
                _GameManager.Draw(spriteBatch);
            }
            else
            {
                _UIManager.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        private Moves InputToMove(KeyboardState keyboardState)
        {
            var keys = keyboardState.GetPressedKeys();
            if (keys.Length < 1) return Moves.None;
            var key = keys[0];

            if (IsGameRunning)
            {
                if (this._settings.Arrows)
                {
                    switch (key)
                    {
                        case Keys.Space:
                            return inputHelper.IsNewPress(key) ? Moves.Fire : Moves.None;
                        case Keys.Left:
                            return Moves.Left;
                        case Keys.Right:
                            return Moves.Right;
                        case Keys.Down:
                            return Moves.Down;
                        case Keys.Up:
                            return Moves.Up;

                        // game menu
                        case Keys.Escape:
                            return Moves.Pause;
                    }
                }
                else
                {
                    //WSAD
                    switch (key)
                    {
                        case Keys.Space:
                            return inputHelper.IsNewPress(key) ? Moves.Fire : Moves.None;
                        case Keys.A:
                            return Moves.Left;
                        case Keys.D:
                            return Moves.Right;
                        case Keys.S:
                            return Moves.Down;
                        case Keys.W:
                            return Moves.Up;

                        // game menu
                        case Keys.Escape:
                            return Moves.Pause;
                    }
                }
            }
            else if (inputHelper.IsNewPress(key))
            {
                switch (key)
                {
                    case Keys.Enter:
                        return Moves.Enter;
                    case Keys.Down:
                        return Moves.Down;
                    case Keys.Up:
                        return Moves.Up;
                }
            }
            return Moves.None;
        }
        private bool InputToChar(KeyboardState keyboardState, out char input, out bool delete)
        {
            input = ' ';
            delete = false;
            var keys = keyboardState.GetPressedKeys();
            if (keys.Length < 1)
                return false;
            var key = keys[0];
            if (inputHelper.IsNewPress(key))
            {
                if (key == Keys.Back)
                {
                    delete = true;
                    return true;
                }
                input = key.ToString()[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        #region IGame implementation
        public void NewGame()
        {
            this.IsGameRunning = true;
            this._GameManager.NewGame(PlayerName);
            this._GameManager.LoadContent(this.Content);
        }

        public void SaveGame()
        {
            var gameState = _GameManager.GetGameState();
            FileManager.SaveGameFile(gameState, PlayerName);
        }

        public void LoadGame(string filename)
        {
            var gameState = FileManager.LoadGameFile(PlayerName, filename);
            _GameManager.LoadGame(gameState);
            IsGameRunning = true;
            this._GameManager.LoadContent(this.Content);
        }

        public void ResumeGame()
        {
            this.IsGameRunning = true;
        }

        public string[] GetSavedGames()
        {
            var saves = FileManager.GetSavedGames(PlayerName);
            return saves;
        }

        public string[][] LoadMapFile(string mapName)
        {
            var mapFile = IO.FileManager.LoadMapFile(mapName);
            return mapFile;
        }

        public Tuple<string, int>[] GetHighScores()
        {
            var highScores = FileManager.GetHighScores();
            return highScores;
        }

        public void UpdateHighScores(string playerName, int score)
        {
            FileManager.AddHighScore(playerName, score);
        }

        public string ToggleOption(OptionType option)
        {
            var result = this._settings.Toggle(option);
            return result;
        }


        public string GetOptionValue(OptionType option)
        {
            return this._settings.GetOptionValue(option); ;
        }
        public void Login(string nickname)
        {
            if (nickname.Length < 1)
            {
                throw new BombermanException("Name must be longer than 0");
            }
            PlayerName = nickname;
            IsPlayerLoggedIn = true;
        }

        public void Quit()
        {
            this.Exit();
        }
        #endregion

    }
}
