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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                txtPath.Text = filePath;
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;
            string textInFile = File.ReadAllText(filePath);
            var transformedText = CompressionHelper.Transformer.Transform(textInFile);

            var encodedMTF = CompressionHelper.MoveToFront.Encoding(transformedText);

            CompressionHelper.Huffman.Build(encodedMTF);    
            BitArray _encodedHuffman = CompressionHelper.Huffman.Encode(encodedMTF);
            CompressionHelper.CompressedFileLength = _encodedHuffman.Length;
            byte[] encodedHuffman = ConvertFromBitArrayToByteArray(_encodedHuffman);
            
            var compressedFilePath = @"D:\" + "Compressed" + Guid.NewGuid().ToString() + ".txt";
            CompressionHelper.CompressedFilePath = compressedFilePath;
            File.WriteAllBytes(compressedFilePath, encodedHuffman);
            var compressForm = new CompressForm();
            compressForm.ShowDialog();
        }
        private byte[] ConvertFromBitArrayToByteArray(BitArray bitArray)
        {
            byte[] byteArray = new byte[bitArray.Length / 8 + 1];
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = 0;
            }

            for (int i = 0; i < bitArray.Length; i++)
            {
                byte curBit = Convert.ToByte(bitArray[i]);
                byteArray[i / 8] <<= 1;
                byteArray[i / 8] |= curBit;
            }

            return byteArray;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
