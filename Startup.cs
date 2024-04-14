using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    internal class Startup
    {
        static void Main(string[] args)
        {
            NumberShredder ns = new NumberShredder();
            Console.WriteLine("GCD: " + MathUtils.GCD(62677181, 62808347));
            Console.WriteLine(ns.PrimeTest(7442109405582674149));

            //Console.WriteLine(MathUtils.PowMod(, 10, 62677181));
            /*
            long x = 1;
            long n = long.MaxValue - 10;
            List<long> factors = [];
            Console.WriteLine("Number: " + n);
            while (true)
            {
                x = ns.TrialDivisions(n);
                n /= x;
                if (x == 1)
                {
                    factors.Add(n);
                    break;
                }
                factors.Add(x);
            }

            long res = 1;

            foreach (var i in factors)
            {
                res *= i;
                Console.WriteLine("Factors: " + i + ", ");
            }

            Console.WriteLine("Result: " + res);
            */
        }
    }
}
