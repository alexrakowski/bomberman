using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager content);
    }
}
