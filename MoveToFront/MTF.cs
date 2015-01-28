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
            List<int> work = new List<int>(257);
            for (int ind = 0; ind < 257; ind++) //O(1)
            {
                work.Add(ind); //O(1)
            }
            return work;
        }
        public byte[] Encoding(byte[] input) //O(n)
        {
            int Length = input.Length;
            byte[] output = new byte[Length];
            List<int> work = Initialization();
            for (int ind = 0; ind < Length; ind++) //O(n)
            {
                byte t = input[ind];
                byte target = (byte)work.IndexOf(t); //O(1)
                output[ind] = target;
                work.Remove(input[ind]); //O(1)
                work.Insert(0, input[ind]); //O(1)
               
            }
            return output;
        }
        public List<int> Decoding(List<int> input) //O(n)
        {
            int Length = input.Count;
            List<int> output = new List<int>(Length); //O(n)
            List<int> work = Initialization(); //O(1)
            for (int ind = 0; ind < Length; ind++) //O(n)
            {
                int t = input[ind];
                int target = work[t]; //O(1)
                output.Add(target); //O(1)
                work.Remove(target); //O(1)
                work.Insert(0, target); //O(1)
            }
            return output;
        }
    }
}
