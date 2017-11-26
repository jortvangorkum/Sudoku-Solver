using System.Collections.Generic;
using System.Drawing;

namespace Sudoku
{
    public class LegeBox
    {
        public int X, Y;
        public Point Vlak;
        public ICollection<int> MogelijkeWaarden;
       

        public LegeBox(int x, int y, Point vlak, ICollection<int> mogelijkewaarden)
        {
            this.X = x;
            this.Y = y;
            this.Vlak = vlak;
            MogelijkeWaarden = mogelijkewaarden;
        }
    }
}