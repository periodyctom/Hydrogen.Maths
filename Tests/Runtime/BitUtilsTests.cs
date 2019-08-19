using System;
using NUnit.Framework;
using Unity.Burst;
using Unity.Jobs;

namespace Hydrogen.Maths.Tests
{
    [TestFixture]
    public class BitUtilsTests
    {
        private const byte kByteBefore = 0x0F;
        private const byte kByteAfter = 0xF0;

        private const ushort kUShortBefore = 0x00FF;
        private const ushort kUShortAfter = 0xFF00;

        private const uint kUIntBefore = 0x0000FFFF;
        private const uint kUIntAfter = 0xFFFF0000;

        private const ulong kULongBefore = 0x00000000FFFFFFFF;
        private const ulong kULongAfter = 0xFFFFFFFF00000000;
        
        [BurstCompile(CompileSynchronously = true)]
        private struct TestJob : IJob
        {
            public void Execute()
            {
                TestByteSimple();
                TestByteComplex();
                TestUShortSimple();
                TestUShortComplex();
                TestUIntSimple();
                TestULongSimple();
                TestULongComplex();
            }
        }
        
        [BurstDiscard]
        private static void Validate<T>(T test, T before, T after)
            where T : unmanaged, IEquatable<T>
        {
            Assert.That(test.Equals(after), $"There was an error swapping the bits! {test}");
            Assert.That(!test.Equals(before), $"Test didn't change from initial bits! {test}");
        }

        private static void TestByteSimple()
        {
            byte test = kByteBefore;

            for (byte i = 0; i < 4; i++)
            {
                byte first = i;
                byte second = (byte) (i + 4);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, kByteBefore, kByteAfter);
        }

        private static void TestByteComplex()
        {
            byte test = kByteBefore;
            
            for (byte i = 0; i < 4; i++)
            {
                byte first = i;
                byte second = (byte) (i + 4);

                test = BitUtils.Swap(test, first, second, 1);
            }

            Validate(test, kByteBefore, kByteAfter);

            test = BitUtils.Swap(test, 0, 4, 4);
            
            Validate(test, kByteAfter, kByteBefore);

            test = BitUtils.Swap(test, 0, 4, 2);
            test = BitUtils.Swap(test, 2, 6, 2);
            
            Validate(test, kByteBefore, kByteAfter);
        }

        private static void TestUShortSimple()
        {
            ushort test = kUShortBefore;

            for (byte i = 0; i < 8; i++)
            {
                byte first = i;
                byte second = (byte) (i + 8);

                test = BitUtils.Swap(test, first, second);
            }

            Validate(test, kUShortBefore, kUShortAfter);
        }

        private static void TestUShortComplex()
        {
            ushort test = kUShortBefore;
            
            for (byte i = 0; i < 8; i++)
            {
                byte first = i;
                byte second = (byte) (i + 8);

                test = BitUtils.Swap(test, first, second, 1);
            }

            Validate(test, kUShortBefore, kUShortAfter);

            test = BitUtils.Swap(test, 0, 8, 8);
            
            Validate(test, kUShortAfter, kUShortBefore);

            test = BitUtils.Swap(test, 0, 8, 4);
            test = BitUtils.Swap(test, 4, 12, 4);
            
            Validate(test, kUShortBefore, kUShortAfter);
        }

        private static void TestUIntSimple()
        {
            uint test = kUIntBefore;

            for (byte i = 0; i < 16; i++)
            {
                byte first = i;
                byte second = (byte) (i + 16);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, kUIntBefore, kUIntAfter);
        }

        private static void TestUIntComplex()
        {
            uint test = kUIntBefore;
            
            for (byte i = 0; i < 16; i++)
            {
                byte first = i;
                byte second = (byte) (i + 16);

                test = BitUtils.Swap(test, first, second, 1);
            }
            
            Validate(test, kUIntBefore, kUIntAfter);

            test = BitUtils.Swap(test, 0, 16, 16);
            
            Validate(test, kUIntAfter, kUIntBefore);

            test = BitUtils.Swap(test, 0, 16, 8);
            test = BitUtils.Swap(test, 8, 24, 8);
            
            Validate(test, kUIntBefore, kUIntAfter);
        }

        private static void TestULongSimple()
        {
            ulong test = kULongBefore;

            for (byte i = 0; i < 32; i++)
            {
                byte first = i;
                byte second = (byte) (i + 32);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, kULongBefore, kULongAfter);
        }

        private static void TestULongComplex()
        {
            ulong test = kULongBefore;
            
            for (byte i = 0; i < 32; i++)
            {
                byte first = i;
                byte second = (byte) (i + 32);

                test = BitUtils.Swap(test, first, second, 1);
            }
            
            Validate(test, kULongBefore, kULongAfter);

            test = BitUtils.Swap(test, 0, 32, 32);
            
            Validate(test, kULongAfter, kULongBefore);

            test = BitUtils.Swap(test, 0, 32, 16);
            test = BitUtils.Swap(test, 16, 48, 16);
            
            Validate(test, kULongBefore, kULongAfter);
        }
        
        [Test]
        public void CanSwapByteSimple()
        {
            TestByteSimple();
        }

        [Test]
        public void CanSwapByteComplex()
        {
            TestByteComplex();
        }

        [Test]
        public void CanSwapUshortSimple()
        {
            TestUShortSimple();
        }

        [Test]
        public void CanSwapUShortComplex()
        {
            TestUShortComplex();
        }

        [Test]
        public void CanSwapUIntSimple()
        {
            TestUIntSimple();
        }

        [Test]
        public void CanSwapUIntComplex()
        {
            TestUIntComplex();
        }

        [Test]
        public void CanSwapULongSimple()
        {
            TestULongSimple();
        }

        [Test]
        public void CanSwapULongComplex()
        {
            TestULongComplex();
        }

        [Test]
        public void EnsureSwapWorksWithBurst()
        {
            new TestJob().Run();
            new TestJob().Schedule().Complete();
        }
    }
}
