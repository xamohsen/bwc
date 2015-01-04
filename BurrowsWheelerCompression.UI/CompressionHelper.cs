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
    public class CompressionHelper
    {
        public BurrowsWheelerTransformationSuffixArray Transformer = new BurrowsWheelerTransformationSuffixArray();
        public MTF MoveToFront = new MTF();
        public HuffmanTree Huffman = new HuffmanTree();
        public int CompressedFileLength;
        public string FilePath;
    }
}
