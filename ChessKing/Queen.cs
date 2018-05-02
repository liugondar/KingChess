using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessKing
{
    class Queen : Chess
    {
        public Queen()
        {
            this.IsQueen = true;
        }
        #region Findway and display         
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            CheckTop(board, row, col);
            CheckBottom(board, row, col);
            CheckRight(board, row, col);
            CheckLeft(board, row, col);

            XetCheoTraiLen(board, row, col);
            XetCheoTraiXuong(board, row, col);
            XetCheoPhaiLen(board, row, col);
            XetCheoPhaiXuong(board, row, col);
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

        #region Find way can be eat and move
        public override void FindSquareCanBeEat(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLenNoChangeBackground(board, row, col);
            XetCheoTraiXuongNoChangeBackground(board, row, col);
            XetCheoPhaiLenNoChangeBackground(board, row, col);
            XetCheoPhaiXuongNoChangeBackground(board, row, col);

            CheckTopNoChangeBackground(board, row, col);
            CheckBottomNoChangeBackground(board, row, col);
            CheckRightNoChangeBackground(board, row, col);
            CheckLeftNoChangeBackground(board, row, col);
        }
        public override void FindSquareCanBeMove(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLenNoChangeBackground(board, row, col);
            XetCheoTraiXuongNoChangeBackground(board, row, col);
            XetCheoPhaiLenNoChangeBackground(board, row, col);
            XetCheoPhaiXuongNoChangeBackground(board, row, col);
            CheckTopNoChangeBackground(board, row, col);
            CheckBottomNoChangeBackground(board, row, col);
            CheckRightNoChangeBackground(board, row, col);
            CheckLeftNoChangeBackground(board, row, col);
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

        #region Find square can protect 
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            XetCheoPhaiXuongFindProtect(board, row, col);
            XetCheoPhaiLenFindProtect(board, row, col);
            XetCheoTraiLenProtect(board, row, col);
            XetCheoTraiXuongProtect(board, row, col);

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
