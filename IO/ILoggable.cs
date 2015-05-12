using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.IO
{
    /// <summary>
    /// Interface for logging.
    /// </summary>
    public interface ILoggable
    {
        /// <summary>
        /// Logs a string.
        /// </summary>
        /// <param name="message">Message to log</param>
        void Log(string message);
        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="exception">
        /// Exception to log
        /// </param>
        void Log(Exception exception);
    }
}
