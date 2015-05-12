using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.UI.Menus;
using Microsoft.Xna.Framework.Input;

namespace Bomberman.UI
{
    class UIManager : IDrawable
    {
        private IGame _iGame;
        private Menu _currentMenu; 

        SpriteFont font;
        Vector2 fontPos;

        public void ShowMainMenu() 
        {
            if (_currentMenu == null)
                _currentMenu = new MainMenu(this._iGame);
        }
        public void ShowLoginMenu()
        {
            if (_currentMenu != null) return;
            _currentMenu = new LoginMenu(this._iGame);
        }
        public void ShowPauseMenu()
        {
            _currentMenu = new PauseMenu(this._iGame);
        }
        public void ShowGameOverMenu()
        {
            _currentMenu = new GameOverMenu(this._iGame);
        }
        public void ShowVictoryMenu()
        {
            _currentMenu = new VictoryMenu(this._iGame);
        }

        public void Update(Moves move)
        {
            if (_currentMenu == null || move == Moves.None) return;

            var nextMenu = _currentMenu.Update(move);
            if (nextMenu != null) _currentMenu = nextMenu;
        }
        public void Update(Moves move, char input, bool delete = false)
        {
            if (_currentMenu == null)
                return;

            if (move != Moves.None)
            {
                var nextMenu = _currentMenu.Update(move);
                if (nextMenu != null) _currentMenu = nextMenu;
            }
            else
                _currentMenu.Update(input, delete);
        }
        public void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("fonts/TestFont");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentMenu == null) return;

            _currentMenu.Draw(spriteBatch, font);
        }
        public UIManager(IGame iGame, GraphicsDeviceManager graphics)
        {
            this._iGame = iGame;
            fontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
        }
    }
}
