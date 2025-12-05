using System.Runtime.CompilerServices;

namespace Huffman.Core.Domain;

/// <summary>
///  Represents a single symbol in the compression alphabet, wrapping a <see cref="byte"/>.
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
