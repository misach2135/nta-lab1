using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class NumberShredder
    {

        public static bool PrimeTest(long n, int k = 1)
        {
            if (n < 0) n = -n;
            if ((n == 2) || (n == 3) || (n == 5)) return true;
            if ((n % 2 == 0) || (n == 1)) return false;

            long x = 1;
            for (int i = 0; i < k; i++)
            {
                Random rand = new Random();
                x = rand.NextInt64(3, n / 2);
                if (MathUtils.GCD(x, n) > 1) return false;
                if (MathUtils.CheckStrongPrime(n, x)) return true;
            }

            return false;

        }


        public static Int64 Factorize(Int64 n)
        {
            return 0;
        }

        public static Int64[] CanonicalFactorization()
        {
            return Array.Empty<Int64>();
        }

        public static Int64 TrialDivisions(Int64 n)
        {
            for (Int64 d = 2; d < Math.Sqrt(n); d++)
            {
                if (n % d == 0) return d;
            }
            return 1;
        }

        // TODO: 3. from metodichka(theoreticly, this shit could not be working properly)
        public static long RhoMethod(long n, Func<long, long, long> f)
        {
            long x = 2;
            long y = 2;
            long d = 1;

            do
            {
                x = f(x, n);
                y = f(f(y, n), n);
                d = MathUtils.GCD(x - y, n);
            } while (d == 1);

            return d;

        }

        public static long BrilhartMorrison(long n)
        {
            Func<long, long> squareMod = x =>
            {
                Int128 t = x * x;
                x = MathUtils.Mod(t, n);
                if (x > n / 2)
                {
                    x = n - x;
                }
                return x;
            };

            // Build factor base
            List<long> factorBase = [-1];

            long bound = (long)Math.Pow(Math.Exp(Math.Sqrt(Math.Log((double)n) * Math.Log(Math.Log((double)n)))), 1 / Math.Sqrt(2));
            for (long i = 2; i < bound; i++)
            {
                if (PrimeTest(i))
                {
                    var temp = MathUtils.LegandreSymbol(n, i);
                    if (temp == 0)
                    {
                        return i;
                    }
                    if (temp == 1)
                    {
                        factorBase.Add(i);
                    }
                }
            }

            long v = 1;
            double alpha = Math.Sqrt(n);
            long a = (long)alpha;
            long u = a;

            List<long> bis = [0, 1, a];

            for (int i = 3; i < factorBase.Count + 4; i++)
            {
                v = (n - u * u) / v;
                alpha = (Math.Sqrt(n) + u) / v;
                a = (long)alpha;
                u = a * v - u;

                long temp = MathUtils.Mod(a * bis[i - 1] + bis[i - 2], n);

                bis.Add(temp);
            }

            List<bool[]> factorBis = [];

            bis.RemoveAt(0);
            bis.RemoveAt(0);

            for (int i = 0; i < bis.Count; i++)
            {

                bool[] arr = new bool[factorBase.Count];

                var b = squareMod(bis[i]);

                arr[0] = b < 0;
                b = Math.Abs(b);

                for (int j = 1; j < factorBase.Count; j++)
                {
                    while (MathUtils.Mod(b, factorBase[j]) == 0)
                    {
                        arr[j] ^= true;
                        b /= factorBase[j];
                    }
                }

                if (b != 1)
                {
                    bis[i] = 0;
                    continue;
                }

                factorBis.Add(arr);
            }

            bis.RemoveAll(x => x == 0);

            var matrix = new BitMatrix(factorBis);
            
            Console.WriteLine("FactorBase: {0}", string.Join(',', factorBase));
            Console.WriteLine("Bis: {0}", string.Join(',', bis));
            Console.WriteLine(matrix);

            matrix.GetAllSolutions();

            List<int> indexes = [];

            var res = MathUtils.SleQuizer(matrix);

            long X = 1;
            long Y = 1;

            foreach (var e in res)
            {
                var b = squareMod(bis[e]);

                X *= bis[e];
                Y *= b;

                X = MathUtils.Mod(X, n);
            }

            Y = (long)Math.Sqrt(Y);
            Y = MathUtils.Mod(Y, n);

            if (X != Y && X != MathUtils.Mod(-Y, n))
            {
                return MathUtils.GCD(X + Y, n);
            }

            return 0;
        }

    }
}
