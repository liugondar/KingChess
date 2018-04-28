﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            // Khóa bàn cờ khi hết cờ
            if (Common.Close) return;
            //Không làm gì hết nếu ô clicked không có quân cờ
            if (this.Chess == null && !Common.IsSelectedSquare) return;
            ThayDoiOCoKhiClickVaoOCoQuanCo();
            Task.Run(() =>
            {
                Thread.Sleep(200); // delay
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.findWayAction();
                });
            });
        }

        private void ThayDoiOCoKhiClickVaoOCoQuanCo()
        {
            // Quân trắng luôn do người chơi dùng nên không cần xét đến AI
            if (Common.IsTurn % 2 == Common.WhiteTurn)
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
            if (Common.IsSelectedSquare == false) RenderWhenNotSelectedYet();
            //selected
            else RenderWhenSelected();
        }


        /// <summary>
        /// Khi click chọn quân cờ lân đầu
        /// </summary>
        private void RenderWhenNotSelectedYet()
        {
            //check square is not Empty 
            if (this.Chess != null)
            {
                Common.IsSelectedSquare = true;
                Common.OldBackGround = Common.Board[this.Row, this.Col].BackColor; //keep background color of chess square 
                this.Chess.FindWay(Common.Board, this.Row, this.Col); //findway can move and eat
                this.findWayAction();
                this.BackColor = System.Drawing.Color.Violet; //change background to violet
                Common.RowSelected = this.Row; //keep the row
                Common.ColSelected = this.Col; //keep the col

            }
        }
        /// <summary>
        /// Khi click nước đi cho quân cờ hoặc click hủy bỏ chọn quân cờ
        /// </summary>
        private void RenderWhenSelected()
        {
            Common.IsSelectedSquare = false;//gan lai bang false de lan sau con thuc hien
            if (Common.CanMove.Contains(this))//inside list Can Move
            {
                Common.RowProQueen = this.Row;
                Common.ColProQueen = this.Col;
                for (int i = 0; i < Common.CanMove.Count; i++)
                {
                    if (Common.CanMove[i].Chess == null)
                        Common.CanMove[i].Image = null;
                }
                ThayDoiHinhAnh();
                Common.IsTurn++; //change turn
                Common.CanMove.Clear();
                KiemTraPhongHau();
                KiemTraQuanVuaConTrenBanCoKhong();
                XuLiKhiDanhVoiAI();
                KiemTraChieuVua();

            }
            else //not inside can move list
            {
                HuyBoChonQuan();
            }
        }

        private void HuyBoChonQuan()
        {
            Common.Board[Common.RowSelected, Common.ColSelected].BackColor = Common.OldBackGround;
            for (int i = 0; i < Common.CanMove.Count; i++)
            {
                if (Common.CanMove[i].Chess == null) Common.CanMove[i].Image = null;
            }
            this.BackChessBoard();
            Common.CanMove.Clear();
        }

        private void KiemTraChieuVua()
        {
            bool isCheck = false;
            ChessSquare Kingtemp = new ChessSquare();

            this.Check(ref isCheck, ref Kingtemp);
            if (isCheck)
            {
                Common.CanMove.Clear();
                Checkmate(Kingtemp);
            }

            Common.CanEat.Clear();
            Common.CanMove.Clear();

            BackChessBoard();
            if (Kingtemp.Chess != null && Kingtemp.Chess.IsKing == true)
                Common.Board[Kingtemp.Row, Kingtemp.Col].BackColor = Color.BlueViolet;
        }

        private void XuLiKhiDanhVoiAI()
        {
            if (Common.Is2PlayerMode == false && Common.IsTurn % 2 == 1)
            {
                //TODO: add mini root de xu li ai
                //this.minimaxRoot();
                this.BackChessBoard();
            }
        }

        private void KiemTraQuanVuaConTrenBanCoKhong()
        {
            int soLuongVua = 0;
            int colorTeam = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess == null) Common.Board[i, j].Image = null;
                    else if (Common.Board[i, j].Chess.IsKing) { soLuongVua++; colorTeam = Common.Board[i, j].Chess.Team; }
                }
            }

            if (soLuongVua == 1)
            {
                if (colorTeam == (int)ColorTeam.White) MessageBox.Show("The White Wins!");
                else MessageBox.Show("The Black Wins!");

                Common.Close = true;
            }
        }

        private void KiemTraPhongHau()
        {
            for (int j = 0; j < 8; j++)
            {
                if (Common.Board[0, j].Chess != null && Common.Board[0, j].Chess.IsPawn) phongHau(ref Common.Board[0, j]);
                if (Common.Board[7, j].Chess != null && Common.Board[7, j].Chess.IsPawn) phongHau(ref Common.Board[7, j]);
            }
        }

        private void ThayDoiHinhAnh()
        {
            //thay doi hinh anh
            this.Image = Common.Board[Common.RowSelected, Common.ColSelected].Image;
            Common.Board[Common.RowSelected, Common.ColSelected].Image = null;
            //tra ve background cu
            Common.Board[Common.RowSelected, Common.ColSelected].BackColor = Common.OldBackGround;
            this.BackChessBoard();
            //thay doi quan co
            this.Chess = Common.Board[Common.RowSelected, Common.ColSelected].Chess;
            Common.Board[Common.RowSelected, Common.ColSelected].Chess = null;
        }

        private void KiemTraChieuBi(ref bool isCheck, ref ChessSquare Kingtemp)
        {
            this.Check(ref isCheck, ref Kingtemp);
            if (isCheck)
            {
                Common.CanMove.Clear();
                Checkmate(Kingtemp);
            }
        }

        protected void minimaxRoot()
        {
            int depth = Common.Depth;
            double valueint = -9999;
            double value = 0;
            double alpha = -10000, beta = 10000;
            bool isMax = true;
            ChessSquare[,] board = new ChessSquare[8, 8];
            ChessSquare[,] bestMove = new ChessSquare[8, 8];
            ChessSquare[,] a = new ChessSquare[8, 8];
            board = Common.Board;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Chess != null && board[i, j].Chess.Team == (int)ColorTeam.Black)
                    {
                        List<ChessSquare> RootTemp = new List<ChessSquare>();

                        int befRow = i;
                        int befCol = j;
                        board[befRow, befCol].Chess.FindWay(board, befRow, befCol);

                        for (int k = 0; k < Common.CanMove.Count; k++)
                        {
                            RootTemp.Add(Common.CanMove[k]);
                        }
                        Common.CanMove.Clear();

                        Chess tempChess = new Chess();
                        Image tempImage = null;

                        // Giả định lần lượt các nước đi quân cờ nếu nó có thể di chuyển
                        //được để tìm được giá trị tốt nhất cho nước đi
                        for (int k = 0; k < RootTemp.Count; k++)
                        {
                            tempChess = board[RootTemp[k].Row, RootTemp[k].Col].Chess;
                            tempImage = board[RootTemp[k].Row, RootTemp[k].Col].Image;

                            board[RootTemp[k].Row, RootTemp[k].Col].Chess = board[befRow, befCol].Chess;
                            board[RootTemp[k].Row, RootTemp[k].Col].Image = board[befRow, befCol].Image;
                            board[befRow, befCol].Chess = null;
                            board[befRow, befCol].Image = null;


                            value = minimax(depth - 1, ref board, alpha, beta, !isMax);
                            if (value >= valueint)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    for (int n = 0; n < 8; n++)
                                    {
                                        bestMove[m, n] = new ChessSquare(board[m, n]);
                                    }
                                }
                                //bestMove = temp;
                                valueint = value;
                            }
                            //phục hồi lại trạng thái ban đầu quân cờ sau khi giả định
                            board[RootTemp[k].Row, RootTemp[k].Col].Undo(ref board, befRow, befCol, tempChess, tempImage);
                        }
                        RootTemp.Clear();
                    }

                }
            }

            for (int k = 0; k < 8; k++)
            {
                for (int l = 0; l < 8; l++)
                {
                    Common.Board[k, l].Row = bestMove[k, l].Row;
                    Common.Board[k, l].Col = bestMove[k, l].Col;
                    Common.Board[k, l].Chess = bestMove[k, l].Chess;
                    Common.Board[k, l].Image = bestMove[k, l].Image;
                }
            }

            Common.IsTurn++;
        }

        private void Undo(ref ChessSquare[,] board, int befRow, int befCol, Chess tempChess, Image tempImage)
        {
            //TODO: complete Undo method serve minimaxroot
        }

        private double minimax(int v1, ref ChessSquare[,] board, double alpha, double beta, bool v2)
        {
            //TODO: write minimax algorithm
            return 0f;
        }
    }

}
