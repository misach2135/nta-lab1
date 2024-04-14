using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab1
{
    internal static class MathUtils
    {
        // TODO: Binary GCD
        public static Int64 GCD(long a, long b)
        {
            if (b == 0) return a;
            return GCD(b, Mod(a, b));
        }

        // TODO: Maybe make faster modulo using https://en.wikipedia.org/wiki/Modulo#Performance_issues
        // TODO: Better make it iterative
        public static long Mod(Int128 a, long n)
        {
            if (a < 0) return Mod(a + n, n);
            return (Int64)(a % (Int128)n);
        }

        public static bool getIthBit(long num, short i)
        {
            long mask = (1L << i);
            return (num & mask) != 0;
        }

        // TODO: Make Gorner scheme(or not)) -- already done)
        // TODO: optimize loop
        public static long PowMod(long a, long n, long mod)
        {
            Int128 res = 1;
            a = Mod(a, mod);
            for (short i = 63; i >= 0; i--)
            {
                if (getIthBit(n, i))
                {
                    res *= a;
                    res = Mod(res, mod);
                }

                if (i != 0)
                {
                    res *= res;
                    res = Mod(res, mod);
                }


            }
            return (Int64)res;
        }

        public static bool checkStrongPrime(long n, long x)
        {
            if (x >= n || x <= 1) throw new Exception("Bad x: its must be in range");
            long s, t;
            s = 0;
            t = n - 1;

            while (t % 2 == 0)
            {
                t >>= 1;
                s++;
            }

            long y = PowMod(x, t, n);

            if ((y == 1) || (y == n - 1))
            {
                return true;
            }

            for (int i = 1; i < s; i++)
            {
                y = PowMod(y, 2, n);
                if (y == n - 1) return true;
            }

            return false;

        }

    }
}
