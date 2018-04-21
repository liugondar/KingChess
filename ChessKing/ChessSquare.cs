﻿using System;
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
        const int WhiteTurn = 0;
        const int BlackTurn = 1;
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
            //Không làm gì hết nếu ô clicked không có quân cờ
            if (this.Chess == null && !Common.IsSelectedSquare) return;

            TraLaiOCoBinhThuongKhiClick2Lan();
            ThayDoiOCoKhiClickVaoOCoQuanCo();
        }

        private void ThayDoiOCoKhiClickVaoOCoQuanCo()
        {
            // Quân trắng luôn do người chơi dùng nên không cần xét đến AI
            if (Common.IsTurn % 2 == WhiteTurn)
            {
                if (Common.IsSelectedSquare == false) // chua click 
                {
                    if (this.Chess.Team == (int)ColorTeam.White)
                    {
                        this.ChangeTurn();
                    }
                    else
                        return;
                }
                else //da click
                {
                    this.ChangeTurn();
                }
            }
            else // black
            {
                if (Common.Is2PlayerMode)
                {
                    if (Common.IsSelectedSquare == false)//chua click
                    {
                        if (this.Chess.Team == (int)ColorTeam.Black) // check xem co dung team dang duoc di hay khong
                        {
                            this.ChangeTurn();
                        }
                        else return;
                    }
                    else this.ChangeTurn();
                }
                else
                {
                    //TODO: Xét lượt cờ đen khi chơi với AI
                }
            }
        }

        private void TraLaiOCoBinhThuongKhiClick2Lan()
        {
            if (Common.RowSelected == this.Row && Common.ColSelected == this.Col) //squares click itself for 2 time, hide the way and return old background
            {
                Common.IsSelectedSquare = false;
                for (int i = 0; i < Common.CanMove.Count; i++)
                {
                    if (Common.CanMove[i].Chess == null) Common.CanMove[i].Image = null;
                }
                this.BackChessBoard();
                this.BackColor = Common.OldBackGround;
                Common.CanMove.Clear();
                Common.RowSelected = -1;
                Common.ColSelected = -1;
                return;
            }
        }

        private void phongHau(ref ChessSquare temp)
        {
            Chess newQueen = new Queen();
            if (temp.Chess.Team == 1) //white
            {
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

        private void Check(ref bool isCheck, ref ChessSquare KingTemp)
        {
            // mặc định chưa bị chiếu tướng isCheck=false
            isCheck = false;
            //check if King is danger
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess != null)
                    {
                        Common.Board[i, j].Chess.FindWay(Common.Board, Common.Board[i, j].Row, Common.Board[i, j].Col);
                        for (int k = 0; k < Common.CanMove.Count; k++)
                        {
                            if (Common.CanMove[k].Chess == null) Common.CanMove[k].Image = null;
                            else
                            {
                                if (Common.CanMove[k].Chess.IsKing)
                                {
                                    isCheck = true;
                                    KingTemp = new ChessSquare(Common.CanMove[k]);
                                }
                            }
                        }
                    }
                    Common.CanMove.Clear();
                }
        }

        private void Checkmate(ChessSquare temp)
        {
            bool isCheckmate = true;

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess != null)
                    {
                        //Kiem tra chung team hay khong
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
                                            isCheckmate = false;
                                            break;
                                        }
                                    }
                                    Common.CanMove.Clear();
                                }
                            }
                        }
                }
            }
            else
            {
                while (Common.CanMove.Count > 0)
                {
                    if (!Common.CanEat.Contains(Common.CanMove[Common.CanMove.Count - 1])) isCheckmate = false;
                    Common.CanMove.Remove(Common.CanMove[Common.CanMove.Count - 1]);
                }
            }

            if (isCheckmate)
            {
                if (temp.Chess.Team == (int)ColorTeam.White) MessageBox.Show("The Black Wins");
                else MessageBox.Show("The White Wins");

                Common.Close = true;
            }
        }

        private void ChangeTurn()
        {
            throw new NotImplementedException();
        }
    }

}
