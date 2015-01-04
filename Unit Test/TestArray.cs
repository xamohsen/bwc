using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Test
{
    public class TestArray
    {
        public List<int> GenerateNextArray(string Last)
        {
            var Count = new Dictionary<char, int>();
            var Next = new List<int>(Last.Count());
            var _count = 0;
            var LastDictionary = new Dictionary<char, List<int>>();
            foreach (var symbol in Last)
            {
                if (Count.ContainsKey(symbol)) Count[symbol]++;
                else Count.Add(symbol, 1);
            }
            foreach (var symbol in Last)
            {
                if (LastDictionary.ContainsKey(symbol))
                    LastDictionary[symbol].Add(_count++);
                else
                {
                    LastDictionary.Add(symbol, new List<int>());
                    LastDictionary[symbol].Add(_count++);
                }
            }
            for (var charIndex = 0; charIndex < 256; charIndex++)
            {
                if(Count.ContainsKey((char)charIndex))
                for (var ind = 0; ind < Count[(char)charIndex]; ind++)
                {
                    Next.Add(LastDictionary[(char)charIndex][ind]);
                }
            }
            return Next;
        }
    }
}
