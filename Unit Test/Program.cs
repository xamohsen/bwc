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
            var test = new TestArray();
            var output = test.GenerateNextArray(last);
            foreach (var t in output)
                Console.Write(t + " ");
            Console.WriteLine();
        }

        //private static byte[] ConvertFromBitArrayToByteArray(BitArray bitArray)
        //{
        //    byte[] byteArray = new byte[bitArray.Length / 8 + 1];
        //    for (int i = 0; i < byteArray.Length; i++)
        //    {
        //        byteArray[i] = 0;
        //    }
        //    for (int i = 0; i < bitArray.Length; i++)
        //    {
        //        byte curBit = Convert.ToByte(bitArray[bitArray.Length - i - 1]);
        //        byteArray[i / 8] <<= 1;
        //        byteArray[i / 8] |= curBit;
        //    }

        //    return byteArray;
        //}

        //private List<Suffixe> Hash(string text)
        //{
        //    var hashedSuffixes = new List<Suffixe>();
        //    var length = text.Count();
        //    var initialSuffixe = new Suffixe() { HashValue = 0, Index = 0, Mods = 0 };
        //    for (var i = length - 1; i >= 0; i--)
        //    {
        //        initialSuffixe.HashValue += initialSuffixe.HashValue * @base + ((int)text[i]);
        //        while (initialSuffixe.HashValue >= mod)
        //        {
        //            initialSuffixe.HashValue -= mod;
        //            initialSuffixe.Mods++;
        //        }
        //    }
        //    var hashValue = initialSuffixe.HashValue;
        //    var index = 1;
        //    hashedSuffixes.Add(initialSuffixe);
        //    for (int i = 0; i < length - 1; i++)
        //    {
        //        //routate the string
        //        char firstChar = text[0];
        //        text = text.Remove(0, 1);
        //        text += firstChar;
        //        //change the hash value for each string
        //        hashValue -= 
        //    }
        //    return hashedSuffixes;
        //}
    }
}
