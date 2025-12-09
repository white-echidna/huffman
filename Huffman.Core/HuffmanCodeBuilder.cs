using System.Runtime.CompilerServices;

using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
/// A helper structure to construct Huffman codes incrementally.
/// </summary>
public readonly struct HuffmanCodeBuilder
{
    private readonly ulong _bits;
    private readonly int _length;

    public HuffmanCodeBuilder()
    {
        _bits = 0;
        _length = 0;
    }

    private HuffmanCodeBuilder(ulong bits, int length)
    {
        _bits = bits;
        _length = length;
    }

    /// <summary>
    /// Appends a '0' bit to the sequence.
    /// </summary>
    /// <returns>A new builder instance (functional style).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HuffmanCodeBuilder AppendZero()
    {
        return new HuffmanCodeBuilder(_bits << 1, _length + 1);
    }

    /// <summary>
    /// Appends a '1' bit to the sequence.
    /// </summary>
    /// <returns>A new builder instance (functional style).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HuffmanCodeBuilder AppendOne()
    {
        return new HuffmanCodeBuilder((_bits << 1) | 1, _length + 1);
    }

    /// <summary>
    /// Finalizes the construction and returns the domain object.
    /// </summary>
    public HuffmanCode Build()
    {
        return new HuffmanCode((uint)_bits, _length);
    }
}
