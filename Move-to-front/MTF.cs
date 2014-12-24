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
            for (int ind = 0; ind < 257; ind++)
            {
                work.Add(ind);
            }
            return work;
        }
        public List<int> Encoding(string input)
        {
            List<int> output = new List<int>();
            List<int> work = Initialization();
            for(int ind = 0; ind < input.Length; ind ++)
            {
                int t = (int)input[ind];
                int target = work.IndexOf(t);
                output.Add(target);
                work.Remove(input[ind]);
                work.Insert(0, (int)input[ind]);
               
            }
            return output;
        }
        public string Decoding(List<int> input)
        {
            string output = "";
            List<int> work = Initialization();
            for (int ind = 0; ind < input.Count; ind++)
            {
                int t = input[ind];
                int target = work.ElementAt(t);
                output += (char)target;
                work.Remove(target);
                work.Insert(0, target);
            }
            return output;
        }
    }
}
