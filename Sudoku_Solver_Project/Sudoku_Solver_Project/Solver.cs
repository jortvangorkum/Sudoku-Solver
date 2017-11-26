using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sudoku
{
    public class Solver
    {
        //LinesX aantal vakjes in de x-richting. LinesY aantal vakjes in de y-richting.
        int LinesX, LinesY;
        //Een int array die de waarde van de sudokuBoard onthoudt.
        public int[,] sudokuBoard;
        //Een list van legeboxen waarin we de mogelijkewaarde kunnen uitproberen.
        List<LegeBox> LegeBoxen;
        //Een thread wordt aangemaakt om de oplossing in te runnen.
        public Thread thread;
        //Een teller die bijhoudt hoe vaak er wordt gereset.
        int garbageteller;
        private ICollection<int>[,] CollectieMogelijkeWaarden;

        public Solver(int linesX, int linesY)
        {
            this.sudokuBoard = new int[linesX, linesY];
            this.LinesX = linesX;
            this.LinesY = linesY;
            this.LegeBoxen = new List<LegeBox>();
            this.thread = new Thread(() => Oplossen(0), 1000000000);
            this.CollectieMogelijkeWaarden = new List<int>[linesX, linesY];
            MogelijkeWaardenCollectionToevoegen();
        }

        //In deze methode wordt gecheckt of een textbox leeg is, zo ja voeg het toe aan de legeboxen list. Zo niet, voeg het aan de sudokuBoard array toe.
        public bool fillsudokuBoardWithNumbers(TextBox[,] TextBoxArray)
        {
            bool res = true;

            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    if (TextBoxArray[t, n].Text != "")
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

        public void legeBoxenToevoegen(TextBox[,] TextBoxArray)
        {
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    if (TextBoxArray[t, n].Text == "")
                    {
                        LegeBoxen.Add(new LegeBox(t, n, WelkeVlak(t, n), new List<int>(CollectieMogelijkeWaarden[t, n])));
                    }
                }
            }
        }

        //Dit is de methode die de sudoku probeert op te lossen, door elk mogelijke waarde af te gaan en als het niet lukt of de volgende mogelijke waarde te proberen of weer terug te gaan naar de vorige vak.
        public void Oplossen(int teller)
        {
            if (teller == LegeBoxen.Count || teller < 0)
            {
                return;
            }
            else
            {
                LegeBox legebox = LegeBoxen[teller];
                MogelijkeWaardenAflopen(legebox, teller);
            }
        }

        //Hierin worden de mogelijke waarden afgelopen.
        public void MogelijkeWaardenAflopen(LegeBox legebox, int teller)
        {

            if (legebox.MogelijkeWaarden.Count == 0)
            {
                ResetMogelijkeWaarde(teller);
                ResetSudokuBoard(teller);
                GarbageCollector();
                Oplossen(--teller);
            }
            else
            {
                int mogelijkewaarde = legebox.MogelijkeWaarden.First();
                legebox.MogelijkeWaarden.Remove(mogelijkewaarde);
                if (VoldoetAanEisen(legebox.X, legebox.Y, mogelijkewaarde, legebox.Vlak))
                {
                    this.sudokuBoard[legebox.X, legebox.Y] = mogelijkewaarde;
                    Oplossen(++teller);
                }
                else
                {
                    MogelijkeWaardenAflopen(legebox, teller);
                }
            }
        }

        //Een mogelijke waarde moet voldoen aan de eisen. Geen cijfer in de horizontale of verticale rij en niet in dezelfde 3 bij 3 vak.
        public bool VoldoetAanEisen(int x, int y, int mogelijkewaarde, Point vlak)
        {
            bool res = false;
            if(CheckVerticaal(x, y, mogelijkewaarde) && CheckHorizontaal(x, y, mogelijkewaarde) && CheckVlak(x, y, mogelijkewaarde, vlak))
            {
                res = true;
            }
            return res;
        }

        //Checkt verticaal of een cijfer al voorkomt.
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

        //Checkt horizontaal of een cijfer al voorkomt.
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

        //Checkt in het vlak of een cijfer al voorkomt.
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

        //Bepaalt in welk vak een vakje zit.
        private Point WelkeVlak (int x, int y)
        {
            Point res = new Point();
            res.X = x / 3 + 1;
            res.Y = y / 3 + 1;
            return res;
        }

        //Reset de mogelijkewaarde van de boxen als de teller wordt terug gezet.
        public void ResetMogelijkeWaarde(int teller)
        {
            for (int t = teller; t < LegeBoxen.Count(); t++)
            {
                LegeBoxen[t].MogelijkeWaarden = null;
                LegeBoxen[t].MogelijkeWaarden = new List<int>(CollectieMogelijkeWaarden[LegeBoxen[t].X, LegeBoxen[t].Y]);
            }
        }

        //Reset de sudokuboard zodat elke waarde 0 is.
        public void ResetSudokuBoard (int teller)
        {
            for (int t = teller; t < LegeBoxen.Count(); t++)
            {
                LegeBox legebox = LegeBoxen[t];
                sudokuBoard[legebox.X, legebox.Y] = 0;
            }
        }

        //Garbage collect naar 1000 keer de teller wordt terug gezet.
        private void GarbageCollector ()
        {
            if(garbageteller >= 1000)
            {
                GC.Collect();
                garbageteller = 0;
            }
            else
            {
                garbageteller++;
            }
        }

        public void MogelijkeWaardenBepalen(TextBox[,] TextBoxArray)
        {
            for (int t = 0; t < 9; t++)
            {
                for (int n = 0; n < 9; n++)
                {
                    if (TextBoxArray[t, n].Text == "")
                    {
                        for(int a = 1; a <= 9; a++)
                        {
                            if (VoldoetAanEisen(t, n, a, WelkeVlak(t, n)))
                            {
                                this.CollectieMogelijkeWaarden[t, n].Add(a);
                            }
                        }
                    }
                }
            }
        }

        public void MogelijkeWaardenCollectionToevoegen()
        {
            for (int t = 0; t < 9; t++)
            {
                for (int n = 0; n < 9; n++)
                {
                    CollectieMogelijkeWaarden[t, n] = new List<int>();
                }
            }
        }
    }
}