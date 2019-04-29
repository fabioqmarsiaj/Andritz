using System;
using System.Collections.Generic;

namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        public Graph(IEnumerable<ILink<T>> links)
        {

        }

        public IObservable<IEnumerable<T>> RoutesBetween(T source, T target)
        {



            throw new NotImplementedException();
        }
    }

    class Vertex
    {
        public String name;
        public int status;
        public int predecessor;
        public int pathLength;

        public Vertex(String name)
        {
            this.name = name;
        }
    }

    class DirectedWeightedGraph
    {
        public readonly int MAX_VERTICES = 30;

        int n;
        int e;
        int[,] adj;
        Vertex[] vertexList;

        private readonly int TEMPORARY = 1;
        private readonly int PERMANENT = 2;
        private readonly int NIL = -1;
        private readonly int INFINITY = 99999;

        public DirectedWeightedGraph()
        {
            adj = new int[MAX_VERTICES, MAX_VERTICES];
            vertexList = new Vertex[MAX_VERTICES];
        }

        private void Dijkstra(int s)
        {
            int v, c;

            for (v = 0; v < n; v++)
            {
                vertexList[v].status = TEMPORARY;
                vertexList[v].pathLength = INFINITY;
                vertexList[v].predecessor = NIL;
            }

            vertexList[s].pathLength = 0;

            while (true)
            {
                c = TempVertexMinPL();

                if(c == NIL)
                {
                    return;
                }
                vertexList[c] = PERMANENT;

                for (v = 0; v < n; v++)
                {
                    if (isAdjacent(c,v) && vertexList[v].status == TEMPORARY)
                    {
                        if (vertexList[c].pathLength + adj[c,v] < vertexList[v].pathLength)
                        {
                            vertexList[v].predecessor = c;
                            vertexList[v].pathLength = vertexList[c].pathLength + adj[c, v];
                        }

                    }
                }
            }
        }

        private int TempVertexMinPL()
        {
            int min = INFINITY;
            int x = NIL;
            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == TEMPORARY && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x;
        }

        public void FindPaths(String source)
        {
            int s = GetIndex(source);

            Dijkstra(s);

            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].pathLength == INFINITY)
                {
                    
                }
                else
                {
                    FindPath(s, v);
                }
            }
        }

        private void FindPath(int s, int v)
        {
            int u, i;
            int[] path = new int[n];
            int sd = 0;
            int count = 0;

            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].predecessor;
                sd += adj[u, v];
                v = u;
            }

            count++;
            path[count] = s;
            
            //AQUI MOSTRARIA O MENOR CAMINHO
        }

        private int GetIndex(String s)
        {
            for (int i = 0; i < n; i++)
            {
                if (s.Equals(vertexList[i].name))
                {
                    return i;
                }
            }
            throw new System.InvalidOperationException("Invalid vertex.");
        }

    }

}
