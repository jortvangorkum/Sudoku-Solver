using System;

namespace Sudoku
{
    class Solver
    {
        int[,] sudokuBoard;

        public Solver()
        {
            this.sudokuBoard = new int[9, 9];
            fillArrayWithNumbers();
            Oplossen();
        }

        public void fillArrayWithNumbers()
        {

        }

        public void Oplossen()
        {

        }
    }
}