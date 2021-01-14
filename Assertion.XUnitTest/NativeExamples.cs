using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace xUnit.NetCore
{

    public class NativeExamples
    {
         
        [Fact]
        public void EqualityChecks()
        {
            string valueToTest_string = "result_1#";
            DateTime valueToTest_datetime = new DateTime(2019, 01, 01);

            var valueToTest_obj = new { Abc = "def", Xyz = true };
            var expectedValue_obj_equal = new { Abc = "def", Xyz = true };
            var expectedValue_obj_notequal = new { Abc = "zoom", Xyz = false }; ;

            
            // (important: expected value comes first!)
            Assert.Equal("result_1#", valueToTest_string);
            Assert.Equal(new DateTime(2019, 01, 01), valueToTest_datetime);
            Assert.Equal(expectedValue_obj_equal, valueToTest_obj);

            Assert.NotEqual("some other result", valueToTest_string);
            Assert.NotEqual(new DateTime(2019, 12, 01), valueToTest_datetime);
            Assert.NotEqual(expectedValue_obj_notequal, valueToTest_obj);
        }


         
        [Fact]
        public void SameObjectChecks()
        {
            var valueToTest = new { Abc = "def", Xyz = true };
            var expectedValue_same = valueToTest;
            var expectedValue_notsame = new { Abc = "def", Xyz = true }; ;
            

            // (important: expected value comes first!)
            Assert.Same(expectedValue_same, valueToTest);
            Assert.NotSame(expectedValue_notsame, valueToTest);

        }

         
        [Fact]
        public void NullChecks()
        {
            var valueToTest = new { Abc = (object)null, Xyz = new object() };

            Assert.Null(valueToTest.Abc);
            Assert.NotNull(valueToTest.Xyz);
            
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

            // Constraint-style asserts:
            Assert.True(bigNumber  > smallNumber);
            Assert.True(bigNumber >= smallNumber);

            Assert.True(smallNumber < bigNumber);
            Assert.True(smallNumber <= bigNumber);

            Assert.True(trueValue);
            Assert.False(falseValue);
            
            Assert.InRange(zero, -100, 5);
            Assert.NotInRange(zero, 1, 10);
            Assert.InRange(jan1, new DateTime(2018, 01, 01), new DateTime(2019, 12, 31));
           
        }

        
        [Fact]
        public void StringChecks()
        {
            var valueToTest = "Abc Def Xyz Bin";

           
            Assert.Contains("Def", valueToTest);
            Assert.DoesNotContain("Asd", valueToTest);
            Assert.StartsWith("Abc", valueToTest);
            Assert.EndsWith("Bin", valueToTest);
            Assert.Equal("abc def xyz bin", valueToTest, ignoreCase: true);
            Assert.NotEqual("asdfgrf", valueToTest, StringComparer.InvariantCultureIgnoreCase);
            Assert.Matches("^Abc.*Bin$", valueToTest);  
            Assert.Matches(new Regex("^Abc.*Bin$"), valueToTest); 
            Assert.DoesNotMatch("^Abc.*Def$", valueToTest);  
            Assert.DoesNotMatch(new Regex("^Abc.*Def$"), valueToTest); 
        }

        
        [Fact]
        public void TypeChecks()
        {
            IList<string> stringList = new List<string>();
            IEnumerable<int> intEnumerable = new int[] { };

            Assert.IsAssignableFrom<string>("abc");
            Assert.IsType<List<string>>(stringList);
            Assert.IsNotType<List<int>>(intEnumerable);
            
        }

        
        [Fact]
        public void CollectionChecks()
        {
            var objArr = new object[] { new object(), 42, "my string" };
            var stringArr = new string[] { "abc", "def", "xyz", "bin", "" };
            var intList = Enumerable.Range(0, 100);

          
            Assert.All(stringArr, s => Assert.IsType<string>(s));
            Assert.All(objArr, Assert.NotNull );

            Assert.Equal(Enumerable.Range(0, 100), intList);
            Assert.NotEqual(Enumerable.Range(1, 5), intList);

           
            Assert.Contains("abc", stringArr);
            Assert.DoesNotContain("zoom", stringArr);

            Assert.Subset(intList.ToHashSet(), Enumerable.Range(5, 20).ToHashSet());
            Assert.Superset(Enumerable.Range(5, 20).ToHashSet(), intList.ToHashSet());

            Assert.Empty(new int[] { });
            Assert.NotEmpty(new int[] { 1, 2 });


        }


        
        [Fact]
        public void ExceptionChecks()
        {
            void MethodThatThrows() { throw new ArgumentException(); }

           
            Assert.Throws<ArgumentException>(() => MethodThatThrows());
           
            Exception ex = Assert.Throws<Exception>((Action)(() => throw new Exception("message")));
            Assert.Equal("message", ex.Message);
        }

         

    }


}
