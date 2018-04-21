using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessKing
{
    class ChessSquare : Button
    {
        string linkBlackQueen = "Image\\Chess_qdt60.png";
        string linkWhiteQueen = "Image\\Chess_qlt60.png";
        #region init fields
        enum ColorTeam
        {
            None,
            White,
            Black,
        };
        public Chess Chess { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public FindWayAction findWayAction;
        public ChessSquare(ChessSquare a)
        {
            Chess = a.Chess;
            Image = a.Image;
            Row = a.Row;
            Col = a.Col;
        }
        public ChessSquare()
        {
            //change properties of button
            this.Size = new System.Drawing.Size(60, 60);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
        #endregion
        /// <summary>
        /// Tạo bàng cờ 8x8 Trống
        /// </summary>
        public void BackChessBoard()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 0)
                            Common.Board[row, col].BackColor = Color.LavenderBlush;
                        else
                            Common.Board[row, col].BackColor = Color.DarkSlateGray;
                    }
                    else
                    {
                        if (col % 2 == 0)
                            Common.Board[row, col].BackColor = Color.DarkSlateGray;
                        else
                            Common.Board[row, col].BackColor = Color.LavenderBlush;
                    }
                }
        }

        private List<ChessSquare[,]> avalBoard = new List<ChessSquare[,]>();

        protected override void OnClick(EventArgs e)
        {
            //TODO: tao event onclick tung o co 
          
        }

        private void phongHau(ref ChessSquare temp)
        {
            Chess newQueen = new Queen();
            if (temp.Chess.Team == 1) //white
            {
                //Common.Board[Common.RowSelected, Common.ColSelected].Image = null;
                //Common.Board[Common.RowSelected, Common.ColSelected].Chess = null;
                newQueen.Team = (int)ColorTeam.White;
                temp.Chess = newQueen;
                temp.Image = Image.FromFile(linkWhiteQueen);
                temp.Chess.Evaluation = 90;
            }
            else //black
            {
                newQueen.Team = (int)ColorTeam.Black;
                temp.Chess = newQueen;
                temp.Image = Image.FromFile(linkBlackQueen);
                temp.Chess.Evaluation = -90;
            }
        }

        private void Check(ref bool temp, ref ChessSquare KingTemp)
        {
            //check if King is danger
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess != null)
                    {
                        Common.Board[i, j].Chess.FindWay(Common.Board, Common.Board[i, j].Row, Common.Board[i, j].Col);
                        for (int k = 0; k < Common.CanMove.Count; k++)
                        {
                            if (Common.CanMove[k].Chess == null)
                            {
                                Common.CanMove[k].Image = null;
                            }
                            else
                            {
                                if (Common.CanMove[k].Chess.IsKing == true)
                                {
                                    temp = true;
                                    KingTemp = new ChessSquare(Common.CanMove[k]);
                                }
                                else
                                { }
                            }
                        }
                    }
                    else { }
                    Common.CanMove.Clear();
                }
            if (temp == true) return;
            temp = false;
        }

        private void Checkmate(ChessSquare temp)
        {
            bool checkmate = true;
            //temp.Chess.FindWay(Common.Board, temp.Row, temp.Col);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess != null)
                    {
                        if (Common.Board[i, j].Chess.Team != temp.Chess.Team)
                        {
                            Common.Board[i, j].Chess.FindWay(Common.Board, Common.Board[i, j].Row, Common.Board[i, j].Col);
                            for (int k = 0; k < Common.CanMove.Count; k++)
                            {
                                if (Common.CanMove[k].Chess == null)
                                {
                                    Common.CanMove[k].Image = null;
                                }
                                Common.CanEat.Add(Common.CanMove[k]);
                            }
                            Common.CanMove.Clear();
                        }
                    }
                    else
                    {
                    }
                }
            Common.CanMove.Clear();

            temp.Chess.FindWay(Common.Board, temp.Row, temp.Col);

            for (int i = 0; i < Common.CanMove.Count; i++)
            {
                if (Common.CanMove[i].Chess == null) Common.CanMove[i].Image = null;
            }

            if (Common.CanMove.Count == 1)
            {
                if (Common.CanEat.Contains(Common.CanMove[Common.CanMove.Count - 1]))
                {
                    ChessSquare temp2 = new ChessSquare(Common.CanMove[Common.CanMove.Count - 1]);
                    Common.CanMove.Clear();

                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (Common.Board[i, j].Chess != null)
                            {
                                if (Common.Board[i, j].Chess.Team == temp.Chess.Team)
                                {
                                    Common.Board[i, j].Chess.FindWay(Common.Board, Common.Board[i, j].Row, Common.Board[i, j].Col);
                                    for (int k = 0; k < Common.CanMove.Count; k++)
                                    {
                                        if (Common.CanMove[k].Chess == null)
                                        {
                                            Common.CanMove[k].Image = null;
                                        }
                                        if (Common.CanMove[k].Col == temp2.Col && Common.CanMove[k].Row == temp2.Row)
                                        {
                                            checkmate = false;
                                            break;
                                        }
                                    }
                                    Common.CanMove.Clear();
                                }
                            }
                            else
                            {
                            }
                        }
                }
            }
            else
            {
                while (Common.CanMove.Count > 0)
                {
                    if (!Common.CanEat.Contains(Common.CanMove[Common.CanMove.Count - 1])) checkmate = false;
                    Common.CanMove.Remove(Common.CanMove[Common.CanMove.Count - 1]);
                }
            }

            if (checkmate == true)
            {
                if (temp.Chess.Team == (int)ColorTeam.White) MessageBox.Show("The Black Wins");
                else MessageBox.Show("The White Wins");

                Common.Close = true;
            }
        }
    }
}
