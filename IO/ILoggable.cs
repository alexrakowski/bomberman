using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.IO
{
    public interface ILoggable
    {
        void Log(string message);
        void Log(Exception exception);
    }
}
