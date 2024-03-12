using Spectre.Console;
using System.Text;

namespace uMethodLib
{
    internal class UtilityMethods
    {
        static readonly Random random = new();

        /// <summary>
        /// Measures the performance of a given action and returns the elapsed time.
        /// </summary>
        /// <typeparam name="T">The return type of the action.</typeparam>
        /// <param name="action">The action to measure.</param>
        /// <returns>The elapsed time for the action execution.</returns>
        public static TimeSpan MeasurePerformance<T>(Func<T> action)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            T result = action();
            watch.Stop();

            if (result == null) 
                return TimeSpan.Zero;

            if (typeof(T) == typeof(bool))
            {
                bool boolResult = (bool)(object)result;
                return boolResult ? watch.Elapsed : TimeSpan.Zero;
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(int[]))
            {
                if (typeof(T) == typeof(int))
                {
                    int intResult = (int)(object)result;
                    return intResult != -1 ? watch.Elapsed : TimeSpan.Zero;
                }
                else
                {
                    var time = watch.Elapsed;
                    var arrayResult = (int[])(object)result;
                    var sorted = IsSorted(arrayResult);
                    return sorted ? time : TimeSpan.Zero;
                }
            }
            else if (typeof(T) == typeof(IList<int>))
            {
                var time = watch.Elapsed;
                var arrayResult = (IList<int>)(object)result;
                var sorted = IsSorted(arrayResult);
                return sorted ? time : TimeSpan.Zero;
            }

            return TimeSpan.Zero;
        }


        /// <summary>
        /// Generates a list of random integers with the specified size.
        /// </summary>
        /// <param name="n">The number of integers to generate.</param>
        /// <returns>A list of random integers.</returns>
        public static (List<int> x, List<int> y) GenerateLargeLists(int n)
        {
            List<int> x = new();
            List<int> commonSublist = new() { 1, 2, 3, 4, 5 };

            int commonSublistLength = commonSublist.Count;
            int xCommonSublistIndex = random.Next(0, n - commonSublistLength);

            for (int i = 0; i < n; i++)
            {
                if (i >= xCommonSublistIndex && i < xCommonSublistIndex + commonSublistLength)
                    x.Add(commonSublist[i - xCommonSublistIndex]);
                else
                    x.Add(random.Next(100));
            }

            return (commonSublist, x);
        }


        public static int GetMin(int x, int y)
        {
            return x <= y ? x : y;
        }


        public static int GetMax(int[] arr, int n)
        {
            var mx = arr[0];
            for (var i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        }

        public static Color[] GenerateGradient(int count)
        {
            var gradient = new Color[count];
            for (int i = 0; i < count; i++)
            {
                int redValue = 255 * i / (count - 1); // Gradually increase the red component
                int greenValue = 255 - redValue; // Gradually decrease the green component
                gradient[count - 1 - i] = new Color((byte)greenValue, (byte)redValue, 0); // Swap green and red and reverse the order
            }
            return gradient;
        }

        public static int[] GenerateIntArray(int length, bool sorted, int maxVal = 0)
        {
            int[] arr = new int[length];

            if (sorted)
                for (int i = 0; i < length; i++)
                    arr[i] = i;
            else
                if (maxVal == 0)
                    for (int i = 0; i < length; i++)
                        arr[i] = random.Next(1, length * 10);
                else
                    for (int i = 0; i < length; i++)
                        arr[i] = random.Next(0, maxVal + 1);

            return arr;
        }

        public static float[] GenerateFloatArray(int length, bool sorted)
        {
            float[] arr = new float[length];
            if (sorted)
                for (int i = 0; i < length; i++)
                    arr[i] = (float)i;
            else
                for (int i = 0; i < length; i++)
                    arr[i] = (float)random.NextDouble() * (length - 1);

            return arr;
        }

        public static int[] GenerateFibonacciSortedArray(int length)
        {
            int[] arr = new int[length];
            arr[0] = 0;
            if (length > 1)
            {
                arr[1] = 1;
                for (int i = 2; i < length; i++)
                    arr[i] = arr[i - 1] + arr[i - 2];
            }

            Array.Sort(arr);
            return arr;
        }

        public static int[,] GenerateRandomGraph(int nodeCount, int maxEdgeWeight)
        {
            int[,] graph = new int[nodeCount, nodeCount];

            for(int i = 0; i < nodeCount; i++) 
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    if (i == j)
                        graph[i, j] = 0;
                    else
                        graph[i, j] = random.Next(1, maxEdgeWeight + 1);
                }
            }
            return graph;
        }

        public static string GenerateRandomStringWithTarget(int n, string target)
        {
            StringBuilder stringBuilder = new(n);
            int targetPosition = random.Next(0, n - target.Length);

            for (int i = 0; i < targetPosition; i++)
            {
                char randomChar = (char)random.Next('a', 'z' + 1);
                stringBuilder.Append(randomChar);
            }

            stringBuilder.Append(target);

            for (int i = targetPosition + target.Length; i < n; i++)
            {
                char randomChar = (char)random.Next('a', 'z' + 1);
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }

        public static string GenerateRandomSubstring(string text, int substringLength)
        {
            int textLength = text.Length;

            if (substringLength > textLength)
                throw new ArgumentException("Substring length cannot exceed the length of the text.");

            int startIndex = random.Next(0, textLength - substringLength + 1);
            return text.Substring(startIndex, substringLength);
        }

        /// <summary>
        /// Verify that a list of type integer is sorted in ascending order.
        /// </summary>
        /// <param name="arr">Collection to verify</param>
        /// <returns></returns>
        private static bool IsSorted(IList<int> arr)
        {
            for (int i = 1; i < arr.Count; i++)
                if (arr[i] < arr[i - 1]) 
                    return false;
            return true;
        }
    }
}
