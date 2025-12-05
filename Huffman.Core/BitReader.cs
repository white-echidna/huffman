using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Huffman.Core;

/// <summary>
///  A high-performance bit reader.
/// </summary>
public ref struct BitReader
{
    private SequenceReader<byte> _reader;
    private ref Accumulator _accumulator;

    public BitReader(ReadOnlySequence<byte> sequence, ref Accumulator accumulator)
    {
        _reader = new SequenceReader<byte>(sequence);
        _accumulator = ref accumulator;
    }

    /// <summary>
    ///  Tries to read a single bit.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryReadBit(out uint bit)
    {
        if (!_accumulator.HasBits)
        {
            if (!TryRefill())
            {
                bit = 0;
                return false;
            }
        }

        bit = _accumulator.PopBit();
        return true;
    }

    /// <summary>
    ///  Tries to read 'count' bits.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryReadBits(int count, out uint result)
    {
        while (_accumulator.BitCount < count)
        {
            if (!TryRefill())
            {
                result = 0;
                return false;
            }
        }

        result = _accumulator.PopBits(count);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool TryRefill()
    {
        if (_accumulator.IsFull) return true;

        if (_reader.TryRead(out byte b))
        {
            _accumulator.AddByte(b);
            return true;
        }

        return false;
    }

    /// <summary>
    ///  Returns the position in the sequence to allow advancing the pipe.
    /// </summary>
    public readonly SequencePosition Position => _reader.Position;


    /// <summary>
    ///  Manages the 64-bit CPU register buffer for reading.
    /// </summary>
    public struct Accumulator
    {
        private ulong _buffer;
        private int _count;

        public readonly bool HasBits
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count > 0;
        }

        public readonly int BitCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count;
        }

        public readonly bool IsFull
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _count > 56;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddByte(byte b)
        {
            _buffer |= (ulong)b << _count;
            _count += 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint PopBit()
        {
            uint bit = (uint)(_buffer & 1);
            _buffer >>= 1;
            _count--;
            return bit;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint PopBits(int n)
        {
            uint result = (uint)_buffer & ((1u << n) - 1);
            _buffer >>= n;
            _count -= n;
            return result;
        }
    }
}
