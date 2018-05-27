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
            CheckNorthWest(board, row, col);
            CheckSouthWest(board, row, col);
            CheckNorthEast(board, row, col);
            CheckSouthEast(board, row, col);
        }
        private void CheckSouthEast(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthEast(ChessSquare[,] board, int row, int col)
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
        private void CheckSouthWest(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthWest(ChessSquare[,] board, int row, int col)
        {
            int j = col - 1;
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
            {
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
            CheckNorthWestNoChangeBackground(board, row, col);
            CheckSouthWestNoChangeBackground(board, row, col);
            CheckNorthEastNoChangeBackground(board, row, col);
            CheckSouthEastNoChangeBackground(board, row, col);
        }
        public override void FindSquareCanBeEat(ChessSquare[,] board, int row, int col)
        {
            CheckNorthWestNoChangeBackground(board, row, col);
            CheckSouthWestNoChangeBackground(board, row, col);
            CheckNorthEastNoChangeBackground(board, row, col);
            CheckSouthEastNoChangeBackground(board, row, col);
        }

        private void CheckSouthEastNoChangeBackground(ChessSquare[,] board, int row, int col)
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
                        Common.CanBeEatTemp.Add(board[i, j]);
                    break;
                }
                j++;
            }
        }
        private void CheckNorthEastNoChangeBackground(ChessSquare[,] board, int row, int col)
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
                        Common.CanBeEatTemp.Add(board[i, j]);
                    break;
                }
                j++;
            }
        }
        private void CheckSouthWestNoChangeBackground(ChessSquare[,] board, int row, int col)
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
                        Common.CanBeEatTemp.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        private void CheckNorthWestNoChangeBackground(ChessSquare[,] board, int row, int col)
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
                        Common.CanBeEatTemp.Add(board[i, j]);
                    break;
                }
                j--;
            }
        }
        #endregion

        #region  find chess can be protected by bishop
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            CheckSouthEastToFindChessCanBeProtect(board, row, col);
            CheckNorthEastToFindChessCanBeProtect(board, row, col);
            CheckNorthWestToFindChessCanBeProtect(board, row, col);
            CheckSouthWestToFindChessCanBeProtect(board, row, col);
        }

        private void CheckSouthEastToFindChessCanBeProtect(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthEastToFindChessCanBeProtect(ChessSquare[,] board, int row, int col)
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
        private void CheckSouthWestToFindChessCanBeProtect(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthWestToFindChessCanBeProtect(ChessSquare[,] board, int row, int col)
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
