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
using System.Diagnostics;

namespace BurrowsWheelerCompression.UI
{
    public partial class CompressForm : Form
    {
        public CompressForm()
        {
            InitializeComponent();
        }

        private void btnCompressedFile_Click(object sender, EventArgs e)
        {
            Process.Start(txtCompressedFilePath.Text);
        }

        private void btnDecompressFile_Click(object sender, EventArgs e)
        {
            byte[] bytesInFile = File.ReadAllBytes(txtCompressedFilePath.Text);
            BitArray bitsInFile = ConvertFromByteArrayToBitArray(bytesInFile);

            List<int> decodedHuffman = CompressionHelper.Huffman.Decode(bitsInFile);
            string decodedMTF = CompressionHelper.MoveToFront.Decoding(decodedHuffman);
            string inversedText = CompressionHelper.Transformer.InverseTransformation(decodedMTF);

            string decompressedFilePath = @"D:\" + "Decompressed" + Guid.NewGuid().ToString() + ".txt";
            CompressionHelper.DecompressedFilePath = decompressedFilePath;
            File.WriteAllText(decompressedFilePath, inversedText);

            var decompressedForm = new DecompressedForm();
            decompressedForm.ShowDialog();
        }

        private void CompressForm_Load(object sender, EventArgs e)
        {
            txtCompressedFilePath.Text = CompressionHelper.CompressedFilePath;
        }

        private BitArray ConvertFromByteArrayToBitArray(byte[] byteArray)
        {
            BitArray _bitsArray = new BitArray(byteArray);

            List<bool> tempBoolList = new List<bool>();
            List<bool> boolList = new List<bool>();
            for (int i = 1; i <= CompressionHelper.CompressedFileLength; i++)
            {
                tempBoolList.Add(_bitsArray[i - 1]);
                if (i % 8 == 0)
                {
                    tempBoolList.Reverse();
                    boolList.AddRange(tempBoolList);
                    tempBoolList.Clear();
                }
            }

            if (tempBoolList.Count > 0)
            {
                tempBoolList.Reverse();
                boolList.AddRange(tempBoolList);
            }

            BitArray bitArray = new BitArray(boolList.ToArray());

            return bitArray;
        }
    }
}
