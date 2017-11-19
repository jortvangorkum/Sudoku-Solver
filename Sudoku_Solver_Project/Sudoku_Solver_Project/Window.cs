using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    public class Window : FormMethodes
    {
        const int LinesX = 9, LinesY = 9, BoxSize = 60, VlakSize = 3 * BoxSize, TextBoxSize = BoxSize - 5;
        TextBox[,] TextBoxArray = new TextBox[LinesX, LinesY];
        Solver solver = new Solver(LinesX, LinesY);

        public Window()
        {
            Button Solve = MakenButton("Solve", 600, 10, 80, 30);
            Button Reset = MakenButton("Reset", 600, 50, 80, 30);
            Panel paneel = MakenPanel(10, 10, LinesX * BoxSize + 1, LinesY * BoxSize + 1);
            fillGridWithTextBox(paneel);

            this.ClientSize += new Size(500, 500);
            this.Text = "Sudoku Solver";
            this.BackColor = Color.White;
            this.Paint += Teken;

            paneel.Paint += TekenPanel;

            Solve.Click += SolveKlik;
            Reset.Click += ResetKlik;
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
            if(this.solver.fillArrayWithNumbers(TextBoxArray))
            {
                this.solver.Oplossen();
                tekenSudokuBoard(this.solver);
            }
        }

        public void ResetKlik(object sender, EventArgs ea)
        {
            Solver solver = new Solver(LinesX, LinesY);
            this.solver = solver;
            cleanSudokuBoard();
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
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for (int n = 0; n <= LinesY - 1; n++)
                {
                    this.TextBoxArray[t, n].Text = solver.sudokuBoard[t, n].ToString();
                }
            }
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
    }
}