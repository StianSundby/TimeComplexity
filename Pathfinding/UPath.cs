namespace uMethodLib.Pathfinding
{
    public class UPath
    {
        #region Dijkstra
        /// <summary>
        /// Function that implements Dijkstra's single source shortest path algorithm
        /// for a graph represented using adjacency matrix representation
        /// </summary>
        /// <param name="graph">graph to search</param>
        /// <param name="src">Starting point</param>
        /// <param name="V">Number of vertices in the graph</param>
        /// <returns></returns>
        public static int[] DijkstraSearch(int[,] graph, int src, int? V)
        {
            V ??= 9;
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
                    if (!sptSet[v] && graph[u, v] != 0 &&
                        dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }
            return dist;
        }


        private static int MinDistance(IReadOnlyList<int> dist, IReadOnlyList<bool> sptSet, int V)
        {
            int min = int.MaxValue, minIndex = -1;
            for (var v = 0; v < V; v++)
            {
                if (sptSet[v] || dist[v] > min) continue;
                min = dist[v];
                minIndex = v;
            }

            return minIndex;
        }
        #endregion

        #region KruskalMst
        public struct Edge
        {
            public int Source;
            public int Destination;
            public int Weight;
        }

        public struct Graph
        {
            public int VerticesCount;
            public int EdgesCount;
            public Edge[] edge;
        }

        public struct Subset
        {
            public int Parent;
            public int Rank;
        }

        public static Graph CreateGraph(int verticesCount, int edgesCount)
        {
            var graph = new Graph
            {
                VerticesCount = verticesCount,
                EdgesCount = edgesCount
            };
            graph.edge = new Edge[graph.EdgesCount];

            return graph;
        }


        public static (Edge[] result, int e) KruskalMst(Graph graph)
        {
            var verticesCount = graph.VerticesCount;
            var result = new Edge[verticesCount];
            var i = 0;
            var e = 0;

            Array.Sort(graph.edge, (a, b) => a.Weight.CompareTo(b.Weight));
            var subsets = new Subset[verticesCount];
            for (var v = 0; v < verticesCount; ++v)
            {
                subsets[v].Parent = v;
                subsets[v].Rank = 0;
            }

            while (e < verticesCount - 1)
            {
                var nextEdge = graph.edge[i++];
                var x = Find(subsets, nextEdge.Source);
                var y = Find(subsets, nextEdge.Destination);

                if (x == y) continue;
                result[e++] = nextEdge;
                Union(subsets, x, y);
            }

            return (result, e);
        }

        private static int Find(Subset[] subsets, int i)
        {
            if (subsets[i].Parent != i)
                subsets[i].Parent = Find(subsets, subsets[i].Parent);

            return subsets[i].Parent;
        }

        private static void Union(Subset[] subsets, int x, int y)
        {
            var xroot = Find(subsets, x);
            var yroot = Find(subsets, y);

            if (subsets[xroot].Rank < subsets[yroot].Rank) subsets[xroot].Parent = yroot;
            else if (subsets[xroot].Rank > subsets[yroot].Rank) subsets[yroot].Parent = xroot;
            else
            {
                subsets[yroot].Parent = xroot;
                ++subsets[xroot].Rank;
            }
        }
        #endregion

        #region PrimMst
        public static int[] PrimMst(int[,] graph, int V)
        {
            var parent = new int[V];
            var key = new int[V];
            var mstSet = new bool[V];

            for (var i = 0; i < V; i++)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (var c = 0; c < V - 1; c++)
            {
                var u = MinKey(key, mstSet, V);
                mstSet[u] = true;
                for (var v = 0; v < V; v++)
                {
                    if (graph[u, v] == 0 || mstSet[v] || graph[u, v] >= key[v]) continue;
                    parent[v] = u;
                    key[v] = graph[u, v];
                }
            }
            return parent;
        }


        private static int MinKey(IReadOnlyList<int> key, IReadOnlyList<bool> mstSet, int V)
        {
            int min = int.MaxValue, minIndex = -1;
            for (var v = 0; v < V; v++)
            {
                if (mstSet[v] || key[v] >= min) continue;
                min = key[v];
                minIndex = v;
            }
            return minIndex;
        }
        #endregion

        //TODO: A*
        //TODO: Jump Point
        //TODO: add other pathfinding algorithms
    }
}
