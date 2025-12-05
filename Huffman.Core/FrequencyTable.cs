using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
///  Accumulates symbol frequencies from byte data streams.
/// </summary>
public class FrequencyTable
{
    /// <summary>
    ///  Gets the table containing frequency counts for each symbol.
    /// </summary>
    public SymbolTable<long> Frequencies { get; } = new();

    /// <summary>
    ///  Processes a chunk of symbols and updates their frequencies.
    /// </summary>
    /// <param name="chunk">A read-only span of symbols to process.</param>
    public void AddChunk(ReadOnlySpan<Symbol> chunk)
    {
        foreach (Symbol symbol in chunk)
        {
            Frequencies[symbol]++;
        }
    }
}
