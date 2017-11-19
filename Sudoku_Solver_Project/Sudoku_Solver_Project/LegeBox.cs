using System.Collections.Generic;
using System.Drawing;

namespace Sudoku
{
    public class LegeBox
    {
        public int X, Y;
        public Point Vlak;
        public ICollection<int> MogelijkeWaarden;

        public LegeBox(int x, int y, Point vlak)
        {
            this.X = x;
            this.Y = y;
            this.Vlak = vlak;
            MogelijkeWaarden = new List<int>();
            MogelijkeWaardenToevoegen();
        }

        private void MogelijkeWaardenToevoegen()
        {
            for (int t = 1; t <= 9; t++)
            {
                this.MogelijkeWaarden.Add(t);
            }
        }
    }
}