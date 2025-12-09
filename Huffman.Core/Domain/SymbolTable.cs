using System.Runtime.CompilerServices;

namespace Huffman.Core.Domain;

/// <summary>
///  Represents a high-performance lookup table indexed by <see cref="Symbol"/>.
/// </summary>
/// <typeparam name="TValue">
///  The type of value stored in the table. Must be a <see langword="struct"/>.
/// </typeparam>
public class SymbolTable<TValue> where TValue : struct
{
    private readonly TValue[] _values = new TValue[Symbol.AlphabetSize];

    /// <summary>
    /// Gets a reference to the value associated with the specified symbol.
    /// </summary>
    /// <param name="symbol">The symbol to use as the index.</param>
    /// <returns>A reference to the value, allowing in-place modification.</returns>
    public ref TValue this[Symbol symbol]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _values[symbol.Value];
    }

    /// <summary>
    /// Creates a new span over the symbol table.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<TValue> AsSpan()
    {
        return _values.AsSpan();
    }
}
