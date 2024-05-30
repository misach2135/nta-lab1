using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Cache;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
            if (a < 0)
            {
                a = -a * (n - 1);
            }

            Int128 res = a % n;
            return (Int64)res;
        }

        public static BigInteger Mod(BigInteger a, long n)
        {
            if (a < 0)
            {
                a = -a * (n - 1);
            }

            BigInteger res = a % n;
            return (long)res;
        }

        public static bool getIthBit(Int128 num, short i)
        {
            long mask = (1L << i);
            return (num & mask) != 0;
        }

        // TODO: optimize loop
        // THIS SHIT DONT WORK WITH SQUARE
        public static long PowMod(long a, Int128 n, long mod)
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

        public static bool CheckStrongPrime(long n, long x)
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

        public static int ComputeBinomial(int n, int k)
        {
            int res = 1;
            for (int i = 1; i <= k; i++)
            {
                res *= (n + 1 - i) / i;
            }
            return res;
        }

        // This shit could work properly
        public static int[] SleQuizer(BitMatrix matrix)
        {
            SortedSet<string> cache = new();
            Random rand = new();

            var getPick = (int pickSize) =>
            {
                SortedSet<int> pick = [];
                while (pick.ToArray<int>().Length != pickSize)
                {
                    pick.Add(rand.Next(0, matrix.RowsCount));
                }
                return pick.ToArray<int>();
            };

            var arrToString = (int[] arr) =>
            {
                StringBuilder sb = new();
                for (int i = 0; i < arr.Length; i++)
                {
                    sb.Append(arr[i]);
                }
                return sb.ToString();
            };

            for (int i = 2; i <= matrix.RowsCount; i++)
            {
                for (int j = 0; j < ComputeBinomial(matrix.RowsCount, i); j++)
                {
                    int[] pick = getPick(i);
                    var strPick = arrToString(pick);
                    if (cache.TryGetValue(strPick, out _))
                    {
                        j--;
                        continue;
                    }
                    cache.Add(strPick);
                    if (matrix.CheckRowsSum(pick))
                    {
                        return pick;
                    }
                }
            }
            return [];
        }

        public static int LegandreSymbol(Int64 n, Int64 p)
        {
            if (n >= p)
            {
                if (n % p == 0)
                {
                    return 0;
                }
                n = Mod(n, p);
            }

            if (n == 1)
            {
                return 1;
            }

            if (n == 2)
            {
                return ((p - 1) * (p + 1) / 8) % 2 == 0 ? 1 : -1;
            }

            long t = ((p - 1) * (n - 1) / 2) % 2 == 0 ? 1 : -1;

            return (int)t * LegandreSymbol(p, n);

        }
    }
}
