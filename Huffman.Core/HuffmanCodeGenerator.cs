using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
///  Responsible for traversing the Huffman Tree and generating the lookup table of codes.
/// </summary>
internal static class HuffmanCodeGenerator
{
    /// <summary>
    ///  Generates a <see cref="SymbolTable{HuffmanCode}"/> from the provided tree topology.
    /// </summary>
    /// <param name="tree">The Huffman tree topology.</param>
    /// <returns>A lookup table mapping Symbols to their corresponding HuffmanCodes.</returns>
    internal static SymbolTable<HuffmanCode> Generate(HuffmanTree tree)
    {
        var codes = new SymbolTable<HuffmanCode>();

        if (tree.Root is HuffmanTree.LeafNode leaf_node)
        {
            var huffman_code = new HuffmanCodeBuilder()
                .AppendZero()
                .Build();

            codes[leaf_node.Symbol] = huffman_code;
            return codes;
        }

        GenerateRecursive(tree.Root, new HuffmanCodeBuilder(), codes);

        return codes;
    }

    private static void GenerateRecursive(
        HuffmanTree.Node node,
        HuffmanCodeBuilder builder,
        SymbolTable<HuffmanCode> table
    )
    {
        switch (node)
        {
            case HuffmanTree.LeafNode leaf_node:
                table[leaf_node.Symbol] = builder.Build();
                break;

            case HuffmanTree.InternalNode internal_node:
                GenerateRecursive(internal_node.Left, builder.AppendZero(), table);
                GenerateRecursive(internal_node.Right, builder.AppendOne(), table);
                break;
        }
    }
}
