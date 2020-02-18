using System;
using NUnit.Framework;
using Unity.Jobs;
using Unity.Burst;
// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local

namespace Hydrogen.Maths.Tests
{
    [TestFixture]
    public class BitPackTests
    {
        [BurstCompile(CompileSynchronously = true)]
        struct TestJob : IJob
        {
            public void Execute()
            {
                TestByteGetAndSet();
                TestUShortGetAndSet();
                TestUIntGetAndSet();
                TestULongGetAndSet();
            }
        }
        
        [BurstDiscard]
        static void Validate<T>(T low, T high, T expectedLow, T expectedHigh)
            where T : unmanaged, IEquatable<T>
        {
            Assert.That(low.Equals(expectedLow), $"Unexpected Value {low:x}, should be: {expectedLow:x}!");
            Assert.That(high.Equals(expectedHigh), $"Unexpected Value {high:x}, should be: {expectedHigh:x}!");
        }

        static void TestByteGetAndSet()
        {
            byte field = 0;

            const byte expectedLow = 0xA;
            const byte expectedHigh = 0xB;
            
            field = BitPack.Set(field, expectedLow, 4);
            field = BitPack.Set(field, expectedHigh, 4, 4);

            var subFieldLow = BitPack.Get(field, 4);
            var subFieldHigh = BitPack.Get(field, 4, 4); 
            
            Validate(subFieldLow, subFieldHigh, expectedLow, expectedHigh);
        }

        static void TestUShortGetAndSet()
        {
            ushort field = 0;

            const ushort expectedLow = 0xAA;
            const ushort expectedHigh = 0xBB;
            
            field = BitPack.Set(field, expectedLow, 8);
            field = BitPack.Set(field, expectedHigh, 8, 8);

            var subFieldLow = BitPack.Get(field, 8);
            var subFieldHigh = BitPack.Get(field, 8, 8);

            Validate(subFieldLow, subFieldHigh, expectedLow, expectedHigh);

            const ushort one = 1;
            const ushort subField = 0xA;
            
            field = 0;
            field = BitPack.Set(field, one, 1);
            field = BitPack.Set(field, subField, 8, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 8, 1);
            
            Validate(subFieldLow, subFieldHigh, one, subField);
        }

        static void TestUIntGetAndSet()
        {
            uint field = 0;

            const uint expectedLow = 0xAAAAu;
            const uint expectedHigh = 0xBBBBu;
            
            field = BitPack.Set(field, expectedLow, 16);
            field = BitPack.Set(field, expectedHigh, 16, 16);

            var subFieldLow = BitPack.Get(field, 16);
            var subFieldHigh = BitPack.Get(field, 16, 16); 
            
            Validate(subFieldLow, subFieldHigh, expectedLow, expectedHigh);
            
            const uint one = 1u;
            const uint subField = 0xAAu;
            
            field = 0;
            field = BitPack.Set(field, one, 1);
            field = BitPack.Set(field, subField, 8, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 8, 1);
            
            Validate(subFieldLow, subFieldHigh, one, subField);
        }

        static void TestULongGetAndSet()
        {
            ulong field = 0;

            const ulong expectedLow = 0xAAAA_AAAAul;
            const ulong expectedHigh = 0xBBBB_BBBBul;

            field = BitPack.Set(field, expectedLow, 32);
            field = BitPack.Set(field, expectedHigh, 32, 32);

            var subFieldLow = BitPack.Get(field, 32);
            var subFieldHigh = BitPack.Get(field, 32, 32); 
            
            Validate(subFieldLow, subFieldHigh, expectedLow, expectedHigh);

            const ulong one = 1ul;
            const ulong subField = 0xAAAAul;
            
            field = 0;
            field = BitPack.Set(field, one, 1);
            field = BitPack.Set(field, subField, 16, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 16, 1);
            
            Validate(subFieldLow, subFieldHigh, one, subField);
        }

        [Test]
        public void EnsureBitPackWorksInBurst()
        {
            new TestJob().Run();
            new TestJob().Schedule().Complete();
        }

        [Test]
        public void EnsureByteMasksAreCorrectLengths()
        {
            Assert.That(BitPack.ByteMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.ByteMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.ByteMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.ByteMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.ByteMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
        }

        [Test]
        public void EnsureUShortMasksAreCorrectLengths()
        {
            Assert.That(BitPack.UShortMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.UShortMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.UShortMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.UShortMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.UShortMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.UShortMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.UShortMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
        }

        [Test]
        public void EnsureUIntMasksAreCorrectLengths()
        {
            Assert.That(BitPack.UIntMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.UIntMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.UIntMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.UIntMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.UIntMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.UIntMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.UIntMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
            
            Assert.That(BitPack.UIntMask(20) == 0xF_FFFF, "A mask of size 20 should be 0xF_FFFF!");
            Assert.That(BitPack.UIntMask(24) == 0xFF_FFFF, "A mask of size 24 should be 0xFF_FFFF!");
            Assert.That(BitPack.UIntMask(28) == 0xFFF_FFFF, "A mask of size 28 should be 0xFFF_FFFF!");
            Assert.That(BitPack.UIntMask(32) == 0xFFFF_FFFF, "A mask of size 32 should be 0xFFFF_FFFF");
        }

        [Test]
        public void EnsureULongMasksAreCorrectLengths()
        {
            Assert.That(BitPack.ULongMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.ULongMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.ULongMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.ULongMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.ULongMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.ULongMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.ULongMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
            
            Assert.That(BitPack.ULongMask(20) == 0xF_FFFF, "A mask of size 20 should be 0xF_FFFF!");
            Assert.That(BitPack.ULongMask(24) == 0xFF_FFFF, "A mask of size 24 should be 0xFF_FFFF!");
            Assert.That(BitPack.ULongMask(28) == 0xFFF_FFFF, "A mask of size 28 should be 0xFFF_FFFF!");
            Assert.That(BitPack.ULongMask(32) == 0xFFFF_FFFF, "A mask of size 32 should be 0xFFFF_FFFF");
            
            Assert.That(BitPack.ULongMask(36) == 0xF_FFFF_FFFF, "A mask of size 36 should be 0xF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(40) == 0xFF_FFFF_FFFF, "A mask of size 40 should be 0xFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(44) == 0xFFF_FFFF_FFFF, "A mask of size 44 should be 0xFFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(48) == 0xFFFF_FFFF_FFFF, "A mask of size 48 should be 0xFFFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(52) == 0xF_FFFF_FFFF_FFFF, "A mask of size 52 should be 0xF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(56) == 0xFF_FFFF_FFFF_FFFF, "A mask of size 56 should be 0xFF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(60) == 0xFFF_FFFF_FFFF_FFFF, "A mask of size 60 should be 0xFFF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.ULongMask(64) == 0xFFFF_FFFF_FFFF_FFFF, "A mask of size 64 should be 0xFFFF_FFFF_FFFF_FFFF!");
        }

        [Test]
        public void DoesByteGetAndSetWorkCorrectly()
        {
            TestByteGetAndSet();
        }

        [Test]
        public void CanCarelessUserCorruptByte()
        {
            byte field = 0;

            field = BitPack.Set(field, 0xA, 4);
            field = BitPack.Set(field, 0xB, 4, 4);

            // oops!
            field = BitPack.Set(field, 0x3, 6, 2);
            
            var subFieldLow = BitPack.Get(field, 4);
            var subFieldHigh = BitPack.Get(field, 4, 4);
            var subFieldOops = BitPack.Get(field, 6, 2);
            
            Assert.That(subFieldLow != 0xA, $"Unexpected Value {subFieldLow:x1}, should NOT be: 0xA!");
            Assert.That(subFieldHigh != 0xB, $"Unexpected Value {subFieldHigh:x1}, should NOT be: 0xB!");
            Assert.That(subFieldOops == 0x3, $"Unexpected Value {subFieldOops:x1}, should be: 0x3!");
        }

        [Test]
        public void DoesUShortGetAndSetWorkCorrectly()
        {
            TestUShortGetAndSet();
        }
        
        [Test]
        public void CanCarelessUserCorruptUShort()
        {
            ushort field = 0;

            field = BitPack.Set(field, 0xAA, 8);
            field = BitPack.Set(field, 0xBB, 8, 8);

            // oops!
            field = BitPack.Set(field, 0xAB, 8, 4);
            
            var subFieldLow = BitPack.Get(field, 8);
            var subFieldHigh = BitPack.Get(field, 8, 8);
            var subFieldOops = BitPack.Get(field, 8, 4);
            
            Assert.That(subFieldLow != 0xAA, $"Unexpected Value {subFieldLow:x2}, should NOT be: 0xAA!");
            Assert.That(subFieldHigh != 0xBB, $"Unexpected Value {subFieldHigh:x2}, should NOT be: 0xBB!");
            Assert.That(subFieldOops == 0xAB, $"Unexpected Value {subFieldOops:x2}, should be: AB!");
        }
        
        [Test]
        public void DoesUIntGetAndSetWorkCorrectly()
        {
            TestUIntGetAndSet();
        }
        
        [Test]
        public void CanCarelessUserCorruptUInt()
        {
            uint field = 0;

            field = BitPack.Set(field, 0xAAAA, 16);
            field = BitPack.Set(field, 0xBBBB, 16, 16);

            // oops!
            field = BitPack.Set(field, 0xABAB, 16, 8);
            
            var subFieldLow = BitPack.Get(field, 16);
            var subFieldHigh = BitPack.Get(field, 16, 16);
            var subFieldOops = BitPack.Get(field, 16, 8);
            
            Assert.That(subFieldLow != 0xAAAA, $"Unexpected Value {subFieldLow:x4}, should NOT be: 0xAAAA!");
            Assert.That(subFieldHigh != 0xBBBB, $"Unexpected Value {subFieldHigh:x4}, should NOT be: 0xBBBB!");
            Assert.That(subFieldOops == 0xABAB, $"Unexpected Value {subFieldOops:x4}, should be: ABAB!");
        }
        
        [Test]
        public void DoesULongGetAndSetWorkCorrectly()
        {
            TestULongGetAndSet();
        }
        
        [Test]
        public void CanCarelessUserCorruptULong()
        {
            ulong field = 0;

            field = BitPack.Set(field, 0xAAAA_AAAA, 32);
            field = BitPack.Set(field, 0xBBBB_BBBB, 32, 32);

            // oops!
            field = BitPack.Set(field, 0xABAB_ABAB, 32, 16);
            
            var subFieldLow = BitPack.Get(field, 32);
            var subFieldHigh = BitPack.Get(field, 32, 32);
            var subFieldOops = BitPack.Get(field, 32, 16);
            
            Assert.That(subFieldLow != 0xAAAA_AAAA, $"Unexpected Value {subFieldLow:x8}, should NOT be: 0xAAAAAAAA!");
            Assert.That(subFieldHigh != 0xBBBB_BBBB, $"Unexpected Value {subFieldHigh:x8}, should NOT be: 0xBBBBBBBB!");
            Assert.That(subFieldOops == 0xABAB_ABAB, $"Unexpected Value {subFieldOops:x8}, should be: ABABABAB!");
        }
    }
}
