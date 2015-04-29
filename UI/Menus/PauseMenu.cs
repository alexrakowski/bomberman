using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
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
