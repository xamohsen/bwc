using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Transformation;
using MoveToFront;
using HuffmanCompression;

namespace BurrowsWheelerCompression.UI
{
    public static class CompressionHelper
    {
        public static BurrowsWheelerTransformation Transformer = new BurrowsWheelerTransformation();
        public static MTF MoveToFront = new MTF();
        public static HuffmanTree Huffman = new HuffmanTree();
        public static long CompressedFileLength;
        public static string CompressedFilePath;
        public static string DecompressedFilePath;
    }
}
