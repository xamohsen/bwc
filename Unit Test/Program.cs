using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MoveToFront;
using HuffmanCompression;
using Transformation;
using System.Collections;

namespace Unit_Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            BitArray array = new BitArray(new int[2]{6, 5});
            
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        private static byte[] ConvertFromBitArrayToByteArray(BitArray bitArray)
        {
            byte[] byteArray = new byte[bitArray.Length / 8 + 1];
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = 0;
            }

            for (int i = 0; i < bitArray.Length; i++)
            {
                byte curBit = Convert.ToByte(bitArray[bitArray.Length - i - 1]);
                byteArray[i / 8] <<= 1;
                byteArray[i / 8] |= curBit;
            }

            return byteArray;
        }
    }
}
