using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChessKing
{
    class Knight : Chess
    {
        string linkPoint = "Image\\circle.png";
        public Knight()
        {
            this.IsKnight = true;
        }
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            Xet2OBenTrenDiThang(board, row, col);
            Xet2OBenTrenDiNgang(board, row, col);
            Xet2OBenDuoiDiThang(board, row, col);
            Xet2OBenDuoiDiNgang(board, row, col);
        }
        private void Xet2OBenDuoiDiNgang(ChessSquare[,] board, int row, int col)
        {
            //row+1
            if (row + 1 < 8)
            {
                //col -2
                if (col - 2 >= 0)
                {
                    if (board[row + 1, col - 2].Chess == null)
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row + 1, col - 2);
                    }
                    else
                    {
                        if (this.Team != board[row + 1, col - 2].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row + 1, col - 2);
                    }
                }

                //col +2
                if (col + 2 < 8)
                {
                    if (board[row + 1, col + 2].Chess == null)
                    {
                        if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                            board[row + 1, col + 2].Image = Image.FromFile(linkPoint);
                        Common.CanMove.Add(board[row + 1, col + 2]);
                    }
                    else
                    {
                        if (this.Team != board[row + 1, col + 2].Chess.Team)
                        {
                            if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                                board[row + 1, col + 2].BackColor = Color.Red;
                            Common.CanMove.Add(board[row + 1, col + 2]);
                        }
                    }
                }
            }

        }
        private void Xet2OBenDuoiDiThang(ChessSquare[,] board, int row, int col)
        {
            //row+2
            if (row + 2 < 8)
            {
                //col -1
                if (col - 1 >= 0)
                {
                    if (board[row + 2, col - 1].Chess == null)
                        Common.ChangeBackgroundColorToCanMove(board, row + 2, col - 1);
                    else
                    {
                        if (this.Team != board[row + 2, col - 1].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row + 2, col - 1);
                    }
                }

                //col+1
                if (col + 1 < 8)
                {
                    if (board[row + 2, col + 1].Chess == null)
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row + 2, col + 1);
                    }
                    else
                    {
                        if (this.Team != board[row + 2, col + 1].Chess.Team)
                        {
                            Common.ChangeBackgroundColorToCanEat(board, row + 2, col + 1);
                        }
                    }
                }
            }
        }
        private void Xet2OBenTrenDiNgang(ChessSquare[,] board, int row, int col)
        {
            //row-1
            if (row - 1 >= 0)
            {
                //col-2 ô bên trái
                if (col - 2 >= 0)
                {
                    if (board[row - 1, col - 2].Chess == null)
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row - 1, col - 2);
                    }
                    else
                    {
                        if (this.Team != board[row - 1, col - 2].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row - 1, col - 2);
                    }
                }

                //col +2 ô bên phải
                if (col + 2 < 8)
                {
                    if (board[row - 1, col + 2].Chess == null)
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row - 1, col + 2);
                    }
                    else
                    {
                        if (this.Team != board[row - 1, col + 2].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row - 1, col + 2);
                    }
                }
            }
        }
        private void Xet2OBenTrenDiThang(ChessSquare[,] board, int row, int col)
        {
            //row-2
            if (row - 2 >= Common.firstRowOfTable)
            {
                //col-1 Ô bên trái
                if (col - 1 >= Common.firstColOfTable)
                {
                    if (Common.IsEmptyChessSquare(board, row - 2, col - 1))
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row - 2, col - 1);
                    }
                    else
                    {
                        if (this.Team != board[row - 2, col - 1].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row - 2, col - 1);
                    }
                }
                //col+1 ô bên phải
                if (col + 1 < 8)
                {
                    if (board[row - 2, col + 1].Chess == null)
                    {
                        Common.ChangeBackgroundColorToCanMove(board, row - 2, col + 1);
                    }
                    else
                    {
                        if (this.Team != board[row - 2, col + 1].Chess.Team)
                            Common.ChangeBackgroundColorToCanEat(board, row - 2, col + 1);
                    }
                }
            }
        }
    }
}