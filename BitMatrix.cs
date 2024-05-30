using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class BitMatrix
    {

        private readonly bool[,] data;

        public BitMatrix()
        {
            data = new bool[0, 0];
        }
        public BitMatrix(int rows, int cols)
        {
            data = new bool[rows, cols];
        }

        public BitMatrix(bool[,] rawBitMatrix)
        {
            data = (rawBitMatrix.Clone() as bool[,]) ?? new bool[0, 0];
        }

        public BitMatrix(List<bool[]> bools)
        {
            if (bools.Count == 0)
            {
                data = new bool[0, 0];
                return;
            }
            data = new bool[bools.Count, bools[0].Length];
            int t = 0;
            bools.ForEach(x =>
            {
                if (x.Length != bools[0].Length) throw new Exception("List of bools must contain equal sized bool arrays");

                for (int i = 0; i < data.GetLength(1); i++)
                {
                    data[t, i] = x[i];
                }

                t++;

            });
        }

        public bool this[int i, int j]
        {
            get
            {
                return data[i, j];
            }
            set
            {
                data[i, j] = value;
            }
        }

        public int RowsCount
        {
            get => data.GetLength(0);
        }

        public int ColsCount
        {
            get => data.GetLength(1);
        }

        public void CombineCols(int col1, int col2)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                data[i, col1] ^= data[i, col2];
            }
        }

        public void CombineRows(int row1, int row2)
        {
            for (int i = 0; i < ColsCount; i++)
            {
                data[row1, i] ^= data[row2, i];
            }
        }

        public void SwapCols(int col1, int col2)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                (data[i, col2], data[i, col1]) = (data[i, col1], data[i, col2]);
            }
        }

        public void SwapRows(int row1, int row2)
        {
            for (int i = 0; i < ColsCount; i++)
            {
                (data[row2, i], data[row1, i]) = (data[row1, i], data[row2, i]);
            }
        }

        // Used for really cursed and bad shit
        public bool CheckRowsSum(params int[] rows)
        {
            bool counter = false;
            for (int i = 0; i < ColsCount; i++)
            {
                bool sum = false;
                for (int t = 0; t < rows.Length; t++)
                {
                    sum ^= data[rows[t], i];
                }
                counter |= sum;
            }
            return !counter;
        }

        // @brief: simplify matrix
        private void RowReduction(out SortedSet<int> markedRows)
        {
            markedRows = [];

            for (int j = 0; j < ColsCount; j++)
            {
                for (int i = 0; i < RowsCount; i++)
                {
                    if (data[i, j])
                    {
                        markedRows.Add(i);
                        for (int t = 0; t < ColsCount; t++)
                        {
                            if (t == j) continue;
                            if (data[i, t])
                            {
                                CombineCols(t, j);
                                //Console.WriteLine("Added column {0} to {1}", j, t);
                                //Console.WriteLine(this);
                            }
                        }
                        break;
                    }
                }
            }
        }

        // ATTENTION! Якщо я красивого не придумаю(а красиве у голову не лізе), то буде щось дуже огидне.
        // Повертає майже всі солюшени
        public List<List<int>> GetAllSolutions()
        {
            List<List<int>> res = [];
            SortedSet<int> markedRows;
            RowReduction(out markedRows);

            // Console.WriteLine("Searching solution");
            // Console.WriteLine("Marked rows(determined): {0}", string.Join(',', markedRows));

            List<int> unmarkedRows = [];

            for (int i = 0; i < RowsCount; i++)
            {
                if (markedRows.Contains(i)) continue;
                unmarkedRows.Add(i);
            }

            // Console.WriteLine("Marked rows(undetermined): {0}", string.Join(',', unmarkedRows));

            foreach (var e in unmarkedRows)
            {
                List<int> solution = [e];
                for (int j = 0; j < ColsCount; j++)
                {
                    if (data[e, j])
                    {
                        foreach (int t in markedRows)
                        {
                            if (data[t, j])
                            {
                                solution.Add(t);
                            }
                        }
                    }
                }
                res.Add(solution);
            }

            return res;

        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < RowsCount; i++)
            {
                sb.Append('|');
                for (int j = 0; j < ColsCount; j++)
                {
                    if (data[i, j]) sb.Append('1');
                    else sb.Append('0');

                    if (j != ColsCount - 1) sb.Append(' ');
                }
                sb.Append("|\n");
            }

            return sb.ToString();
        }

    }
}
