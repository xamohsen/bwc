using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriorityQueue;
using System.Collections;

namespace HuffmanCompression
{
    public class HuffmanTree
    {
        private List<Node> NodesList = new List<Node>();
        private PriorityQueue.PriorityQueue<Node> Queue = new PriorityQueue.PriorityQueue<Node>();
        private Dictionary<char, Node> LeefDectionry = new Dictionary<char, Node>();
        public int[] Frequencies = new int[256];
        private Node Root { get; set; }
        public short[] TreeArray { get; set; }

        public void BuildTree()
        {
            LeefDectionry.Clear();
            for (int i = 0; i < 256; i++)
            {
                if (Frequencies[i] > 0)
                {
                    Queue.Enqueue(new Node { Symbol = (char)i, Frequency = Frequencies[i], ISLeaf = true });
                    //LeefDectionry.Add((char)i, new Node { Symbol = (char)i, Frequency = Frequencies[i], ISLeaf =true });
                }
            }

            while (Queue.Count() > 1)
            {
                var node1 = Queue.Dequeue();
                var node2 = Queue.Dequeue();
                var parent = new Node()
                {
                    Symbol = '*',
                    Frequency = node1.Frequency + node2.Frequency,
                    Left = node1,
                    Right = node2,
                    ISLeaf = false,
                };
                node1.Parent = node2.Parent = parent;
                node1.Bit = false;
                node2.Bit = true;
                if (node1.ISLeaf) LeefDectionry.Add(node1.Symbol,node1);
                if (node2.ISLeaf) LeefDectionry.Add(node2.Symbol, node2);
                Queue.Enqueue(parent);
            }

            Root = Queue.Dequeue();

            int height = GetHight(Root);
            int arraySize = (int)Math.Pow(2, height);

            TreeArray = new short[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                TreeArray[i] = -1;
            }

            this.SaveToArray(0, Root);
        }

        public void Build(byte[] input)
        {

            for (int i = 0; i < 256; i++)
            {
                Frequencies[i] = 0;
            }
            foreach (var _symbol in input)
            {
                Frequencies[(int)_symbol]++;
            }
            this.BuildTree();
        }

        private void SaveToArray(int index, Node root)
        {
            TreeArray[index] = (short)root.Symbol;

            if (IsLeaf(root))
                return;

            SaveToArray(2 * index + 1, root.Left);
            SaveToArray(2 * index + 2, root.Right);
        }

        public BitArray Encode(byte[] source)
        {
            List<bool> encodedSource = new List<bool>();
            Dictionary<byte, List<bool>> Vist = new Dictionary<byte, List<bool>>();
            int Length = source.Length;
            for (int i = 0; i < Length; i++)
            {
                if (Vist.ContainsKey(source[i]))
                {
                    encodedSource.AddRange(Vist[source[i]]);
                    continue;
                }
                Node CurNode = LeefDectionry[(char)source[i]];
                List<bool> templiast = new List<bool>();
                while (CurNode != Root) // start from leaf note "faster"
                {
                    templiast.Add(CurNode.Bit);
                    CurNode = CurNode.Parent;
                }
                
                templiast.Reverse();
                Vist.Add(source[i], templiast);
                encodedSource.AddRange(templiast);
                // List<bool> encodedSymbol = this.Root.Traverse((char)source[i], new List<bool>());  //start from root "slower"
                 // encodedSource.AddRange(encodedSymbol);
            }
            //encodedSource.Reverse();
            BitArray bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

        public List<int> Decode(BitArray bits)
        {
            Node current = Root;
            List<int> decodedList = new List<int>();
            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    //decoded += current.Symbol;
                    decodedList.Add((int)current.Symbol);
                    current = Root;
                }
            }


            //int Length = decoded.Length;
            //for (int i = 0; i < Length; i++)
            //{
            //    decodedList.Add((int)decoded[i]);
            //}

            return decodedList;
        }

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }

        public bool IsLeaf(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            return ((left >= TreeArray.Length || TreeArray[left] == -1) && (right >= TreeArray.Length || TreeArray[right] == -1));
        }

        private int GetHight(Node node)
        {
            if (IsLeaf(node))
                return 1;
            return 1 + Math.Max(GetHight(node.Left), GetHight(node.Right));
        }
    }
}
