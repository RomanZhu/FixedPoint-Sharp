using Deterministic.FixedPoint;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests {
    public class FP_randomTests {
        [Test]
        public void BoolTest()
        {
            var random = new Random(645);
            random.NextBool().Should().Be(true);
            
            random.SetState(435);
            random.NextBool().Should().Be(false);
        }
        
        [Test]
        public void IntTest()
        {
            var random = new Random(645);
            random.NextInt().Should().Be(-1975191795);
            
            random.SetState(435);
            random.NextInt().Should().Be(-2030414680);
        }

        [Test]
        public void IntMaxTest()
        {
            var random = new Random(345345346);
            for (uint i = 5; i < 100; i++) {
                random.NextInt(30).Should().BeLessThan(31);
            }
        }
        
        [Test]
        public void IntMinMaxTest()
        {
            var random = new Random(345345346);
            for (var i = 0; i < 100; i++) { 
                random.NextInt(-30, 30).Should().BeInRange(-30, 30);
            }
        }
        
        [Test]
        public void FpTest()
        {
            var random = new Random(645);
            random.NextFp().value.Should().Be(2628L);
            
            random.SetState(435);
            random.NextFp().value.Should().Be(1786L);
        }
        
        [Test]
        public void FpMaxTest()
        {
            var random = new Random(345345346);
            for (uint i = 5; i < 100; i++) {
                var val = random.NextFp(fp._100);
                val.Should().BeLessThan(fp._100);
            }
        }
        
        [Test]
        public void FpMinMaxTest()
        {
            var random = new Random(345345346);
            for (uint i = 5; i < 100; i++) {
                var val = random.NextFp(fp._99, fp._100);
                val.Should().BeInRange(fp._99, fp._100);
            }
        }
    }
}