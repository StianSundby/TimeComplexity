using static uMethodLib.UtilityMethods;

namespace uMethodLib.Sort
{
    public class USort
    {
        #region BubbleSort
        public static IList<int> BubbleSort(IList<int> arr)
        {
            var n = arr.Count;
            for (var i = 0; i < n - 1; i++)
                for (var j = 0; j < n - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                    }

            return arr;
        }
        #endregion

        #region BucketSort
        public static IList<float> BucketSort(IList<float> arr, int n)
        {
            if (n <= 0) return arr;

            var buckets = new List<float>[n];
            for (var i = 0; i < n; i++)
                buckets[i] = new List<float>();

            for (var i = 0; i < n; i++)
            {
                var idx = arr[i] * n;
                buckets[(int)idx].Add(arr[i]);
            }

            for (var i = 0; i < n; i++)
                buckets[i].Sort();

            var index = 0;
            for (var i = 0; i < n; i++)
                for (var j = 0; j < buckets[i].Count; j++)
                    arr[index++] = buckets[i][j];

            return arr;
        }
        #endregion

        #region CountingSort
        public static char[] CountingSort(char[] arr)
        {
            var n = arr.Length;
            var output = new char[n];
            var count = new int[256];

            for (var i = 0; i < 256; ++i)
                count[i] = 0;

            for (var i = 0; i < n; ++i)
                ++count[arr[i]];

            for (var i = 1; i <= 255; ++i)
                count[i] += count[i - 1];

            for (var i = n - 1; i >= 0; i--)
            {
                output[count[arr[i]] - 1] = arr[i];
                --count[arr[i]];
            }

            for (var i = 0; i < n; ++i)
                arr[i] = output[i];

            return arr;
        }
        #endregion

        #region HeapSort
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

                if (l < N && arr[l] > arr[largest]) largest = l;
                if (r < N && arr[r] > arr[largest]) largest = r;
                if (largest == i) return;
                (arr[i], arr[largest]) = (arr[largest], arr[i]);

                i = largest;
            }
        }
        #endregion

        #region InsertionSort
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
        public int[] MergeSort(int[] arr, int l, int r)
        {
            if (l >= r) return arr;
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
        public static int[] QuickSort(int[] arr, int l, int r)
        {
            if (l >= r) return arr;
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
                if (arr[j] >= pivot) continue;
                i++;
                Swap(arr, i, j);
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }
        #endregion

        #region RadixSort
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
    }
}
