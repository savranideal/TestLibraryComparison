using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assertion.MSTest
{
    [TestClass]
    public class NativeExamples
    { 
        [TestMethod]
        public void EqualityChecks()
        {

            bool valueToTest_bool = true;
            string valueToTest_string = "some result";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { abc = "def", asd = true };
            var expectedValue_obj_equal = new { abc = "def", asd = true };
            var expectedValue_obj_notequal = new { abc = "zoom", asd = false }; ;

            // ilk parametre expected value olmalı
            Assert.AreEqual(true, valueToTest_bool);
            Assert.AreEqual("some result", valueToTest_string);
            Assert.AreEqual(new DateTime(2019, 01, 01), valueToTest_datetime);
            Assert.AreEqual(expectedValue_obj_equal, valueToTest_obj);

            Assert.AreNotEqual(false, valueToTest_bool);
            Assert.AreNotEqual("some other result", valueToTest_string);
            Assert.AreNotEqual(new DateTime(2019, 12, 01), valueToTest_datetime);
            Assert.AreNotEqual(expectedValue_obj_notequal, valueToTest_obj);
        }
         
        [TestMethod]
        public void SameObjectChecks()
        {
            var valueToTest = new { abc = "def", asd = true };
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { abc = "def", asd = true }; ;

            // ilk parametre expected value olmalı
            Assert.AreSame(expectedValue_same, valueToTest);
            Assert.AreNotSame(expectedValue_notsame, valueToTest);

        }
         
        [TestMethod]
        public void NullChecks()
        {
            var valueToTest = new {abc = (object) null, asd = new object()};

            Assert.IsNull(valueToTest.abc);
            Assert.IsNotNull(valueToTest.asd);
        }
         
        [TestMethod]
        public void ComparisonChecks()
        {
            int bigNumber = int.MaxValue;
            int smallNumber = int.MinValue;

            bool trueValue = true;
            bool falseValue = false;

            Assert.IsTrue(bigNumber > smallNumber);
            Assert.IsTrue(bigNumber >= smallNumber);

            Assert.IsTrue(smallNumber < bigNumber);
            Assert.IsTrue(smallNumber <= bigNumber);

            Assert.IsTrue(trueValue);
            Assert.IsFalse(falseValue);

        }
         
        [TestMethod]
        public void StringChecks()
        {
            var valueToTest = "Abc Def Klm Xyz";

            StringAssert.Contains(valueToTest, "Def");
            StringAssert.StartsWith(valueToTest, "Abc");
            StringAssert.EndsWith(valueToTest, "Xyz");
            StringAssert.Matches(valueToTest, new Regex("^Abc.*Xyz"));
            StringAssert.DoesNotMatch(valueToTest, new Regex("^Abc.*Xyx$")); 

        } 
        [TestMethod]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };
            
            Assert.IsInstanceOfType(stringList, typeof(List<string>));
            Assert.IsNotInstanceOfType(intEnumerable, typeof(List<int>));
          
        }
         
        [TestMethod]
        public void CollectionChecks()
        {
            var objArr = new object[] { new object(), 42, "my string" };
            var stringArr = new object[] { "abc", "def", "asd", "xyz", "" };
            var intList = Enumerable.Range(0, 100).ToList();

            CollectionAssert.AllItemsAreInstancesOfType(stringArr, typeof(string));
            CollectionAssert.AllItemsAreNotNull(objArr);
             
            CollectionAssert.AreEqual(Enumerable.Range(0, 100).ToList(), intList);
            CollectionAssert.AreNotEqual(Enumerable.Range(1, 5).ToList(), intList);

            CollectionAssert.AreEquivalent(new string[] { "def", "asd", "", "xyz", "abc" }, stringArr);
            CollectionAssert.AreNotEquivalent(new string[] { "def", "asd" }, stringArr);

            CollectionAssert.Contains(stringArr, "abc");
            CollectionAssert.DoesNotContain(stringArr, "ttt");

            CollectionAssert.IsSubsetOf(Enumerable.Range(5, 20).ToList(), intList);
            CollectionAssert.IsNotSubsetOf(Enumerable.Range(-1, 1).ToList(), intList);

        }
         
        [TestMethod]
        public void ExceptionChecks()
        {
            void MethodThatThrows() { throw new ArgumentException(); } 
            Assert.ThrowsException<ArgumentException>(() => MethodThatThrows());
            Assert.ThrowsException<ArgumentException>(() => throw new ArgumentException());

            Exception ex = Assert.ThrowsException<Exception>(() => throw new Exception("message"));
            Assert.AreEqual("message", ex.Message);
            
        }
         


    }
}
