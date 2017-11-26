﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Sudoku
{
    public class Window : FormMethodes
    {
        const int LinesX = 9, LinesY = 9, BoxSize = 60, VlakSize = 3 * BoxSize, TextBoxSize = BoxSize - 5;
        public TextBox[,] TextBoxArray = new TextBox[LinesX, LinesY];
        Solver solver = new Solver(LinesX, LinesY);
        String fileName;
        Stopwatch stopwatch = new Stopwatch();

        public Window()
        {
            Button Solve = MakenButton("Solve", 600, 10, 80, 30);
            Button Reset = MakenButton("Reset", 600, 50, 80, 30);
            Button Voorbeeld = MakenButton("Voorbeeld", 600, 90, 80, 30);
            Button Open = MakenButton("Open", 600, 130, 80, 30);
            Button Opslaan = MakenButton("Opslaan", 600, 170, 80, 30);
            Panel paneel = MakenPanel(10, 10, LinesX * BoxSize + 1, LinesY * BoxSize + 1);
            fillGridWithTextBox(paneel);

            this.ClientSize += new Size(500, 500);
            this.Text = "Sudoku Solver";
            this.BackColor = Color.White;
            this.Paint += Teken;

            paneel.Paint += TekenPanel;

            Solve.Click += SolveKlik;
            Reset.Click += ResetKlik;
            Voorbeeld.Click += VoorbeeldKlik;
            Open.Click += OpenKlik;
            Opslaan.Click += OpslaanKlik;


        }

        public void Teken(object sender, PaintEventArgs pea)
        {

        }

        public void TekenPanel(object sender, PaintEventArgs pea)
        {
            this.tekenGrid(pea.Graphics);
            this.tekenDikkeGrid(pea.Graphics);
        }

        public void SolveKlik(object sender, EventArgs ea)
        {
            this.solver.MeestVoorkomendeWaarde(TextBoxArray);

            if (this.solver.fillArrayWithNumbers(TextBoxArray))
            {
                stopwatch.Start();
                this.solver.thread.Start();
                tekenSudokuBoard(this.solver);
            }
        }

        public void ResetKlik(object sender, EventArgs ea)
        {
            Solver solver = new Solver(LinesX, LinesY);
            this.solver = solver;
            cleanSudokuBoard();
        }

        public void VoorbeeldKlik(object sender, EventArgs ea)
        {
            CijferToevoegenTextBox(0, 1, 5);
            CijferToevoegenTextBox(0, 3, 6);
            CijferToevoegenTextBox(0, 7, 4);
            CijferToevoegenTextBox(1, 0, 4);
            CijferToevoegenTextBox(1, 2, 1);
            CijferToevoegenTextBox(1, 4, 2);
            CijferToevoegenTextBox(1, 6, 7);
            CijferToevoegenTextBox(1, 8, 9);
            CijferToevoegenTextBox(2, 1, 9);
            CijferToevoegenTextBox(2, 2, 8);
            CijferToevoegenTextBox(2, 7, 3);
            CijferToevoegenTextBox(3, 0, 9);
            CijferToevoegenTextBox(3, 5, 2);
            CijferToevoegenTextBox(4, 1, 8);
            CijferToevoegenTextBox(4, 7, 2);
            CijferToevoegenTextBox(5, 3, 3);
            CijferToevoegenTextBox(5, 8, 1);
            CijferToevoegenTextBox(6, 1, 3);
            CijferToevoegenTextBox(6, 6, 4);
            CijferToevoegenTextBox(6, 7, 9);
            CijferToevoegenTextBox(7, 0, 7);
            CijferToevoegenTextBox(7, 2, 9);
            CijferToevoegenTextBox(7, 4, 5);
            CijferToevoegenTextBox(7, 6, 6);
            CijferToevoegenTextBox(7, 8, 3);
            CijferToevoegenTextBox(8, 1, 2);
            CijferToevoegenTextBox(8, 5, 9);
            CijferToevoegenTextBox(8, 7, 8);
        }

        private void OpenKlik(object sender, EventArgs ea)
        {
            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.Title = "Open...";
            dialoog.Filter = "Tekst|*.txt";
            if (dialoog.ShowDialog() == DialogResult.OK)
            {
                this.fileName = dialoog.FileName;
                this.Lees();
                this.Refresh();
            }
        }

        private void OpslaanKlik(object sender, EventArgs ea)
        {
            SaveFileDialog dialoog = new SaveFileDialog();
            dialoog.Filter = "Tekst|*.txt";
            dialoog.Title = "Opslaan als...";
            if (dialoog.ShowDialog() == DialogResult.OK)
            {
                this.fileName = dialoog.FileName;
                this.Schrijven();
            }
        }

        public void tekenGrid(Graphics gr)
        {
            Pen pBlack = Pens.Black;

            for (int t = 0; t <= LinesX; t++)
            {
                for (int n = 0; n <= LinesY; n++)
                {
                    gr.DrawLine(pBlack, 0, BoxSize * n, LinesX * BoxSize, BoxSize * n);
                    gr.DrawLine(pBlack, BoxSize * t, 0, BoxSize * t, LinesY * BoxSize);
                }
            }
        }

        public void tekenDikkeGrid(Graphics gr)
        {
            Pen pBlack = new Pen(Brushes.Black, 4);

            for (int t = 0; t <= LinesX / 3; t++)
            {
                for (int n = 0; n <= LinesY / 3; n++)
                {
                    gr.DrawLine(pBlack, 0, VlakSize * n, LinesX * VlakSize, VlakSize * n);
                    gr.DrawLine(pBlack, VlakSize * t, 0, VlakSize * t, LinesY * VlakSize);
                }
            }
        }

        public void fillGridWithTextBox(Panel paneel)
        {
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for(int n = 0; n <= LinesY - 1; n++)
                {
                    this.TextBoxArray[t, n] = MakenTextBox("", BoxSize * n + (BoxSize / 2 - TextBoxSize / 2), BoxSize * t + (BoxSize / 2 - TextBoxSize / 2), TextBoxSize, TextBoxSize, paneel, TextBoxSize - 20);
                }
            }
        }

        public void tekenSudokuBoard(Solver solver)
        {
            while(solver.thread.IsAlive)
            {

            }

            stopwatch.Stop();

            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    if (solver.sudokuBoard[t, n] != 0)
                    {
                        this.TextBoxArray[t, n].Text = solver.sudokuBoard[t, n].ToString();
                    }
                }
            }

            Debug.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            stopwatch.Reset();
        }

        public void cleanSudokuBoard()
        {
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    this.TextBoxArray[t, n].Text = null;
                }
            }
        }

        private void CijferToevoegenTextBox(int x, int y, int waarde)
        {
            TextBoxArray[x, y].Text = waarde.ToString();
        }

        private void Lees()
        {
            int teller = 0;
            foreach (string regel in File.ReadAllLines(fileName))
            {
                string[] obj = regel.Split(' ');

                try
                {
                    for (int t = 0; t < 9; t++)
                    {
                        if(obj[t] != "0")
                        {
                            TextBoxArray[teller, t].Text = obj[t];
                        }
                    }
                    teller++;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
        }

        private void Schrijven()
        {
            string regel = "";

            using (StreamWriter textfile = new StreamWriter(this.fileName))
            {
                for (int t = 0; t < 9; t++)
                {
                    for (int n = 0; n < 9; n++)   
                    {
                        if(TextBoxArray[t, n].Text != "")
                        {
                            regel += TextBoxArray[t, n].Text + " ";
                        }
                        else
                        {
                            regel += "0 ";
                        }
                    }
                    textfile.WriteLine(regel);
                    regel = "";
                }
            }
        }
    }
}