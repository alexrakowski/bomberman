using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;

namespace Bomberman
{
    abstract class Element : IDrawable
    {
        /* _position - Element's position on the screen, to enable drawing it, movement's animation etc. Real values.*/
        public Vector2 Position;

        public abstract Texture2D GetTexture();
        public void Draw(SpriteBatch spriteBatch)
        {
            var texture = GetTexture();
            var rectangle = new Rectangle((int)Position.X, (int)Position.Y, MapElement.WIDTH, MapElement.HEIGHT);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public abstract void LoadContent(ContentManager content);
    }
}
