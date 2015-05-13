using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bomberman.IO
{
    /// <summary>
    /// Logger displaying messages with a message box.
    /// </summary>
    public class BoxLogger : ILoggable
    {
        /// <summary>
        /// Logs a string message.
        /// </summary>
        /// <param name="message">
        /// Message to log
        /// </param>
        public void Log(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Logs exception
        /// </summary>
        /// <param name="exception">
        /// Exception to log
        /// </param>
        public void Log(Exception exception)
        {
            MessageBox.Show(exception.Message, "Exception caught");
        }
    }
}
