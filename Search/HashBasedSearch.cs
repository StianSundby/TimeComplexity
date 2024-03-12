namespace uMethodLib.Search
{
    public class HashBasedSearch
    {
        /// <summary>
        /// Performs a Hash-Based search for a target value in the hash table.
        /// Typically works better on very large datasets.
        /// </summary>
        /// <param name="hashTable">Hash table mapping array elements to their indices.</param>
        /// <param name="target">Target value to find.</param>
        /// <returns>Index of the target if found, or -1 if the target is not in the hash table.</returns>
        public static int HashSearch(int[] arr, int target)
        {
            var hashTable = ConvertArrayToHashTable(arr);
            if (hashTable.ContainsKey(target)) 
                return hashTable[target];
            return -1;
        }

        /// <summary>
        /// Converts an array to a hash table where elements become keys and their indices become values.
        /// </summary>
        /// <param name="arr">Array to convert to a hash table.</param>
        /// <returns>Hash table mapping array elements to their indices.</returns>
        private static Dictionary<int, int> ConvertArrayToHashTable(int[] arr)
        {
            var hashTable = new Dictionary<int, int>();

            for (int i = 0; i < arr.Length; i++)
                if (!hashTable.ContainsKey(arr[i])) 
                    hashTable[arr[i]] = i; 

            return hashTable;
        }
    }
}
