
# BitPack

The BitPack utility class contains useful functions for packing data into the smallest amount of usable bits. This is useful for keeping runtime and file memory usage low, especially when dealing with config/read-only data.

Do note that Little-endian byte order is assumed.

# BitUtils

The BitUtils contains extra functions for Bit operations.
Currently, this is mainly the Swap family of overloads for either swaping singular bits, or blocks of bits of length N. Both sets of overloads allow you to specify the bit indices of the swap points.

Do note that Little-endian byte order is assumed.