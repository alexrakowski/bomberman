using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
    class SettingsMenu : Menu
    {
        private static string[] SettingsNames = {"Controls: ", "Sound: ",  "Music: "};
        protected override Menu MakeAction()
        {
            switch (_currentPosition)
            {
                // Controls
                case 0:
                    string result = this._iGame.ToggleOption(OptionType.Controls);
                    this._positions[_currentPosition] = SettingsNames[_currentPosition] + result;
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
            this._positions = new string[SettingsNames.Length + 1];
            SettingsNames.CopyTo(_positions, 0);
            this._positions[_positions.Length - 1] = "Back";
            for (int i = 0; i < SettingsNames.Length; ++i)
            {
                if (i == 0)
                    this._positions[i] += this._iGame.GetOptionValue(OptionType.Controls);
                else
                    this._positions[i] += "On";
            }
        }
    }
}
