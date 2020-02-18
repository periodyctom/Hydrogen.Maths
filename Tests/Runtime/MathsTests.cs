using System;
using NUnit.Framework;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;

namespace Hydrogen.Maths.Tests
{
    [TestFixture]
    public class MathsTests
    {
        static void TestInt4x4_Select_WorksCorrectly()
        {
            var zero = int4x4.zero;
            var identity = int4x4.identity;
            var one = (int4x4)1;
            var zebraLoInts = new int4x4(1, 0, 1, 0);
            var zebraHiInts = new int4x4(0, 1, 0, 1);

            var zebraLoIdentity = new int4x4(new int4(1, 0, 0, 0), 0, new int4(0, 0, 1, 0), 0);
            var zebraHiIdentity = new int4x4(0, new int4(0, 1, 0, 0), 0, new int4(0, 0, 0, 1));

            var t = new bool4x4(true);
            var f = new bool4x4(false);
            
            var zebraLo = new bool4x4(true, false, true, false);
            var zebraHi = new bool4x4(false, true, false, true);
            
            Validate(Maths.Select(zero, one, f), zero);
            Validate(Maths.Select(zero, one, t), one);
            Validate(Maths.Select(zero, one, zebraLo), zebraLoInts);
            Validate(Maths.Select(zero, one, zebraHi), zebraHiInts);
            
            Validate(Maths.Select(zero, identity, f), zero);
            Validate(Maths.Select(zero, identity, t), identity);
            Validate(Maths.Select(zero, identity, zebraLo), zebraLoIdentity);
            Validate(Maths.Select(zero, identity, zebraHi), zebraHiIdentity);
        }

        [BurstDiscard]
        static void Validate(int4x4 test, int4x4 control)
        {
            Assert.That(test.Equals(control), $"There was an error in selecting the ints! {test} {control}");
        }
        
        [Test]
        public void Int4x4_Select_WorksCorrectly()
        {
            TestInt4x4_Select_WorksCorrectly();
        }

        [Test]
        public void Int4x4_Select_WorksCorrectly_WithBurst()
        {
            new TestJob().Run();
            new TestJob().Schedule().Complete();
        }
        
        [BurstCompile]
        struct TestJob : IJob
        {
            public void Execute() => TestInt4x4_Select_WorksCorrectly();
        }
    }
}
