using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace lab1
{
    internal class Startup
    {
        static void Main(string[] args)
        {
            NumberShredder ns = new NumberShredder();

            long num = 691534156424661573;

            Console.WriteLine("Number: {0}", num);

            //BitMatrix matrix = new(new bool[,]
            //{
            //    { true, true, false, false},
            //    { true, true, false, true},
            //    { false, true, true, true},
            //    { false, false, true, false},
            //    { false, false, false, true}
            //});

            //Console.WriteLine(matrix);

            //SortedSet<int> list = [];

            //matrix.GetAllSolutions();

            //Console.WriteLine(matrix);

            //Console.WriteLine(string.Join(',', list));

            // Console.WriteLine(NumberShredder.RhoMethod(num, (x, n) => { return MathUtils.Mod(x * x + 1, n); }));

            //Console.WriteLine(NumberShredder.BrilhartMorrison(num));


            long[] factors = NumberShredder.CanonicalFactorization(num, (x, n) => { return MathUtils.Mod(x * x + 1, n); });
            foreach (long factor in factors)
            {
                Console.WriteLine(factor);
            }

        }

    }
}
