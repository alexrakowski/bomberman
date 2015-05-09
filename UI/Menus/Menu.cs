using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.UI.Menus
{
    abstract class Menu
    {
        protected string[] _positions;
        protected IGame _iGame;
        protected int _currentPosition;
        protected Menu _parentMenu = null;

        public SpriteFont SpriteFont { get; protected set; }
        public String Title { get; protected set; }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            //Draw the title
            Color fontColor = Color.Black;
            Vector2 fontPos = new Vector2(100, 100);
            Vector2 FontOrigin = spriteFont.MeasureString(this.Title);
            FontOrigin.X -= GameConstants.DisplayWidth / 2;

            spriteBatch.DrawString(spriteFont, this.Title, fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);

            for (int i = 0; i < _positions.Length; ++i)
            {
                fontPos.Y += 20;
                //Display current position in red
                fontColor = (i == _currentPosition) ? Color.Red : Color.Black;

                spriteBatch.DrawString(spriteFont, _positions[i], fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
        public Menu Update(Moves move) 
        {
            switch (move)
            {
                case Moves.Up:
                    _currentPosition = _currentPosition == 0 ? _positions.Length - 1: _currentPosition - 1;
                    break;
                case Moves.Down:
                    _currentPosition = (_currentPosition + 1) % _positions.Length;
                    break;
                case Moves.Enter:
                    var nextMenu = MakeAction();
                    return nextMenu;
            }
            return null;
        }
        public void Update(char input, bool delete)
        {
            ProcessInput(input, delete);
        }
        /// <summary>
        /// Performs action corresponding to the currently selected option, i.e. disable sound,
        /// launch new game, show another menu etc.
        /// </summary>
        /// <returns>
        /// Instantiated Menu object, if action should show another menu.
        /// Otherwise, null
        /// </returns>
        protected abstract Menu MakeAction();
        protected abstract void ProcessInput(char input, bool delete = false);

        public Menu(IGame iGame, Menu parentMenu) : this(iGame)
        {
            this._parentMenu = parentMenu;
        }
        public Menu(IGame iGame) 
        { 
            if (iGame == null) throw new BombermanException("IGame cannot be null"); 
            this._iGame = iGame; 
        }
    }
}
