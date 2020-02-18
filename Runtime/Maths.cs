using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Hydrogen.Maths
{
    public class Maths
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int4x4 Select(int4x4 a, int4x4 b, bool4x4 c)
        {
            return new int4x4(
                math.select(a.c0, b.c0, c.c0),
                math.select(a.c1, b.c1, c.c1),
                math.select(a.c2, b.c2, c.c2),
                math.select(a.c3, b.c3, c.c3));
        }
    }
}
