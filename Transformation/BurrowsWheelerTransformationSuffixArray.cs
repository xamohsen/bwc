using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transformation
{
    class Suffix : IComparable
    {
        public int Index { get; set; }
        public int Rank { get; set; }
        public int NextRank { get; set; }

        public int CompareTo(object _suffix)
        {
            Suffix suffix = (Suffix)_suffix;
            if (this.Rank > suffix.Rank || (this.Rank == suffix.Rank && this.NextRank > suffix.NextRank))
                return 1;
            else
                return -1;
        }
    }

    public class BurrowsWheelerTransformationSuffixArray
    {
        public int OriginalSuffixIndex { get; set; }

        public BurrowsWheelerTransformationSuffixArray()
        { }

        private Suffix[] CreateInitializedSuffixArray(ref string text)
        {
            text += (char)0;
            Suffix[] suffixes = new Suffix[text.Length];

            for (int i = 0; i < suffixes.Length; i++)
            {
                suffixes[i] = new Suffix();
                suffixes[i].Index = i;
                suffixes[i].Rank = (int)text[i];
                suffixes[i].NextRank = (i + 1) < text.Length ? (int)text[i + 1] : -1;
            }

            return suffixes;
        }

        public int[] GenerateSuffixArray(ref string text)
        {
            Suffix[] suffixes = CreateInitializedSuffixArray(ref text);

            Array.Sort<Suffix>(suffixes);

            for (int k = 2; k < suffixes.Length; k *= 2)
            {
                int curRank = 0;
                int prevRank = suffixes[0].Rank;
                suffixes[0].Rank = curRank;
                int[] indexes = new int[suffixes.Length];

                for (int i = 1; i < suffixes.Length; i++)
                {
                    if (suffixes[i].Rank == prevRank && suffixes[i].NextRank == suffixes[i - 1].NextRank)
                    {
                        suffixes[i].Rank = curRank;
                    }
                    else
                    {
                        suffixes[i].Rank = ++curRank;
                    }

                    prevRank = suffixes[i].Rank;
                    indexes[suffixes[i].Index] = i;
                }

                for (int i = 0; i < text.Length; i++)
                {
                    int nextIndex = suffixes[i].Index + k;
                    suffixes[i].NextRank = (nextIndex < text.Length) ? suffixes[indexes[nextIndex]].Rank : 0;
                }

                Array.Sort<Suffix>(suffixes);
            }

            int[] suffixArray = new int[suffixes.Length];

            int ind = 0;
            foreach (var suffix in suffixes)
            {
                suffixArray[ind++] = suffix.Index;
            }

            return suffixArray;
        }

        public string Transform(string text)
        {
            int[] suffixArray = GenerateSuffixArray(ref text);
            string transformedText = "";
            for (int i = 0; i < suffixArray.Length; i++)
            {
                if (suffixArray[i] == 0)
                    OriginalSuffixIndex = i;

                int ind = (suffixArray[i] == 0) ? (text.Length - 1) : (suffixArray[i] - 1);
                transformedText += text[ind];
            }
            return transformedText;
        }
        public string InverseTransformation(string text)
        {
            int index = text.Count() - 1;
            int[] Count = new int[256];
            int[] Sum = new int[256];
            int[] CountPrevSymbols = new int[text.Count()];
            int sum = 0, V = OriginalSuffixIndex;
            //char[] OriginalText = new char[text.Count()];
            string OriginalText = "";
            char Curnt_Char = text[V];
            for (int i = 0; i < 256; i++)
                Count[i] = Sum[i] = 0;
            for (int i = 0; i < text.Count(); i++)
            {
                CountPrevSymbols[i] = Count[(int)text[i]];
                Count[(int)text[i]]++;
            }
            for (int i = 0; i < 256; i++)
            {
                Sum[i] = sum;
                sum += Count[i];
            }
            OriginalText += Curnt_Char;
            index--;
            while (index >= 0)
            {
                //duke blue devils
                if (Count[(int)Curnt_Char] > 0) Count[(int)Curnt_Char]--;
                V = CountPrevSymbols[V] + Sum[(int)Curnt_Char];
                Curnt_Char = text[V];
                OriginalText += Curnt_Char;
                index--;
            }
            char[] arr = OriginalText.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public int[] GenerateNextArray(string transformedText)
        {
            var Count = new Dictionary<char, int>();
            var Next = new List<int>(transformedText.Count());
            var _count = 0;
            var LastDictionary = new Dictionary<char, List<int>>();
            foreach (var symbol in transformedText)
            {
                if (symbol == '\0') continue;
                if (Count.ContainsKey(symbol))
                {
                    Count[symbol]++;
                }
                else
                {
                    Count.Add(symbol, 1);
                }
            }
            foreach (var symbol in transformedText)
            {
                if (symbol == '\0') continue;
                if (LastDictionary.ContainsKey(symbol))
                {
                    LastDictionary[symbol].Add(_count++);
                }
                else
                {
                    LastDictionary.Add(symbol, new List<int>());
                    LastDictionary[symbol].Add(_count++);
                }
            }
            for (var charIndex = 1; charIndex < 256; charIndex++)
            {
                if (Count.ContainsKey((char)charIndex))
                {
                    for (var ind = 0; ind < Count[(char)charIndex]; ind++)
                    {
                        Next.Add(LastDictionary[(char)charIndex][ind]);
                    }
                }
            }

            return Next.ToArray();
        }

    }
}
