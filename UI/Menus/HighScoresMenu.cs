using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
    class HighScoresMenu : Menu
    {
        protected override Menu MakeAction()
        {
            if (_currentPosition < this._positions.Length - 1)
            {
                // do nothing 
                return null;
            }
            if (_currentPosition == this._positions.Length - 1)
            {
                var prevMenu = _parentMenu == null ? new MainMenu(this._iGame) : _parentMenu;
                return prevMenu;
            }
            return null;
        }

        public HighScoresMenu(IGame iGame, Menu parentMenu)
            : this(iGame)
        {
            this._parentMenu = parentMenu;
        }
        public HighScoresMenu(IGame iGame)
            : base(iGame)
        {
            this.Title = "High Scores";
            var highScores = _iGame.GetHighScores();
            this._positions = new string[highScores.Length + 1];

            for (int i = 0; i < highScores.Length; ++i)
            {
                var highScore = highScores[i];
                this._positions[i] = highScore.Item1 + ": " + highScore.Item2.ToString();
            }

            this._positions[highScores.Length] = "Back";
        }
    }
}
