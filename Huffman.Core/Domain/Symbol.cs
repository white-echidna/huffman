using System.Runtime.CompilerServices;

namespace Huffman.Core.Domain;

/// <summary>
/// Represents a single symbol in the compression alphabet, wrapping a <see cref="byte"/>.
/// </summary>
/// <param name="Value">The underlying byte value of the symbol.</param>
public readonly record struct Symbol(byte Value)
{
    public const int AlphabetSize = 256;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Symbol(byte value) => new(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(Symbol symbol) => symbol.Value;
}

/// <summary>
/// Represents the contiguous sequence of symbols.
/// </summary>
public readonly struct SymbolSequence
{
    /// <summary>Gets an enumerator.</summary>
    public Enumerator GetEnumerator() => new();

    /// <summary>Enumerates the symbols in the sequential order.</summary>
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref struct Enumerator()
    {
        /// <summary>The current symbol to yield.</summary>
        private int _index = -1;

        /// <summary>Advances the enumerator to the next symbol.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            int index = _index + 1;
            if (index < Symbol.AlphabetSize)
            {
                _index = index;
                return true;
            }
            return false;
        }

        /// <summary>Gets current symbol.</summary>
        public readonly Symbol Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Symbol)_index;
        }
    }
}
