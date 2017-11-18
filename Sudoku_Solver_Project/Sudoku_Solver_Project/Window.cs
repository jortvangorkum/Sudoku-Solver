using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    public class Window : FormMethodes
    {
        const int LinesX = 9, LinesY = 9, BoxSize = 60;
        TextBox[,] TextBoxen = new TextBox[LinesX, LinesY];

        public Window()
        {
            Button Solve = MakenButton("Solve", 600, 10, 80, 30); 
            Panel paneel = MakenPanel(0, 0, LinesX * BoxSize + 1, LinesY * BoxSize + 1);
            fillGridWithTextBox(paneel);

            this.ClientSize += new Size(500, 500);
            this.Text = "Sudoku Solver";
            this.BackColor = Color.White;
            this.Paint += Teken;

            paneel.Paint += TekenPanel;

            Solve.Click += SolveKlik;
        }

        public void Teken(object sender, PaintEventArgs pea)
        {

        }

        public void TekenPanel(object sender, PaintEventArgs pea)
        {
            this.tekenGrid(pea.Graphics);
        }

        public void SolveKlik(object sender, EventArgs ea)
        {
            Solver solver = new Solver();
        }

        public void tekenGrid(Graphics gr)
        {
            Pen pBlack = Pens.Black;

            for (int t = 0; t <= LinesX; t++)
            {
                for (int n = 0; n <= LinesY; n++)
                {
                    //De breedte en hoogte van de vakjes is gelijk aan de diameter van de stenen.
                    gr.DrawLine(pBlack, 0, BoxSize * n, LinesX * BoxSize, BoxSize * n);
                    gr.DrawLine(pBlack, BoxSize * t, 0, BoxSize * t, LinesY * BoxSize);
                }
            }
        }

        public void fillGridWithTextBox(Panel paneel)
        {
            for (int t = 0; t <= LinesX - 1; t++)
            {
                for(int n = 0; n <= LinesY - 1; n++)
                {
                    this.TextBoxen[t, n] = MakenTextBox("", BoxSize * t, BoxSize * n, BoxSize, BoxSize, paneel, BoxSize - 20);
                }
            }
        }
    }
}