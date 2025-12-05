using System.Buffers;

using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
///  Decompresses data by traversing the Huffman Tree.
///  Maintains state between chunks, allowing for streaming decompression.
/// </summary>
public class Decoder
{
    private readonly HuffmanTree _tree;
    private HuffmanTree.Node _currentNode;
    private BitReader.Accumulator _accumulator;

    internal Decoder(HuffmanTree tree)
    {
        _tree = tree;
        _currentNode = _tree.Root;
        _accumulator = new BitReader.Accumulator();
    }

    /// <summary>
    ///  Decodes a chunk of compressed data.
    /// </summary>
    /// <param name="input">Input sequence of bytes (compressed).</param>
    /// <param name="output">Output writer for decoded bytes.</param>
    public void Decode(ReadOnlySequence<byte> input, IBufferWriter<byte> output)
    {
        // 1. Создаем читалку поверх входных данных и нашего аккумулятора
        var reader = new BitReader(input, ref _accumulator);

        // Кэшируем корень для скорости (чтобы не лезть в this._tree.Root каждый раз)
        var root = _tree.Root;

        // 2. Обработка краевого случая: Дерево из одного узла
        if (root is HuffmanTree.LeafNode singleLeaf)
        {
            // В этом случае каждый бит '0' кодирует один символ.
            // Мы просто считаем биты и пишем символы.
            while (reader.TryReadBit(out _))
            {
                WriteSymbol(output, singleLeaf.Symbol);
            }
            return;
        }

        // 3. Основной цикл декодирования
        // Мы используем локальную переменную node для скорости, 
        // а в конце сохраним её обратно в _currentNode.
        var node = _currentNode;

        while (reader.TryReadBit(out uint bit))
        {
            // Спускаемся по дереву
            // Так как мы проверили singleLeaf выше, здесь root и все промежуточные узлы — InternalNode.
            // Но C# компилятор этого не знает, поэтому используем cast.

            if (node is HuffmanTree.InternalNode internalNode)
            {
                node = (bit == 0) ? internalNode.Left : internalNode.Right;
            }

            // Если дошли до листа
            if (node is HuffmanTree.LeafNode leaf)
            {
                WriteSymbol(output, leaf.Symbol);
                node = root!; // Возвращаемся в корень
            }
        }

        // 4. Сохраняем состояние для следующего чанка
        _currentNode = node;
    }

    private static void WriteSymbol(IBufferWriter<byte> writer, Symbol symbol)
    {
        Span<byte> span = writer.GetSpan(1);
        span[0] = symbol.Value;
        writer.Advance(1);
    }
}
