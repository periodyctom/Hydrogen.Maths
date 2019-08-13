using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Unity.Jobs;
using Unity.Burst;

// ReSharper disable once CheckNamespace
namespace Hydrogen.Maths.Tests
{
    public class BitPackTests
    {
        [BurstCompile(CompileSynchronously = true)]
        [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local")]
        struct TestJob : IJob
        {
            public void Execute()
            {
                // byte
                byte field0 = 0;

                field0 = BitPack.Set(field0, 0xA, 4);
                field0 = BitPack.Set(field0, 0xB, 4, 4);

                Validate(BitPack.Get(field0, 4), BitPack.Get(field0, 4, 4), 0xA, 0xB);
                
                //ushort
                ushort field1 = 0;

                field1 = BitPack.Set(field1, 0xAA, 8);
                field1 = BitPack.Set(field1, 0xBB, 8, 8);
                
                Validate(BitPack.Get(field1, 8), BitPack.Get(field1, 8, 8), 0xAA, 0xBB);
                
                field1 = 0;
                field1 = BitPack.Set(field1, 1, 1);
                field1 = BitPack.Set(field1, 0xAA, 8, 1);
                
                Validate(BitPack.Get(field1, 1), BitPack.Get(field1, 8, 1), 1, 0xAA);
                
                //uint
                uint field2 = 0;

                field2 = BitPack.Set(field2, 0xAAAA, 16);
                field2 = BitPack.Set(field2, 0xBBBB, 16, 16);
                Validate(BitPack.Get(field2, 16), BitPack.Get(field2, 16, 16), 0xAAAA, 0xBBBB);
                
                field2 = 0;
                field2 = BitPack.Set(field2, 1, 1);
                field2 = BitPack.Set(field2, 0xAA, 8, 1);
                Validate(BitPack.Get(field2, 1), BitPack.Get(field2, 8, 1), 1, 0xAA);
                
                //ulong
                ulong field3 = 0;

                field3 = BitPack.Set(field3, 0xAAAA_AAAA, 32);
                field3 = BitPack.Set(field3, 0xBBBB_BBBB, 32, 32);
                Validate(BitPack.Get(field3, 32), BitPack.Get(field3, 32, 32), 0xAAAA_AAAA, 0xBBBB_BBBB);
            
                field3 = 0;
                field3 = BitPack.Set(field3, 1, 1);
                field3 = BitPack.Set(field3, 0xAAAA, 16, 1);
                Validate( BitPack.Get(field3, 1), BitPack.Get(field3, 16, 1), 1, 0xAAAA);
            }
            
            [BurstDiscard]
            public void Validate(byte subFieldLow, byte subFieldHigh, byte expectedLow, byte expectedHigh)
            {
                Assert.That(subFieldLow == expectedLow, $"Unexpected Value {subFieldLow:x1}, should be: {expectedHigh:x1}!");
                Assert.That(subFieldHigh == expectedHigh, $"Unexpected Value {subFieldHigh:x1}, should be: {expectedLow:x1}!");
            }

            [BurstDiscard]
            public void Validate(ushort subFieldLow, ushort subFieldHigh, ushort expectedLow, ushort expectedHigh)
            {
                Assert.That(subFieldLow == expectedLow, $"Unexpected Value {subFieldLow:x2}, should be: {expectedHigh:x2}!");
                Assert.That(subFieldHigh == expectedHigh, $"Unexpected Value {subFieldHigh:x2}, should be: {expectedLow:x2}!");
            }
            
            [BurstDiscard]
            public void Validate(uint subFieldLow, uint subFieldHigh, uint expectedLow, uint expectedHigh)
            {
                Assert.That(subFieldLow == expectedLow, $"Unexpected Value {subFieldLow:x4}, should be: {expectedHigh:x4}!");
                Assert.That(subFieldHigh == expectedHigh, $"Unexpected Value {subFieldHigh:x4}, should be: {expectedLow:x4}!");
            }
            
            [BurstDiscard]
            public void Validate(ulong subFieldLow, ulong subFieldHigh, ulong expectedLow, ulong expectedHigh)
            {
                Assert.That(subFieldLow == expectedLow, $"Unexpected Value {subFieldLow:x8}, should be: {expectedHigh:x8}!");
                Assert.That(subFieldHigh == expectedHigh, $"Unexpected Value {subFieldHigh:x8}, should be: {expectedLow:x8}!");
            }
        }

        [Test]
        public void EnsureAllKindsWorkInBurst()
        {
            new TestJob().Run();
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
        public void EnsureUshortMasksAreCorrectLengths()
        {
            Assert.That(BitPack.UshortMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.UshortMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.UshortMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.UshortMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.UshortMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.UshortMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.UshortMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
        }
        
        [Test]
        public void EnsureUintMasksAreCorrectLengths()
        {
            Assert.That(BitPack.UintMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.UintMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.UintMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.UintMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.UintMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.UintMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.UintMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
            
            Assert.That(BitPack.UintMask(20) == 0xF_FFFF, "A mask of size 20 should be 0xF_FFFF!");
            Assert.That(BitPack.UintMask(24) == 0xFF_FFFF, "A mask of size 24 should be 0xFF_FFFF!");
            Assert.That(BitPack.UintMask(28) == 0xFFF_FFFF, "A mask of size 28 should be 0xFFF_FFFF!");
            Assert.That(BitPack.UintMask(32) == 0xFFFF_FFFF, "A mask of size 32 should be 0xFFFF_FFFF");
        }
        
        [Test]
        public void EnsureUlongMasksAreCorrectLengths()
        {
            Assert.That(BitPack.UlongMask(0) == 0x0, "A mask of size 0 should be 0x0!");
            Assert.That(BitPack.UlongMask(1) == 0x1, "A mask of size 1 should be 0x1!");
            Assert.That(BitPack.UlongMask(2) == 0x3, "A mask of size 2 should be 0x3!");
            Assert.That(BitPack.UlongMask(4) == 0xF, "A mask of size 4 should be 0xF!");
            Assert.That(BitPack.UlongMask(8) == 0xFF, "A mask of size 8 should be 0xFF!");
            
            Assert.That(BitPack.UlongMask(12) == 0xFFF, "A mask of size 12 should be 0xFFF!");
            Assert.That(BitPack.UlongMask(16) == 0xFFFF, "A mask of size 16 should be 0xFFFF!");
            
            Assert.That(BitPack.UlongMask(20) == 0xF_FFFF, "A mask of size 20 should be 0xF_FFFF!");
            Assert.That(BitPack.UlongMask(24) == 0xFF_FFFF, "A mask of size 24 should be 0xFF_FFFF!");
            Assert.That(BitPack.UlongMask(28) == 0xFFF_FFFF, "A mask of size 28 should be 0xFFF_FFFF!");
            Assert.That(BitPack.UlongMask(32) == 0xFFFF_FFFF, "A mask of size 32 should be 0xFFFF_FFFF");
            
            Assert.That(BitPack.UlongMask(36) == 0xF_FFFF_FFFF, "A mask of size 36 should be 0xF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(40) == 0xFF_FFFF_FFFF, "A mask of size 40 should be 0xFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(44) == 0xFFF_FFFF_FFFF, "A mask of size 44 should be 0xFFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(48) == 0xFFFF_FFFF_FFFF, "A mask of size 48 should be 0xFFFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(52) == 0xF_FFFF_FFFF_FFFF, "A mask of size 52 should be 0xF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(56) == 0xFF_FFFF_FFFF_FFFF, "A mask of size 56 should be 0xFF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(60) == 0xFFF_FFFF_FFFF_FFFF, "A mask of size 60 should be 0xFFF_FFFF_FFFF_FFFF!");
            Assert.That(BitPack.UlongMask(64) == 0xFFFF_FFFF_FFFF_FFFF, "A mask of size 64 should be 0xFFFF_FFFF_FFFF_FFFF!");
        }

        [Test]
        public void DoesByteGetAndSetWorkCorrectly()
        {
            byte field = 0;

            field = BitPack.Set(field, 0xA, 4);
            field = BitPack.Set(field, 0xB, 4, 4);

            byte subFieldLow = BitPack.Get(field, 4);
            byte subFieldHigh = BitPack.Get(field, 4, 4); 
            
            Assert.That(subFieldLow == 0xA, $"Unexpected Value {subFieldLow:x1}, should be: 0xA!");
            Assert.That(subFieldHigh == 0xB, $"Unexpected Value {subFieldHigh:x1}, should be: 0xB!");
        }
        
        [Test]
        public void CanCarelessUserCorruptByte()
        {
            byte field = 0;

            field = BitPack.Set(field, 0xA, 4);
            field = BitPack.Set(field, 0xB, 4, 4);

            // oops!
            field = BitPack.Set(field, 0x3, 6, 2);
            
            byte subFieldLow = BitPack.Get(field, 4);
            byte subFieldHigh = BitPack.Get(field, 4, 4);
            byte subFieldOops = BitPack.Get(field, 6, 2);
            
            Assert.That(subFieldLow != 0xA, $"Unexpected Value {subFieldLow:x1}, should NOT be: 0xA!");
            Assert.That(subFieldHigh != 0xB, $"Unexpected Value {subFieldHigh:x1}, should NOT be: 0xB!");
            Assert.That(subFieldOops == 0x3, $"Unexpected Value {subFieldOops:x1}, should be: 0x3!");
        }
        
        [Test]
        public void DoesUshortGetAndSetWorkCorrectly()
        {
            ushort field = 0;

            field = BitPack.Set(field, 0xAA, 8);
            field = BitPack.Set(field, 0xBB, 8, 8);

            ushort subFieldLow = BitPack.Get(field, 8);
            ushort subFieldHigh = BitPack.Get(field, 8, 8); 
            
            Assert.That(subFieldLow == 0xAA, $"Unexpected Value {subFieldLow:x2}, should be: 0xAA!");
            Assert.That(subFieldHigh == 0xBB, $"Unexpected Value {subFieldHigh:x2}, should be: 0xBB!");
            
            field = 0;
            field = BitPack.Set(field, 1, 1);
            field = BitPack.Set(field, 0xAA, 8, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 8, 1);
            
            Assert.That(subFieldLow == 1, $"Unexpected Value {subFieldLow:x2}, should be: 1!");
            Assert.That(subFieldHigh == 0xAA, $"Unexpected Value {subFieldHigh:x2}, should be: 0xAA!");
        }
        
        [Test]
        public void CanCarelessUserCorruptUshort()
        {
            ushort field = 0;

            field = BitPack.Set(field, 0xAA, 8);
            field = BitPack.Set(field, 0xBB, 8, 8);

            // oops!
            field = BitPack.Set(field, 0xAB, 8, 4);
            
            ushort subFieldLow = BitPack.Get(field, 8);
            ushort subFieldHigh = BitPack.Get(field, 8, 8);
            ushort subFieldOops = BitPack.Get(field, 8, 4);
            
            Assert.That(subFieldLow != 0xAA, $"Unexpected Value {subFieldLow:x2}, should NOT be: 0xAA!");
            Assert.That(subFieldHigh != 0xBB, $"Unexpected Value {subFieldHigh:x2}, should NOT be: 0xBB!");
            Assert.That(subFieldOops == 0xAB, $"Unexpected Value {subFieldOops:x2}, should be: AB!");
        }
        
        [Test]
        public void DoesUintGetAndSetWorkCorrectly()
        {
            uint field = 0;

            field = BitPack.Set(field, 0xAAAA, 16);
            field = BitPack.Set(field, 0xBBBB, 16, 16);

            uint subFieldLow = BitPack.Get(field, 16);
            uint subFieldHigh = BitPack.Get(field, 16, 16); 
            
            Assert.That(subFieldLow == 0xAAAA, $"Unexpected Value {subFieldLow:x4}, should be: 0xAAAA!");
            Assert.That(subFieldHigh == 0xBBBB, $"Unexpected Value {subFieldHigh:x4}, should be: 0xBBBB!");
            
            field = 0;
            field = BitPack.Set(field, 1, 1);
            field = BitPack.Set(field, 0xAA, 8, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 8, 1);
            
            Assert.That(subFieldLow == 1, $"Unexpected Value {subFieldLow:x4}, should be: 1!");
            Assert.That(subFieldHigh == 0xAA, $"Unexpected Value {subFieldHigh:x4}, should be: 0xAA!");
        }
        
        [Test]
        public void CanCarelessUserCorruptUint()
        {
            uint field = 0;

            field = BitPack.Set(field, 0xAAAA, 16);
            field = BitPack.Set(field, 0xBBBB, 16, 16);

            // oops!
            field = BitPack.Set(field, 0xABAB, 16, 8);
            
            uint subFieldLow = BitPack.Get(field, 16);
            uint subFieldHigh = BitPack.Get(field, 16, 16);
            uint subFieldOops = BitPack.Get(field, 16, 8);
            
            Assert.That(subFieldLow != 0xAAAA, $"Unexpected Value {subFieldLow:x4}, should NOT be: 0xAAAA!");
            Assert.That(subFieldHigh != 0xBBBB, $"Unexpected Value {subFieldHigh:x4}, should NOT be: 0xBBBB!");
            Assert.That(subFieldOops == 0xABAB, $"Unexpected Value {subFieldOops:x4}, should be: ABAB!");
        }
        
        [Test]
        public void DoesUlongGetAndSetWorkCorrectly()
        {
            ulong field = 0;

            field = BitPack.Set(field, 0xAAAA_AAAA, 32);
            field = BitPack.Set(field, 0xBBBB_BBBB, 32, 32);

            ulong subFieldLow = BitPack.Get(field, 32);
            ulong subFieldHigh = BitPack.Get(field, 32, 32); 
            
            Assert.That(subFieldLow == 0xAAAA_AAAA, $"Unexpected Value {subFieldLow:x8}, should be: 0xAAAAAAAA!");
            Assert.That(subFieldHigh == 0xBBBB_BBBB, $"Unexpected Value {subFieldHigh:x8}, should be: 0xBBBBBBBB!");
            
            field = 0;
            field = BitPack.Set(field, 1, 1);
            field = BitPack.Set(field, 0xAAAA, 16, 1);
            
            subFieldLow = BitPack.Get(field, 1);
            subFieldHigh = BitPack.Get(field, 16, 1);
            
            Assert.That(subFieldLow == 1, $"Unexpected Value {subFieldLow:x8}, should be: 1!");
            Assert.That(subFieldHigh == 0xAAAA, $"Unexpected Value {subFieldHigh:x8}, should be: 0xAAAA!");
        }
        
        [Test]
        public void CanCarelessUserCorruptUlong()
        {
            ulong field = 0;

            field = BitPack.Set(field, 0xAAAA_AAAA, 32);
            field = BitPack.Set(field, 0xBBBB_BBBB, 32, 32);

            // oops!
            field = BitPack.Set(field, 0xABAB_ABAB, 32, 16);
            
            ulong subFieldLow = BitPack.Get(field, 32);
            ulong subFieldHigh = BitPack.Get(field, 32, 32);
            ulong subFieldOops = BitPack.Get(field, 32, 16);
            
            Assert.That(subFieldLow != 0xAAAA_AAAA, $"Unexpected Value {subFieldLow:x8}, should NOT be: 0xAAAAAAAA!");
            Assert.That(subFieldHigh != 0xBBBB_BBBB, $"Unexpected Value {subFieldHigh:x8}, should NOT be: 0xBBBBBBBB!");
            Assert.That(subFieldOops == 0xABAB_ABAB, $"Unexpected Value {subFieldOops:x8}, should be: ABABABAB!");
        }
    }
}
