using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class MathService
    { 
        //public double Sum(params double[] ps)
        //{
        //    return ps.Sum();
        //}
         
        //public double Multiplication(params double[] ps)
        //{
        //    var l = ps[0];
        //    for (int i = 1; i < ps.Length; i++)
        //        l = l * ps[i];
        //    return l;
        //} 

        public double Division(double param1, double param2)
        {

            if (param2 == 0)
                throw new DivideByZeroException(); 
            return param1 / param2;

        }

    }
}
