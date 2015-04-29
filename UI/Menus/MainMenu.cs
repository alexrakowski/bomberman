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

        public MainMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Menu";
            string[] positions = { "New Game", "Load Game", "High Scores", "Settings", "Exit" };
            this._positions = positions;
        }
    }
}
