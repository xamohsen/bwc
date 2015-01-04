using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using HuffmanCompression;
using Transformation;
using System.Collections;

namespace Unit_Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            string last = "fdafcaaaabb";
            int ind = 2;
            var output = InverseTransformation(last, ind);
            foreach (var t in output)
                Console.Write(t);
            Console.WriteLine();
        }
        public static string InverseTransformation(string text, int OriginalSuffixIndex)
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
                //this is a test.
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
    }
}
