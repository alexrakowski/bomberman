using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    /// <summary>
    /// Interface for drawable objects
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws the objects.
        /// </summary>
        /// <param name="spriteBatch">
        /// The batch to draw into
        /// </param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Load neccessary content.
        /// </summary>
        /// <param name="content">
        /// ContentManager to load content from
        /// </param>
        void LoadContent(ContentManager content);
    }
}
