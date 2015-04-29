using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Utils
{
    static class Parser
    {
        public static Tuple<int, int> ParseIntTuple(string text)
        {
            int elem1, elem2;

            if (text.Length < 3)
            {
                throw new BombermanException("Could not parse int Tuple - string too short. \n" + text);
            }

            string tmp = text.Substring(1, text.Length - 2);
            int commaPosition = tmp.IndexOf(',');
            if (commaPosition == -1)
            {
                throw new BombermanException("Could not parse int Tuple - string does not contain a comma between first and last position. \n" + text);
            }
            elem1 = Int32.Parse(tmp.Substring(0, commaPosition));
            elem2 = Int32.Parse(tmp.Substring(commaPosition + 1)); 

            return new Tuple<int, int>(elem1, elem2);
        }
    }
}
