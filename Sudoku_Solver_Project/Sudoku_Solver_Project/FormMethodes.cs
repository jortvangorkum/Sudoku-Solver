using System.Windows.Forms;
using System.Drawing;

namespace Sudoku
{
    public class FormMethodes : Form
    {
        //Methode om een label aan te maken.
        public Label MakenLabel(string naam, int x, int y, int w, int h, int lettergrootte)
        {
            Label tekst;
            tekst = new Label();
            tekst.Location = new Point(x, y);
            tekst.Size = new Size(w, h);
            tekst.Font = new Font(tekst.Font.FontFamily, lettergrootte);
            tekst.Text = naam;
            Controls.Add(tekst);
            return tekst;

        }

        //Methode om een textbox aan te maken.
        public TextBox MakenTextBox(string waarde, int x, int y, int w, int h)
        {
            TextBox Box;
            Box = new TextBox();
            Box.Location = new Point(x, y);
            Box.Size = new Size(w, h);
            Box.Text = waarde;
            Controls.Add(Box);
            return Box;
        }

        //Methode om Buttons aan te maken.
        public Button MakenButton(string naam, int x, int y, int w, int h)
        {
            Button knop;
            knop = new Button();
            knop.Location = new Point(x, y);
            knop.Size = new Size(w, h);
            knop.Text = naam;
            Controls.Add(knop);
            return knop;
        }

        //Methode om een ListBox aan te maken.
        public ListBox MakenListBox(string naam1, string naam2, string naam3, string naam4, int x, int y, int w, int h)
        {
            ListBox list1;
            list1 = new ListBox();
            list1.Location = new Point(x, y);
            list1.Size = new Size(w, h);
            list1.Items.Add(naam1);
            list1.Items.Add(naam2);
            list1.Items.Add(naam3);
            list1.Items.Add(naam4);
            Controls.Add(list1);
            return list1;
        }

        //Methode om een Paneel aan te maken.
        public Panel MakenPanel(int x, int y, int w, int h)
        {
            Panel paneel;
            paneel = new Panel();
            paneel.Location = new Point(x, y);
            paneel.Size = new Size(w, h);
            Controls.Add(paneel);
            return paneel;
        }
    }
}