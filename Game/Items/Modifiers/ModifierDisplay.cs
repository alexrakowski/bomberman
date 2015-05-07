using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Bomberman.Game.Serialization;

namespace Bomberman.Game.Items.Modifiers
{
    /// <summary>
    /// Class for displaying information about a modifier.
    /// </summary>
    class ModifierDisplay : IDrawable
    {
        private static SpriteFont spriteFont;

        private string _text;
        private int _time = 2000;
        public bool IsFinished { get; protected set; }

        public void Update(int elapsedTime)
        {
            _time -= elapsedTime;
            this.IsFinished = _time < 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the title
            Color fontColor = Color.Black;
            Vector2 fontPos = new Vector2(100, 100);
            Vector2 FontOrigin = spriteFont.MeasureString(this._text) / 2;

            spriteBatch.DrawString(spriteFont, this._text, fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
        public static void LoadClassContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("fonts/TestFont");
        }
        public void LoadContent(ContentManager content)
        {
            LoadClassContent(content);
        }

        public ModifierDisplay(Modifier modifier)
        {
            this._text = modifier.GetType().Name;
        }
    }
}
