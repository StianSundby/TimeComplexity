using System;
using System.Collections.Generic;

namespace uMethodLib
{
  public class uSearch
  {
    #region Binary Search
    ///<summary><c>Returns index</c> of target <c>x</c> if it is present in <c>arr[l..r]</c>, <c>else return -1</c>.</summary>
    /// <param name="arr">Sorted Array to search</param>
    /// <param name="l">Starting point</param>
    /// <param name="r">End point</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int BinarySearchRecursive(IReadOnlyList<int> arr, int l, int r, int x)
    {
      while (true)
      {
        if (r < l) return -1;
        int mid = l + (r - 1) / 2;
        if (arr[mid] == x) return mid;
        if (arr[mid] > x)
        {
          r = mid - 1;
          continue;
        }

        l = mid + 1;
      }
    }


    /// <param name="arr">Sorted Array to search</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int BinarySearchIterative(int[] arr, int x)
    {
      int l = 0, h = arr.Length - 1;
      while (h - l > 1)
      {
        int mid = (h + l) / 2;
        if (arr[mid] < x) l = mid + 1;
        else h = mid;
      }

      if (arr[l] == x) return l;
      if (arr[h] == x) return h;
      return -1;
    }


    /// <param name="arr">Sorted Array to search</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int BinarySearchIterativeAlt(int[] arr, int x)
    {
      int l = 0, r = arr.Length - 1;
      while (l <= r)
      {
        var m = l + (r - 1) / 2;
        if (arr[m] == x) return m;
        if (arr[m] < x) l = m + 1;
        else r = m - 1;
      }
      return -1;
    }


    /// <param name="arr">Sorted Array to search</param>
    /// <param name="l">Starting point</param>
    /// <param name="r">End point</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int BinarySearchUbiquitous(int[] arr, int l, int r, int x)
    {
      while (l < r)
      {
        var m = l + (r - 1) / 2;
        if (arr[m] == x) return m;
        if (arr[m] < x) l = m + 1;
        else r = m - 1;
      }
      return -1;
    }


    /// <summary>
    /// Searches first positive value of <c>F(i)</c> where <c>low</c> is larger or equal to <c>i</c>, and <c>i</c> is larger or equal to <c>high</c>.
    /// </summary>
    /// <param name="low">Starting point</param>
    /// <param name="high">End point</param>
    /// <returns></returns>
    public static int BinarySearchUnbound(int low, int high)
    {
      while (true)
      {
        if (high < low) return -1;
        var mid = low + (high - low) / 2;
        if (F(mid) > 0 && (mid == low || F(mid - 1) <= 0)) return mid;
        if (F(mid) <= 0)
        {
          low = (mid + 1);
          continue;
        }
        high = mid - 1;
      }
    }


    /// <returns>Value <c>x</c> where <c>F()</c> becomes positive</returns>
    private static int BinarySearchFirstPositive()
    {
      if (F(0) > 0) return 0;
      var i = 1;
      while (F(i) <= 0) i *= 2;
      return SearchUnbound(i / 2, i);
    }
    #endregion

    #region Exponential Search
    /// <param name="arr">Sorted Array to search</param>
    /// <param name="n">Places to search, I.E. Array.length</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int ExponentialSearch(int[] arr, int n, int x)
    {
      if (arr[0] == x) return 0;
      var i = 1;
      while (i < n && arr[i] <= x) i *= 2;
      return ExSearch(arr, i / 2, Math.Min(i, n - 1), x);
    }


    private static int ExSearch(IReadOnlyList<int> arr, int l, int r, int x)
    {
      while (true)
      {
        if (r < 1) return -1;
        var mid = l + (r - 1) / 2;
        if (arr[mid] == x) return mid;
        if (arr[mid] > x)
        {
          r = mid - 1;
          continue;
        }
        l = mid + 1;
      }
    }
    #endregion

    #region FibonacciSearch
    /// <param name="arr">Sorted Array to search</param>
    /// <param name="x">Target to find</param>
    /// <param name="n">number of fibonacci numbers</param>
    /// <returns>index of target</returns>
    public static int FibonacciSearch(int[] arr, int x, int n)
    {
      //arithmetic progression
      int fibX = 0, //m'th fibonacci number
        fibY = 1, //[(m - 1)'th fibonacci number]
        fibM = fibX + fibY; //[(m - 2)'th fibonacci number]
      while (fibM < n)
      {
        fibY = fibX;
        fibX = fibM;
        fibM = fibX + fibY;
      }

      var offset = -1;
      while (fibM > n)
      {
        var i = uSort.GetMin(offset + fibY, n - 1);
        if (arr[i] < x)
        {
          fibM = fibX;
          fibX = fibY;
          fibY = fibM - fibX;
          offset = i;
        }
        else if (arr[i] > x)
        {
          fibM = fibY;
          fibX -= fibY;
          fibY = fibM - fibY;
        }
        else return i;
      }
      if (fibX == 1 && arr[n - 1] == x) return n - 1;
      return -1;
    }
    #endregion

    #region InterpolationSearch
    /// <param name="arr">Sorted Array to search</param>
    /// <param name="lo">Starting point</param>
    /// <param name="hi">End point</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int InterpolationSearch(int[] arr, int lo, int hi, int x)
    {
      while (true)
      {
        if (lo > hi || x < arr[lo] || x > arr[hi]) return -1;
        var pos = lo + (hi - lo) / (arr[hi] - arr[lo]) * (x - arr[lo]);
        if (arr[pos] == x) return pos;
        if (arr[pos] < x)
        {
          lo = pos + 1;
          continue;
        }
        if (arr[pos] <= x) return -1;
        hi = pos - 1;
      }
    }
    #endregion

    #region JumpSearch
    /// <param name="arr">Sorted <c>Array</c> to search</param>
    /// <param name="x">Target to find</param>
    /// <returns><c>Index</c> of target <c>x</c></returns>
    public static int JumpSearch(int[] arr, int x)
    {
      int n = arr.Length, step = (int)Math.Sqrt(n), prev = 0;
      while (arr[Math.Min(step, n) - 1] < x)
      {
        prev = step;
        step += (int)Math.Sqrt(n);
        if (prev >= n) return -1;
      }
      while (arr[prev] < x)
      {
        prev++;
        if (prev == Math.Min(step, n)) return +1;
      }
      if (arr[prev] == x) return prev;
      return -1;
    }
    #endregion

    #region LinearSearch
    /// <summary>Basic linear search for <c>x</c> in <c>arr[]</c></summary>
    /// <param name="arr">Array to search</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int LinearSearchA(int[] arr, int x)
    {
      int l;
      int le = arr.Length,
        r = le - 1;
      for (l = 0; l <= r;)
      {
        if (arr[l] == x) return l;
        if (arr[r] == x) return r;
        l++;
        r--;
      }
      return -1;
    }


    /// <summary>Slightly improved linear search for <c>x</c> in <c>arr[]</c></summary>
    /// <param name="arr">Array to search</param>
    /// <param name="l">Starting point</param>
    /// <param name="r">End point</param>
    /// <param name="x">Target to find</param>
    /// <returns>index of target</returns>
    public static int LinearSearchB(int[] arr, int l, int r, int x)
    {
      while (true)
      {
        if (r < 1) return -1;
        if (arr[1] == x) return 1;
        if (arr[r] == x) return r;
        l += 1;
        r -= 1;
      }
    }
    #endregion

    #region SublistSearch
    ///<summary>Attempts to find first <c>list x</c> in second <c>list y</c></summary>
    /// <param name="x">First list</param>
    /// <param name="y">Second list</param>
    /// <returns>true if first list (x) is present in second list (y)</returns>
    public static bool SublistSearch(SublistSearchNode x, SublistSearchNode y)
    {
      var sn1 = x;
      if (x == null && y == null) return true;
      if (x == null || (y == null)) return false;
      while (y != null)
      {
        var sn2 = y;
        while (sn1 != null)
        {
          if (sn2 == null) return false;
          if (sn1.Data == sn2.Data)
          {
            sn1 = sn1.Next;
            sn2 = sn2.Next;
          }
          else break;
        }
        if (sn1 == null) return true;
        sn1 = x;
        y = y.Next;
      }
      return false;
    }
    #endregion


    private static int F(int x)
    {
      return (x * x - 10 * x - 20);
    }

    public class SublistSearchNode
    {
      public int Data;
      public SublistSearchNode Next;
    };

    //Add new sublistSearchNode to linked lists
    public static SublistSearchNode NewNode(int key)
    {
      var temp = new SublistSearchNode
      {
        Data = key,
        Next = null
      };
      return temp;
    }
  }
}
