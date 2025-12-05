using System.Buffers;
using System.Runtime.CompilerServices;

using Huffman.Core.Domain;

namespace Huffman.Core;

/// <summary>
///  A high-performance bit writer.
/// </summary>
public class BitWriter(IBufferWriter<byte> output)
{
    private readonly IBufferWriter<byte> _output = output;
    private Accumulator _accumulator = new();

    /// <summary>
    ///  Writes a HuffmanCode to the output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(HuffmanCode code)
    {
        Write(code.Bits, code.Length);
    }

    /// <summary>
    ///  Writes raw bits to the output.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(uint bits, int length)
    {
        _accumulator.Push(bits, length);

        if (_accumulator.IsFull32)
        {
            FlushAccumulator();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void FlushAccumulator()
    {
        Span<byte> span = _output.GetSpan(4);

        uint chunk = _accumulator.PopUInt32();

        Unsafe.WriteUnaligned(ref span[0], chunk);

        _output.Advance(4);
    }

    /// <summary>
    ///  Flushes any remaining bits to the output byte-by-byte.
    ///  Should be called when all data has been written.
    /// </summary>
    public void Flush()
    {
        if (_accumulator.HasData)
        {
            const int maxBytesNeeded = 4;

            Span<byte> span = _output.GetSpan(maxBytesNeeded);
            int written = 0;

            while (_accumulator.HasData)
            {
                span[written++] = _accumulator.PopByte();
            }

            _output.Advance(written);
        }
    }

    internal struct Accumulator
    {
        private ulong _buffer;
        private int _count;

        public readonly bool IsFull32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count >= 32;
        }

        public readonly bool HasData
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(uint bits, int length)
        {
            _buffer |= (ulong)bits << _count;
            _count += length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint PopUInt32()
        {
            uint result = (uint)_buffer;
            _buffer >>= 32;
            _count -= 32;
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte PopByte()
        {
            byte result = (byte)_buffer;
            _buffer >>= 8;
            _count = Math.Max(0, _count - 8);
            return result;
        }
    }
}
