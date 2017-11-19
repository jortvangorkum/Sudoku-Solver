using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    public class Solver
    {
        int LinesX, LinesY;
        int teller = 0;
        public int[,] sudokuBoard;
        List<LegeBox> LegeBoxen;

        public Solver(int linesX, int linesY)
        {
            this.sudokuBoard = new int[linesX, linesY];
            this.LinesX = linesX;
            this.LinesY = linesY;
            this.LegeBoxen = new List<LegeBox>();
        }

        public bool fillArrayWithNumbers(TextBox[,] TextBoxArray)
        {
            bool res = true;

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
                            return res = false;
                        }
                    }
                }
            }
            return res;
        }


        public void Oplossen()
        {
            if (teller == LegeBoxen.Count || teller < 0)
            {
                return;
            }
            else
            {
                LegeBox legebox = LegeBoxen[teller];
                MogelijkeWaardenAflopen(legebox);
            }
        }

        public void MogelijkeWaardenAflopen(LegeBox legebox)
        {
            if(legebox.MogelijkeWaarden.Count == 0)
            {
                teller--;
                Oplossen();
            }
            else
            {
                int mogelijkewaarde = legebox.MogelijkeWaarden.First();
                legebox.MogelijkeWaarden.Remove(mogelijkewaarde);
                if (VoldoetAanEisen(legebox.X, legebox.Y, mogelijkewaarde, legebox.Vlak))
                {
                    this.sudokuBoard[legebox.X, legebox.Y] = mogelijkewaarde;
                    teller++;
                    Oplossen();
                }
                else
                {
                    MogelijkeWaardenAflopen(legebox);
                }
            }
        }

        public bool VoldoetAanEisen(int x, int y, int mogelijkewaarde, Point vlak)
        {
            bool res = false;
            if(CheckVerticaal(x, y, mogelijkewaarde) && CheckHorizontaal(x, y, mogelijkewaarde) && CheckVlak(x, y, mogelijkewaarde, vlak))
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
                for (int n = vlak.Y * 3 - 3; n < vlak.Y * 3; n++)
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