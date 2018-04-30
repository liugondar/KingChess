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
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            CheckRightToLeft(board, row, col);
            CheckLeftToRight(board, row, col);
            CheckUpToDown(board, row, col);
            CheckDownToUp(board, row, col);
        }
        private void CheckDownToUp(ChessSquare[,] board, int row, int col)
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

        private void CheckUpToDown(ChessSquare[,] board, int row, int col)
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

        private void CheckLeftToRight(ChessSquare[,] board, int row, int col)
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

        private void CheckRightToLeft(ChessSquare[,] board, int row, int col)
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

        #region Clone find way without change background
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            CheckRightToLeftNoChangeBackground(board, row, col);
            CheckLeftToRightNoChangeBackground(board, row, col);
            CheckUpToDownNoChangeBackground(board, row, col);
            CheckDownToUpNoChangeBackground(board, row, col);
        }
        private void CheckDownToUpNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanMove.Add(board[i, col]);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.CanEat.Add(board[i, col]);
                    break;
                }
            }

        }

        private void CheckUpToDownNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i < 8; i++)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanMove.Add(board[i, col]);

                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        Common.CanEat.Add(board[i, col]);
                    break;
                }
            }
        }

        private void CheckLeftToRightNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int j = col + 1; j <= Constants.lastColOfTable; j++)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                {
                    Common.CanMove.Add(board[row, j]);
                }
                else
                {
                    if (this.Team != board[row, j].Chess.Team)
                        Common.CanEat.Add(board[row, j]);

                    break;
                }
            }
        }

        private void CheckRightToLeftNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            for (int j = col - 1; j >= Constants.firstColOfTable; j--)
            {
                if (Common.IsEmptyChessSquare(board, row, j))
                    //load blue poin on button, in the way of piece
                    Common.CanMove.Add(board[row, j]);
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[row, j].Chess.Team)
                    {
                        Common.CanEat.Add(board[row, j]);
                    }
                    break;
                }
            }
        }

        #endregion
    }
}
