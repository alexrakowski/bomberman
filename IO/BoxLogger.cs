using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bomberman.IO
{
    class BoxLogger : ILoggable
    {
        public void Log(string message)
        {
            MessageBox.Show(message);
        }

        public void Log(Exception exception)
        {
            MessageBox.Show(exception.Message, "Exception caught");
        }
    }
}
