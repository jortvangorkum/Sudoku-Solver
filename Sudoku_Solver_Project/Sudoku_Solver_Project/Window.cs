using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    public class Window : FormMethodes
    {
        const int Gridx = 9, Gridy = 9, BoxSize = 60;

        public Window()
        {
            this.ClientSize += new Size(500, 500);
            this.Paint += Teken;
            this.MouseClick += Klik;
            this.Text = "Sudoku Solver";
            this.BackColor = Color.White;
        }

        public void Teken(object sender, PaintEventArgs pea)
        {
            this.tekenGrid(pea.Graphics);
        }

        public void Klik(object sender, EventArgs ea)
        {

        }

        public void tekenGrid(Graphics gr)
        {
            Pen pBlack = Pens.Black;

            for (int t = 0; t <= Gridx; t++)
            {
                for (int n = 0; n <= Gridy; n++)
                {
                    //De breedte en hoogte van de vakjes is gelijk aan de diameter van de stenen.
                    gr.DrawLine(pBlack, 0, BoxSize * n, Gridx * )
                }
            }
        }
    }
}