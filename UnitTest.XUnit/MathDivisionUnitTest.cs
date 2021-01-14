using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace UnitTest.XUnit
{
    public class MathDivisionUnitTest : IClassFixture<MathDivisionFixture>
    {

        private MathDivisionFixture _mathDivisionFixture;
        public MathDivisionUnitTest(MathDivisionFixture mathDivisionFixture)
        {
            _mathDivisionFixture = mathDivisionFixture;
        }

        [Theory]
        [InlineData(4, 2)]
        [InlineData(0, 2)]

        public void Division_Positive(double a, double b)
        {
            Assert.True(_mathDivisionFixture.MathService.Division(a, b) == a / b);
        }

        [Theory]
        [InlineData(-4, -2)]
        [InlineData(0, -2)]
        public void Division_TwoNegative(double a, double b)
        {
            Assert.True(_mathDivisionFixture.MathService.Division(a, b) == a / b);
        }
        [Theory]
        [InlineData(-4, 2)]
        [InlineData(4, -2)]
        public void Division_NegativeWithPositive(double a, double b)
        {
            Assert.True(_mathDivisionFixture.MathService.Division(a, b) == a / b);
        }

        [Theory]
        [InlineData(-4, 0)]
        [InlineData(4, 0)]
        [InlineData(0, 0)]
        public void Division_Zero_ThrowsDivideByZeroException(double a, double b)
        {
            var ex = Assert.Throws<DivideByZeroException>(() =>
              {
                  _mathDivisionFixture.MathService.Division(a, b);
              });

        }
    }

    }
