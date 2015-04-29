using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
    abstract class Menu
    {
        protected string[] _positions;
        protected IGame _iGame;
        protected int _currentPosition;
        protected Menu _parentMenu = null;

        public SpriteFont SpriteFont { get; protected set; }
        public String Title { get; protected set; }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            //Draw the title
            Color fontColor = Color.Black;
            Vector2 fontPos = new Vector2(100, 100);
            Vector2 FontOrigin = spriteFont.MeasureString(this.Title) / 2;

            spriteBatch.DrawString(spriteFont, this.Title, fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            for (int i = 0; i < _positions.Length; ++i)
            {
                fontPos.Y += 20;
                //Display current position in red
                fontColor = (i == _currentPosition) ? Color.Red : Color.Black;

                spriteBatch.DrawString(spriteFont, _positions[i], fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
        public Menu Update(Moves move) 
        {
            switch (move)
            {
                case Moves.Up:
                    _currentPosition = _currentPosition == 0 ? _positions.Length - 1: _currentPosition - 1;
                    break;
                case Moves.Down:
                    _currentPosition = (_currentPosition + 1) % _positions.Length;
                    break;
                case Moves.Enter:
                    var nextMenu = MakeAction();
                    return nextMenu;
            }
            return null;
        }
        /// <summary>
        /// Performs action corresponding to the currently selected option, i.e. disable sound,
        /// launch new game, show another menu etc.
        /// </summary>
        /// <returns>
        /// Instantiated Menu object, if action should show another menu.
        /// Otherwise, null
        /// </returns>
        protected abstract Menu MakeAction();

        public Menu(IGame iGame, Menu parentMenu) : this(iGame)
        {
            this._parentMenu = parentMenu;
        }
        public Menu(IGame iGame) 
        { 
            if (iGame == null) throw new BombermanException("IGame cannot be null"); 
            this._iGame = iGame; 
        }
    }

    class MainMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // New Game
                case 0:
                    _iGame.NewGame();
                    break;
                // Load Game
                case 1:
                    _iGame.LoadGame();
                    break;
                //High Scores
                case 2:
                    throw new NotImplementedException();
                    break;

                 //Settings
                case 3:
                    return new SettingsMenu(this._iGame, this);
                    break;
                //Exit
                case 4:
                    _iGame.Quit();
                    break;
            }
            return null;
        }

        public MainMenu(IGame iGame) : base(iGame) 
        {
            this.Title = "Menu";
            string [] positions = { "New Game", "Load Game", "High Scores", "Settings", "Exit"};
            this._positions = positions;
        }
    }
    class SettingsMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Controls
                case 0:
                    throw new NotImplementedException();
                    break;
                // Sounds
                case 1:
                    throw new NotImplementedException();
                    break;
                //Music
                case 2:
                    throw new NotImplementedException();
                    break;

                //Back
                case 3:
                    var prevMenu = _parentMenu == null ? new MainMenu(this._iGame) : _parentMenu;
                    return prevMenu;
            }
            return null;
        }

        public SettingsMenu(IGame iGame, Menu parentMenu)
            : this(iGame)
        {
            this._parentMenu = parentMenu;
        }
        public SettingsMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Settings";
            string[] positions = { "Controls: arrows", "Sounds: On", "Music: On", "Back" };
            this._positions = positions;
        }
    }
    class PauseMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Resume
                case 0:
                    _iGame.ResumeGame();
                    break;
                // Save Game
                case 1:
                    _iGame.SaveGame();
                    break;
                //Exit
                case 2:
                    return new MainMenu(this._iGame);
            }
            return null;
        }

        public PauseMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Pause";
            string[] positions = { "Resume", "Save Game", "Return to Main Menu" };
            this._positions = positions;
        }
    }
}
