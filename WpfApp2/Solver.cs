using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Solver
    {


        //Check if checksum equals 45 in the row.
        private bool containsRow(int[,] field)
        {
            int compare = 0;
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                compare = 0;
                for (int j = 0; j < 9; j++)
                {
                    compare += field[i, j];

                }

                if (compare.Equals(45)) count++;
            }
            if (count == 9) return true;
            return false;
        }

        //Check if checksum equals 45 in the col.
        private bool containsCol(int[,] field)
        {
            int compare = 0;
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                compare = 0;
                for (int j = 0; j < 9; j++)
                {
                    compare += field[j, i];

                }

                if (compare.Equals(45)) count++;
            }
            if (count == 9) return true;
            return false;
        }

        //Check if checksum equals 45 in the 3x3 box.
        private bool containsGrid(int[,] field)
        {
            int compare = 0;
            int count = 0;
            int row = 0;
            int col;
            int a = 0;
            int colz = 0;

            do
            {
                switch (a)
                {
                    case 0: row = 0; colz = 0; break;
                    case 1: row = 0; colz = 3; break;
                    case 2: row = 0; colz = 6; break;
                    case 3: row = 3; colz = 0; break;
                    case 4: row = 3; colz = 3; break;
                    case 5: row = 3; colz = 6; break;
                    case 6: row = 6; colz = 0; break;
                    case 7: row = 6; colz = 3; break;
                    case 8: row = 6; colz = 6; break;


                }
                for (int i = 0; i < 3; i++)
                {
                    col = colz;
                    for (int j = 0; j < 3; j++)
                    {
                        compare += field[row, col];

                        col++;
                    }
                    row++;
                    if (compare.Equals(45)) count++;

                }
                a++;
                compare = 0;
            } while (a < 9);
            if (count == 9) return true;
            return false;
        }

        private bool check(int[,] field)
        {
            if (containsRow(field)) return true;
            if (containsCol(field)) return true;
            if (containsGrid(field)) return true;
            else return false;
        }

        //Check if sudoku is valid.
        public int solved(int[,] field)
        {
            int solver = 0;


            if (check(field) == true)
            {               
                solver++;
            }
            return solver;
        }
    }
}
