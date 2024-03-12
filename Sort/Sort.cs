using static uMethodLib.UtilityMethods;

namespace uMethodLib.Sort
{
    public class Sort
    {
        #region BubbleSort
        /// <summary>
        /// Sorts a list of integers using the Bubble Sort algorithm.
        /// Bubble Sort repeatedly steps through the list, compares adjacent elements, and swaps them if they are in the wrong order.
        /// It continues to do this until no more swaps are needed, indicating that the list is sorted.
        /// Time Complexity: O(n^2) in the worst and average cases, O(n) in the best case (when the list is already sorted).
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] BubbleSort(int[] array)
        {
            int n = array.Length;
            bool swapped;

            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        (array[i], array[i - 1]) = (array[i - 1], array[i]);
                        swapped = true;
                    }
                }
            } while (swapped);

            return array;
        }

        #endregion

        #region BucketSort
        /// <summary>
        /// Sorts a list of integers using the Bucket Sort algorithm.
        /// Bucket Sort divides the input list into a number of "buckets" and sorts each bucket individually, often using another sorting algorithm.
        /// It then concatenates the sorted buckets to obtain the final sorted list.
        /// Time Complexity: O(n^2) in the worst case, O(n + n^2/k + k) in the average case, where k is the number of buckets.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <param name="n">The number of buckets to use for sorting.</param>
        /// <returns>The sorted list of integers.</returns>
        public static IList<int> BucketSort(IList<int> arr, int n)
        {
            n = CalculateOptimalBucketCount(n);
            if (n <= 0) 
                return arr;

            int minValue = arr.Min();
            int maxValue = arr.Max();

            int range = maxValue - minValue + 1;

            var buckets = new List<int>[n];
            for (var i = 0; i < n; i++)
                buckets[i] = new List<int>();

            double bucketWidth = (double)range / n;

            for (var i = 0; i < n; i++)
            {
                int bucketIndex = (int)Math.Floor((arr[i] - minValue) / bucketWidth);
                bucketIndex = Math.Min(bucketIndex, n - 1);
                buckets[bucketIndex].Add(arr[i]);
            }

            for (var i = 0; i < n; i++)
                buckets[i].Sort();

            var index = 0;
            for (var i = 0; i < n; i++)
                foreach (var item in buckets[i])
                    arr[index++] = item;

            return arr;
        }

        public static int CalculateOptimalBucketCount(int n)
        {
            const double k = 1.5;
            var sqrtN = Math.Sqrt(n);
            var bc = (int)(sqrtN * k);
            bc = Math.Max(bc, 1);
            return bc;
        }

        #endregion

        #region CountingSort
        /// <summary>
        /// Sorts a list of integers using the Counting Sort algorithm.
        /// Counting Sort counts the occurrences of each element in the array and then reconstructs the sorted array.
        /// Time Complexity: O(n + k), where n is the number of elements in the array and k is the range of input.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] CountingSort(int[] arr)
        {
            int n = arr.Length;
            int outputMax = 255;

            int[] output = new int[n];
            Array.Copy(arr, output, n);

            int[] count = new int[outputMax + 1];

            for (int i = 0; i < n; ++i) // Count the occurrences of each element in the input array
            {
                if (output[i] < 0)
                    output[i] = 0;
                else if (output[i] > outputMax)
                    output[i] = outputMax;

                ++count[output[i]];
            }

            for (int i = 1; i <= outputMax; ++i) // Calculate cumulative counts
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--) // Build the output array
            {
                arr[count[output[i]] - 1] = output[i];
                --count[output[i]];
            }

            return arr;
        }
        #endregion

        #region HeapSort
        /// <summary>
        /// Sorts a list of integers using the Heap Sort algorithm.
        /// Heap Sort uses a binary heap data structure to sort the elements in ascending order.
        /// Time Complexity: O(n * log(n)) in all cases.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] HeapSort(int[] arr)
        {
            var N = arr.Length;
            for (var i = N / 2 - 1; i >= 0; i--)
                Heap(arr, N, i);

            for (var i = N - 1; i > 0; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]);
                Heap(arr, i, 0);
            }

            return arr;
        }

        private static void Heap(IList<int> arr, int N, int i)
        {
            while (true)
            {
                var largest = i;
                var l = 2 * i + 1;
                var r = 2 * i + 2;

                if (l < N && arr[l] > arr[largest]) 
                    largest = l;
                if (r < N && arr[r] > arr[largest]) 
                    largest = r;
                if (largest == i) 
                    return;

                (arr[i], arr[largest]) = (arr[largest], arr[i]);

                i = largest;
            }
        }
        #endregion

        #region InsertionSort
        /// <summary>
        /// Sorts a list of integers using the Insertion Sort algorithm.
        /// Insertion Sort builds the final sorted array one item at a time.
        /// Time Complexity: O(n^2) in the worst case and average case, O(n) in the best case (when the list is already sorted).
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] InsertionSort(int[] arr)
        {
            var n = arr.Length;
            for (var i = 1; i < n; ++i)
            {
                var key = arr[i];
                var j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j -= 1;
                }
                arr[j + 1] = key;
            }

            return arr;
        }
        #endregion

        #region MergeSort
        /// <summary>
        /// Sorts a list of integers using the Merge Sort algorithm.
        /// Merge Sort divides the array into smaller, sorted arrays, and then merges them back into a single, sorted array.
        /// Time Complexity: O(n * log(n)) in all cases.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] MergeSort(int[] arr, int l, int r)
        {
            if (l >= r) 
                return arr;
            var m = l + (r - l) / 2;
            MergeSort(arr, l, m);
            MergeSort(arr, m + 1, r);
            Merge(arr, l, m, r);

            return arr;
        }


        private static void Merge(IList<int> arr, int l, int m, int r)
        {
            var n1 = m - l + 1;
            var n2 = r - m;
            var L = new int[n1];
            var R = new int[n2];
            int i, j;

            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            i = 0;
            j = 0;
            var k = l;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
        #endregion

        #region QuickSort
        /// <summary>
        /// Sorts a list of integers using the Quick Sort algorithm.
        /// Quick Sort uses a divide-and-conquer strategy to sort the elements.
        /// Time Complexity: O(n^2) in the worst case (rare), O(n * log(n)) in the average and best cases.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <param name="l">The first element to check. Will check all indices between this and <c>r</c></param>
        /// <param name="r">The last element to check. Will check all indices between this and <c>l</c></param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] QuickSort(int[] arr, int l, int r)
        {
            if (l >= r) 
                return arr;
            var pi = Partition(arr, l, r);
            QuickSort(arr, l, pi - 1);
            QuickSort(arr, pi + 1, r);

            return arr;
        }
        private static void Swap(IList<int> arr, int i, int j)
        {
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }

        private static int Partition(IList<int> arr, int low, int high)
        {
            var pivot = arr[high];
            var i = low - 1;

            for (var j = low; j <= high - 1; j++)
            {
                if (arr[j] >= pivot) 
                    continue;
                i++;
                Swap(arr, i, j);
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }
        #endregion

        #region RadixSort
        /// <summary>
        /// Sorts a list of integers using the Radix Sort algorithm.
        /// Radix Sort is a non-comparative sorting algorithm that works by distributing elements into 10 buckets (0 to 9) based on their digits from right to left.
        /// It repeats this process for each digit, resulting in a sorted array.
        /// Time Complexity: O(n * k), where n is the number of elements and k is the number of digits in the maximum element.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <param name="n">The number of elements in the list.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] RadixSort(int[] arr, int n)
        {
            var m = GetMax(arr, n);
            for (var exp = 1; m / exp > 0; exp *= 10)
                CountSort(arr, n, exp);
            return arr;
        }


        private static void CountSort(IList<int> arr, int n, int exp)
        {
            var output = new int[n];
            var count = new int[10];
            int i;

            for (i = 0; i < 10; i++) count[i] = 0;
            for (i = 0; i < n; i++) count[arr[i] / exp % 10]++;
            for (i = 1; i < 10; i++) count[i] += count[i - 1];
            for (i = n - 1; i >= 0; i--)
            {
                output[count[arr[i] / exp % 10] - 1] = arr[i];
                count[arr[i] / exp % 10]--;
            }

            for (i = 0; i < n; i++)
                arr[i] = output[i];
        }
        #endregion

        #region SelectionSort
        /// <summary>
        /// Sorts a list of integers using the Selection Sort algorithm.
        /// Selection Sort repeatedly selects the minimum element from the unsorted part of the array and moves it to the beginning.
        /// Time Complexity: O(n^2) in all cases.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static IList<int> SelectionSort(IList<int> arr)
        {
            var n = arr.Count;
            for (var i = 0; i < n - 1; i++)
            {
                var min_idx = i;
                for (var j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                        min_idx = j;

                (arr[min_idx], arr[i]) = (arr[i], arr[min_idx]);
            }

            return arr;
        }
        #endregion

        #region Shell Sort
        /// <summary>
        /// Sorts a list of integers using the Shell Sort algorithm.
        /// Shell Sort is a variation of insertion sort that works by sorting elements that are distant apart first and then progressively reducing the gap between them.
        /// Time Complexity: Depends on the chosen gap sequence. Worst known time complexity is O(n^(4/3)), but it depends on the gap sequence.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] ShellSort(int[] arr)
        {
            int n = arr.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = arr[i];
                    int j;
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                        arr[j] = arr[j - gap];

                    arr[j] = temp;
                }
            }

            return arr;
        }
        #endregion

        #region Comb Sort
        /// <summary>
        /// Sorts a list of integers using the Comb Sort algorithm.
        /// Comb Sort is an improvement over Bubble Sort and works by comparing elements that are far apart, gradually reducing the gap between elements.
        /// Time Complexity: O(n^2) in the worst case, but it's generally more efficient than Bubble Sort.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] CombSort(int[] arr)
        {
            int n = arr.Length;
            int gap = n;
            bool swapped = true;

            while (gap > 1 || swapped)
            {
                gap = GetNextGap(gap);
                swapped = false;

                for (int i = 0; i < n - gap; i++)
                {
                    if (arr[i] > arr[i + gap])
                    {
                        (arr[i + gap], arr[i]) = (arr[i], arr[i + gap]);
                        swapped = true;
                    }
                }
            }

            return arr;
        }

        private static int GetNextGap(int gap)
        {
            gap = gap * 10 / 13;
            if (gap < 1)
                return 1;

            return gap;
        }
        #endregion

        #region Bidirectional Bubble Sort
        /// <summary>
        /// Sorts a list of integers using the Bidirectional Bubble Sort algorithm, also known as Cocktail Shaker Sort.
        /// Bidirectional Bubble Sort is a variation of Bubble Sort where elements are sorted in both directions, which can improve performance in some cases.
        /// Time Complexity: O(n^2) in the worst case, but it performs better than standard Bubble Sort in some scenarios.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] BidirectionalBubbleSort(int[] arr)
        {
            int n = arr.Length;
            bool swapped = true;
            int start = 0;
            int end = n - 1;

            while (swapped)
            {
                swapped = false;

                for (int i = start; i < end; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        (arr[i + 1], arr[i]) = (arr[i], arr[i + 1]);
                        swapped = true;
                    }
                }

                if (!swapped)
                {
                    break;
                }

                swapped = false;
                end--;

                for (int i = end; i >= start; i--)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        (arr[i + 1], arr[i]) = (arr[i], arr[i + 1]);
                        swapped = true;
                    }
                }

                start++;
            }

            return arr;
        }
        #endregion

        #region Odd-Even Sort
        /// <summary>
        /// Sorts a list of integers using the Odd-Even Sort algorithm, also known as Brick Sort.
        /// Odd-Even Sort compares and swaps elements at odd and even positions until the list is sorted.
        /// Time Complexity: O(n^2) in the worst case, making it less efficient than many other sorting algorithms.
        /// </summary>
        /// <param name="arr">The list of integers to be sorted.</param>
        /// <returns>The sorted list of integers.</returns>
        public static int[] OddEvenSort(int[] arr)
        {
            int n = arr.Length;
            bool sorted = false;

            while (!sorted)
            {
                sorted = true;

                for (int i = 1; i <= n - 2; i += 2)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        (arr[i + 1], arr[i]) = (arr[i], arr[i + 1]);
                        sorted = false;
                    }
                }

                for (int i = 0; i <= n - 2; i += 2)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        (arr[i + 1], arr[i]) = (arr[i], arr[i + 1]);
                        sorted = false;
                    }
                }
            }

            return arr;
        }
        #endregion
    }
}