using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
    class LoadGameMenu : Menu
    {
        protected override Menu MakeAction()
        {
            if (_currentPosition < this._positions.Length - 1)
            {
                this._iGame.LoadGame(_positions[_currentPosition]);
            }
            if (_currentPosition == this._positions.Length - 1)
            {
                var prevMenu = _parentMenu == null ? new MainMenu(this._iGame) : _parentMenu;
                return prevMenu;
            }
            return null;
        }

        public LoadGameMenu(IGame iGame, Menu parentMenu)
            : this(iGame)
        {
            this._parentMenu = parentMenu;
        }
        public LoadGameMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "Select File:";
            var saves = _iGame.GetSavedGames();
            this._positions = new string[saves.Length + 1];
            saves.CopyTo(this._positions, 0);
            this._positions[saves.Length] = "Back";
        }

        protected override void ProcessInput(char input, bool delete = false)
        {
        }
    }
}
