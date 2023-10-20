using static uMethodLib.Search.SublistSearch;
using static uMethodLib.UtilityMethods;
using uMethodLib.Search;
using System;

namespace uMethodLib
{
    internal class Tests
    {
        #region Sorted Search
        /// <summary>
        /// Performs performance tests for various search algorithms using a sorted dataset.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        public static Dictionary<string, TimeSpan> PerformSortedSearchTests(int n)
        {
            // Generate a random target value
            Random random = new();


            // Create a fibonacci sorted array
            int[] fibArray = GenerateFibonacciSortedArray(n);
            int fibTarget = fibArray[random.Next(0, n - 1)];

            // Create a sorted test array
            int[] testArray = GenerateArray(n, true);
            int target = random.Next(1, n);

            // Measure the performance of various search algorithms and store the results in a dictionary
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
        #endregion

        #region
        public static Dictionary<string, TimeSpan> PerformUnsortedSearchTests(int n)
        {
            Random random = new();
            int[] testArray = GenerateArray(n, false); // Create an unsorted test array
            int target = testArray[random.Next(1, n)]; // Pick a random target value

            // Measure the performance of various search algorithms and store the results in a dictionary
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
        #endregion

        #region First Positive Search
        /// <summary>
        /// Performs performance tests for first positive search on a sorted array.
        /// </summary>
        /// <param name="n">The size of the dataset to test.</param>
        public static Dictionary<string, TimeSpan> PerformFirstPositiveTests(int n)
        {
            // Create an array with negative values and a single positive value
            Random random = new();
            var testArray = new int[n];
            for (int i = 0; i < n; i++) testArray[i] = -i;
            testArray[random.Next(0, n - 1)] = 1;

            var linearResult = MeasurePerformance(() => UnsortedSearch.BasicFirstPositive(testArray));

            Array.Sort(testArray);
            var unboundResult = MeasurePerformance(() => SortedSearch.BinarySearchUnbound(testArray, 0, n - 1));

            // Measure the performance of the search algorithms and store the results in a dictionary
            var results = new Dictionary<string, TimeSpan>
            {
                ["Linear (unsorted)"] = linearResult,
                ["Binary (Unbound, sorted)"] = unboundResult
            };

            return results;
        }
        #endregion

        #region Sublist Search
        /// <summary>
        /// Performs performance tests for sublist search.
        /// </summary>
        /// <param name="n">The size of the dataset to test (will be reduced).</param>
        public static Dictionary<string, TimeSpan> PerformSublistSearchTests(int n)
        {
            var (x, y) = GenerateLargeLists(100);
            var (xNode, yNode) = (CreateSublistSearchNode(x), CreateSublistSearchNode(y));
            // Measure the performance of the sublist search algorithms and store the results in a dictionary
            var results = new Dictionary<string, TimeSpan>
            {
                ["Basic Sublist Search"] = MeasurePerformance(() => BasicSublistSearch(x, y)),
                ["Adv Sublist Search"] = MeasurePerformance(() => AdvSublistSearch(xNode, yNode))
            };

            return results;
        }
        #endregion

        //TODO: add test section for sublist search vs. the regular way of doing it
    }
}
