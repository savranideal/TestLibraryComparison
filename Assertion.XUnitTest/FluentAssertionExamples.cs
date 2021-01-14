using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using Xunit;


namespace xUnit.NetCore
{
    public class FluentAssertionExamples
    {

        /* 
         * https://fluentassertions.com/introduction 
         */
         
        [Fact]
        public void EqualityChecks()
        {
          
            bool valueToTest_bool = true;
            string valueToTest_string = "result_1#";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Abc = "def", Xyz = true };
            var expectedValue_obj_equal = new { Abc = "def", Xyz = true };
            var expectedValue_obj_notequal = new { Abc = "zoom", Xyz = false }; ;


            valueToTest_bool.Should().Be(true);
            valueToTest_string.Should().Be("result_1#");
            valueToTest_datetime.Should().Be(new DateTime(2019, 01, 01));
            valueToTest_obj.Should().Be(expectedValue_obj_equal);

            valueToTest_string.Should().NotBe("some other result");
            valueToTest_datetime.Should().NotBe(new DateTime(2019, 12, 01));
            valueToTest_obj.Should().NotBe(expectedValue_obj_notequal);

        }
         
        [Fact]
        public void SameObjectChecks()
        {
            var valueToTest = new {Abc = "def", Xyz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Abc = "def", Xyz = true }; ;

            valueToTest.Should().BeSameAs(expectedValue_same);
            valueToTest.Should().NotBeSameAs(expectedValue_notsame);

        }
         
        [Fact]
        public void NullChecks()
        {
            var valueToTest = new { Abc = (object) null, Xyz = new object() };

            valueToTest.Abc.Should().BeNull();
            valueToTest.Xyz.Should().NotBeNull();
        }
         
        [Fact]
        public void ComparisonChecks()
        {
            int bigNumber = int.MaxValue;
            int smallNumber = int.MinValue;
            int zero = 0;

            bool trueValue = true;
            bool falseValue = false;

            DateTime jan1 = new DateTime(2019, 01, 01);

           
            bigNumber.Should().BeGreaterThan(smallNumber);
            bigNumber.Should().BeGreaterOrEqualTo(smallNumber);

            smallNumber.Should().BeLessThan(bigNumber);
            smallNumber.Should().BeLessOrEqualTo(bigNumber);

            trueValue.Should().BeTrue();
            falseValue.Should().BeFalse();

            bigNumber.Should().BePositive();
            smallNumber.Should().BeNegative();

            zero.Should().BeInRange(-100, 5);
            zero.Should().NotBeInRange(1, 10);
            jan1.Should().BeAfter(new DateTime(2018, 01, 01)).And.BeBefore(new DateTime(2019, 12, 31));

            zero.Should().BeOneOf(42, 0, 100);

            2.333333d.Should().BeApproximately(2.3, 0.5);
            jan1.Should().BeCloseTo(new DateTime(2019, 01, 10), 10.Days());
           
        }
         
        [Fact]
        public void StringChecks()
        {
            var valueToTest = "Abc Def Xyz Bin";
          
            
            //"asdf".Should().BeEmpty();
            valueToTest.Should().NotBeEmpty();
            valueToTest.Should().Contain("Def");
            valueToTest.Should().NotContain("Bang");
            valueToTest.Should().StartWith("Abc");
            valueToTest.Should().NotStartWith("Def");
            valueToTest.Should().EndWith("Bin");
            valueToTest.Should().NotEndWith("Xyz");
            valueToTest.Should().BeEquivalentTo("abc def xyz bin");
            valueToTest.Should().NotBeEquivalentTo("something else");
            valueToTest.Should().MatchRegex("^Abc.*Bin$"); // param is a regex pattern
            valueToTest.Should().NotMatchRegex("^Abc.*Def$"); // param is a regex pattern
            valueToTest.Should().Match("Abc*Bin"); // param is a wildcard pattern
            valueToTest.Should().NotMatch("Abc*Def"); // param is a wildcard pattern


        }
         
        [Fact]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };


            stringList.Should().BeOfType(typeof(List<string>));
            stringList.Should().BeOfType<List<string>>();

            intEnumerable.Should().NotBeOfType(typeof(List<int>));
            intEnumerable.Should().NotBeOfType< List<int>>();

            stringList.Should().BeAssignableTo(typeof(IEnumerable<string>));
            stringList.Should().BeAssignableTo<IEnumerable<string>>();

            stringList.Should().NotBeAssignableTo(typeof(string[]));
            stringList.Should().NotBeAssignableTo<string[]>();


        }
         
        [Fact]
        public void CollectionChecks()
        {
            var objArr = new object[] {new object(), 42, "my string"};
            var stringArr = new object[] {"abc", "def", "xyz", "bin", ""};
            var intList = Enumerable.Range(0, 100);


            stringArr.Should().ContainItemsAssignableTo<string>();
            intList.Should().Contain(x => x >= 3);
            objArr.Should().NotContainNulls();

            intList.Should().OnlyHaveUniqueItems();


            intList.Should().Equal(Enumerable.Range(0, 100));
            intList.Should().NotEqual(Enumerable.Range(1, 5));

            stringArr.Should().BeEquivalentTo(new string[] { "def", "xyz", "", "bin", "abc" });
            stringArr.Should().NotBeEquivalentTo(new string[] { "def", "xyz" });

            stringArr.Should().Contain("abc");
            stringArr.Should().NotContain("zoom");

            Enumerable.Range(5, 20).Should().BeSubsetOf(intList);
            Enumerable.Range(-1, 1).Should().NotBeSubsetOf(intList);

            new int[] { }.Should().BeEmpty();
            intList.Should().NotBeEmpty();

            new int[] { 1, 2, 3 }.Should().BeInAscendingOrder();
            new int[] { 2, 1, 3 }.Should().NotBeInDescendingOrder();

            string[] sarray = new string[] { "a", "aa", "aaa" };
            sarray.Should().BeInAscendingOrder(new StringLengthComparer());

            intList.Should().HaveCount(100);

            intList.Should().OnlyContain(x => x >= 0);
           
        }
         
        [Fact]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); }


            Action act = () => { return; };
            act.Should().NotThrow();

            act = () => MethodThatThrows();

            act.Should().Throw<ArgumentException>();

            act = () => throw new Exception("message");

            act.Should().Throw<Exception>().And.Message.Should().Be("message");

            // Require an ApplicationException - derived types fail!
            act = () => throw new ApplicationException("message");
            act.Should().ThrowExactly<ApplicationException>();

        }
         
         
        [Fact]
        public void MultipleCriteriaChecks()
        {
            var aNumber = 5.0;

            aNumber.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(10);
            aNumber.Should().BeOfType(typeof(double)).And.BeInRange(0.0, 10.0);

            using (new AssertionScope()) 
            {
                aNumber.Should().BeOfType(typeof(double));
                aNumber.Should().BeInRange(0.0, 10.0);
            }
            
        }

         
        private class StringLengthComparer : IComparer<object>
        {
            public int Compare(object x, object y)
            {
                if (x == null || y == null)
                {
                    if (x == y) return 0;

                    if (x == null)
                        return -1;
                    else return 1;
                }

                if (x is string xs && y is string ys)
                {
                    return xs.Length.CompareTo(ys.Length);
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
