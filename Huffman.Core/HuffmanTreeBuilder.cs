using Huffman.Core.Domain;

namespace Huffman.Core;

internal class HuffmanTreeBuilder(SymbolTable<long> frequencies)
{
    private readonly SymbolTable<long> _frequencies = frequencies;

    public HuffmanTree Build()
    {
        var priority_queue = new PriorityQueue<HuffmanTree.Node, long>();
        priority_queue.EnsureCapacity(Symbol.AlphabetSize);

        foreach (Symbol symbol in SymbolTable<long>.Symbols)
        {
            var frequency = _frequencies[symbol];
            if (frequency > 0)
            {
                priority_queue.Enqueue(new HuffmanTree.LeafNode(symbol), frequency);
            }
        }

        while (priority_queue.Count > 1)
        {
            priority_queue.TryDequeue(out var left_node, out long left_freq);
            priority_queue.TryDequeue(out var right_node, out long right_freq);

            var parent_node = new HuffmanTree.InternalNode(left_node!, right_node!);
            var parent_freq = left_freq + right_freq;

            priority_queue.Enqueue(parent_node, parent_freq);
        }

        var root = priority_queue.Dequeue();

        return new HuffmanTree(root);
    }
}
