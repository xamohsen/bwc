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
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();
        private List<Node> NodesList = new List<Node>();
        private PriorityQueue.PriorityQueue<Node> Queue = new PriorityQueue.PriorityQueue<Node>();
        public Node Root { get; set; }

        public void Build(List<int> input)
        {
            foreach (var _symbol in input)
            {
                char symbol = (char)_symbol;
                if (!Frequencies.ContainsKey(symbol))
                    Frequencies.Add(symbol, 1);
                else
                    Frequencies[symbol]++;
            }
            foreach (var symbol in Frequencies)
            {
                Queue.Enqueue(new Node { Symbol = symbol.Key, Frequency = symbol.Value });
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
                    Right = node2
                };
                Queue.Enqueue(parent);
            }
            this.Root = Queue.Dequeue();
        }

        public BitArray Encode(List<int> _source)
        {
            int[] source = _source.ToArray();
            List<bool> encodedSource = new List<bool>();
            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse((char)source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }
            BitArray bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

        public List<int> Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";
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
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            List<int> decodedList = new List<int>();

            for (int i = 0; i < decoded.Length; i++)
            {
                decodedList.Add((int)decoded[i]);
            }

            return decodedList;
        }

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
