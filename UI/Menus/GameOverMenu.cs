using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.UI.Menus
{
    class GameOverMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Main Menu
                case 0:
                    return new MainMenu(this._iGame);
                    break;
                // Quit
                case 1:
                    _iGame.Quit();
                    break;
            }
            return null;
        }

        public GameOverMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Game Over!";
            string[] positions = { "Return to Main Menu", "Exit" };
            this._positions = positions;
        }
    }
}
