using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
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
}
