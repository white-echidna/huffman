using System.Runtime.InteropServices;

namespace Huffman.Core.Domain;

/// <summary>
///  Represents a variable-length Huffman code consisting of a bit sequence and its length.
/// </summary>
/// <param name="bits">The bit sequence (codes are stored in the lower bits).</param>
/// <param name="length">The number of significant bits in the code.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct HuffmanCode(uint bits, int length)
{
    public readonly uint Bits = bits;
    public readonly int Length = length;
}
