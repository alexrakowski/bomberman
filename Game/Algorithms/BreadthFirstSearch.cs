using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Game.Algorithms
{
    static class BreadthFirstSearch
    {
        public static int ComputePath(List<int>[] graph, int from, int to)
        {
            BfsVertex[] vertices = new BfsVertex[graph.Length];
            for (int i = 0; i < vertices.Length; ++i)
            {
                vertices[i].Distance = -1;
            }
            vertices[from].Distance = 0;

            Queue<int> q = new Queue<int>();
            q.Enqueue(from);

            bool found = false;
            while (q.Count > 0 && !found)
            {
                int v = q.Dequeue();
                var adjacentVertices = graph[v];
                foreach (int w in adjacentVertices)
                {
                    if (vertices[w].Distance == -1)
                    {
                        //not visited
                        vertices[w].Distance = vertices[v].Distance + 1;
                        vertices[w].Previous = v;
                        if (w == to)
                        {
                            found = true;
                        }
                        q.Enqueue(w);
                    }
                }
            }
            if (!found)
                return -1;
            int next = GetNextVertice(vertices, from, to);
            return next;
        }
        private static int GetNextVertice(BfsVertex[] vertices, int from, int to)
        {
            int current, prev = to;
            do
            {
                current = prev;
                prev = vertices[current].Previous;
            } while (prev != from);
            return current;
        }
        struct BfsVertex
        {
            public int Previous;
            public int Distance;
        }
    }
}
