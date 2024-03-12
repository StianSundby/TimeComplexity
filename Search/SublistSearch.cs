namespace uMethodLib.Search
{
    public class SublistSearch
    {
        /// <summary>
        /// Basic for loop search - checks each individual index in order.
        /// Time Complexity: O(m * n), where m is the length of the first list and n is the length of the second list.
        /// </summary>
        /// <param name="x">First List</param>
        /// <param name="y">Second List</param>
        /// <returns>true if first list (x) is present in second list (y)</returns>
        public static bool BasicSublistSearch(List<int>? x, List<int>? y)
        {
            if (x == null || y == null) return false;

            int j = 0;
            for (int i = 0; i < y.Count; i++)
            {
                if (y[i] == x[j])
                {
                    j++; // Move to the next element in 'x'
                    if (j == x.Count) // If all elements in 'x' have been found
                        return true; 
                }
                else j = 0; // If the current element in 'y' does not match the current element in 'x', reset the counter for 'x'
            }
            return false;
        }
        /// <summary>
        /// Attempts to find the first sublist 'x' in the second sublist 'y'. Sublist search iteratively 
        /// checks if one list is present as a contiguous subsequence in another list. This method is used 
        /// for identifying the presence of a sublist within another sublist.
        /// Time Complexity: O(m + n), where m is the length of the first list and n is the length of the second list.
        /// </summary>
        /// <param name="listOneTarget">First list</param>
        /// <param name="y">Second list</param>
        /// <returns>true if first list (x) is present in second list (y)</returns>
        public static bool AdvSublistSearch(SublistSearchNode? x, SublistSearchNode? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            var sn1 = x; //first sublist node

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
                if (sn1 == null) 
                    return true; // Sublist found in the second list
                sn1 = x; // Reset the first sublist node for the next iteration
                y = y.Next; // Move to the next node in the second list
            }
            return false;
        }

        /// <summary>
        /// Creates a linked list of SublistSearchNode from a list of integers.
        /// Time complexity: O(m), where m is the length of the list.
        /// </summary>
        /// <param name="list">The list of integers to convert into a linked list.</param>
        /// <returns>A linked list of SublistSearchNode, or null if the input list is null or empty.</returns>
        public static SublistSearchNode? CreateSublistSearchNode(List<int>? list)
        {
            if (list == null || list.Count == 0) 
                return null;

            SublistSearchNode? head = null;
            SublistSearchNode? current = null;

            foreach (int value in list) //create nodes for each value
            {
                var newNode = NewNode(value);

                if (head == null)
                {
                    head = newNode;
                    current = newNode;
                }
                else
                {
                    if (current != null)
                    {
                        current.Next = newNode;
                        current = newNode;
                    }
                }
            }

            return head;
        }

        /// <summary>
        /// Creates a new node for the Sublist Search.
        /// Time complexity: O(m), where m is the length of the list.
        /// </summary>
        /// <param name="key">The data to store in the node.</param>
        /// <returns>A new SublistSearchNode containing the specified data.</returns>
        private static SublistSearchNode NewNode(int key)
        {
            var temp = new SublistSearchNode
            {
                Data = key,
                Next = null
            };
            return temp;
        }
    }

    public class SublistSearchNode
    {
        public int Data;
        public SublistSearchNode? Next;
    }
}
