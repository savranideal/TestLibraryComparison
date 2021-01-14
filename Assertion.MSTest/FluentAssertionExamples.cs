﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Assertion.MSTest
{
    [TestClass]
    public class FluentAssertionExamples
    {

        /* 
         * https://fluentassertions.com/introduction 
         */

         
        [TestMethod]
        public void EqualityChecks()
        {
          
            bool valueToTest_bool = true;
            string valueToTest_string = "result_1#";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Foo = "abc", Baz = true };
            var expectedValue_obj_equal = new { Foo = "abc", Baz = true };
            var expectedValue_obj_notequal = new { Foo = "zoom", Baz = false }; ;


            valueToTest_bool.Should().Be(true);
            valueToTest_string.Should().Be("result_1#");
            valueToTest_datetime.Should().Be(new DateTime(2019, 01, 01));
            valueToTest_obj.Should().Be(expectedValue_obj_equal);

            valueToTest_string.Should().NotBe("not_result_1#");
            valueToTest_datetime.Should().NotBe(new DateTime(2019, 12, 01));
            valueToTest_obj.Should().NotBe(expectedValue_obj_notequal);

        }

         
        [TestMethod]
        public void SameObjectChecks()
        {
            var valueToTest = new {Foo = "abc", Baz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Foo = "abc", Baz = true }; ;

            valueToTest.Should().BeSameAs(expectedValue_same);
            valueToTest.Should().NotBeSameAs(expectedValue_notsame);

        }
         
        [TestMethod]
        public void NullChecks()
        {
            var valueToTest = new { Foo = (object) null, Baz = new object() };

            valueToTest.Foo.Should().BeNull();
            valueToTest.Baz.Should().NotBeNull();
        }
         
        [TestMethod]
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

       

         
        [TestMethod]
        public void StringChecks()
        {
            var valueToTest = "Foo abc Baz Bin";
          
            
            //"asdf".Should().BeEmpty();
            valueToTest.Should().NotBeEmpty();
            valueToTest.Should().Contain("abc");
            valueToTest.Should().NotContain("Bang");
            valueToTest.Should().StartWith("Foo");
            valueToTest.Should().NotStartWith("abc");
            valueToTest.Should().EndWith("Bin");
            valueToTest.Should().NotEndWith("Baz");
            valueToTest.Should().BeEquivalentTo("foo abc baz bin");
            valueToTest.Should().NotBeEquivalentTo("something else");
            valueToTest.Should().MatchRegex("^Foo.*Bin$"); // param is a regex pattern
            valueToTest.Should().NotMatchRegex("^Foo.*abc$"); // param is a regex pattern
            valueToTest.Should().Match("Foo*Bin"); // param is a wildcard pattern
            valueToTest.Should().NotMatch("Foo*abc"); // param is a wildcard pattern


        }

         
        [TestMethod]
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

         
        [TestMethod]
        public void CollectionChecks()
        {
            var objArr = new object[] {new object(), 42, "my string"};
            var stringArr = new object[] {"foo", "abc", "baz", "bin", ""};
            var intList = Enumerable.Range(0, 100);


            stringArr.Should().ContainItemsAssignableTo<string>();
            intList.Should().Contain(x => x >= 3);
            objArr.Should().NotContainNulls();

            intList.Should().OnlyHaveUniqueItems();


            intList.Should().Equal(Enumerable.Range(0, 100));
            intList.Should().NotEqual(Enumerable.Range(1, 5));

            stringArr.Should().BeEquivalentTo(new string[] { "abc", "baz", "", "bin", "foo" });
            stringArr.Should().NotBeEquivalentTo(new string[] { "abc", "baz" });

            stringArr.Should().Contain("foo");
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
         
        [TestMethod]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); } 
            Action act = () => { return; };
            act.Should().NotThrow(); 
            act = () => MethodThatThrows(); 
            act.Should().Throw<ArgumentException>(); 
            act = () => throw new Exception("message"); 
            act.Should().Throw<Exception>().And.Message.Should().Be("message"); 
            act = () => throw new ArgumentNullException("message");
            act.Should().ThrowExactly<ArgumentNullException>();

        }
         
        [TestMethod]
        public void MultipleCriteriaChecks()
        {
            var aNumber = 5.0; 
            aNumber.Should().BeGreaterOrEqualTo(0).And.BeLessOrEqualTo(10);
            aNumber.Should().BeOfType(typeof(double)).And.BeInRange(0.0, 10.0); 

            using (var k=new AssertionScope()) 
            {
                aNumber.Should().BeOfType(typeof(double));
                aNumber.Should().BeInRange(0.0, 10.0);
            }
            
        }



    }
}
