﻿namespace ChessKing
{
    class Knight : Chess
    {
        public Knight()
        {
            this.IsKnight = true;
        }

        #region Find way and display
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
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
                        Common.ChangeBackgroundColorToCanMove(board, row + 1, col + 2);
                    }
                    else
                    {
                        if (this.Team != board[row + 1, col + 2].Chess.Team)
                        {
                            Common.ChangeBackgroundColorToCanEat(board, row + 1, col + 2);
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
            if (row - 2 >= Constants.firstRowOfTable)
            {
                //col-1 Ô bên trái
                if (col - 1 >= Constants.firstColOfTable)
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
        #endregion

        #region find way can be eat and move
        public override void FindSquaresCanEat(ChessSquare[,] board, int row, int col)
        {
            Xet2OBenTrenDiThangNoChangeBackground(board, row, col);
            Xet2OBenTrenDiNgangNoChangeBackground(board, row, col);
            Xet2OBenDuoiDiThangNoChangeBackground(board, row, col);
            Xet2OBenDuoiDiNgangNoChangeBackground(board, row, col);
        }
        public override void FindSquaresCanMove(ChessSquare[,] board, int row, int col)
        {
            Xet2OBenTrenDiThangNoChangeBackground(board, row, col);
            Xet2OBenTrenDiNgangNoChangeBackground(board, row, col);
            Xet2OBenDuoiDiThangNoChangeBackground(board, row, col);
            Xet2OBenDuoiDiNgangNoChangeBackground(board, row, col);
        }
        private void Xet2OBenDuoiDiNgangNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            //row+1
            if (row + 1 < 8)
            {
                //col -2
                if (col - 2 >= 0)
                {
                    if (board[row + 1, col - 2].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row + 1, col - 2]);
                    }
                    else
                    {
                        if (this.Team != board[row + 1, col - 2].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row + 1, col - 2]);
                    }
                }

                //col +2
                if (col + 2 < 8)
                {
                    if (board[row + 1, col + 2].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row + 1, col + 2]);
                    }
                    else
                    {
                        if (this.Team != board[row + 1, col + 2].Chess.Team)
                        {
                            Common.CanBeEatTemp.Add(board[row + 1, col + 2]);
                        }
                    }
                }
            }

        }
        private void Xet2OBenDuoiDiThangNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            //row+2
            if (row + 2 < 8)
            {
                //col -1
                if (col - 1 >= 0)
                {
                    if (board[row + 2, col - 1].Chess == null)
                        Common.CanBeMoveTemp.Add(board[row + 2, col - 1]);
                    else
                    {
                        if (this.Team != board[row + 2, col - 1].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row + 2, col - 1]);
                    }
                }

                //col+1
                if (col + 1 < 8)
                {
                    if (board[row + 2, col + 1].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row + 2, col + 1]);
                    }
                    else
                    {
                        if (this.Team != board[row + 2, col + 1].Chess.Team)
                        {
                            Common.CanBeEatTemp.Add(board[row + 2, col + 1]);
                        }
                    }
                }
            }
        }
        private void Xet2OBenTrenDiNgangNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            //row-1
            if (row - 1 >= 0)
            {
                //col-2 ô bên trái
                if (col - 2 >= 0)
                {
                    if (board[row - 1, col - 2].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row - 1, col - 2]);
                    }
                    else
                    {
                        if (this.Team != board[row - 1, col - 2].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row - 1, col - 2]);
                    }
                }

                //col +2 ô bên phải
                if (col + 2 < 8)
                {
                    if (board[row - 1, col + 2].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row - 1, col + 2]);
                    }
                    else
                    {
                        if (this.Team != board[row - 1, col + 2].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row - 1, col + 2]);
                    }
                }
            }
        }
        private void Xet2OBenTrenDiThangNoChangeBackground(ChessSquare[,] board, int row, int col)
        {
            //row-2
            if (row - 2 >= Constants.firstRowOfTable)
            {
                //col-1 Ô bên trái
                if (col - 1 >= Constants.firstColOfTable)
                {
                    if (Common.IsEmptyChessSquare(board, row - 2, col - 1))
                    {
                        Common.CanBeMoveTemp.Add(board[row - 2, col - 1]);
                    }
                    else
                    {
                        if (this.Team != board[row - 2, col - 1].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row - 2, col - 1]);
                    }
                }
                //col+1 ô bên phải
                if (col + 1 < 8)
                {
                    if (board[row - 2, col + 1].Chess == null)
                    {
                        Common.CanBeMoveTemp.Add(board[row - 2, col + 1]);
                    }
                    else
                    {
                        if (this.Team != board[row - 2, col + 1].Chess.Team)
                            Common.CanBeEatTemp.Add(board[row - 2, col + 1]);
                    }
                }
            }
        }
        #endregion

        #region  Tìm ô có thể bảo vệ được
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            Xet2OBenTrenDiThangToFindProtectObject(board, row, col);
            Xet2OBenTrenDiNgangToFindProtectObject(board, row, col);
            Xet2OBenDuoiDiThangToFindProtectObject(board, row, col);
            Xet2OBenDuoiDiNgangToFindProtectObject(board, row, col);
        }
        private void Xet2OBenDuoiDiNgangToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            //row+1
            if (row + 1 < 8)
            {
                //col -2
                if (col - 2 >= 0)
                {
                    if (board[row + 1, col - 2].Chess != null)
                    {
                        if (this.Team == board[row + 1, col - 2].Chess.Team)
                            Common.CanBeProtect.Add(board[row + 1, col - 2]);
                    }
                }

                //col +2
                if (col + 2 < 8)
                {
                    if (board[row + 1, col + 2].Chess != null)
                    {
                        if (this.Team == board[row + 1, col + 2].Chess.Team)
                            Common.CanBeProtect.Add(board[row + 1, col + 2]);
                    }
                }
            }

        }
        private void Xet2OBenDuoiDiThangToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            //row+2
            if (row + 2 < 8)
            {
                //col -1
                if (col - 1 >= 0)
                {
                    if (board[row + 2, col - 1].Chess != null)
                    {
                        if (this.Team == board[row + 2, col - 1].Chess.Team)
                            Common.CanBeProtect.Add(board[row + 2, col - 1]);
                    }
                }

                //col+1
                if (col + 1 < 8)
                {
                    if (board[row + 2, col + 1].Chess != null)
                    {
                        if (this.Team == board[row + 2, col + 1].Chess.Team)
                            Common.CanBeProtect.Add(board[row + 2, col + 1]);
                    }
                }
            }
        }
        private void Xet2OBenTrenDiNgangToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            //row-1
            if (row - 1 >= 0)
            {
                //col-2 ô bên trái
                if (col - 2 >= 0)
                {
                    if (board[row - 1, col - 2].Chess != null)
                    {
                        if (this.Team == board[row - 1, col - 2].Chess.Team)
                            Common.CanBeProtect.Add(board[row - 1, col - 2]);
                    }
                }
                //col +2 ô bên phải
                if (col + 2 < 8)
                {
                    if (board[row - 1, col + 2].Chess != null)
                    {
                        if (this.Team == board[row - 1, col + 2].Chess.Team)
                            Common.CanBeProtect.Add(board[row - 1, col + 2]);
                    }
                }
            }
        }
        private void Xet2OBenTrenDiThangToFindProtectObject(ChessSquare[,] board, int row, int col)
        {
            //row-2
            if (row - 2 >= Constants.firstRowOfTable)
            {
                //col-1 Ô bên trái
                if (col - 1 >= Constants.firstColOfTable)
                {
                    if (!Common.IsEmptyChessSquare(board, row - 2, col - 1))
                    {
                        if (this.Team == board[row - 2, col - 1].Chess.Team)
                            Common.CanBeProtect.Add(board[row - 2, col - 1]);
                    }
                }
                //col+1 ô bên phải
                if (col + 1 < 8)
                {
                    if (board[row - 2, col + 1].Chess != null)
                    {
                        if (this.Team != board[row - 2, col + 1].Chess.Team)
                            Common.CanBeProtect.Add(board[row - 2, col + 1]);
                    }
                }
            }
        }
        #endregion
    }
}