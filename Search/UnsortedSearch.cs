namespace uMethodLib.Search
{
    internal class UnsortedSearch
    {
        #region Linear Search
        /// <summary>
        /// Performs a linear search for a target value in an unsorted array.
        /// Linear search scans the array element by element iteratively until it finds the target.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">Array to search.</param>
        /// <param name="target">Target to find.</param>
        /// <returns>Index of the target if found, or -1 if the target is not in the array.</returns>
        public static int BasicLinearSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == target)
                    return i;
            return -1;
        }

        /// <summary>
        /// Performs a linear search for a target value in an unsorted array.
        /// This method uses a modification to check the last element first, potentially improving search time.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">Array to search.</param>
        /// <param name="n">Number of elements in the array.</param>
        /// <param name="x">Target to find.</param>
        /// <returns>Index of the target if found, or -1 if the target is not in the array.</returns>
        public static int LinearSearch(int[] arr, int n, int x)
        {
            if (arr[n - 1] == x) 
                return arr[n - 1];

            int backup = arr[n - 1];
            arr[n - 1] = x;

            for (int i = 0; ; i++)
            {
                if (arr[i] == x)
                {
                    arr[n - 1] = backup;
                    if (i < n - 1) 
                        return i;
                    return -1;
                }
            }
        }
        #endregion

        #region Front and Back Search
        /// <summary>
        /// Performs a search for a target value in an unsorted array by checking both the front and back of the array.
        /// Time Complexity: O(n/2) in the worst case, which simplifies to O(n).
        /// </summary>
        /// <param name="arr">Array to search.</param>
        /// <param name="target">Target to find.</param>
        /// <returns>Index of the target if found, or -1 if the target is not in the array.</returns>
        public static int FrontAndBackSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length / 2; i++)
                if (arr[i] == target) 
                    return i;
            
            for (int i = arr.Length - 1; i >= arr.Length / 2; i--)
                if (arr[i] == target) 
                    return i;
            
            return -1;
        }
        #endregion

        #region Parallel Search
        /// <summary>
        /// Performs a parallel search for a target value in an unsorted array. 
        /// It divides the array into chunks and uses PLINQ to search in parallel.
        /// Typically works better on very large datasets.
        /// Time Complexity: O(n/p), where n is the array length and p is the number of available processors. 
        /// In practice, it can significantly improve search performance for very large datasets.
        /// </summary>
        /// <param name="arr">Array to search.</param>
        /// <param name="target">Target to find.</param>
        /// <returns>Index of the target if found, or -1 if the target is not in the array.</returns>
        public static int ParallelSearch(int[] arr, int target)
        {
            int chunkSize = arr.Length / Environment.ProcessorCount;

            return ParallelEnumerable.Range(0, Environment.ProcessorCount)
                .Select(i =>
                {
                    int start = i * chunkSize;
                    int end = (i == Environment.ProcessorCount - 1) ? arr.Length : (i + 1) * chunkSize;

                    for (int j = start; j < end; j++)
                        if (arr[j] == target) 
                            return j;
                    
                    return -1;
                })
                .FirstOrDefault(index => index != -1); // Find the first non-negative index
        }
        #endregion

        #region First Positive
        /// <summary>
        /// Searches for the first positive value of a function F using binary search. It starts from index 0 
        /// and incrementally explores the function values until the first positive value is found. This method 
        /// is designed for simplicity and common use cases.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">The sorted array to search.</param>
        /// <returns>Value <c>x</c> where <c>F()</c> becomes positive</returns>
        public static int BasicFirstPositive(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] > 0)
                    return i;
            return -1;
        }
        #endregion
    }
}
