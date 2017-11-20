using System;
using System.Windows.Forms;

namespace Sudoku
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new Window());
        }
    }
}