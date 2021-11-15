using System;
using System.Globalization;
using Deterministic.FixedPoint;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    public class FP_Tests {
        private const string PRECISION_FORMAT = "F4";
        private const int    ROUNDING         = 5;
        
        [Test]
        public void ToStringTest()
        {
            var originalFp = fp._1 - fp._0_01;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("0.9900");
            
            originalFp = fp._1 - fp._0_01 *fp._0_01;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("0.9999");
            
            originalFp = fp._1;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("1.0000");
            
            originalFp = fp._1 + fp._0_01;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("1.0100");
            
            originalFp = fp._1 + fp._0_01 * fp._0_01;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("1.0001");
            
            originalFp = fp._0_01;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("0.0100");
            
            originalFp = fp._0_50;
            Math.Round(originalFp.AsDouble, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture).Should().Be("0.5000");
        }
        
        [Test]
        public void SlowStringParsingTest() {
            fp.Parse("5").AsFloatRounded.Should().BeApproximately(5, 0.0001f);
            fp.Parse("5.").AsFloatRounded.Should().BeApproximately(5, 0.0001f);
            fp.Parse(".1").AsFloatRounded.Should().BeApproximately(0.1f, 0.0001f);
            fp.Parse("5.1").AsFloatRounded.Should().BeApproximately(5.1f, 0.0001f);
            fp.Parse("5.45111111").AsFloatRounded.Should().BeApproximately(5.4511f, 0.0001f);
            
            fp.Parse("-5").AsFloatRounded.Should().BeApproximately(-5, 0.0001f);
            fp.Parse("-5.").AsFloatRounded.Should().BeApproximately(-5, 0.0001f);
            fp.Parse("-.1").AsFloatRounded.Should().BeApproximately(-0.1f, 0.0001f);
            fp.Parse("-5.1").AsFloatRounded.Should().BeApproximately(-5.1f, 0.0001f);
        }

        [Test]
        public void SlowFromToStringTest() {
            var from  = -100.0;
            var to    = 100.0;
            var delta = 0.0001;

            for (var v = from; v < to; v += delta) {
                var parsedString       = Math.Round(v, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture);
                var parsedFp           = fp.Parse(parsedString);
                var convertedBackFloat = parsedFp.AsDouble;
                convertedBackFloat.Should().BeApproximately(v, 0.0001);
            }
        }
        
        [Test]
        public void FromToStringTest() {
            var from  = -100.0;
            var to    = 100.0;
            var delta = 0.0001;

            for (var v = from; v < to; v += delta) {
                var parsedString       = Math.Round(v, ROUNDING).ToString(PRECISION_FORMAT, CultureInfo.InvariantCulture);
                var parsedFp           = fp.ParseUnsafe(parsedString);
                var convertedBackFloat = parsedFp.AsDouble;
                convertedBackFloat.Should().BeApproximately(v, 0.0001);
            }
        }

        [Test]
        public void FromFloatTest() {
            var from  = -100.0;
            var to    = 100.0;
            var delta = 0.0001;

            for (var v = from; v < to; v += delta) {
                var parsedFp = fp.ParseUnsafe((float)v);
                var convertedBackFloat = parsedFp.AsDouble;
                convertedBackFloat.Should().BeApproximately(v, 0.0001);
            }
        }

        [Test]
        public void AsIntTest() {
            var from  = -65000f;
            var to    = 65000;
            var delta = 0.1f;

            for (float v = from; v < to; v += delta) {
                var originalInt = (int) Math.Floor(v);
                var parsedFp           = fp.ParseUnsafe(v);
                var convertedBack = parsedFp.AsInt;
                convertedBack.Should().Be(originalInt);
            }
        }
    }
}