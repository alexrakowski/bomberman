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
                    break;
                // Save Game
                case 1:
                    _iGame.Login(_positions[0]);
                    return new MainMenu(this._iGame);
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
            string[] positions = { "", "Ok", "Quit" };
            this._positions = positions;
        }

        protected override void ProcessInput(char input, bool delete = false)
        {
            if (delete)
            {
                if (_positions[0].Length > 1)
                {
                    int substringLength = _positions[0].Length - 2;
                    _positions[0] = _positions[0].Substring(0, substringLength);
                }
                return;
            }
            _positions[0] += input;
        }
    }
}
