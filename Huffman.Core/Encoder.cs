using System.Runtime.InteropServices;

using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
/// Responsible for encoding raw data using a pre-calculated Huffman code table.
/// </summary>
public class Encoder(SymbolTable<HuffmanCode> codes, BitWriter writer)
{
    private readonly SymbolTable<HuffmanCode> _codes = codes;
    private readonly BitWriter _writer = writer;

    /// <summary>
    /// Encodes a chunk of raw bytes and writes them to the BitWriter.
    /// </summary>
    /// <param name="data">A span of raw bytes to compress.</param>
    public void Encode(ReadOnlySpan<byte> data)
    {
        ReadOnlySpan<Symbol> symbols = MemoryMarshal.Cast<byte, Symbol>(data);

        foreach (Symbol symbol in symbols)
        {
            HuffmanCode code = _codes[symbol];
            _writer.Write(code);
        }
    }
}
