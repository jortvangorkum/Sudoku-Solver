using System.Collections.Generic;

namespace Sudoku
{
    public class LegeBox
    {
        int X, Y;
        ICollection<int> MogelijkeWaarden;

        public LegeBox(int x, int y)
        {
            this.X = x;
            this.Y = y;
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