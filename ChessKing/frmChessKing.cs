using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace ChessKing
{
    public delegate void FindWayAction();

    enum ColorTeam
    {
        None,
        White,
        Black,
    };

    public partial class frmChessKing : Form
    {
        WMPLib.WindowsMediaPlayer Player = new WMPLib.WindowsMediaPlayer();

        ChessSquare[,] Board = new ChessSquare[8, 8];

        string linkWhiteCastle = "Image\\Chess_rlt60.png";
        string linkWhiteBishop = "Image\\Chess_blt60.png";
        string linkWhiteKnight = "Image\\Chess_nlt60.png";
        string linkWhiteQueen = "Image\\Chess_qlt60.png";
        string linkWhiteKing = "Image\\Chess_klt60.png";
        string linkWhitePawn = "Image\\Chess_plt60.png";
        string linkBlackCastle = "Image\\Chess_rdt60.png";
        string linkBlackBishop = "Image\\Chess_bdt60.png";
        string linkBlackKnight = "Image\\Chess_ndt60.png";
        string linkBlackQueen = "Image\\Chess_qdt60.png";
        string linkBlackKing = "Image\\Chess_kdt60.png";
        string linkBlackPawn = "Image\\Chess_pdt60.png";



        public frmChessKing()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            Player.URL = "Sound.mp3";
            Player.settings.autoStart = true;
        }
        private void frmChessKing_Load(object sender, EventArgs e)
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    ChessSquare temp = new ChessSquare();
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 0) temp.BackColor = Color.LavenderBlush;
                        else temp.BackColor = Color.DarkSlateGray;
                    }
                    else
                    {
                        if (col % 2 == 0) temp.BackColor = Color.DarkSlateGray;
                        else temp.BackColor = Color.LavenderBlush;
                    }

                    temp.Location = new Point((col + 1) * 60, (row + 1) * 60);
                    temp.Row = row;
                    temp.Col = col;

                    Board[row, col] = temp;

                    this.Controls.Add(Board[row, col]);
                }
        }


        #region Events
        private void bntQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Common.Depth = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Common.Depth = 2;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Common.Depth = 3;
        }
        private void veryHardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.Depth = 4;
        }

        private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "http://www.wikihow.com/Play-Chess";
            System.Diagnostics.Process.Start(url);
        }

        private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frmHelp = new Form3();
            frmHelp.ShowDialog();
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.controls.play();
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Player.controls.stop();
        }

        private void teamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frmAbout = new Form2();
            frmAbout.ShowDialog();
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/thanhthinn1997/simple-chess-programming/wiki/Gi%E1%BB%9Bi-thi%E1%BB%87u-Game-C%E1%BB%9D-vua";
            System.Diagnostics.Process.Start(url);
        }

        #endregion
    }
}

