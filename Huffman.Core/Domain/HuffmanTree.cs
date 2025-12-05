namespace Huffman.Core.Domain;

/// <summary>
/// Represents the topology of a Huffman Tree.
/// Responsible only for holding the root node.
/// </summary>
internal class HuffmanTree(HuffmanTree.Node root)
{
    internal Node Root { get; } = root;

    internal abstract class Node { }

    internal class InternalNode(Node left, Node right) : Node
    {
        public Node Left { get; } = left;
        public Node Right { get; } = right;
    }

    internal class LeafNode(Symbol symbol) : Node
    {
        public Symbol Symbol { get; } = symbol;
    }
}
