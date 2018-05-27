using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
        WMPLib.WindowsMediaPlayer announcerNewGameWelcome= new WMPLib.WindowsMediaPlayer();
        SoundManager soundManager;

        private bool dangkeo;
        private Point diemkeo;

        #region enit board image
        ChessSquare[,] Board = new ChessSquare[8, 8];
      
        #endregion
        public frmChessKing()
        {
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox2.BackColor = Color.Transparent;
            Common.Is2PlayerMode = true;
            soundManager = new SoundManager();
            soundManager.InitMusic();

            FlashScreen FlashScr = new FlashScreen();
            FlashScr.ShowDialog();
            this.Refresh();
        }
        private void frmChessKing_Load(object sender, EventArgs e)
        {
            for (int row = 0; row < 8; row++)
            {
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

            for (int row = 0; row < 8; row++)
            {
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

                    Common.VirtualBoard[row, col] = temp;
                    Board[row, col].findWayAction += new FindWayAction(OnAction);

                }
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
                    Board[1, i].Image = Image.FromFile(Constants.linkBlackPawn);
                    Board[1, i].Chess.Evaluation = -10;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[6, i - 8].Chess = tempChess;
                    Board[6, i - 8].Image = Image.FromFile(Constants.linkWhitePawn);
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
                    Board[0, 0].Image = Image.FromFile(Constants.linkBlackCastle);
                    Board[0, 7].Image = Image.FromFile(Constants.linkBlackCastle);
                    Board[0, 0].Chess.Evaluation = -50;
                    Board[0, 7].Chess.Evaluation = -50;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 0].Chess = tempChess;
                    Board[7, 7].Chess = tempChess;
                    Board[7, 0].Image = Image.FromFile(Constants.linkWhiteCastle);
                    Board[7, 7].Image = Image.FromFile(Constants.linkWhiteCastle);
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
                    Board[0, 1].Image = Image.FromFile(Constants.linkBlackKnight);
                    Board[0, 6].Image = Image.FromFile(Constants.linkBlackKnight);
                    Board[0, 1].Chess.Evaluation = -30;
                    Board[0, 6].Chess.Evaluation = -30;

                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 1].Chess = tempChess;
                    Board[7, 6].Chess = tempChess;
                    Board[7, 1].Image = Image.FromFile(Constants.linkWhiteKnight);
                    Board[7, 6].Image = Image.FromFile(Constants.linkWhiteKnight);
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
                    Board[0, 2].Image = Image.FromFile(Constants.linkBlackBishop);
                    Board[0, 5].Image = Image.FromFile(Constants.linkBlackBishop);
                    Board[0, 2].Chess.Evaluation = -30;
                    Board[0, 5].Chess.Evaluation = -30;
                }
                else
                {
                    tempChess.Team = (int)ColorTeam.White;
                    Board[7, 2].Chess = tempChess;
                    Board[7, 5].Chess = tempChess;
                    Board[7, 2].Image = Image.FromFile(Constants.linkWhiteBishop);
                    Board[7, 5].Image = Image.FromFile(Constants.linkWhiteBishop);
                    Board[7, 2].Chess.Evaluation = 30;
                    Board[7, 5].Chess.Evaluation = 30;

                }
            }

            //Queen
            tempChess = new Queen();
            tempChess.Team = (int)ColorTeam.Black;
            Board[0, 3].Chess = tempChess;
            Board[0, 3].Image = Image.FromFile(Constants.linkBlackQueen);
            Board[0, 3].Chess.Evaluation = -90;

            tempChess = new Queen();
            tempChess.Team = (int)ColorTeam.White;
            Board[7, 3].Chess = tempChess;
            Board[7, 3].Image = Image.FromFile(Constants.linkWhiteQueen);
            Board[7, 3].Chess.Evaluation = 90;

            //King
            tempChess = new King();
            tempChess.Team = (int)ColorTeam.Black;
            Board[0, 4].Chess = tempChess;
            Board[0, 4].Image = Image.FromFile(Constants.linkBlackKing);
            Board[0, 4].Chess.Evaluation = -900;

            tempChess = new King();
            tempChess.Team = (int)ColorTeam.White;
            Board[7, 4].Chess = tempChess;
            Board[7, 4].Image = Image.FromFile(Constants.linkWhiteKing);
            Board[7, 4].Chess.Evaluation = 900;

            Common.Board = Board;
        }
        private void OnAction()
        {
            //Refresh board
            if (Common.Is2PlayerMode)
            {
                if (Common.IsTurn % 2 == Constants.WhiteTurn)
                {
                    pictureBox2.Visible = true;
                    pictureBox1.Visible = false;
                }
                else //Lượt cờ đen
                {
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = false;
                }
                //Kết thúc lượt đổi vị trí kết thúc lượt
                
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
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Common.Depth = 2;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Common.Depth = 3;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void veryHardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.Depth = 4;
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
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
            soundManager.SwapMusicState();
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            soundManager.SwapMusicState();
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void onToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            soundManager.SwapSoundState();
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }

        private void offToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            soundManager.SwapSoundState();
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
        }
        private void teamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frmAbout = new Form2();
            frmAbout.ShowDialog();
        }
        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "https://vi.wikipedia.org/wiki/C%E1%BB%9D_vua";
            System.Diagnostics.Process.Start(url);
        }
      
        #endregion
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            InitNewGame();
        }

        private void InitNewGame()
        {
            ResetBanCo();
            Display();
            Common.IsPlaying = true;
            Common.Close = false;
            Common.Is2PlayerMode = twoplayerToolStripMenuItem1.Checked ?
                true : false;

            soundManager.PlayAnnouncerWelcome();
           
            if (Common.Is2PlayerMode)
            {
                MessageBox.Show("White go first");
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
            }
        }

        private void ResetBanCo()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    Board[row, col].Chess = null;
                    Board[row, col].Image = null;
                }
            Common.ResetPropToDefault(); 
        }
        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void oneplayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
            InitNewGame();

        }
        private void twoplayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UncheckOtherToolStripMenuItems((ToolStripMenuItem)sender);
            InitNewGame();

        }

        private void subietm2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        public void UncheckOtherToolStripMenuItems(ToolStripMenuItem selectedMenuItem)
        {
            selectedMenuItem.Checked = true;

            // Select the other MenuItens from the ParentMenu(OwnerItens) and unchecked this,
            // The current Linq Expression verify if the item is a real ToolStripMenuItem
            // and if the item is a another ToolStripMenuItem to uncheck this.
            foreach (var ltoolStripMenuItem in (from object
                                                    item in selectedMenuItem.Owner.Items
                                                let ltoolStripMenuItem = item as ToolStripMenuItem
                                                where ltoolStripMenuItem != null
                                                where !item.Equals(selectedMenuItem)
                                                select ltoolStripMenuItem))
                (ltoolStripMenuItem).Checked = false;

            // This line is optional, for show the mainMenu after click
        }

      
    }
}

