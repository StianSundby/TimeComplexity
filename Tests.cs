using static uMethodLib.Search.SublistSearch;
using static uMethodLib.UtilityMethods;
using static uMethodLib.Sort.Sort;
using uMethodLib.Pathfinding;
using uMethodLib.Search;
using TimeComplexity.MST;
using TimeComplexity.Pathfinding;
using static uMethodLib.Search.StringSearch;

namespace uMethodLib
{
    internal class Tests
    {
        static readonly Random random = new();
        #region Search
        /// <summary>
        /// Performs performance tests for various search algorithms using a sorted dataset.
        /// All datasets has an increase of 1 on each index, except the dataset used for Fibonacci Search,
        /// which requires that each indices is a number from the Fibonacci sequence, in ascending order.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        /// /// <returns>The results of each test - I.E. how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformSortedSearchTests(int n)
        {
            int[] fibArray = GenerateFibonacciSortedArray(n);
            int fibTarget = fibArray[random.Next(0, n - 1)];

            int[] testArray = GenerateIntArray(n, true);
            int target = random.Next(1, n);

            var results = new Dictionary<string, TimeSpan>
            {
                ["Binary (Recursive)"] = MeasurePerformance(() => SortedSearch.BinarySearchRecursive(testArray, 0, n - 1, target)),
                ["Binary (Iterative)"] = MeasurePerformance(() => SortedSearch.BinarySearchIterative(testArray, target)),
                ["Ternary"] = MeasurePerformance(() => SortedSearch.TernarySearch(testArray, target)),
                ["Exponential"] = MeasurePerformance(() => SortedSearch.ExponentialSearch(testArray, n - 1, target)),
                ["Fibonacci"] = MeasurePerformance(() => SortedSearch.FibonacciSearch(fibArray, fibTarget)), //only works on a fibonacci sorted array.
                ["Interpolation"] = MeasurePerformance(() => SortedSearch.InterpolationSearch(testArray, 0, n - 1, target)),
                ["Jump Search"] = MeasurePerformance(() => SortedSearch.JumpSearch(testArray, target)),
                ["Linear (v1)"] = MeasurePerformance(() => SortedSearch.LinearSearchA(testArray, target)),
                ["Linear (v2)"] = MeasurePerformance(() => SortedSearch.LinearSearchB(testArray, 0, n - 1, target)),
                ["Basic For-Loop"] = MeasurePerformance(() => SortedSearch.BasicForLoopSearch(testArray, target)),
                ["Hash Based"] = MeasurePerformance(() => HashBasedSearch.HashSearch(testArray, target)),
                ["IndexOf()"] = MeasurePerformance(() => Array.IndexOf(testArray, target))
                //TODO: Maybe add Exponential Interpolation Search
            };

                return results;
            }

        /// <summary>
        /// Performs performance tests for various search algorithms using an unsorted dataset.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        /// /// <returns>The results of each test - I.E. how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformUnsortedSearchTests(int n)
        {
            int[] testArray = GenerateIntArray(n, false);
            int target = testArray[random.Next(1, n)];

            var results = new Dictionary<string, TimeSpan>
            {
                ["Basic Linear"] = MeasurePerformance(() => UnsortedSearch.BasicLinearSearch(testArray, target)),
                ["Linear"] = MeasurePerformance(() => UnsortedSearch.LinearSearch(testArray, n, target)),
                ["Front and Back"] = MeasurePerformance(() => UnsortedSearch.FrontAndBackSearch(testArray, target)),
                ["Parallel"] = MeasurePerformance(() => UnsortedSearch.ParallelSearch(testArray, target)),
                ["Hash Based"] = MeasurePerformance(() => HashBasedSearch.HashSearch(testArray, target)),
            };

            return results;
        }

        /// <summary>
        /// Performs performance tests for first positive search on an unsorted array with only 1 positive integer.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        /// /// <returns>The results of each test - I.E. how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformFirstPositiveTests(int n)
        {
            var testArray = new int[n];
            for (int i = 0; i < n; i++) testArray[i] = -i;
            testArray[random.Next(0, n - 1)] = 1;

            var linearResult = MeasurePerformance(() => UnsortedSearch.BasicFirstPositive(testArray));

            Array.Sort(testArray);
            var unboundResult = MeasurePerformance(() => SortedSearch.BinarySearchUnbound(testArray, 0, n - 1));

            var results = new Dictionary<string, TimeSpan>
            {
                ["Linear (unsorted)"] = linearResult,
                ["Binary (Unbound, sorted)"] = unboundResult
            };

            return results;
        }

        /// <summary>
        /// Performs performance tests for sublist search.
        /// Which means it checks if one list is present within another
        /// </summary>
        /// <param name="n">The size of the dataset to test (will be reduced).</param>
        /// <returns>The results of each test - I.E. how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformSublistSearchTests(int n)
        {
            var (x, y) = GenerateLargeLists(n);
            var (xNode, yNode) = (CreateSublistSearchNode(x), CreateSublistSearchNode(y));

            var results = new Dictionary<string, TimeSpan>
            {
                ["Basic Sublist Search"] = MeasurePerformance(() => BasicSublistSearch(x, y)),
                ["Adv Sublist Search"] = MeasurePerformance(() => AdvSublistSearch(xNode, yNode))
            };

            return results;
        }
        #endregion

        #region String Search
        /// <summary>
        /// Performs performance tests for various string search algorithms.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        /// <returns>The results of each test - i.e., how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformStringSearchTests(int n)
        {
            string text = GenerateRandomStringWithTarget(n, "target");
            string target = "target";

            var results = new Dictionary<string, TimeSpan>
            {
                ["Brute Force"] = MeasurePerformance(() => BruteForceSearch(text, target)),
                ["KMP"] = MeasurePerformance(() => KMPSearch(text, target)),
                ["Boyer-Moore"] = MeasurePerformance(() => BoyerMooreSearch(text, target)),
                ["Rabin-Karp"] = MeasurePerformance(() => RabinKarpSearch(text, target)),
                ["Aho-Corasick"] = MeasurePerformance(() => AhoCorasickSearch(text, target)),
            };

            return results;
        }
        #endregion

        #region Sort
        /// <summary>
        /// Performs performance tests for various sorting algorithms using unsorted datasets.
        /// CountingSort is the only one with a special dataset - all indices has a minimum value of 0 and a maximum value of 255
        /// </summary>
        /// <param name="n">The size of the dataset to test. Will be divided by 100</param>
        /// <returns>The results of each test - I.E. how long each algorithm took to complete.</returns>
        public static Dictionary<string, TimeSpan> PerformSortTests(int n)
        {
            n /= 100;
            int[] testArray = GenerateIntArray(n, false);
            int[] countTestArray = GenerateIntArray(n, false, 255);


            var results = new Dictionary<string, TimeSpan>
            {
                ["Bubble Sort"] = MeasurePerformance(() => BubbleSort(testArray)),
                ["Bidrectional Bubble Sort"] = MeasurePerformance(() => BidirectionalBubbleSort(testArray)),
                ["Bucket Sort"] = MeasurePerformance(() => BucketSort(testArray, n)),
                ["Counting Sort"] = MeasurePerformance(() => CountingSort(countTestArray)),
                ["Heap Sort"] = MeasurePerformance(() => HeapSort(testArray)),
                ["Insertion Sort"] = MeasurePerformance(() => InsertionSort(testArray)),
                ["Merge Sort"] = MeasurePerformance(() => MergeSort(testArray, 0, n - 1)),
                ["Quick Sort"] = MeasurePerformance(() => QuickSort(testArray, 0, n - 1)),
                ["Radix Sort"] = MeasurePerformance(() => RadixSort(testArray, n)),
                ["Selection Sort"] = MeasurePerformance(() => SelectionSort(testArray)),
                ["Shell Sort"] = MeasurePerformance(() => ShellSort(testArray)),
                ["Comb Sort"] = MeasurePerformance(() => CombSort(testArray)),
                ["Odd-Even Sort"] = MeasurePerformance(() => OddEvenSort(testArray)),
            };

            return results;
        }
        #endregion

        #region Pathfinding
        //public static Dictionary<string, TimeSpan> PerformPathfindingTests(int n)
        //{
        //    n /= 100;
        //    int[,] graph = GenerateRandomGraph(n, 500);
        //    int startNode = random.Next(0, n);

        //    var results = new Dictionary<string, TimeSpan>()
        //    {
        //        ["Dijkstra's Algorithm"] = MeasurePerformance(() => Dijkstra.DijkstraSearch(graph, startNode, startNode)),
        //        //TODO: A*
        //        //TODO: D*
        //        //TODO: Cycle Detection
        //        //TODO: Breadth-first
        //        //TODO: Depth-first
        //        //TODO: Jump Point
        //        //TODO: Johnson's Algorithm
        //        //TODO: Bellman-Ford
        //        //TODO: Floyd-Warshall
        //        //TODO: Yen's K
        //        //TODO: All-Pairs
        //        //TODO: Single Source
        //        //TODO: Maximum Flow
        //        //TODO: Prim MST
        //        //TODO: Kruskal MST
        //        //TODO: Random Walk
        //        //TODO: Greedy
        //    };

        //    return results;
        //}
        #endregion
    }
}