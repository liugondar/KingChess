using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessKing
{
    class Bishop : Chess
    {
        public Bishop()
        {
            this.IsBishop = true;
        }

        #region find way and display
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLen(board, row, col);
            XetCheoTraiXuong(board, row, col);
            XetCheoPhaiLen(board, row, col);
            XetCheoPhaiXuong(board, row, col);
        }
        private void XetCheoPhaiXuong(ChessSquare[,] board, int row, int col)
        {
            int j;
            j = col + 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j > Constants.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                    break;
                }
                j++;
            }
        }
        private void XetCheoPhaiLen(ChessSquare[,] board, int row, int col)
        {
            int j = col + 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                if (j > Constants.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    if (this.Team != board[i, j].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                    break;
                }
                j++;
            }
        }
        private void XetCheoTraiXuong(ChessSquare[,] board, int row, int col)
        {
            int j = col - 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j < Constants.firstColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                    break;
                }
                j--;
            }
        }
        private void XetCheoTraiLen(ChessSquare[,] board, int row, int col)
        {
            // Khởi đầu bỏ qua vị trí bishop hiện tại, xét ô trái trên đầu tiên
            int j = col - 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                if (j < Constants.firstColOfTable) break;

                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color team ,if diffirence about team color, change back color
                    bool isDifferentTeam = this.Team != board[i, j].Chess.Team;
                    if (isDifferentTeam)
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                    break;
                }
                j--;
            }
        }
        #endregion

        #region find way can be eat and move 
        /// <summary>
        /// in bishop is find way can be move is find way can be eat
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public override void FindSquareCanBeMove(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLenNoChangeBackground(board, row, col);
            XetCheoTraiXuongNoChangeBackground(board, row, col);
            XetCheoPhaiLenNoChangeBackground(board, row, col);
            XetCheoPhaiXuongNoChangeBackground(board, row, col);
        }
        public override void FindSquareCanBeEat(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLenNoChangeBackground(board, row, col);
            XetCheoTraiXuongNoChangeBackground(board, row, col);
            XetCheoPhaiLenNoChangeBackground(board, row, col);
            XetCheoPhaiXuongNoChangeBackground(board, row, col);
        }

        private void XetCheoPhaiXuongNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            int j;
            j = col + 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j > Constants.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    Common.CanBeMoveTemp.Add(board[i, j]);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                        Common.CanBeEat.Add(board[i, j]);
                    break;
                }
                j++;
            }
        }
        private void XetCheoPhaiLenNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            int j = col + 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                if (j > Constants.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    Common.CanBeMoveTemp.Add(board[i, j]);

                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                        Common.CanBeEat.Add(board[i, j]);
                    break;
                }
                j++;
            }
        }
        private void XetCheoTraiXuongNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            int j = col - 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j < Constants.firstColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    Common.CanBeMoveTemp.Add(board[i, j]);

                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                        Common.CanBeEat.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        private void XetCheoTraiLenNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            // Khởi đầu bỏ qua vị trí bishop hiện tại, xét ô trái trên đầu tiên
            int j = col - 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                if (j < Constants.firstColOfTable) break;

                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    Common.CanBeMoveTemp.Add(board[i, j]);
                }
                else
                {
                    //square is not empty, check color team ,if diffirence about team color, change back color
                    bool isDifferentTeam = this.Team != board[i, j].Chess.Team;
                    if (isDifferentTeam)
                        Common.CanBeEat.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        #endregion

        #region  Tìm ô có thể bảo vệ được
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            XetCheoPhaiXuongFindProtect(board, row, col);
            XetCheoPhaiLenFindProtect(board, row, col);
            XetCheoTraiLenProtect(board, row, col);
            XetCheoTraiXuongProtect(board, row, col);
        }

        private void XetCheoPhaiXuongFindProtect(ChessSquare[,] board, int row, int col)
        {
            int j;
            j = col + 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j > Constants.lastColOfTable) break;
                if (!Common.IsEmptyChessSquare(board, i, j))
                {
                    if (this.Team == board[i, j].Chess.Team)
                    {
                        Common.CanBeProtect.Add(board[i, j]);
                    }
                    break;
                }
                j++;
            }
        }
        private void XetCheoPhaiLenFindProtect(ChessSquare[,] board, int row, int col)
        {
            int j = col + 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                if (j > Constants.lastColOfTable) break;
                if (!Common.IsEmptyChessSquare(board, i, j))
                {
                    if (this.Team == board[i, j].Chess.Team)
                        Common.CanBeProtect.Add(board[i, j]);

                    break;
                }
                j++;
            }
        }
        private void XetCheoTraiXuongProtect(ChessSquare[,] board, int row, int col)
        {
            int j = col - 1;
            for (int i = row + 1; i <= Constants.lastRowOfTable; i++)
            {
                if (j < Constants.firstColOfTable) break;
                if (!Common.IsEmptyChessSquare(board, i, j))
                {
                    if (this.Team == board[i, j].Chess.Team)
                        Common.CanBeProtect.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        private void XetCheoTraiLenProtect(ChessSquare[,] board, int row, int col)
        {
            // Khởi đầu bỏ qua vị trí bishop hiện tại, xét ô trái trên đầu tiên
            int j = col - 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
                // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                if (j < Constants.firstColOfTable) break;

                if (!Common.IsEmptyChessSquare(board, i, j))
                {
                    if (this.Team == board[i, j].Chess.Team)
                        Common.CanBeProtect.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        #endregion

    }
}
