using FixedPoint;
using FluentAssertions;
using NUnit.Framework;

namespace FPTesting
{
    public class fixmathTests
    {
        [Test]
        public void CosTest()
        {
            var value  = fp._0_25;
            var result = fixmath.Cos(value);
            result.AsFloat.Should().BeApproximately(0.969f, 0.001f);
        }
        
        [Test]
        public void SinTest()
        {
            var value  = fp._0_25;
            var result = fixmath.Sin(value);
            result.AsFloat.Should().BeApproximately(0.247f, 0.001f);
        }
        
        [Test]
        public void SinCosTest()
        {
            var value  = fp._0_25;
            fixmath.SinCos(value, out var sin, out var cos);
            sin.AsFloat.Should().BeApproximately(0.247f, 0.001f);
            cos.AsFloat.Should().BeApproximately(0.969f, 0.001f);
        }
        
        [Test]
        public void TanTest()
        {
            var value  = fp._0_25;
            fixmath.SinCosTan(value, out _, out _, out var result);
            result.AsFloat.Should().BeApproximately(0.255f, 0.001f);
        }
        
        [Test]
        public void SqrtTest()
        {
            var value = fp._5 * fp._5;
            var result = fixmath.Sqrt(value);
            result.Should().Be(fp._5);
        }
        
        [Test]
        public void SqrtFastTest()
        {
            var value  = fp._5 * fp._5;
            var result = fixmath.Sqrt(value);
            result.Should().Be(fp._5);
        }
        
        [Test]
        public void FloorTest()
        {
            var value  = fp._0_25;
            var result = fixmath.Floor(value);
            result.Should().Be(fp._0);
            
            result = fixmath.Floor(-value);
            result.Should().Be(-fp._1);
        }
        
        [Test]
        public void CeilTest()
        {
            var value  = fp._0_25;
            var result = fixmath.Ceil(value);
            result.Should().Be(fp._1);

            result = fixmath.Ceil(-fp._4 - fp._0_25);
            result.Should().Be(-fp._4);
        }
        
        [Test]
        public void RoundToIntTest()
        {
            var value = fp._5 + fp._0_25;
            var result = fixmath.RoundToInt(value);
            result.Should().Be(5);
            
            result = fixmath.RoundToInt(value + fp._0_33);
            result.Should().Be(6);
            
            result = fixmath.RoundToInt(value + fp._0_25);
            result.Should().Be(6);
        }
        
        [Test]
        public void MinTest()
        {
            var value1 = fp._0_25;
            var value2 = fp._0_33;
            var result = fixmath.Min(value1, value2);
            result.Should().Be(value1);
            
            result = fixmath.Min(-value1, -value2);
            result.Should().Be(-value2);
        }
        
        [Test]
        public void MaxTest()
        {
            var value1 = fp._0_25;
            var value2 = fp._0_33;
            var result = fixmath.Max(value1, value2);
            result.Should().Be(value2);
            
            result = fixmath.Max(-value1, -value2);
            result.Should().Be(-value1);
        }
        
        [Test]
        public void AbsTest()
        {
            var value  = fp._0_25;
            var result = fixmath.Abs(value);
            result.Should().Be(value);
            
            result = fixmath.Abs(-value);
            result.Should().Be(value);
        }
        
        [Test]
        public void ClampTest()
        {
            var result = fixmath.Clamp(fp._0_33, fp._0_25, fp._0_75);
            result.Should().Be(fp._0_33);
            
            result = fixmath.Clamp(fp._1, fp._0_33, fp._0_75);
            result.Should().Be(fp._0_75);
            
            result = fixmath.Clamp(fp._0, fp._0_33, fp._0_75);
            result.Should().Be(fp._0_33);
        }
        
        
        [Test]
        public void LerpTest()
        {
            var result = fixmath.Lerp(fp._2, fp._4, fp._0_25);
            result.Should().Be(fp._2 + fp._0_50);
            
            result = fixmath.Lerp(fp._2, fp._4, fp._0);
            result.Should().Be(fp._2);
            
            result = fixmath.Lerp(fp._2, fp._4, fp._1);
            result.Should().Be(fp._4);
            
            result = fixmath.Lerp(fp._2, fp._4, fp._0_50);
            result.Should().Be(fp._3);
        }

        [Test]
        public void SignTest()
        {
            var result = fixmath.Sign(fp._0_25);
            result.Should().Be(fp._1);
            
            result = fixmath.Sign(-fp._0_25);
            result.Should().Be(-fp._1);
        }
        
        [Test]
        public void IsOppositeSignTest()
        {
            var result = fixmath.IsOppositeSign(fp._0_25, -fp._0_20);
            result.Should().Be(true);
            
            result = fixmath.IsOppositeSign(fp._0_25, fp._0_20);
            result.Should().Be(false);            
            
            result = fixmath.IsOppositeSign(-fp._0_25, -fp._0_20);
            result.Should().Be(false);
        }

        [Test]
        public void Sin2Test() {
            fixmath.SinCosTan(fp._0_20, out var sin, out var cos, out var tan);
            var bbb = sin;
        }
    }
}