using System;
using Xunit;

namespace UnitTest.Assertion
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
        [Fact]
        public void MultipleCriteriaChecks()
        {
            object aNumber = 5.0;

            Assert.That(aNumber, Is.AssignableTo<int>().Or.AssignableTo<double>());
            Assert.That(aNumber, Is.GreaterThanOrEqualTo(0).And.LessThanOrEqualTo(10));

        }
    }
}
