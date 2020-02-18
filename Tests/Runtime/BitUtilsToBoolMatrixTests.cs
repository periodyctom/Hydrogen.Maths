using NUnit.Framework;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Hydrogen.Maths.Tests
{
    [TestFixture]
    public class BitUtilsToBoolMatrixTests
    {
        static readonly bool4x2 k_ByteLow = new bool4x2(true, false);
        static readonly bool4x2 k_ByteHi = new bool4x2(false, true);
        static readonly bool4x2 k_ByteMid = new bool4x2(new bool4(false, false, true, true), new bool4(true, true, false, false));
        static readonly bool4x2 k_ByteZebraLo = new bool4x2(new bool4(true, true, false, false), new bool4(true, true, false, false));
        static readonly bool4x2 k_ByteZebraHi = new bool4x2(new bool4(false, false, true, true), new bool4(false, false, true, true));
        
        static readonly bool4x4 k_UShortLow = new bool4x4(true, true, false, false);
        static readonly bool4x4 k_UShortHi = new bool4x4(false, false, true, true);
        static readonly bool4x4 k_UShortMid = new bool4x4(false, true, true, false);
        static readonly bool4x4 k_UShortZebraLo = new bool4x4(true, false, true, false);
        static readonly bool4x4 k_UShortZebraHi = new bool4x4(false, true, false, true);

        [BurstDiscard]
        static void Validate(bool4x2 converted, bool4x2 control)
        {
            Assert.That(converted.Equals(control), $"There was an error in converting the bits! {converted} {control}");
        }

        [BurstDiscard]
        static void Validate(bool4x4 converted, bool4x4 control)
        {
            Assert.That(converted.Equals(control), $"There was an error in converting the bits! {converted} {control}");
        }

        static void TestByte()
        {
            const byte lo = 0x0F;
            const byte hi = 0xF0;
            const byte mid = 0b00111100;
            const byte zebraLo = 0b00110011;
            const byte zebraHi = 0b11001100;

            Validate(lo.ToBool4x2(), k_ByteLow);
            Validate(hi.ToBool4x2(), k_ByteHi);
            Validate(mid.ToBool4x2(), k_ByteMid);
            Validate(zebraLo.ToBool4x2(), k_ByteZebraLo);
            Validate(zebraHi.ToBool4x2(), k_ByteZebraHi);
        }

        static void TestUShort()
        {
            const ushort lo = 0x00FF;
            const ushort hi = 0xFF00;
            const ushort mid = 0x0FF0;
            const ushort zebraLo = 0x0F0F;
            const ushort zebraHi = 0xF0F0;

            Validate(lo.ToBool4x4(), k_UShortLow);
            Validate(hi.ToBool4x4(), k_UShortHi);
            Validate(mid.ToBool4x4(), k_UShortMid);
            Validate(zebraLo.ToBool4x4(), k_UShortZebraLo);
            Validate(zebraHi.ToBool4x4(), k_UShortZebraHi);
        }
        
        
        [Test]
        public void ByteToBool4x2_ConvertsCorrectly()
        {
            TestByte();
        }

        [Test]
        public void UShortToBool4x4_ConvertsCorrectly()
        {
            TestUShort();
        }

        [Test]
        public void ToBool4xX_WorksWithBurst()
        {
            new TestJob().Run();
            new TestJob().Schedule().Complete();
        }

        [BurstCompile]
        struct TestJob : IJob
        {
            public void Execute()
            {
                TestByte();
                TestUShort();
            }
        }
    }
}
