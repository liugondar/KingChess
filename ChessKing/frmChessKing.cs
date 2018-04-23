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

        private const int WhiteTurn = 0;
        private const int BlackTurn = 1;
        ChessSquare[,] Board = new ChessSquare[8, 8];
        private bool dangkeo;
        private Point diemkeo;
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

            FlashScreen FlashScr = new FlashScreen();
            FlashScr.ShowDialog();
            this.Refresh();
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
                    Board[row, col].findWayAction += new FindWayAction(OnAction);

                    this.Controls.Add(Board[row, col]);
                }
        }
        private void Display()
        {
            Chess tempChess;
            //pawn
            for (int i = 0; i < 16; i++)
            {
                tempChess = new Pawn();
                if (i < 8)
                {
                    tempChess.Team = (int)ColorTeam.Black;
                    Board[1, i].Chess = tempChess;
                    Board[1, i].Image = Image.FromFile(linkBlackPawn);
                    Board[1, i].Chess.Evaluation = -10;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[6, i - 8].Chess = tempChess;
                    Board[6, i - 8].Image = Image.FromFile(linkWhitePawn);
                    Board[6, i - 8].Chess.Evaluation = 10;
                }
            }

            //Castle
            for (int i = 0; i < 4; i++)
            {
                tempChess = new Castle();
                if (i < 2)
                {
                    tempChess.Team = (int)ColorTeam.Black;
                    Board[0, 0].Chess = tempChess;
                    Board[0, 7].Chess = tempChess;
                    Board[0, 0].Image = Image.FromFile(linkBlackCastle);
                    Board[0, 7].Image = Image.FromFile(linkBlackCastle);
                    Board[0, 0].Chess.Evaluation = -50;
                    Board[0, 7].Chess.Evaluation = -50;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 0].Chess = tempChess;
                    Board[7, 7].Chess = tempChess;
                    Board[7, 0].Image = Image.FromFile(linkWhiteCastle);
                    Board[7, 7].Image = Image.FromFile(linkWhiteCastle);
                    Board[7, 0].Chess.Evaluation = 50;
                    Board[7, 7].Chess.Evaluation = 50;

                }
            }

            //Knight
            for (int i = 0; i < 4; i++)
            {
                tempChess = new Knight();
                if (i < 2)
                {
                    tempChess.Team = (int)ColorTeam.Black;
                    Board[0, 1].Chess = tempChess;
                    Board[0, 6].Chess = tempChess;
                    Board[0, 1].Image = Image.FromFile(linkBlackKnight);
                    Board[0, 6].Image = Image.FromFile(linkBlackKnight);
                    Board[0, 1].Chess.Evaluation = -30;
                    Board[0, 6].Chess.Evaluation = -30;

                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 1].Chess = tempChess;
                    Board[7, 6].Chess = tempChess;
                    Board[7, 1].Image = Image.FromFile(linkWhiteKnight);
                    Board[7, 6].Image = Image.FromFile(linkWhiteKnight);
                    Board[7, 1].Chess.Evaluation = 30;
                    Board[7, 6].Chess.Evaluation = 30;
                }
            }

            //Bishop
            for (int i = 0; i < 4; i++)
            {
                tempChess = new Bishop();
                if (i < 2)
                {
                    tempChess.Team = (int)ColorTeam.Black;
                    Board[0, 2].Chess = tempChess;
                    Board[0, 5].Chess = tempChess;
                    Board[0, 2].Image = Image.FromFile(linkBlackBishop);
                    Board[0, 5].Image = Image.FromFile(linkBlackBishop);
                    Board[0, 2].Chess.Evaluation = -30;
                    Board[0, 5].Chess.Evaluation = -30;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 2].Chess = tempChess;
                    Board[7, 5].Chess = tempChess;
                    Board[7, 2].Image = Image.FromFile(linkWhiteBishop);
                    Board[7, 5].Image = Image.FromFile(linkWhiteBishop);
                    Board[7, 2].Chess.Evaluation = 30;
                    Board[7, 5].Chess.Evaluation = 30;

                }
            }

            //Queen
            tempChess = new Queen();
            tempChess.Team = (int)ColorTeam.Black;
            Board[0, 3].Chess = tempChess;
            Board[0, 3].Image = Image.FromFile(linkBlackQueen);
            Board[0, 3].Chess.Evaluation = -90;

            tempChess = new Queen();
            tempChess.Team = (int)ColorTeam.White;
            Board[7, 3].Chess = tempChess;
            Board[7, 3].Image = Image.FromFile(linkWhiteQueen);
            Board[7, 3].Chess.Evaluation = 90;

            //King
            tempChess = new King();
            tempChess.Team = (int)ColorTeam.Black;
            Board[0, 4].Chess = tempChess;
            Board[0, 4].Image = Image.FromFile(linkBlackKing);
            Board[0, 4].Chess.Evaluation = -900;

            tempChess = new King();
            tempChess.Team = (int)ColorTeam.White;
            Board[7, 4].Chess = tempChess;
            Board[7, 4].Image = Image.FromFile(linkWhiteKing);
            Board[7, 4].Chess.Evaluation = 900;

            Common.Board = Board;
        }
        private void OnAction()
        {
            //Refresh board
            if (Common.Is2PlayerMode == true)
            {
                if (Common.IsTurn % 2 == WhiteTurn) 
                {
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                }
                else //Lượt cờ đen
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                }
            }
            // Ket thuc van co
            if (Common.Close == true)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Board[i, j].Image = null;
                    }
                }
            }
            Board = Common.Board;
        }
        #region Dùng chuột di chuyển form
        private void ChessBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dangkeo = true;
                diemkeo = new Point(e.X, e.Y);
            }
            else dangkeo = false;
        }

        private void ChessBoard_MouseUp(object sender, MouseEventArgs e)
        {
            dangkeo = false;
        }

        private void ChessBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (dangkeo)
            {
                Point diemden;
                diemden = this.PointToScreen(new Point(e.X, e.Y));
                diemden.Offset(-diemkeo.X, -diemkeo.Y);
                this.Location = diemden;
            }
        }
        #endregion
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
        private void btn2Player_Click(object sender, EventArgs e)
        {
            Display();
            Common.Is2PlayerMode = true;
            bnt1Player.Enabled = false;
            btn2Player.Enabled = false;
            MessageBox.Show("White go first");
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }
        private void bnt1Player_Click(object sender, EventArgs e)
        {
            Display();
            Common.Is2PlayerMode = false;
            bnt1Player.Enabled = false;
            btn2Player.Enabled = false;
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
        }
        #endregion

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            ResetBanCo();
        }

        private void ResetBanCo()
        {
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}

