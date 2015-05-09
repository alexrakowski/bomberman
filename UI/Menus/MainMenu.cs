using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
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
                    return new LoadGameMenu(this._iGame, this);
                //High Scores
                case 2:
                    return new HighScoresMenu(this._iGame, this);
                //Settings
                case 3:
                    return new SettingsMenu(this._iGame, this);
                //Exit
                case 4:
                    _iGame.Quit();
                    break;
            }
            return null;
        }

        public MainMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Main Menu";
            string[] positions = { "New Game", "Load Game", "High Scores", "Settings", "Exit" };
            this._positions = positions;
        }

        protected override void ProcessInput(char input, bool delete = false)
        {
        }
    }
}
