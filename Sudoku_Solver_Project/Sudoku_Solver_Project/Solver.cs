using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    class Solver
    {
        int LinesX, LinesY;
        int teller = 0;
        int[,] sudokuBoard;
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
                        LegeBoxen.Add(new LegeBox(t, n, WelkeVlak(t, n)));
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
            

            if (LegeBoxen.Count == 0)
            {
                return;
            }
            else
            {
                LegeBox legebox = LegeBoxen[teller];
                if(VoldoetAanEisen(legebox.X, legebox.Y, legebox.MogelijkeWaarden.First(), legebox.Vlak))
                {
                    teller++;
                    Oplossen();
                }
                else
                {
                    return;
                }
            }
        }

        public bool VoldoetAanEisen(int x, int y, int mogelijkewaarde, Point vlak)
        {
            bool res = false;
            if(CheckVerticaal(x, y, mogelijkewaarde) && CheckHorizontaal(x, y, mogelijkewaarde))
            {
                res = true;
            }
            return res;
        }

        private bool CheckVerticaal(int x, int y, int mogelijkewaarde)
        {
            bool res = false;
            for(int t = 0; t <= LinesY - 1; t++)
            {
                if (sudokuBoard[x, t].Equals(mogelijkewaarde))
                {
                    return res = false;
                }
                else
                {
                    res = true;
                }
            }
            return res;
        }

        private bool CheckHorizontaal (int x, int y, int mogelijkewaarde)
        {
            bool res = false;
            for (int t = 0; t <= LinesX - 1; t++)
            {
                if (sudokuBoard[t, y].Equals(mogelijkewaarde))
                {
                    return res = false;
                }
                else
                {
                    res = true;
                }
            }
            return res;
        }

        private bool CheckVlak (int x, int y, int mogelijkewaarde, Point vlak)
        {
            bool res = false;
            for (int t = vlak.X * 3 - 3; t < vlak.X * 3; t++)
            {
                for (int n = vlak.X * 3 - 3; n < vlak.Y * 3; n++)
                {
                    if(sudokuBoard[t, n].Equals(mogelijkewaarde))
                    {
                        return res = false;
                    }
                    else
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        private Point WelkeVlak (int x, int y)
        {
            Point res = new Point();
            res.X = x / 3 + 1;
            res.Y = y / 3 + 1;
            return res;
        }
    }
}