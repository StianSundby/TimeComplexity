using Spectre.Console;

namespace uMethodLib
{
    internal class UtilityMethods
    {
        /// <summary>
        /// Measures the performance of a given action and returns the elapsed time.
        /// </summary>
        /// <typeparam name="T">The return type of the action.</typeparam>
        /// <param name="action">The action to measure.</param>
        /// <returns>The elapsed time for the action execution.</returns>
        public static TimeSpan MeasurePerformance<T>(Func<T> action)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew(); // Start a stopwatch to measure elapsed time
            T result = action(); // Execute the action
            watch.Stop(); // Stop the stopwatch

            if (result == null) return TimeSpan.Zero; 

            if (typeof(T) == typeof(bool))
            {
                bool boolResult = (bool)(object)result; // Unbox the result as a boolean
                return boolResult ? watch.Elapsed : TimeSpan.Zero; // If the result is true, return the elapsed time; otherwise, return TimeSpan.Zero
            }
            else if (typeof(T) == typeof(int))
            {
                int intResult = (int)(object)result; // Unbox the result as an integer
                return intResult != -1 ? watch.Elapsed : TimeSpan.Zero; // If the result is not -1, return the elapsed time; otherwise, return TimeSpan.Zero
            }

            return TimeSpan.Zero; // Return TimeSpan.Zero for any other type of result
        }

        /// <summary>
        /// Generates a list of random integers with the specified size.
        /// </summary>
        /// <param name="n">The number of integers to generate.</param>
        /// <returns>A list of random integers.</returns>
        public static (List<int> x, List<int> y) GenerateLargeLists(int n)
        {
            // Generate two large lists of integers, with one common section at random indices
            Random random = new Random();
            List<int> x = new();
            List<int> commonSublist = new() { 1, 2, 3, 4, 5 };

            int commonSublistLength = commonSublist.Count;
            int xCommonSublistIndex = random.Next(0, n - commonSublistLength);
            int yCommonSublistIndex = random.Next(0, n - commonSublistLength);

            for (int i = 0; i < n; i++)
            {
                if (i >= xCommonSublistIndex && i < xCommonSublistIndex + commonSublistLength)
                    x.Add(commonSublist[i - xCommonSublistIndex]);
                else
                    x.Add(random.Next(100));
            }

            return (commonSublist, x);
        }

        /// <summary>
        /// Prints the test results to the console.
        /// </summary>
        /// <param name="results">A dictionary containing test results with algorithm names as keys and execution times as values.</param>
        public static void PrintResults(Dictionary<string, TimeSpan> results)
        {
            int nameWidth = 40;
            var sortedResults = results.OrderBy(pair => pair.Value); // Sort the results by execution time

            foreach (var result in sortedResults)
            {
                Console.Write($"{result.Key.PadRight(nameWidth)}");
                string timeString = result.Value == TimeSpan.Zero ? "Not found." : $"{result.Value.TotalMilliseconds:0.00} ms";
                Console.WriteLine($"{timeString}");
            }
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
                int redValue = (int)(255 * i / (count - 1)); // Gradually increase the red component
                int greenValue = 255 - redValue; // Gradually decrease the green component
                gradient[count - 1 - i] = new Color((byte)greenValue, (byte)redValue, 0); // Swap green and red and reverse the order
            }
            return gradient;
        }

        public static int[] GenerateArray(int length, bool sorted)
        {
            Random rnd = new();
            int[] arr = new int[length];

            if (sorted)
                for (int i = 0; i < length; i++)
                    arr[i] = i;
            else
                for (int i = 0; i < length; i++)
                    arr[i] = rnd.Next(1, length - 1);
            
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
    }
}
