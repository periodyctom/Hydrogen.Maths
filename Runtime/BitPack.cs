using System.Runtime.CompilerServices;
using static Unity.Mathematics.math;

namespace Hydrogen.Maths
{
    /// <summary>
    /// Helpers for packing bitfields into integers of various sizes.
    /// </summary>
    public static class BitPack
    {
        /// <summary>
        /// Gets a bitmask of an arbitrary length for the type.
        /// </summary>
        /// <param name="length">Length of the mask in bits.</param>
        /// <returns>The mask bit pattern.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ByteMask(byte length)
        {
            unchecked
            {
                return (byte) ((byte) (1ul << length) - 1);
            }
        }

        /// <summary>
        /// Gets a bitmask of an arbitrary length for the type.
        /// </summary>
        /// <param name="length">Length of the mask in bits.</param>
        /// <returns>The mask bit pattern.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort UShortMask(byte length)
        {
            unchecked
            {
                return (ushort) ((ushort)(1ul << length) - 1);
            }
        }

        /// <summary>
        /// Gets a bitmask of an arbitrary length for the type.
        /// </summary>
        /// <param name="length">Length of the mask in bits.</param>
        /// <returns>The mask bit pattern.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint UIntMask(byte length)
        {
            unchecked
            {
                return (uint)(1ul << length) - 1;
            }
        }

        /// <summary>
        /// Gets a bitmask of an arbitrary length for the type.
        /// </summary>
        /// <param name="length">Length of the mask in bits.</param>
        /// <returns>The mask bit pattern.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ULongMask(byte length)
        {
            unchecked
            {
                return select(0ul, 1ul << length, length < 64) - 1ul;
            }
        }

        /// <summary>
        /// Gets a bit pattern starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Get(byte field, byte length) => (byte) (field & ByteMask(length));

        /// <summary>
        /// Gets a bit pattern starting at the given offset.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Get(byte field, byte length, sbyte offset) => (byte) ((field >> offset) & ByteMask(length));

        /// <summary>
        /// Gets a bit pattern starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <returns>The extracted sub-field.</returns>
        public static ushort Get(ushort field, byte length) => (ushort) (field & UShortMask(length));

        /// <summary>
        /// Gets a bit pattern starting at the given offset.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Get(ushort field, byte length, sbyte offset) =>
            (ushort) ((field >> offset) & UShortMask(length));

        /// <summary>
        /// Gets a bit pattern starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Get(uint field, byte length) => field & UIntMask(length);

        /// <summary>
        /// Gets a bit pattern starting at the given offset.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Get(uint field, byte length, sbyte offset) => (field >> offset) & UIntMask(length);
        
        /// <summary>
        /// Gets a bit pattern starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Get(ulong field, byte length) => field & ULongMask(length);

        /// <summary>
        /// Gets a bit pattern starting at the given offset.
        /// </summary>
        /// <param name="field">Bit field to extract from.</param>
        /// <param name="length">Length of sub-field to extract.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Get(ulong field, byte length, sbyte offset) => (field >> offset) & ULongMask(length);

        /// <summary>
        /// Sets a part of a bit field with the given value, starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <returns>The modified bit field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Set(byte field, byte value, byte length)
        {
            byte mask = ByteMask(length);
            return (byte) ((field & ~mask) | (value & mask));
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, at the given offset.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Set(byte field, byte value, byte length, sbyte offset)
        {
            byte mask = ByteMask(length);
            return (byte) ((field & ~(mask << offset)) | ((value & mask) << offset));
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <returns>The modified bit field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Set(ushort field, ushort value, byte length)
        {
            ushort mask = UShortMask(length);
            return (ushort) ((field & ~mask) | (value & mask));
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, at the given offset.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Set(ushort field, ushort value, byte length, sbyte offset)
        {
            ushort mask = UShortMask(length);
            return (ushort) ((field & ~(mask << offset)) | ((value & mask) << offset));
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <returns>The modified bit field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Set(uint field, uint value, byte length)
        {
            uint mask = UIntMask(length);
            return (field & ~mask) | (value & mask);
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, at the given offset.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Set(uint field, uint value, byte length, sbyte offset)
        {
            uint mask = UIntMask(length);
            return (field & ~(mask << offset)) | ((value & mask) << offset);
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, starting at the LSB.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <returns>The modified bit field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Set(ulong field, ulong value, byte length)
        {
            ulong mask = ULongMask(length);
            return (field & ~mask) | (value & mask);
        }

        /// <summary>
        /// Sets a part of a bit field with the given value, at the given offset.
        /// </summary>
        /// <param name="field">Bit field to modify.</param>
        /// <param name="value">Value to set the sub-field to.</param>
        /// <param name="length">Length of sub-field to set.</param>
        /// <param name="offset">Offset into the bit field from the LSB.</param>
        /// <returns>The extracted sub-field.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Set(ulong field, ulong value, byte length, sbyte offset)
        {
            ulong mask = ULongMask(length);
            return (field & ~(mask << offset)) | ((value & mask) << offset);
        }
    }
}
