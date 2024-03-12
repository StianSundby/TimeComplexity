namespace TimeComplexity.MST
{
    internal class Kruskal
    {
        public static (Edge[] result, int e) KruskalMst(Graph graph)
        {
            var verticesCount = graph.VerticesCount;
            var result = new Edge[verticesCount];
            var i = 0;
            var e = 0;

            Array.Sort(graph.Edges, (a, b) => a.Weight.CompareTo(b.Weight));
            var subsets = new Subset[verticesCount];

            for (var v = 0; v < verticesCount; ++v)
            {
                subsets[v] = new Subset { Parent = v, Rank = 0 };
            }

            while (e < verticesCount - 1)
            {
                var nextEdge = graph.Edges[i++];
                var x = Find(subsets, nextEdge.Source);
                var y = Find(subsets, nextEdge.Destination);

                if (x == y)
                    continue;
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

            if (subsets[xroot].Rank < subsets[yroot].Rank)
            {
                subsets[xroot].Parent = yroot;
            }
            else if (subsets[xroot].Rank > subsets[yroot].Rank)
            {
                subsets[yroot].Parent = xroot;
            }
            else
            {
                subsets[yroot].Parent = xroot;
                ++subsets[xroot].Rank;
            }
        }

        public static Graph CreateKruskalGraph(int verticesCount)
        {
            var graph = new Graph
            {
                VerticesCount = verticesCount,
                Edges = new Edge[verticesCount * (verticesCount - 1) / 2]
            };

            return graph;
        }

        public struct Edge
        {
            public int Source;
            public int Destination;
            public int Weight;
        }

        public struct Graph
        {
            public int VerticesCount;
            public Edge[] Edges;
        }

        public struct Subset
        {
            public int Parent;
            public int Rank;
        }
    }
}
