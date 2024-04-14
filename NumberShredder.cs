using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class NumberShredder
    {

        public bool PrimeTest(long n, int k = 1)
        {
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            long x = 1;
            for (int i = 0; i < k; i++)
            {
                Random rand = new Random();
                x = rand.NextInt64(3, n / 2);
                if (MathUtils.GCD(x, n) > 1) return false;
                if (MathUtils.checkStrongPrime(n, x)) return true;
            }

            return false;

        }


        public Int64 Factorize(Int64 n)
        {
            return 0;
        }

        public Int64[] CanonicalFactorization()
        {
            return Array.Empty<Int64>();
        }

        public Int64 TrialDivisions(Int64 n)
        {
            for (Int64 d = 2; d < Math.Sqrt(n); d++)
            {
                if (n % d == 0) return d;
            }
            return 1;
        }

        private void RhoMethod()
        {

        }

        private void PomeranceMethod()
        {

        }

    }
}
