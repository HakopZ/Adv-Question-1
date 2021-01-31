using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advance_Quesiton_1
{
    public class SpellCheck
    {
        public Dictionary<string, string> WordSoundexes;
        public SpellCheck(string path)
        {
            var words = File.ReadAllLines(path);
            AddCollectionToDictionary(words);  
        }
        public SpellCheck(List<string> words)
        {
            AddCollectionToDictionary(words);
        }
        public void AddCollectionToDictionary(IEnumerable<string> x)
        {

            WordSoundexes = new Dictionary<string, string>();
            foreach (var word in x)
            {
                if (word.All(char.IsLetter))
                {
                    WordSoundexes.Add(word, Soundex.GetSoundex(word));
                }
            }
        }
        public List<string> CheckWord(string word)
        {
            string soundex = Soundex.GetSoundex(word);
            var closeWords = WordSoundexes.Where(x => x.Value == soundex).Select(x => x.Key).ToList();
            if(closeWords.Count == 0)
            {
                return default;
            }
            List<(string w, int l)> levDis = new List<(string w, int l)>();
            foreach(var cword in closeWords)
            {
                levDis.Add((cword, LevenshteinDistance.Compute(cword, word)));
            }
            return levDis.OrderBy(x => x.l).Take(5).Select(x=>x.w).ToList();
        }
        
    }
}
