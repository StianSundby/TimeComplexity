namespace uMethodLib.Pathfinding
{
    public class Dijkstra
    {
        /// <summary>
        /// Function that implements Dijkstra's single source shortest path algorithm
        /// for a graph represented using adjacency matrix'
        /// </summary>
        /// <param name="graph">graph to search</param>
        /// <param name="src">Starting point</param>
        /// <param name="V">Number of vertices in the graph</param>
        /// <returns></returns>
        public static int[] DijkstraSearch(int[,] graph, int src, int? V)
        {
            V ??= 9;

            if (src < 0 || src >= V)
            {
                // Handle the case where src is outside the valid range, e.g., by returning a default result.
                return new int[(int)V]; // Or any other suitable default result.
            }

            var dist = new int[(int)V];
            var sptSet = new bool[(int)V];

            for (var i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            dist[src] = 0;

            for (var count = 0; count < V - 1; count++)
            {
                var u = MinDistance(dist, sptSet, (int)V);
                sptSet[u] = true;

                for (var v = 0; v < V; v++)
                {
                    if (!sptSet[v] && graph[u, v] != 0 && dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                    }
                }
            }

            return dist;
        }


        private static int MinDistance(IReadOnlyList<int> dist, IReadOnlyList<bool> sptSet, int V)
        {
            int min = int.MaxValue, minIndex = -1;
            for (var v = 0; v < V; v++)
            {
                if (sptSet[v] || dist[v] > min) 
                    continue;
                min = dist[v];
                minIndex = v;
            }

            return minIndex;
        }
    }
}
