
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TestLibrary;

namespace UnitTest.MsTest
{
    [TestClass]
    public class MathDivisionUnitTest
    {

        private MathService _mathService;

        public MathDivisionUnitTest()
        {
            _mathService = new MathService();

        }
        [DataTestMethod]
        [DataRow(4, 2)]
        [DataRow(0, 2)]
        public void Division_Positive(double a, double b)
        {
            Assert.AreEqual(_mathService.Division(a, b), a / b);
        }
        [DataTestMethod]
        [DataRow(-4, -2)]
        [DataRow(0, -2)]
        public void Division_TwoNegative(double a, double b)
        {
            Assert.AreEqual(_mathService.Division(a, b), a / b);
        }

        [DataTestMethod]
        [DataRow(4, -2)]
        [DataRow(-4, 2)]
        public void Division_NegativeWithPositive(double a, double b)
        {
            Assert.AreEqual(_mathService.Division(a, b), a / b);
        }
        [DataTestMethod]
        [DataRow(-4, 0)]
        [DataRow(4, 0)]
        [DataRow(0, 0)]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Division_Zero_ThrowsDivideByZeroException(double a, double b)
        {
            _mathService.Division(a, b);
        }

    }
}
