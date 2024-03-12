namespace uMethodLib.Search
{
    internal class StringSearch
    {
        #region Brute Force Search
        /// <summary>
        /// Brute force search for a target substring in a text.
        /// Time Complexity: O((n-m+1)m) where n is the length of the text and m is the length of the target string
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="target">The target substring to find.</param>
        /// <returns>The index of the target substring if found, otherwise -1.</returns>
        public static int BruteForceSearch(string text, string target)
        {
            int n = text.Length;
            int m = target.Length;

            for (int i = 0; i <= n - m; i++)
            {
                int j;
                for (j = 0; j < m; j++)
                { 
                    if (text[i + j] != target[j]) break; //Compare characters in the current window
                }

                if (j == m) return i; //If we reached the end of the pattern, a match is found
            }

            return -1;
        }
        #endregion

        #region Knuth-Morris-Pratt Algorithm (KMP)
        /// <summary>
        /// Knuth-Morris-Pratt (KMP) algorithm for substring search.
        /// The algorithm uses an auxiliary array (LPS array) to skip unnecessary comparisons.
        /// It efficiently handles cases where a mismatch occurs by using the LPS array to shift the pattern.
        /// Time Complexity: O(n + m) where n is the length of the text and m is the length of the pattern.
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="pattern">The pattern (substring) to find.</param>
        /// <returns>The index of the pattern if found, otherwise -1.</returns>
        public static int KMPSearch(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int[] lps = ComputeLPSArray(pattern);

            int i = 0; //index for text[]
            int j = 0; //index for pattern[]

            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    j++;
                    i++;
                }

                if (j == m) return i - j;
                else if (i < n && pattern[j] != text[i])
                {
                    if (j != 0) j = lps[j - 1];
                    else i++;
                }
            }

            return -1;
        }

        private static int[] ComputeLPSArray(string pattern)
        {
            //The Longest Prefix Suffix (LPS) array is used to avoid unnecessary character comparisons.
            //It represents the length of the longest proper prefix that is also a suffix for each substring.

            int m = pattern.Length;
            int[] lps = new int[m];
            int len = 0;
            int i = 1;

            while (i < m)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                        len = lps[len - 1];
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
        #endregion

        #region Boyer-Moore Algorithm
        /// <summary>
        /// Boyer-Moore algorithm for substring search.
        /// The algorithm uses two tables (Bad Character and Good Suffix) to skip characters efficiently.
        /// It determines the shift based on the character mismatch and selects the maximum shift value.
        /// Time Complexity: O(n/m) to O(nm) where n is the length of the text and m is the length of the pattern.
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="pattern">The pattern (substring) to find.</param>
        /// <returns>The index of the pattern if found, otherwise -1.</returns>
        public static int BoyerMooreSearch(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;
            int[] badChar = ComputeBadCharTable(pattern);
            int s = 0;

            while (s <= n - m)
            {
                int j = m - 1;
                while (j >= 0 && pattern[j] == text[s + j]) 
                    j--;
                if (j < 0) 
                    return s;
                s += Math.Max(1, j - badChar[text[s + j]]);
            }

            return -1;
        }

        public static int[] ComputeBadCharTable(string pattern)
        {
            //The Bad Character table represents the rightmost occurrence of each character in the pattern.
            //It helps to determine the shift when a mismatch occurs.

            int m = pattern.Length;
            int[] badChar = new int[256];

            for (int i = 0; i < 256; i++)
                badChar[i] = -1;

            for (int i = 0; i < m; i++)
                badChar[pattern[i]] = i;

            return badChar;
        }
        #endregion

        #region Rabin-Karp
        /// <summary>
        /// Rabin-Karp algorithm for substring search.
        /// The algorithm uses hashing to efficiently compare the pattern with substrings of the text.
        /// It calculates and compares hash values, reducing the number of character comparisons.
        /// Time Complexity: O((n-m+1)m) where n is the length of the text and m is the length of the pattern.
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="pattern">The pattern (substring) to find.</param>
        /// <returns>The index of the pattern if found, otherwise -1.</returns>
        public static int RabinKarpSearch(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;

            int patternHash = pattern.GetHashCode();

            for (int i = 0; i <= n - m; i++)
            {
                if (patternHash == text.Substring(i, m).GetHashCode() && text.Substring(i, m) == pattern)
                    return i;
            }

            return -1;
        }
        #endregion

        #region Aho-Corasick algorithm
        /// <summary>
        /// Aho-Corasick algorithm for substring search.
        /// The Aho-Corasick algorithm efficiently searches for multiple patterns simultaneously.
        /// It builds a Trie with failure links for efficient traversal and pattern matching.
        /// Time Complexity: O(n + m + z) where n is the length of the text, m is the length of the pattern,
        /// and z is the number of matches found.
        /// </summary>
        /// <param name="text">The text to search within.</param>
        /// <param name="pattern">The pattern (substring) to find.</param>
        /// <returns>The index of the pattern if found, otherwise -1.</returns>
        public static int AhoCorasickSearch(string text, string pattern)
        {
            AhoCorasickAutomaton automaton = new();
            int patternIndex = 1;

            automaton.AddPattern(pattern, patternIndex);
            automaton.BuildFailureLinks();

            List<int> matches = automaton.Search(text);

            if (matches.Count > 0)
                return matches[0];
            
            else
                return -1;
        }

        private class TrieNode
        {
            //The TrieNode class represents a node in the Trie structure used by the Aho-Corasick algorithm.
            //It contains information about children, failure links, and output links.

            public Dictionary<char, TrieNode> Children { get; }
            public TrieNode? FailureLink { get; set; }
            public List<int> Output { get; }

            public TrieNode()
            {
                Children = new Dictionary<char, TrieNode>();
                FailureLink = null;
                Output = new List<int>();
            }
        }

        private class AhoCorasickAutomaton
        {
            //The AhoCorasickAutomaton class encapsulates the Trie structure and provides methods for building
            //failure links and searching for patterns efficiently.

            private readonly TrieNode root;

            public AhoCorasickAutomaton()
            {
                root = new TrieNode();
            }

            public void AddPattern(string pattern, int patternIndex)
            {
                TrieNode currentNode = root;

                foreach (char c in pattern)
                {
                    if (!currentNode.Children.ContainsKey(c))
                        currentNode.Children[c] = new TrieNode();

                    currentNode = currentNode.Children[c];
                }

                currentNode.Output.Add(patternIndex);
            }

            public void BuildFailureLinks()
            {
                Queue<TrieNode?> queue = new();

                foreach (var child in root.Children)
                {
                    child.Value.FailureLink = root;
                    queue.Enqueue(child.Value);
                }

                while (queue.Count > 0)
                {
                    TrieNode? currentNode = queue.Dequeue();

                    if (currentNode == null)
                        continue;

                    foreach (var child in currentNode.Children)
                    {
                        char key = child.Key;
                        TrieNode? childNode = child.Value;
                        queue.Enqueue(childNode);

                        TrieNode? failureNode = currentNode.FailureLink;
                        while (failureNode != null && !failureNode.Children.ContainsKey(key))
                            failureNode = failureNode.FailureLink;

                        childNode.FailureLink = failureNode?.Children[key] ?? root;

                        if (childNode.FailureLink != null)
                            childNode.Output.AddRange(childNode.FailureLink.Output);
                    }
                }
            }

            public List<int> Search(string text)
            {
                List<int> matches = new();
                TrieNode? currentNode = root;

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];

                    while (currentNode != null && !currentNode.Children.ContainsKey(c))
                        currentNode = currentNode.FailureLink;

                    if (currentNode == null)
                        currentNode = root;
                    else
                    {
                        currentNode = currentNode.Children[c];
                        if (currentNode != null)
                        {
                            foreach (int patternIndex in currentNode.Output)
                                matches.Add(i - patternIndex);
                        }
                    }
                }

                return matches;
            }

        }
        #endregion
    }
}
