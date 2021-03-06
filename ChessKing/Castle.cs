﻿namespace ChessKing
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

        #region find way can be eat and move without change background
        /// <summary>
        /// in castle is find way can be move is find way can be eat
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public override void FindSquaresCanEat(ChessSquare[,] board, int row, int col)
        {
            CheckLeftNoChangeBackground(board, row, col);
            CheckRightNoChangeBackground(board, row, col);
            CheckBottomNoChangeBackground(board, row, col);
            CheckTopNoChangeBackground(board, row, col);
        }
        public override void FindSquaresCanMove(ChessSquare[,] board, int row, int col)
        {
            CheckLeftNoChangeBackground(board, row, col);
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
                        Common.CanBeEatTemp.Add(board[i, col]);
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
                        Common.CanBeEatTemp.Add(board[i, col]);
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
                        Common.CanBeEatTemp.Add(board[row, j]);

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
                        Common.CanBeEatTemp.Add(board[row, j]);
                    }
                    break;
                }
            }
        }

        #endregion

        #region find chess can be protected by castle 
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            CheckLeftToFindProtectObject(board, row, col);
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
        private void CheckLeftToFindProtectObject(ChessSquare[,] board, int row, int col)
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
