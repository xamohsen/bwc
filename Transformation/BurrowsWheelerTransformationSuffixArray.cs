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
            if (this.Rank == suffix.Rank){
                if(this.NextRank < suffix.NextRank)return -1;
                if (this.NextRank > suffix.NextRank) return 1;
                return 0;
            }
            if(this.Rank < suffix.Rank)
                return -1;
            if (this.Rank > suffix.Rank)
                return 1;
            else
                return 0;
        }
    }

    public class BurrowsWheelerTransformationSuffixArray
    {
        public int OriginalSuffixIndex { get; set; }
        public BurrowsWheelerTransformationSuffixArray()
        { }
        private Suffix[] CreateInitializedSuffixArray(byte[] text) //O(n)
        {
            int Length = text.Length;
            Suffix[] suffixes = new Suffix[Length];
            for (int i = 0; i < suffixes.Length; i++) //O(n)
            {
                suffixes[i] = new Suffix();
                suffixes[i].Index = i;
                suffixes[i].Rank = (int)(text[i]);
                suffixes[i].NextRank = (i + 1) < Length ? (int)(text[i + 1]) : -1;
                                     //((i+1) < n)? (txt[i + 1] - 'a'): -1;
            }
            return suffixes;
        }

        public int[] GenerateSuffixArray(byte[] text) //O(n Log n)
        {
            Suffix[] suffixes = CreateInitializedSuffixArray(text); //O(n)
            
            Array.Sort<Suffix>(suffixes);

            //CountSort(suffixes, false); //O(n)
            //CountSort(suffixes, true); //O(n)

            int Length = suffixes.Length;
            int [] indexes = new int[suffixes.Length];
            for (int k = 4; k < 2 * Length; k *= 2) //O(Log n) ----- O(n Log n)
            {
                int curRank = 0;
                int prevRank = suffixes[0].Rank;
                suffixes[0].Rank = curRank;
                int txtLength = text.Length;
                indexes[suffixes[0].Index] = 0;
                for (int i = 1; i < suffixes.Length; i++) //O(n)
                {
                    if (suffixes[i].Rank == prevRank && suffixes[i].NextRank == suffixes[i - 1].NextRank)
                    {
                        prevRank = suffixes[i].Rank;
                        suffixes[i].Rank = curRank;
                    }
                    else
                    {
                        prevRank = suffixes[i].Rank;
                        suffixes[i].Rank = ++curRank;
                    }
                    indexes[suffixes[i].Index] = i;
                }
                for (int i = 0; i < txtLength; i++) //O(n)
                {
                    int nextindex = suffixes[i].Index + k / 2;
                    suffixes[i].NextRank = (nextindex < txtLength) ?
                                          suffixes[indexes[nextindex]].Rank : -1;
                }
                Array.Sort<Suffix>(suffixes);
                //CountSort(suffixes, false); //O(n)
                //CountSort(suffixes, true); //O(n)
            }
            int[] suffixArray = new int[suffixes.Length];
            int ind = 0;
            foreach (var suffix in suffixes) //O(n)
            {
                suffixArray[ind++] = suffix.Index;
            }
            return suffixArray;
        }

        private void CountSort(Suffix[] suffixes, bool sortRank)
        {
            var count = new List<Suffix>[suffixes.Length + 256]; //O(1)
            int count_length = count.Length;

            for (int i = 0; i < count_length; i++) //O(n)
            {
                count[i] = new List<Suffix>(); //O(1)
            }

            foreach (var suffix in suffixes)//O(n)
            {
                int ind = (sortRank == true) ? suffix.Rank : (suffix.NextRank + 1);//O(1)
                count[ind].Add(suffix);//O(1)
            }

            int index = 0; //O(1)

            foreach (var suffixList in count) //O(n)------> o(n^2)
            {
                if (suffixList.Count == 0) //O(1)
                    continue;   //O(1)

                foreach (var suffix in suffixList) //O(n)
                {
                    suffixes[index++] = suffix; //O(1)
                }
            }
        }

        public byte[] Transform(byte[] text) //O(n Log n)
        {
            int Length = text.Length;
            Length ++;
            byte[] temp = new byte[Length];
            text.CopyTo(temp, 0); //O(n)
            temp[Length - 1] = (byte)'&';
            text = temp;
            int[] suffixArray = GenerateSuffixArray(text); //O(n Log n)
            byte[] transformedText = new byte[Length];
            for (int i = 0; i < Length; i++) //O(n)
            {
                if (suffixArray[i] == 0)
                    OriginalSuffixIndex = i;

                int ind = (suffixArray[i] == 0) ? (text.Length - 1) : (suffixArray[i] - 1);
                transformedText [i] = text[ind];
            }
            return transformedText;
        }
        public byte[] InverseTransformation(List<int> text) //O(n)
        {
            int index = text.Count - 1;
            int Length = text.Count;
            int[] Count = new int[256];
            int[] Sum = new int[256];
            int[] CountPrevSymbols = new int[Length];
            int sum = 0;
            int V = OriginalSuffixIndex;
            byte[] OriginalText = new byte[Length-1];
            byte Curnt_Char = (byte)text[V];
            for (int i = 0; i < 256; i++)
            { Count[i] = Sum[i] = 0; }

            for (int i = 0; i < Length; i++) //O(n)
            {
                CountPrevSymbols[i] = Count[(int)text[i]];
                Count[(int)text[i]]++;
            }
            for (int i = 0; i < 256; i++)
            {
                if (Count[i] > 0) Sum[i] = sum;
                sum += Count[i];
            }
            //OriginalText[index]= Curnt_Char;
            index--;
            while (index >= 0) //O(n)
            {
                //duke blue devils
                V = (int)(CountPrevSymbols[V] + Sum[(int)Curnt_Char]);
                Curnt_Char = (byte)text[V];
                OriginalText[index] = Curnt_Char;
                index--;
            }
            return OriginalText;
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
