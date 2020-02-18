using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Hydrogen.Maths
{
    /// <summary>
    /// Utilities with working with bits.
    /// For packing bits into fields, see the <seealso cref="BitPack"/> utility class.
    /// </summary>
    public static class BitUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x2 ToBool4x2(this byte value)
        {
            var lo = new bool4((value & 0x01) != 0, (value & 0x02) != 0, (value & 0x04) != 0, (value & 0x08) != 0);
            var hi = new bool4((value & 0x10) != 0, (value & 0x20) != 0, (value & 0x40) != 0, (value & 0x80) != 0);

            return new bool4x2(lo, hi);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool4x4 ToBool4x4(this ushort value)
        {
            var lo = ToBool4x2((byte)(value & 0x00FF));
            var hi = ToBool4x2((byte)((value & 0xFF00) >> 8));

            return new bool4x4(lo.c0, lo.c1, hi.c0, hi.c1);
        }

        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// but using a constant 1 bit swap length
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Swap(byte b, byte i, byte j)
        {
            var x = (byte) (((b >> i) ^ (b >> j)) & 1);
            var r = (byte) (b ^ ((x << i) | (x << j)));
            return r;
        }
        
        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <param name="n">number of bits to swap</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Swap(byte b, byte i, byte j, byte n)
        {
            unchecked
            {
                var x = (byte) (((b >> i) ^ (b >> j)) & ((1 << n) - 1)); // XOR temporary
                var r = (byte) (b ^ ((x << i) | (x << j)));
                return r;
            }
        }
        
        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// but using a constant 1 bit swap length
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Swap(ushort b, byte i, byte j)
        {
            var x = (ushort) (((b >> i) ^ (b >> j)) & 1);
            var r = (ushort) (b ^ ((x << i) | (x << j)));
            return r;
        }

        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <param name="n">number of bits to swap</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Swap(ushort b, byte i, byte j, byte n)
        {
            unchecked
            {
                var x = (ushort) (((b >> i) ^ (b >> j)) & ((1 << n) - 1)); // XOR temporary
                var r = (ushort) (b ^ ((x << i) | (x << j)));
                return r;
            }
        }
        
        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// but using a constant 1 bit swap length
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Swap(uint b, byte i, byte j)
        {
            var x = ((b >> i) ^ (b >> j)) & 1u;
            var r = b ^ ((x << i) | (x << j));
            return r;
        }

        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <param name="n">number of bits to swap</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Swap(uint b, byte i, byte j, byte n)
        {
            unchecked
            {
                var x = ((b >> i) ^ (b >> j)) & ((1u << n) - 1u); // XOR temporary
                var r = b ^ ((x << i) | (x << j));
                return r;
            }
        }
        
        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// but using a constant 1 bit swap length
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Swap(ulong b, byte i, byte j)
        {
            var x = ((b >> i) ^ (b >> j)) & 1ul;
            var r = b ^ ((x << i) | (x << j));
            return r;
        }

        /// <summary>
        /// Derived from: http://graphics.stanford.edu/~seander/bithacks.html#SwappingBitsXO
        /// </summary>
        /// <param name="b">Bit pattern to swap</param>
        /// <param name="i">1st swap position</param>
        /// <param name="j">2nd swap position</param>
        /// <param name="n">number of bits to swap</param>
        /// <returns>Post-swapped bit pattern</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Swap(ulong b, byte i, byte j, byte n)
        {
            unchecked
            {
                var x = ((b >> i) ^ (b >> j)) & ((1ul << n) - 1ul); // XOR temporary
                var r = b ^ ((x << i) | (x << j));
                return r;
            }
        }
    }
}
