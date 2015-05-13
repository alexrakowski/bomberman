using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;

namespace Bomberman
{
    /// <summary>
    /// Abstract class for elements to be drawn in the game.
    /// </summary>
    public abstract class Element : IDrawable
    {
        /// <summary>
        /// Element's position on the screen, to enable drawing it, movement's animation etc. Real values.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// In this method's implementation you should specify how/which texture are you using to draw.
        /// </summary>
        /// <returns>
        /// Element's texture to draw.
        /// </returns>
        public abstract Texture2D GetTexture();

        /// <summary>
        /// Draw the element using it's position,
        /// and texture provided by GetTexture method.
        /// </summary>
        /// <param name="spriteBatch">
        /// SpriteBatch to draw into
        /// </param>
        public void Draw(SpriteBatch spriteBatch)
        {
            var texture = GetTexture();
            var rectangle = new Rectangle((int)Position.X, (int)Position.Y, MapElement.WIDTH, MapElement.HEIGHT);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        /// <summary>
        /// This method should specify how and which of the element's resources to load.
        /// </summary>
        /// <param name="content">
        /// ContentManager to load content from
        /// </param>
        public abstract void LoadContent(ContentManager content);
    }
}
