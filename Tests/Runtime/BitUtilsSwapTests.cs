using System;
using NUnit.Framework;
using Unity.Burst;
using Unity.Jobs;

namespace Hydrogen.Maths.Tests
{
    [TestFixture]
    public class BitUtilsSwapTests
    {
        
        
        [BurstCompile(CompileSynchronously = true)]
        struct TestJob : IJob
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
        static void Validate<T>(T test, T before, T after)
            where T : unmanaged, IEquatable<T>
        {
            Assert.That(test.Equals(after), $"There was an error swapping the bits! {test}");
            Assert.That(!test.Equals(before), $"Test didn't change from initial bits! {test}");
        }

        static void TestByteSimple()
        {
            var test = TestConstants.ByteBefore;

            for (byte i = 0; i < 4; i++)
            {
                var first = i;
                var second = (byte) (i + 4);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, TestConstants.ByteBefore, TestConstants.ByteAfter);
        }

        static void TestByteComplex()
        {
            var test = TestConstants.ByteBefore;
            
            for (byte i = 0; i < 4; i++)
            {
                var first = i;
                var second = (byte) (i + 4);

                test = BitUtils.Swap(test, first, second, 1);
            }

            Validate(test, TestConstants.ByteBefore, TestConstants.ByteAfter);

            test = BitUtils.Swap(test, 0, 4, 4);
            
            Validate(test, TestConstants.ByteAfter, TestConstants.ByteBefore);

            test = BitUtils.Swap(test, 0, 4, 2);
            test = BitUtils.Swap(test, 2, 6, 2);
            
            Validate(test, TestConstants.ByteBefore, TestConstants.ByteAfter);
        }

        static void TestUShortSimple()
        {
            var test = TestConstants.UShortBefore;

            for (byte i = 0; i < 8; i++)
            {
                var first = i;
                var second = (byte) (i + 8);

                test = BitUtils.Swap(test, first, second);
            }

            Validate(test, TestConstants.UShortBefore, TestConstants.UShortAfter);
        }

        static void TestUShortComplex()
        {
            var test = TestConstants.UShortBefore;
            
            for (byte i = 0; i < 8; i++)
            {
                var first = i;
                var second = (byte) (i + 8);

                test = BitUtils.Swap(test, first, second, 1);
            }

            Validate(test, TestConstants.UShortBefore, TestConstants.UShortAfter);

            test = BitUtils.Swap(test, 0, 8, 8);
            
            Validate(test, TestConstants.UShortAfter, TestConstants.UShortBefore);

            test = BitUtils.Swap(test, 0, 8, 4);
            test = BitUtils.Swap(test, 4, 12, 4);
            
            Validate(test, TestConstants.UShortBefore, TestConstants.UShortAfter);
        }

        static void TestUIntSimple()
        {
            var test = TestConstants.UIntBefore;

            for (byte i = 0; i < 16; i++)
            {
                var first = i;
                var second = (byte) (i + 16);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, TestConstants.UIntBefore, TestConstants.UIntAfter);
        }

        static void TestUIntComplex()
        {
            var test = TestConstants.UIntBefore;
            
            for (byte i = 0; i < 16; i++)
            {
                var first = i;
                var second = (byte) (i + 16);

                test = BitUtils.Swap(test, first, second, 1);
            }
            
            Validate(test, TestConstants.UIntBefore, TestConstants.UIntAfter);

            test = BitUtils.Swap(test, 0, 16, 16);
            
            Validate(test, TestConstants.UIntAfter, TestConstants.UIntBefore);

            test = BitUtils.Swap(test, 0, 16, 8);
            test = BitUtils.Swap(test, 8, 24, 8);
            
            Validate(test, TestConstants.UIntBefore, TestConstants.UIntAfter);
        }

        static void TestULongSimple()
        {
            var test = TestConstants.ULongBefore;

            for (byte i = 0; i < 32; i++)
            {
                var first = i;
                var second = (byte) (i + 32);

                test = BitUtils.Swap(test, first, second);
            }
            
            Validate(test, TestConstants.ULongBefore, TestConstants.ULongAfter);
        }

        static void TestULongComplex()
        {
            var test = TestConstants.ULongBefore;
            
            for (byte i = 0; i < 32; i++)
            {
                var first = i;
                var second = (byte) (i + 32);

                test = BitUtils.Swap(test, first, second, 1);
            }
            
            Validate(test, TestConstants.ULongBefore, TestConstants.ULongAfter);

            test = BitUtils.Swap(test, 0, 32, 32);
            
            Validate(test, TestConstants.ULongAfter, TestConstants.ULongBefore);

            test = BitUtils.Swap(test, 0, 32, 16);
            test = BitUtils.Swap(test, 16, 48, 16);
            
            Validate(test, TestConstants.ULongBefore, TestConstants.ULongAfter);
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
