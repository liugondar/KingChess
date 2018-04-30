using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChessKing
{
    class Castle : Chess
    {
        public Castle()
        {
            this.IsCastle = true;
        }
        #region find way and display
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            CheckLeft(board, row, col);
            CheckRight(board, row, col);
            CheckBottom(board, row, col);
            CheckTop(board, row, col);
        }
        private void CheckTop(ChessSquare[,] board, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.ChangeBackgroundColorToCanMove(board, i, col);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, i, col);
                    break;
                }
            }

        }
        private void CheckBottom(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i < 8; i++)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.ChangeBackgroundColorToCanMove(board, i, col);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, i, col);
                    break;
                }
            }
        }
        private void CheckRight(ChessSquare[,] board, int row, int col)
        {
            for (int j = col + 1; j <= Constants.lastColOfTable; j++)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                {
                    Common.ChangeBackgroundColorToCanMove(board, row, j);
                }
                else
                {
                    if (this.Team != board[row, j].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, row, j);

                    break;
                }
            }
        }
        private void CheckLeft(ChessSquare[,] board, int row, int col)
        {
            for (int j = col - 1; j >= Constants.firstColOfTable; j--)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, row, j);
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[row, j].Chess.Team)
                    {
                        Common.ChangeBackgroundColorToCanEat(board, row, j);
                    }
                    break;
                }
            }
        }
        #endregion
        #region Clone find way without change background
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            CheckLeftToFindProjectObject(board, row, col);
            CheckRightNoChangeBackground(board, row, col);
            CheckBottomNoChangeBackground(board, row, col);
            CheckTopNoChangeBackground(board, row, col);
        }
        private void CheckTopNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanBeMoveTemp.Add(board[i, col]);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.CanBeEat.Add(board[i, col]);
                    break;
                }
            }

        }
        private void CheckBottomNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i < 8; i++)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanBeMoveTemp.Add(board[i, col]);

                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.CanBeEat.Add(board[i, col]);
                    break;
                }
            }
        }
        private void CheckRightNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int j = col + 1; j <= Constants.lastColOfTable; j++)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                {
                    Common.CanBeMoveTemp.Add(board[row, j]);
                }
                else
                {
                    if (this.Team != board[row, j].Chess.Team)
                        Common.CanBeEat.Add(board[row, j]);

                    break;
                }
            }
        }
        private void CheckLeftNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int j = col - 1; j >= Constants.firstColOfTable; j--)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                    //load blue poin on button, in the way of piece
                    Common.CanBeMoveTemp.Add(board[row, j]);
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[row, j].Chess.Team)
                    {
                        Common.CanBeEat.Add(board[row, j]);
                    }
                    break;
                }
            }
        }

        #endregion

        #region  Tìm ô có thể bảo vệ được
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            CheckLeftToFindProjectObject(board, row, col);
            CheckRightToFindProtectObject(board, row, col);
            CheckBottomToFindProtectObject(board, row, col);
            CheckTopToFindProtectObject(board, row, col);
        }
        private void CheckTopToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (!Common.IsEmptyChessSquare(board, i, col))
                {
                    if (this.Team == board[i, col].Chess.Team)
                        Common.CanBeProtect.Add(board[i, col]);
                    break;
                }
            }

        }
        private void CheckBottomToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i < 8; i++)
            {
                if (!Common.IsEmptyChessSquare(board, i, col))
                {
                    if (this.Team == board[i, col].Chess.Team)
                        Common.CanBeProtect.Add(board[i, col]);
                    break;
                }
            }
        }
        private void CheckRightToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            for (int j = col + 1; j <= Constants.lastColOfTable; j++)
            {
                if (!Common.IsEmptyChessSquare(board, row, j))
                {
                    if (this.Team == board[row, j].Chess.Team)
                        Common.CanBeProtect.Add(board[row, j]);
                    break;
                }
            }
        }
        private void CheckLeftToFindProjectObject(ChessSquare[,] board, int row, int col)
        {
            for (int j = col - 1; j >= Constants.firstColOfTable; j--)
            {
                if (!Common.IsEmptyChessSquare(board, row, j))
                {
                    if (this.Team == board[row, j].Chess.Team)
                        Common.CanBeProtect.Add(board[row, j]);
                    break;
                }

            }
        }
        #endregion
    }
}
