using NUnit.Framework;
using System;
using TestLibrary;

namespace UnitTest.NUnit
{
    [TestFixture]
    [Parallelizable()]
    public class MathDivisionUnitTest
    {

        private MathService _mathService;

        [SetUp]
        public void SetUp()
        {
            _mathService = new MathService();
        }

        [Test]
        [TestCase(4,2)]
        [TestCase(0,2)]
        
        public void Division_Positive(double a,double b)
        {
            Assert.IsTrue(_mathService.Division(a, b) == a/b);
        }

        [Test]
        [TestCase(-4, -2)]
        [TestCase(0, -2)] 
        public void Division_TwoNegative(double a, double b)
        {
            Assert.IsTrue(_mathService.Division(a, b) == a / b);
        }
        [Test]
        [TestCase(-4, 2)]
        [TestCase(4, -2)] 
        public void Division_NegativeWithPositive(double a, double b)
        {
            Assert.IsTrue(_mathService.Division(a, b) == a / b);
             
        }

        [Test]
        [TestCase(-4, 0)]
        [TestCase(4, 0)] 
        [TestCase(0, 0)] 
        public void Division_Zero_ThrowsDivideByZeroException(double a, double b)
        {
            var ex=Assert.Throws<DivideByZeroException>(() =>
            {
                _mathService.Division(a, b);
            }, "Division_Zero");
 
        }

    }
}