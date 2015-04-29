using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.UI.Menus
{
    class LoginMenu : Menu
    {
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Resume
                case 0:
                    throw new NotImplementedException();
                    break;
                // Save Game
                case 1:
                    _iGame.Login(_positions[0]);
                    break;
                //Exit
                case 2:
                    _iGame.Quit();
                    break;
            }
            return null;
        }

        public LoginMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Enter your Nickname";
            string[] positions = { "Alek", "Ok", "Quit" };
            this._positions = positions;
        }
    }
}
