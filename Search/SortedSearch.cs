using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace uMethodLib.Search
{
    public class SortedSearch
    {
        #region Binary Search
        /// <summary>
        /// Performs a binary search to find the index of the target value in a sorted array within the specified range. 
        /// Binary search works by repeatedly dividing the array into halves and narrowing down the search range until the 
        /// target is found or determined to be absent.
        /// Time Complexity: O(log n)
        /// </summary>
        /// <param name="arr">The sorted array to search.</param>
        /// <param name="left">The starting index of the current search range.</param>
        /// <param name="right">The ending index of the current search range.</param>
        /// <param name="target">The value to find within the array.</param>
        /// <returns>The index of the target value if found, or -1 if the target is not in the array.</returns>
        public static int BinarySearchRecursive(IReadOnlyList<int> arr, int left, int right, int target)
        {
            while (left <= right)
            {
                int mid = left + (right - left) / 2; //Get middle index

                if (arr[mid] == target) 
                    return mid; //If value at mid is target, return index
                if (arr[mid] > target) 
                    right = mid - 1; //Update 'right' for left subarray
                else 
                    left = mid + 1; //Update 'left' for right subarray
            }
            return -1; // Target not found in the array, return -1.
        }

        /// <summary>
        /// Performs an iterative binary search to find the index of the target value in a sorted array. 
        /// Binary search iteratively divides the array into halves and narrows down the search range 
        /// until the target is found or determined to be absent. This method is the standard iterative 
        /// binary search.
        /// </summary>
        /// <param name="arr">Sorted Array to search</param>
        /// <param name="x">Target to find</param>
        /// <returns>index of target</returns>
        public static int BinarySearchIterative(int[] arr, int x)
        {
            int l = 0, h = arr.Length - 1;
            while (h - l > 1)
            {
                int mid = (h + l) / 2; //Get middle index
                if (arr[mid] < x) 
                    l = mid + 1;
                else 
                    h = mid;
            }

            if (arr[l] == x) 
                return l;
            if (arr[h] == x) 
                return h;
            return -1;
        }

        /// <summary>
        /// Searches for a target value in a sorted array within a specified range using binary search. 
        /// Binary search iteratively narrows down the search range by dividing it into halves until the 
        /// target is found or determined to be absent. This method allows searching within a specific range.
        /// </summary>
        /// <param name="arr">Sorted Array to search</param>
        /// <param name="l">Starting point</param>
        /// <param name="r">End point</param>
        /// <param name="x">Target to find</param>
        /// <returns>index of target</returns>
        public static int BinarySearchUbiquitous(int[] arr, int l, int r, int x)
        {
            while (l <= r)
            {
                int m = l + (r - l) / 2;
                if (arr[m] == x)
                    return m;
                else if (arr[m] < x)
                    l = m + 1;
                else
                    r = m - 1; 
            }
            return -1;
        }
        #endregion

        #region Ternary Search
        /// <summary>
        /// Performs a ternary search for a target value in a sorted array. Ternary search divides
        /// the array into three parts and narrows down the search range by comparing with two
        /// midpoints.
        /// Time Complexity: O(log3(n))
        /// </summary>
        /// <param name="arr">Sorted array to search.</param>
        /// <param name="target">Target to find.</param>
        /// <returns>Index of the target if found; otherwise, -1.</returns>
        public static int TernarySearch(int[] arr, int target)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left <= right)
            {
                int mid1 = left + (right - left) / 3;
                int mid2 = left + 2 * (right - left) / 3;

                if (arr[mid1] == target) 
                    return mid1;
                if (arr[mid2] == target) 
                    return mid2;

                if (target < arr[mid1])
                    right = mid1 - 1;
                else if (target > arr[mid2])
                    left = mid2 + 1;
                else
                {
                    left = mid1 + 1;
                    right = mid2 - 1;
                }
            }

            return -1;
        }
        #endregion

        #region Exponential Search
        /// <summary>
        /// Searches for a target value in a sorted array using exponential search. Exponential search 
        /// first finds a range where the target is likely to be and then performs a binary search within 
        /// that range. This method combines exponential and binary search to efficiently find the target.
        /// Time Complexity: O(log n)
        /// </summary>
        /// <param name="arr">Sorted Array to search</param>
        /// <param name="n">Places to search, I.E. Array.length</param>
        /// <param name="target">Target to find</param>
        /// <returns>index of target</returns>
        public static int ExponentialSearch(int[] arr, int n, int target)
        {
            if (arr[0] == target) 
                return 0;

            int i = 1;
            while (i < n && arr[i] <= target) 
                i *= 2;

            return ExSearch(arr, i / 2, Math.Min(i, n - 1), target);
        }

        private static int ExSearch(int[] arr, int l, int r, int x)
        {
            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if (arr[mid] == x) 
                    return mid;
                if (arr[mid] < x) 
                    l = mid + 1;
                else 
                    r = mid - 1;
            }

            return -1;
        }
        #endregion

        #region FibonacciSearch
        /// <summary>
        /// Searches for a target value in a sorted array using Fibonacci search. Fibonacci search narrows 
        /// down the search range using Fibonacci numbers and a binary search approach. This method leverages 
        /// the Fibonacci sequence to locate the target.
        /// Time Complexity: O(log n)
        /// </summary>
        /// <param name="arr">Sorted Array to search</param>
        /// <param name="target">Target to find</param>
        /// <returns>index of target</returns>
        public static int FibonacciSearch(int[] arr, int target)
        {
            int fibMMinus2 = 0; // (m-2)'th Fibonacci number
            int fibMMinus1 = 1; // (m-1)'th Fibonacci number
            int fib = fibMMinus1 + fibMMinus2; // m'th Fibonacci number

            while (fib < arr.Length)
            {
                fibMMinus2 = fibMMinus1;
                fibMMinus1 = fib;
                fib = fibMMinus1 + fibMMinus2;
            }

            int offset = -1; // Used to keep track of the eliminated range
            while (fib > 1)
            {
                int i = Math.Min(offset + fibMMinus2, arr.Length - 1);

                if (arr[i] < target)
                {
                    fib = fibMMinus1;
                    fibMMinus1 = fibMMinus2;
                    fibMMinus2 = fib - fibMMinus1;
                    offset = i;
                }
                else if (arr[i] > target)
                {
                    fib = fibMMinus2;
                    fibMMinus1 -= fibMMinus2;
                    fibMMinus2 = fib - fibMMinus1;
                }
                else
                    return i; // Target found
            }

            if (fibMMinus1 == 1 && arr[offset + 1] == target)
                return offset + 1;   

            return -1; // Target not found
        }
        #endregion

        #region InterpolationSearch
        /// <summary>
        /// Searches for a target value in a sorted array using interpolation search. Interpolation search 
        /// estimates the position of the target value in the array and narrows down the search range 
        /// iteratively. This method is based on interpolation for efficient search.
        /// Time Complexity: O(log log n) on average for uniformly distributed data. In the worst case, it can go up to O(n).
        /// </summary>
        /// <param name="arr">Sorted Array to search</param>
        /// <param name="start">Starting point</param>
        /// <param name="end">End point</param>
        /// <param name="target">Target to find</param>
        /// <returns>index of target</returns>
        public static int InterpolationSearch(int[] arr, int start, int end, int target)
        {
            while (true)
            {
                if (start > end || target < arr[start] || target > arr[end]) 
                    return -1;

                var pos = start + (end - start) / (arr[end] - arr[start]) * (target - arr[start]);

                if (arr[pos] == target) 
                    return pos;

                if (arr[pos] < target)
                {
                    start = pos + 1;
                    continue;
                }

                if (arr[pos] <= target) 
                    return -1;

                end = pos - 1;
            }
        }
        #endregion

        #region JumpSearch
        /// <summary>
        /// Searches for a target value in a sorted array using jump search. Jump search divides 
        /// the array into blocks and narrows down the search range by jumping iteratively to the 
        /// relevant block. This method employs a block-jumping strategy for efficient searching.
        /// Time Complexity: O(√n)
        /// </summary>
        /// <param name="arr">Sorted <c>Array</c> to search</param>
        /// <param name="target">Target to find</param>
        /// <returns><c>Index</c> of target <c>x</c></returns>
        public static int JumpSearch(int[] arr, int target)
        {
            int n = arr.Length, step = (int)Math.Sqrt(n), prev = 0;
            while (arr[Math.Min(step, n) - 1] < target)
            {
                prev = step;
                step += (int)Math.Sqrt(n);
                if (prev >= n) 
                    return -1;
            }
            while (arr[prev] < target)
            {
                prev++;
                if (prev == Math.Min(step, n)) 
                    return +1;
            }
            if (arr[prev] == target) 
                return prev;

            return -1;
        }
        #endregion

        #region LinearSearch
        /// <summary>
        /// Performs a basic linear search for a target value in an array. Linear search scans the 
        /// array element by element iteratively until it finds the target or determines that it's 
        /// not in the array. This is a straightforward linear search.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">Array to search</param>
        /// <param name="target">Target to find</param>
        /// <returns>index of target</returns>
        public static int LinearSearchA(int[] arr, int target)
        {
            int left;
            int le = arr.Length,
              right = le - 1;
            for (left = 0; left <= right;)
            {
                if (arr[left] == target) 
                    return left;

                if (arr[right] == target) 
                    return right;

                left++;
                right--;
            }
            return -1;
        }


        /// <summary>
        /// Performs a slightly improved linear search for a target value in an array. This version 
        /// of linear search iteratively scans the array from both ends towards the center to potentially 
        /// reduce the number of comparisons. This method is an iterative improvement of linear search.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">Array to search</param>
        /// <param name="l">Starting point</param>
        /// <param name="r">End point</param>
        /// <param name="x">Target to find</param>
        /// <returns>index of target</returns>
        public static int LinearSearchB(int[] arr, int l, int r, int x)
        {
            while (true)
            {
                if (r < 1) 
                    return -1;

                if (arr[1] == x) 
                    return 1;

                if (arr[r] == x) 
                    return r;

                l += 1;
                r -= 1;
            }
        }
        #endregion

        #region First Positive
        /// <summary>
        /// Searches for the first positive value of F(i) in a function F using binary search. It allows 
        /// you to specify a custom search range by providing the starting point (low) and the end point (high). 
        /// The method iteratively narrows down the search range to find the point where F(i) becomes positive. 
        /// This method offers versatility in range selection.
        /// Time Complexity: O(log n)
        /// </summary>
        /// <param name="arr">The sorted array to search.</param>
        /// <param name="low">Starting point</param>
        /// <param name="high">End point</param>
        /// <returns></returns>
        public static int BinarySearchUnbound(int[] arr, int low = 0, int high = int.MaxValue)
        {
            while (true)
            {
                if (high < low)
                    return -1;
                var mid = low + (high - low) / 2;
                if (arr[mid] > 0 && (mid == low || arr[mid - 1] <= 0))
                    return mid;
                if (arr[mid] <= 0)
                {
                    low = mid + 1;
                    continue;
                }
                high = mid - 1;
            }
        }
        #endregion

        #region Basic For Loop Search
        /// <summary>
        /// Basic for loop search - checks each individual index in order.
        /// Time Complexity: O(n)
        /// </summary>
        /// <param name="arr">Array to search</param>
        /// <param name="target">Target to find</param>
        /// <returns>index of target</returns>
        public static int BasicForLoopSearch(int[] arr, int target)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                    return i;
            }
            return -1;
        }
        #endregion
    }
}
