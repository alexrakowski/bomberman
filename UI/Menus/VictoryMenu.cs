using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.UI.Menus
{
    class VictoryMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Main Menu
                case 0:
                    return new MainMenu(this._iGame);
                // Quit
                case 1:
                    _iGame.Quit();
                    break;
            }
            return null;
        }

        public VictoryMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Congratulations! You won the game!";
            string[] positions = { "Return to Main Menu", "Exit" };
            this._positions = positions;
        }

        protected override void ProcessInput(char input, bool delete = false)
        {
        }
    }
}
