using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveToFront
{
    public class MTF
    {
        public List<int> Initialization()
        {
            List<int> work = new List<int>();
            for (int ind = 0; ind < 257; ind++) //O(1)
            {
                work.Add(ind);
            }
            return work;
        }
        public List<int> Encoding(string input) //O(n)
        {
            List<int> output = new List<int>();
            List<int> work = Initialization();
            for (int ind = 0; ind < input.Length; ind++) //O(n)
            {
                int t = (int)input[ind];
                int target = work.IndexOf(t);
                output.Add(target); //O(1)
                work.Remove(input[ind]); //O(1)
                work.Insert(0, (int)input[ind]); //O(1)
               
            }
            return output;
        }
        public string Decoding(List<int> input)
        {
            string output = input.ToString(); //O(n)
            List<int> work = Initialization();
            int Length = input.Count;
            for (int ind = 0; ind < Length; ind++) //O(n)
            {
                int t = input[ind];
                int target = work[t];
                output += (char)target;
                work.Remove(target); //O(1)
                work.Insert(0, target); //O(1)
            }
            return output;
        }
    }
}
