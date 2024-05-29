using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace lab1
{
    internal class Startup
    {
        static void Main(string[] args)
        {
            NumberShredder ns = new NumberShredder();

            long num = 17873;

            Console.WriteLine("Number: {0}", num);

            BitMatrix matrix = new(new bool[,]
            {
                {true, true, false, false },
                {true, true, false, true },
                {false, true, true, true },
                { false, false, true, false},
                { false, false, false, true}
            });

            Console.WriteLine(matrix);

            SortedSet<int> list = [];

            Console.WriteLine(matrix);

            Console.WriteLine(string.Join(',', list));
            
            Console.WriteLine(NumberShredder.BrilhartMorrison(num));

        }

    }
}
