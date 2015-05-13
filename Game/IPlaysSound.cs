using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Game
{
    /// <summary>
    /// Interface for playing sounds.
    /// </summary>
    public interface IPlaysSound
    {
        /// <summary>
        /// Method prompting the object to play sound.
        /// </summary>
        void PlaySound();
    }
}
