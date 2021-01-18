using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advance_Quesiton_1
{
    class LevenshteinDistance
    {
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }
            for (int i = 0; i <= n; d[i, 0] = i++)
            {}

            for (int j = 0; j <= m; d[0, j] = j++)
            {}
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
    class TrieNode
    {
        private string word;
        public string Word
        {
            get
            {
                return IsWord is false ? "" : word;
            }
            set 
            {
                word = value;
            }
        }
        public char Letter { get; private set; }
        public Dictionary<char, TrieNode> Children { get; private set; }
        public bool IsWord { get; set; }

        public TrieNode(char c)
        {
            Children = new Dictionary<char, TrieNode>();
            Letter = c;
            IsWord = false;
        }
    }

    public class Trie
    {
        TrieNode root;

        public Trie()
        {
            Clear();
        }

        public void Clear()
        {
            root = new TrieNode(' ');
        }

        public void Insert(string word)
        {
            var children = root.Children;

            for (var pos = 0; pos < word.Length; pos++)
            {
                var letter = word[pos];
                TrieNode tempTrieNode;

                if (children.ContainsKey(letter))
                {
                    tempTrieNode = children[letter];
                }
                else
                {
                    tempTrieNode = new TrieNode(letter);  
                    children.Add(letter, tempTrieNode);
                }

                children = tempTrieNode.Children;

                if (pos == word.Length - 1)
                {
                    tempTrieNode.Word = word;
                    tempTrieNode.IsWord = true;
                }
            }
        }

        public bool Contains(string word)
        {
            var searchNode = SearchNode(word);
 
            if (searchNode != null && searchNode.IsWord)
            {
                return true;
            }

            return false;
        }

        private TrieNode SearchNode(string word)
        {
            var tempChildren = root.Children;
            TrieNode tempNode = null;

            foreach (var current in word)
            {
                if (tempChildren.ContainsKey(current))
                {
                    tempNode = tempChildren[current];
                    tempChildren = tempNode.Children;
                }
                else
                {
                    return null;
                }
            }

            return tempNode;
        }
        public List<string> SpellCheck(string word)
        {
            List<string> res = new List<string>();
            if(Contains(word))
            {
                res.Add(word);
                return res;
            }
            List<string> outputs = new List<string>();
            GetAllWords(root, outputs, "");
            var filter = outputs.Where(x => x.Length >= word.Length - 1 || x.Length <= word.Length + 1).ToList();
            List<(int Distance, string result)> pairs = new List<(int Distance, string result)>();
            
            foreach(var w in filter)
            {
                int d = LevenshteinDistance.Compute(word, w);
                pairs.Add((d, w));
            }

            return pairs.OrderBy(x => x.Distance).Take(5).Select(x=>x.result).ToList();

        }
        public List<string> GetAllwordsMatchingPrefix(string prefix)
        {
            var allWords = new List<string>();

            var node = SearchNode(prefix);

            GetAllWords(node, allWords, prefix);

            return allWords;
        }

        private void GetAllWords(TrieNode node, List<string> allWords, string prefix)
        {
            if (node == null)
            {
                return;
            }

            foreach ((char letter, TrieNode trieNode) in node.Children)
            {
                GetAllWords(trieNode, allWords, prefix + trieNode.Letter);
            }

            if (node.IsWord)
            {
                allWords.Add(prefix);
            }
        }

        public bool Remove(string prefix)
        {
            var foundNode = SearchNode(prefix);

            if (foundNode == null || prefix.Length == 0)
            {
                return false;
            }

            foundNode.IsWord = false;
            return true;

        }
    }
}