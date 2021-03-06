﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Assertion.MSTest
{
    [TestClass]
    public class ShouldlyAssertExamples
    {

         
        [TestMethod]
        public void EqualityChecks()
        {
          
            bool valueToTest_bool = true;
            string valueToTest_string = "result_1#";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Abc = "def", Xyz = true };
            var expectedValue_obj_equal = new { Abc = "def", Xyz = true };
            var expectedValue_obj_notequal = new { Abc = "zoom", Xyz = false }; ;


            valueToTest_bool.ShouldBe(true);
            valueToTest_string.ShouldBe("result_1#");
            valueToTest_datetime.ShouldBe(new DateTime(2019, 01, 01));
            valueToTest_obj.ShouldBe(expectedValue_obj_equal);

            valueToTest_bool.ShouldNotBe(false);
            valueToTest_string.ShouldNotBe("not_result_1#");
            valueToTest_datetime.ShouldNotBe(new DateTime(2019, 12, 01));
            valueToTest_obj.ShouldNotBe(expectedValue_obj_notequal);

        }


         
        [TestMethod]
        public void SameObjectChecks()
        {
            var valueToTest = new {Abc = "def", Xyz = true};
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Abc = "def", Xyz = true }; ;


            valueToTest.ShouldBeSameAs(expectedValue_same);
            valueToTest.ShouldNotBeSameAs(expectedValue_notsame);

        }

         
        [TestMethod]
        public void NullChecks()
        {
            var valueToTest = new { Abc = (object) null, Xyz = new object() };
            valueToTest.Abc.ShouldBeNull();
            valueToTest.Xyz.ShouldNotBeNull();
            
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

            bigNumber.ShouldBeGreaterThan(smallNumber);
            bigNumber.ShouldBeGreaterThanOrEqualTo(smallNumber);

            smallNumber.ShouldBeLessThan(bigNumber);
            smallNumber.ShouldBeLessThanOrEqualTo(bigNumber);

            trueValue.ShouldBeTrue();
            falseValue.ShouldBeFalse();

            bigNumber.ShouldBePositive();
            smallNumber.ShouldBeNegative();

            zero.ShouldBeInRange(-100, 5);
            zero.ShouldNotBeInRange(1, 10);
            jan1.ShouldBeInRange(new DateTime(2018, 01, 01), new DateTime(2019, 12, 31));

            zero.ShouldBeOneOf(42, 0, 100);

            2.333333d.ShouldBe(2.3, 0.5);
            jan1.ShouldBe(new DateTime(2019, 01, 10), TimeSpan.FromDays(10));
            
        }
         
        [TestMethod]
        public void StringChecks()
        {
            var valueToTest = "Abc def Xyz Bin";

            "".ShouldBeEmpty();
            valueToTest.ShouldNotBeEmpty();
            valueToTest.ShouldContain("def");
            valueToTest.ShouldNotContain("Bang");
            valueToTest.ShouldStartWith("Abc");
            valueToTest.ShouldNotStartWith("def");
            valueToTest.ShouldEndWith("Bin");
            valueToTest.ShouldNotEndWith("Xyz");
            valueToTest.ShouldMatch("^Abc.*Bin$"); // param is a regex pattern
            valueToTest.ShouldNotMatch("^Abc.*def$"); // param is a regex pattern

        }

         
        [TestMethod]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };

            stringList.ShouldBeAssignableTo(typeof(IEnumerable<string>));
            stringList.ShouldBeAssignableTo<IEnumerable<string>>();

            stringList.ShouldNotBeAssignableTo(typeof(string[]));
            stringList.ShouldNotBeAssignableTo<string[]>();


            intEnumerable.ShouldBeOfType(typeof(int[])); //must be exact type
            intEnumerable.ShouldBeOfType<int[]>(); //must be exact type


            stringList.ShouldNotBeOfType(typeof(IEnumerable<string>)); //must be exact type
            stringList.ShouldNotBeOfType<IEnumerable<string>>(); //must be exact type


        }

         
        [TestMethod]
        public void CollectionChecks()
        {
            var objArr = new object[] {new object(), 42, "my string"};
            var stringArr = new object[] {"Abc", "def", "Xyz", "bin", ""};
            var intList = Enumerable.Range(0, 100);

            stringArr.ShouldAllBe(x => x is string);
            intList.ShouldAllBe(x => x >= 0);
            objArr.ShouldAllBe(x => x != null);

            intList.ShouldBeUnique();


            intList.ShouldBe(Enumerable.Range(0, 100));
            intList.ShouldNotBe(Enumerable.Range(1, 5));

            stringArr.ShouldBe(new string[] { "def", "Xyz", "", "bin", "Abc" }, true);

            stringArr.ShouldContain("Abc");
            stringArr.ShouldNotContain("zoom");

            Enumerable.Range(5, 20).ShouldBeSubsetOf(intList);

            new int[] { }.ShouldBeEmpty();
            intList.ShouldNotBeEmpty();

            new int[] { 1, 2, 3 }.ShouldBeInOrder();




            string[] sarray = new string[] { "a", "aa", "aaa" };
            sarray.ShouldBeInOrder(SortDirection.Ascending, new StringLengthComparer());

            intList.Count().ShouldBe(100);

            intList.ShouldAllBe(x => x >= 0);


        }

         
        [TestMethod]
        public void ExceptionChecks()
        {

            void MethodThatThrows() { throw new ArgumentException(); }

            void MethodThatDoesNotThrow() { return;}


            Action actionThatThrows = MethodThatThrows;
            Action actionThatDoesNotThrow = MethodThatDoesNotThrow;

            actionThatThrows.ShouldThrow<ArgumentException>();
            actionThatThrows.ShouldThrow(typeof(ArgumentException));
            actionThatDoesNotThrow.ShouldNotThrow();

            var ex = ((Action)(() => throw new Exception("message")))
                .ShouldThrow<Exception>();
            ex.Message.ShouldBe("message");

        }

       
        [TestMethod]
        public void MultipleCriteriaChecks()
        {
            double aNumber = 5.0; 
            aNumber.ShouldSatisfyAllConditions(
                () => aNumber.ShouldBeAssignableTo<double>(),
                () => aNumber.ShouldBeGreaterThanOrEqualTo(0.0),
                () => aNumber.ShouldBeLessThanOrEqualTo(10.0)
            ); 
        }


        private class StringLengthComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {

                if (x == null || y == null)
                {
                    if (x == y) return 0;

                    if (x == null)
                        return -1;
                    else return 1;
                }

                return x.Length.CompareTo(y.Length);
            }
        }

    }
}
