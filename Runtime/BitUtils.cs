using System.Runtime.CompilerServices;

namespace Hydrogen.Maths
{
    /// <summary>
    /// Utilities with working with bits.
    /// For packing bits into fields, see the <seealso cref="BitPack"/> utility class.
    /// </summary>
    public static class BitUtils
    {
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
            byte x = (byte) (((b >> i) ^ (b >> j)) & 1);
            byte r = (byte) (b ^ ((x << i) | (x << j)));
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
                byte x = (byte) (((b >> i) ^ (b >> j)) & ((1 << n) - 1)); // XOR temporary
                byte r = (byte) (b ^ ((x << i) | (x << j)));
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
            ushort x = (ushort) (((b >> i) ^ (b >> j)) & 1);
            ushort r = (ushort) (b ^ ((x << i) | (x << j)));
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
                ushort x = (ushort) (((b >> i) ^ (b >> j)) & ((1 << n) - 1)); // XOR temporary
                ushort r = (ushort) (b ^ ((x << i) | (x << j)));
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
            uint x = ((b >> i) ^ (b >> j)) & 1u;
            uint r = b ^ ((x << i) | (x << j));
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
                uint x = ((b >> i) ^ (b >> j)) & ((1u << n) - 1u); // XOR temporary
                uint r = b ^ ((x << i) | (x << j));
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
            ulong x = ((b >> i) ^ (b >> j)) & 1ul;
            ulong r = b ^ ((x << i) | (x << j));
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
                ulong x = ((b >> i) ^ (b >> j)) & ((1ul << n) - 1ul); // XOR temporary
                ulong r = b ^ ((x << i) | (x << j));
                return r;
            }
        }
    }
}
