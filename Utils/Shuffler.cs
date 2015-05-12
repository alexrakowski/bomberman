using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Utils
{
    public static class Shuffler
    {
        private static Random rand;
        private static void InitRandom()
        {
            if (rand == null)
            {
                rand = new Random();
            }
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            InitRandom();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
