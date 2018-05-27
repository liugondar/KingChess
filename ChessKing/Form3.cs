using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChessKing
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            Process.Start("Help\\HD.pdf");
            this.Close();
        }
    }
}
