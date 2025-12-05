namespace Huffman.Core.Domain;

/// <summary>
///  Represents the topology of a Huffman Tree.
/// </summary>
internal class HuffmanTree(HuffmanTree.Node root)
{
    public Node Root { get; } = root;

    public abstract class Node { }

    public class InternalNode(Node left, Node right) : Node
    {
        public Node Left { get; } = left;
        public Node Right { get; } = right;
    }

    public class LeafNode(Symbol symbol) : Node
    {
        public Symbol Symbol { get; } = symbol;
    }
}
