using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Generate
    {
        private int nmbr;
        private int coordinateX = 0;
        private int coordinateY = 0;
        private int[,] field;

        //Check if number is already used in row.
        private bool containsRow(int[,] field, int row, int nmbr)
        {
            for (int i = 0; i < 9; i++)
            {
                if (field[row, i] == nmbr)
                {
                    setPosX(row);
                    setPosY(i);
                    return true;
                }
            }
            return false;
        }

        //Check if number is already used in column.
        private bool containsCol(int[,] field, int col, int nmbr)
        {
            for (int i = 0; i < 9; i++)
            {
                if (field[i, col] == nmbr)
                {
                    setPosX(i);
                    setPosY(col);
                    return true;
                }
            }
            return false;
        }

        //Check if number is already used in 3x3 box.
        private bool containsGrid(int[,] field, int row, int col, int nmbr)
        {

            int colz = 0;
            if (row <= 2) row = 0;
            if (row <= 5 && row >= 3) row = 3;
            if (row <= 8 && row >= 6) row = 6;

            if (col <= 2) colz = 0;
            if (col <= 5 && col >= 3) colz = 3;
            if (col <= 8 && col >= 6) colz = 6;

            col = colz;
            for (int i = 0; i < 3; i++)
            {
                col = colz;
                for (int j = 0; j < 3; j++)
                {
                    if (field[row, col] == nmbr)
                    {
                        setPosX(row);
                        setPosY(col);
                        return true;
                    }
                    col++;
                }
                row++;
            }

            return false;
        }

        private bool check(int[,] field, int row, int col, int nmbr)
        {
            if (containsRow(field, row, nmbr)) return true;
            if (containsCol(field, col, nmbr)) return true;
            if (containsGrid(field, row, col, nmbr)) return true;
            else return false;
        }


        //Generates a valid sudoku. Saved in a [9,9] array.
        public int[,] sudoku(int difficulty)
        {
            int complete = 81;
            int counter = 0;
            field = new int[9, 9];
            int ersetzt = 0;
            int x;
            int y;
            Random random = new Random();

            //Runs while the array still has missing numbers.
            while (complete > 0)
            {

                //fiss array with random numbers. while number is valid.
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (field[i, j] == 0)
                        {


                            do
                            {

                                nmbr = random.Next(1, 10);
                                
                                // if there is no matching number, the last blocking number gets removed.
                                if (counter > 50)
                                {                                  
                                    field[getPosY(), getPosX()] = 0;
                                    complete++;
                                    counter = 0;
                                    ersetzt++;
                                    setPosX(0);
                                    setPosY(0);
                                }
                                counter++;
                            } while (check(field, i, j, nmbr));

                            counter = 0;
                           
                            field[i, j] = nmbr;
                            complete--;                      
                        }
                      
                    }
                }
            }

            // Remove numbers from the valid sudoku. These numbers, replaced by 0, are the numbers, the user has to enter later.
            while (difficulty > 0)
            {
                x = random.Next(0, 9);
                y = random.Next(0, 9);

                if (field[x, y] != 0)
                {
                    field[x, y] = 0;
                    difficulty--;
                }

            }

            return field;


        }

        //(get)Coordinate for blocking number.
        private int getPosY()
        {
            return coordinateX;
        }

        //(get)Coordinate for blocking number.
        private int getPosX()
        {
            return coordinateY;
        }

        //(set)Coordinate for blocking number.
        public void setPosX(int coordinateX)
        {
            this.coordinateX = coordinateX;
        }

        //(set)Coordinate for blocking number.
        public void setPosY(int coordinateY)
        {
            this.coordinateY = coordinateY;
        }

    }
}

