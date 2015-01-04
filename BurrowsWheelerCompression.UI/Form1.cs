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
    public partial class Form1 : Form
    {
        private CompressionHelper Helper = new CompressionHelper();
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnCompress_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;
            string textInFile = File.ReadAllText(filePath);
            var compressedFilePath = @"E:\" + "Compressed" + Guid.NewGuid().ToString() + ".bin";
            Helper.FilePath = compressedFilePath;
            var transformedText = Helper.Transformer.Transform(textInFile);
            var encodedMTF = Helper.MoveToFront.Encoding(transformedText);
            Helper.Huffman.Build(encodedMTF);
            BitArray _encodedHuffman = Helper.Huffman.Encode(encodedMTF);
            Helper.CompressedFileLength = _encodedHuffman.Length;
            byte[] encodedHuffman = ConvertFromBitArrayToByteArray(_encodedHuffman);
            SaveToFile(compressedFilePath, encodedHuffman);
            var form = new OpenFileForm();
            form.ShowDialog();
        }
        private void btnDeCompress_Click(object sender, EventArgs e)
        {
            string filePath = txtPath.Text;
            byte[] bytesInFile = LoadFromFile(filePath);
            BitArray bitsInFile = ConvertFromByteArrayToBitArray(bytesInFile);
            List<int> decodedHuffman = Helper.Huffman.Decode(bitsInFile);
            string decodedMTF = Helper.MoveToFront.Decoding(decodedHuffman);
            string inversedText = Helper.Transformer.InverseTransformation(decodedMTF);
            string decompressedFilePath = @"E:\" + "Decompressed" + Guid.NewGuid().ToString() + ".txt";
            Helper.FilePath = decompressedFilePath;
            File.WriteAllText(decompressedFilePath, inversedText);
            var form = new OpenFileForm();
            form.ShowDialog();
        }

        private void SaveToFile(string compressedFilePath, byte[] encodedHuffman)
        {
            using (var stream = new FileStream(compressedFilePath, FileMode.Create))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Helper.Transformer.OriginalSuffixIndex);
                writer.Write(Helper.CompressedFileLength);
                int[] charsFrequencies = Helper.Huffman.Frequencies;
                for (int i = 0; i < charsFrequencies.Length; i++)
                {
                    writer.Write(charsFrequencies[i]);
                }
                writer.Write(encodedHuffman);
            }
        }
        private byte[] LoadFromFile(string compressedFilePath)
        {
            using (var stream = new FileStream(compressedFilePath, FileMode.Open))
            using (var reader = new BinaryReader(stream))
            {
                int pos = 0;
                int fileLength = (int)reader.BaseStream.Length;
                Helper.Transformer.OriginalSuffixIndex = reader.ReadInt32();
                pos += sizeof(int);
                Helper.CompressedFileLength = reader.ReadInt32();
                pos += sizeof(int);
                int[] frequencies = new int[256];
                for (int i = 0; i < frequencies.Length; i++)
                {
                    frequencies[i] = reader.ReadInt32();
                    pos += sizeof(int);
                }
                Helper.Huffman.Frequencies = frequencies;
                Helper.Huffman.BuildTree();
                var bytesInFile = new List<byte>();
                while (pos < fileLength)
                {
                    bytesInFile.Add(reader.ReadByte());
                    pos += sizeof(byte);
                }
                return bytesInFile.ToArray();
            }
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
        private BitArray ConvertFromByteArrayToBitArray(byte[] byteArray)
        {
            BitArray _bitsArray = new BitArray(byteArray);

            List<bool> tempBoolList = new List<bool>();
            List<bool> boolList = new List<bool>();
            for (int i = 1; i <= Helper.CompressedFileLength; i++)
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
