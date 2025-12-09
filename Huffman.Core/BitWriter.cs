using System.Buffers;
using System.Runtime.CompilerServices;

using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
/// A high-performance bit writer.
/// </summary>
public class BitWriter(IBufferWriter<byte> output)
{
    private readonly IBufferWriter<byte> _output = output;
    private Accumulator _accumulator = new();

    /// <summary>
    /// Writes a HuffmanCode to the output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(HuffmanCode code)
    {
        Write(code.Bits, code.Length);
    }

    /// <summary>
    /// Writes raw bits to the output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(uint bits, int length)
    {
        _accumulator.Push(bits, length);

        if (_accumulator.Count > 32)
        {
            FlushUint32();
        }
    }

    /// <summary>
    /// Flushes UInt32 to the output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FlushUint32()
    {
        Span<byte> span = _output.GetSpan(4);

        uint chunk = _accumulator.PopUInt32();

        Unsafe.WriteUnaligned(ref span[0], chunk);

        _output.Advance(4);
    }

    /// <summary>
    /// Flushes any remaining bits to the output byte-by-byte.
    /// </summary>
    public void Flush()
    {
        if (_accumulator.Count == 0)
        {
            return;
        }

        const int maxBytesNeeded = 8;
        Span<byte> span = _output.GetSpan(maxBytesNeeded);

        int written = 0;
        while (_accumulator.Count > 0)
        {
            span[written++] = _accumulator.PopByte();
        }

        _output.Advance(written);
    }

    internal struct Accumulator
    {
        private ulong _buffer;
        private int _count;

        public readonly int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(uint bits, int length)
        {
            _buffer |= (ulong)bits << _count;
            _count += length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte PopByte()
        {
            byte result = (byte)_buffer;
            _buffer >>= 8;
            _count -= 8;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint PopUInt32()
        {
            uint result = (uint)_buffer;
            _buffer >>= 32;
            _count -= 32;
            return result;
        }
    }
}
