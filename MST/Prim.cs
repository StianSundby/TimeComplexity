namespace TimeComplexity.MST
{
    internal class Prim
    {
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
    }
}
