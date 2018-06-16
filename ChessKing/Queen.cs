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

            CheckNorthWest(board, row, col);
            CheckSouthWest(board, row, col);
            CheckNorthEast(board, row, col);
            CheckNorthSouth(board, row, col);
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
        private void CheckNorthSouth(ChessSquare[,] board, int row, int col)
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
        public override void FindSquaresCanEat(ChessSquare[,] board, int row, int col)
        {
            CheckNorthWestNoChangeBackground(board, row, col);
            CheckSouthWestNoChangeBackground(board, row, col);
            CheckNorthEastNoChangeBackground(board, row, col);
            CheckSouthEastNoChangeBackground(board, row, col);

            CheckTopNoChangeBackground(board, row, col);
            CheckBottomNoChangeBackground(board, row, col);
            CheckRightNoChangeBackground(board, row, col);
            CheckLeftNoChangeBackground(board, row, col);
        }
        public override void FindSquaresCanMove(ChessSquare[,] board, int row, int col)
        {
            CheckNorthWestNoChangeBackground(board, row, col);
            CheckSouthWestNoChangeBackground(board, row, col);
            CheckNorthEastNoChangeBackground(board, row, col);
            CheckSouthEastNoChangeBackground(board, row, col);
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

        #region Find square can protect 
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            CheckSouthEastToFindProtectChess(board, row, col);
            CheckNorthEastToFindProtectChess(board, row, col);
            CheckNorthWestToFindProtectChess(board, row, col);
            CheckSouthWestToFindProtectChess(board, row, col);

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

        private void CheckSouthEastToFindProtectChess(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthEastToFindProtectChess(ChessSquare[,] board, int row, int col)
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
        private void CheckSouthWestToFindProtectChess(ChessSquare[,] board, int row, int col)
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
        private void CheckNorthWestToFindProtectChess(ChessSquare[,] board, int row, int col)
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
