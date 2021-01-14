using System;
using TestLibrary;

namespace UnitTest.XUnit
{
    public class MathDivisionFixture : IDisposable
    { 
        public MathService MathService { get; set; }
     
        public  MathDivisionFixture()
        {
            MathService = new MathService();
        }

        public void Dispose()
        {
           
        }
    }
}
