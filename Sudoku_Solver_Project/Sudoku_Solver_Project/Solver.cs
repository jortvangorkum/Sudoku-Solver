using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sudoku
{
    class Solver
    {
        int[,] sudokuBoard;
        int LinesX, LinesY;
        List<LegeBox> LegeBoxen;

        public Solver(int linesX, int linesY)
        {
            this.sudokuBoard = new int[linesX, linesY];
            this.LinesX = linesX;
            this.LinesY = linesY;
            this.LegeBoxen = new List<LegeBox>();
        }

        public void fillArrayWithNumbers(TextBox[,] TextBoxArray)
        {
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    if(TextBoxArray[t, n].Text == "")
                    {
                        LegeBoxen.Add(new LegeBox(t, n));
                    }
                    else
                    {
                        try
                        {
                            sudokuBoard[t, n] = Int32.Parse(TextBoxArray[t, n].Text);
                        }
                        catch
                        {
                            MessageBox.Show("Vul alleen getallen in.");
                        }
                    }
                }
            }
        }


        public void Oplossen()
        {

        }
    }
}