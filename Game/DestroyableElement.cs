using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game
{
    /// <summary>
    /// Abstract class representing an element that can be destroyed,
    /// eg. by the bomb's explosion.
    /// </summary>
    public abstract class DestroyableElement : GameElement, IDestroyable
    {
        /// <summary>
        /// Invoke this method when the element should be destroyed.
        /// </summary>
        /// <returns>
        /// Number of points scored for destroying this object.
        /// </returns>
        public abstract int Destroy();

        /// <summary>
        /// Returns the points value of the element.
        /// </summary>
        /// <returns>points value of the element</returns>
        public abstract int GetValue();

        /// <summary>
        /// Boolean field indicating if the element is dead.
        /// </summary>
        public bool IsDead { get; protected set; }
        /// <summary>
        /// Boolean field indicating if the element is indestructible.
        /// </summary>
        public bool IsIndestructible { get; set; }
    }
}
