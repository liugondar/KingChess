using System;

namespace ChessKing
{
    class King : Chess
    {
        public King()
        {
            this.IsKing = true;
        }
        #region Find enable chess square to move or eat and display
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            CheckHasKingCanCastling(board, row, col);
            CheckTopSquares(board, row, col);
            CheckBottomSquares(board, row, col);
            CheckLeftSquare(board, row, col);
            CheckRightSquare(board, row, col);
        }
        private void CheckHasKingCanCastling(ChessSquare[,] board, int row, int col)
        {
            // row= 7 col=4 is default location white king
            // row= 7 col=0 is default location white queen side castle
            // row= 7 col=7 is default location white king side castle
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                if (Common.isWhiteKingMoved) return;
                if (Common.isWhiteKingChecked) return;

                bool isWhiteQueenSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, Constants.rowWhiteChessDefault, 0)
                    && !Common.isLeftWhiteCastleMoved
                    && CheckAvailableQueenPath(board, row: Constants.rowWhiteChessDefault, KnightCol: 1, BishopCol: 2,
                    QueenCol: 3, team: (int)ColorTeam.White);

                bool isWhiteKingSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, Constants.rowWhiteChessDefault, 7)
                    && !Common.isRightWhiteCastleMoved
                    && CheckAvailableKingPath(board, row: Constants.rowWhiteChessDefault, KnightCol: 6, BishopCol: 5,
                    team: (int)ColorTeam.White);
                if (isWhiteQueenSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, Constants.rowWhiteChessDefault, 2);

                if (isWhiteKingSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, Constants.rowWhiteChessDefault, 6);
            }
            // row= 0 col=4 is default location black king
            // row= 0 col=0 is default location black queen side castle
            // row= 0 col=7 is default location black king side castle
            else
            {
                if (Common.isBlackKingMoved) return;
                if (Common.isBlackKingChecked) return;

                bool isBlackQueenSideCastleAvailable =
                     !Common.IsEmptyChessSquare(board, Constants.rowBlackChessDefault, 0)
                     && !Common.isLeftBlackCastleMoved
                     && CheckAvailableQueenPath(board, row: Constants.rowBlackChessDefault, KnightCol: 1, BishopCol: 2, QueenCol: 3,
                     team: (int)ColorTeam.Black);

                bool isBlackKingSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, Constants.rowBlackChessDefault, 7)
                    && !Common.isRightBlackCastleMoved
                    && CheckAvailableKingPath(board, row: Constants.rowBlackChessDefault, KnightCol: 6, BishopCol: 5,
                    team: (int)ColorTeam.Black);

                if (isBlackQueenSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, Constants.rowBlackChessDefault, 2);

                if (isBlackKingSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, Constants.rowBlackChessDefault, 6);
            }
        }
        private bool CheckAvailableQueenPath(ChessSquare[,] board, int row, int KnightCol, int BishopCol, int QueenCol, int team)
        {
            if (!Common.IsEmptyChessSquare(board, row, QueenCol)) return false;
            if (!Common.IsEmptyChessSquare(board, row, KnightCol)) return false;
            if (!Common.IsEmptyChessSquare(board, row, BishopCol)) return false;

            if (Common.IsDangerSquareToMove(board, row, QueenCol, team)) return false;
            if (Common.IsDangerSquareToMove(board, row, BishopCol, team)) return false;

            return true;
        }
        private bool CheckAvailableKingPath(ChessSquare[,] board, int row, int KnightCol, int BishopCol, int team)
        {
            if (!Common.IsEmptyChessSquare(board, row, KnightCol)) return false;
            if (!Common.IsEmptyChessSquare(board, row, BishopCol)) return false;

            if (Common.IsDangerSquareToMove(board, row, KnightCol, team)) return false;
            if (Common.IsDangerSquareToMove(board, row, BishopCol, team)) return false;

            return true;
        }

        private void CheckRightSquare(ChessSquare[,] board, int row, int col)
        {
            if (col < Constants.lastColOfTable)
            {
                // Kiểm tra ô bên phải nếu không ở cột đầu tiên
                ChangeSquaresIfNeeded(board, row, col + 1);
            }
        }
        private void CheckLeftSquare(ChessSquare[,] board, int row, int col)
        {
            if (col > Constants.firstColOfTable)
            {
                // Kiểm tra cột bên trái nếu không ở cột đầu tiên
                ChangeSquaresIfNeeded(board, row, col - 1);
            }
        }
        private void CheckBottomSquares(ChessSquare[,] board, int row, int col)
        {
            if (row < Constants.lastRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía dưới nếu không ở vị trí cột đầu tiên
                if (col > Constants.firstColOfTable)
                    ChangeSquaresIfNeeded(board, row + 1, col - 1);

                // Kiểm tra ô chéo bên phải phía dưới nếu không ở vị trí cột cuối cùng
                if (col < Constants.lastColOfTable)
                    ChangeSquaresIfNeeded(board, row + 1, col + 1);
                // Kiểm tra ô phía dưới nếu không phải ở vị trí hàng cuối cùng
                if (row < Constants.lastColOfTable)
                    ChangeSquaresIfNeeded(board, row + 1, col);
            }
        }
        private void CheckTopSquares(ChessSquare[,] board, int row, int col)
        {
            if (row > Constants.firstRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía trên nếu vị trí không ở cột đầu tiên
                if (col > Constants.firstColOfTable)
                    ChangeSquaresIfNeeded(board, row - 1, col - 1);
                // Kiểm tra ô chéo bên phải phía trên nếu vị trí không ở cột cuối cùng
                if (col < Constants.lastColOfTable)
                    ChangeSquaresIfNeeded(board, row - 1, col + 1);
                //Kiểm tra ô phía bên trên nếu vị trí không ở hàng đầu tiên
                if (row > Constants.firstRowOfTable)
                    ChangeSquaresIfNeeded(board, row - 1, col);
            }
        }
        private void ChangeSquaresIfNeeded(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row, col))
            {
                if (!Common.IsDangerSquareToMove(board, row, col, this.Team))
                    Common.ChangeBackgroundColorToCanMove(board, row, col);
            }
            else
            {
                if (this.Team != board[row, col].Chess.Team)
                {
                    if (Common.IsProtected(board, row, col, board[row, col].Chess.Team)) return;
                    Common.ChangeBackgroundColorToCanEat(board, row, col);
                }
            }
        }
        #endregion

        #region Check square in checking path to make sure no teamate chess can protect king
        /// <summary>
        /// return true if checked team has a chess can be protect king
        /// else return false
        /// </summary>
        /// <param name="board"></param>
        /// <param name="kingCheckedSquare"></param>
        /// <param name="chessCheckSquare"></param>
        /// <returns></returns>
        public bool IsKingCanBeProtect(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            if (kingCheckedSquare.Chess == null) return true;
            if (chessCheckSquare.Chess == null) return true;
            Chess chessCheck = chessCheckSquare.Chess;

            if (chessCheck.IsKnight) return IsKingCanBeProtectWithCheckByKnight(board, kingCheckedSquare, chessCheckSquare);
            if (chessCheck.IsBishop) return IsKingCanBeProtectWithCheckByBishop(board, kingCheckedSquare, chessCheckSquare);
            if (chessCheck.IsQueen) return IsKingCanBeProtectWithCheckByQueen(board, kingCheckedSquare, chessCheckSquare);
            if (chessCheck.IsCastle) return IsKingCanBeProtectWithCheckByCastle(board, kingCheckedSquare, chessCheckSquare);
            if (chessCheck.IsPawn) return IsKingCanBeProtectWithCheckByPawn(board, kingCheckedSquare, chessCheckSquare);

            return false;
        }

        private bool IsKingCanBeProtectWithCheckByKnight(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            // Knight can be cancel attack just by kill him
            // so don't need to check checking path
            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, chessCheckSquare.Col]);
            return IsSquareInPathCanBeProtect(board);
        }

        private bool IsKingCanBeProtectWithCheckByPawn(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            // Pawn can be cancel attack just by kill him
            // so don't need to check checking path
            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, chessCheckSquare.Col]);
            return IsSquareInPathCanBeProtect(board);
        }

        private bool IsKingCanBeProtectWithCheckByCastle(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, chessCheckSquare.Col]);
            //Castle is in bottom side ofking
            if (kingCheckedSquare.Row < chessCheckSquare.Row)
            {
                for (int i = chessCheckSquare.Row - 1; i >= 0; i--)
                {
                    if (kingCheckedSquare.Row < i)
                    {
                        if (Common.IsEmptyChessSquare(board, i, chessCheckSquare.Col))
                            Common.SquaresCheckingPath.Add(board[i, chessCheckSquare.Col]);
                    }
                    else if (kingCheckedSquare.Row - 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, chessCheckSquare.Col, i);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Castle is in the top side of the king
            if (kingCheckedSquare.Row > chessCheckSquare.Row)
            {
                for (int i = chessCheckSquare.Row + 1; i <= 7; i++)
                {
                    if (kingCheckedSquare.Row > i)
                    {
                        if (Common.IsEmptyChessSquare(board, i, chessCheckSquare.Col))
                            Common.SquaresCheckingPath.Add(board[i, chessCheckSquare.Col]);
                    }
                    else if (kingCheckedSquare.Row + 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, chessCheckSquare.Col, i);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Castle is in the right side of theking
            if (kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                for (int i = chessCheckSquare.Col - 1; i >= 0; i--)
                {
                    if (kingCheckedSquare.Col < i)
                    {
                        if (Common.IsEmptyChessSquare(board, chessCheckSquare.Row, i))
                            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, i]);
                    }
                    else if (kingCheckedSquare.Col - 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, chessCheckSquare.Col);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Castle is in left side of the king
            if (kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                for (int i = chessCheckSquare.Col + 1; i <= 7; i++)
                {
                    if (kingCheckedSquare.Col > i)
                    {
                        if (Common.IsEmptyChessSquare(board, chessCheckSquare.Row, i))
                            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, i]);
                    }
                    else if (kingCheckedSquare.Col + 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, chessCheckSquare.Row);
                    }
                }
            }
            return false;
        }

        private bool IsKingCanBeProtectWithCheckByQueen(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, chessCheckSquare.Col]);
            // Queen in south east king
            if (kingCheckedSquare.Row < chessCheckSquare.Row
                && kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                // Get squares in check path to king
                int j = chessCheckSquare.Col - 1;
                for (int i = chessCheckSquare.Row - 1; i >= Constants.firstRowOfTable; i--)
                {
                    // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                    if (j < Constants.firstColOfTable) break;
                    // Ô trong checking path từ bishop tới king
                    if (kingCheckedSquare.Row < i && kingCheckedSquare.Col < j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    // nếu có ô ngay sau king trên đường checking path, 
                    // loại bỏ ô đó khỏi list can move hoac eat của king
                    else if (kingCheckedSquare.Row - 1 == i && kingCheckedSquare.Col - 1 == j)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, j, i);
                    }

                    j--;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen in south west king
            if (kingCheckedSquare.Row < chessCheckSquare.Row
              && kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                int j = chessCheckSquare.Col + 1;
                for (int i = chessCheckSquare.Row - 1; i >= Constants.firstRowOfTable; i--)
                {
                    if (j > Constants.lastColOfTable) break;
                    if (kingCheckedSquare.Row < i && kingCheckedSquare.Col > j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (kingCheckedSquare.Row - 1 == i && kingCheckedSquare.Col + 1 == j)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }
                    j++;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen in north east king
            if (kingCheckedSquare.Row > chessCheckSquare.Row
              && kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                int j = chessCheckSquare.Col - 1;
                for (int i = chessCheckSquare.Row + 1; i <= Constants.lastRowOfTable; i++)
                {
                    if (j < Constants.firstColOfTable) break;
                    if (kingCheckedSquare.Row > i && kingCheckedSquare.Col < j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (i == kingCheckedSquare.Row + 1 && j == kingCheckedSquare.Col - 1)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }

                    j--;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen in north west king
            if (kingCheckedSquare.Row > chessCheckSquare.Row
              && kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                int j;
                j = chessCheckSquare.Col + 1;
                for (int i = chessCheckSquare.Row + 1; i <= Constants.lastRowOfTable; i++)
                {
                    if (j > Constants.lastColOfTable) break;
                    if (kingCheckedSquare.Row > i && kingCheckedSquare.Col > j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (i == kingCheckedSquare.Row + 1 && j == kingCheckedSquare.Col + 1)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }
                    j++;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            // Queen is in the bottom side of king
            if (kingCheckedSquare.Row < chessCheckSquare.Row)
            {
                for (int i = chessCheckSquare.Row - 1; i >= 0; i--)
                {
                    if (kingCheckedSquare.Row < i)
                    {
                        if (Common.IsEmptyChessSquare(board, i, chessCheckSquare.Col))
                            Common.SquaresCheckingPath.Add(board[i, chessCheckSquare.Col]);
                    }
                    else if (kingCheckedSquare.Row - 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, chessCheckSquare.Col, i);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen is in the top side of the king
            if (kingCheckedSquare.Row > chessCheckSquare.Row)
            {
                for (int i = chessCheckSquare.Row + 1; i <= 7; i++)
                {
                    if (kingCheckedSquare.Row > i)
                    {
                        if (Common.IsEmptyChessSquare(board, i, chessCheckSquare.Col))
                            Common.SquaresCheckingPath.Add(board[i, chessCheckSquare.Col]);
                    }
                    else if (kingCheckedSquare.Row + 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, chessCheckSquare.Col, i);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen is in the right side of theking
            if (kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                for (int i = chessCheckSquare.Col - 1; i >= 0; i--)
                {
                    if (kingCheckedSquare.Col < i)
                    {
                        if (Common.IsEmptyChessSquare(board, chessCheckSquare.Row, i))
                            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, i]);
                    }
                    else if (kingCheckedSquare.Col - 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, chessCheckSquare.Col);
                    }
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Queen is in left side of the king
            if (kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                for (int i = chessCheckSquare.Col + 1; i <= 7; i++)
                {
                    if (kingCheckedSquare.Col > i)
                    {
                        if (Common.IsEmptyChessSquare(board, chessCheckSquare.Row, i))
                            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, i]);
                    }
                    else if (kingCheckedSquare.Col + 1 == i)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, chessCheckSquare.Row);
                    }
                }
            }
            return false;
        }

        private bool IsKingCanBeProtectWithCheckByBishop(ChessSquare[,] board, ChessSquare kingCheckedSquare, ChessSquare chessCheckSquare)
        {
            Common.SquaresCheckingPath.Add(board[chessCheckSquare.Row, chessCheckSquare.Col]);
            // Bishop in south east king
            if (kingCheckedSquare.Row < chessCheckSquare.Row
                && kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                // Get squares in check path to king
                int j = chessCheckSquare.Col - 1;
                for (int i = chessCheckSquare.Row - 1; i >= Constants.firstRowOfTable; i--)
                {
                    // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                    if (j < Constants.firstColOfTable) break;
                    // Ô trong checking path từ bishop tới king
                    if (kingCheckedSquare.Row < i && kingCheckedSquare.Col < j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    // nếu có ô ngay sau king trên đường checking path, 
                    // loại bỏ ô đó khỏi list can move hoac eat của king
                    else if (kingCheckedSquare.Row - 1 == i && kingCheckedSquare.Col - 1 == j)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, j, i);
                    }

                    j--;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Bishop in south west king
            if (kingCheckedSquare.Row < chessCheckSquare.Row
              && kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                int j = chessCheckSquare.Col + 1;
                for (int i = chessCheckSquare.Row - 1; i >= Constants.firstRowOfTable; i--)
                {
                    if (j > Constants.lastColOfTable) break;
                    if (kingCheckedSquare.Row < i && kingCheckedSquare.Col > j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (kingCheckedSquare.Row - 1 == i && kingCheckedSquare.Col + 1 == j)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }
                    j++;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Bishop in north east king
            if (kingCheckedSquare.Row > chessCheckSquare.Row
              && kingCheckedSquare.Col < chessCheckSquare.Col)
            {
                int j = chessCheckSquare.Col - 1;
                for (int i = chessCheckSquare.Row + 1; i <= Constants.lastRowOfTable; i++)
                {
                    if (j < Constants.firstColOfTable) break;
                    if (kingCheckedSquare.Row > i && kingCheckedSquare.Col < j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (i == kingCheckedSquare.Row + 1 && j == kingCheckedSquare.Col - 1)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }

                    j--;
                }
                return IsSquareInPathCanBeProtect(board);
            }

            //Bishop in north west king
            if (kingCheckedSquare.Row > chessCheckSquare.Row
              && kingCheckedSquare.Col > chessCheckSquare.Col)
            {
                int j;
                j = chessCheckSquare.Col + 1;
                for (int i = chessCheckSquare.Row + 1; i <= Constants.lastRowOfTable; i++)
                {
                    if (j > Constants.lastColOfTable) break;
                    if (kingCheckedSquare.Row > i && kingCheckedSquare.Col > j)
                    {
                        if (Common.IsEmptyChessSquare(board, i, j))
                            Common.SquaresCheckingPath.Add(board[i, j]);
                    }
                    else if (i == kingCheckedSquare.Row + 1 && j == kingCheckedSquare.Col + 1)
                    {
                        RemoveElementCanBeMoveOrEatAfterKingInCheckPath(board, i, j);
                    }
                    j++;
                }
                return IsSquareInPathCanBeProtect(board);
            }
            return false;
        }

        private void RemoveElementCanBeMoveOrEatAfterKingInCheckPath(ChessSquare[,] board, int col, int row)
        {
            if (Common.IsEmptyChessSquare(board, row, col))
            {
                Common.CanBeMove.RemoveAll(r => r.Col == col && r.Row == row);
            }
            else
            {
                Common.CanBeEat.RemoveAll(r => r.Col == col && r.Row == row);
            }
        }

        private bool IsSquareInPathCanBeProtect(ChessSquare[,] board)
        {
            for (int i = 0; i < Common.SquaresCheckingPath.Count; i++)
            {
                if (Common.IsSquareCanBeAttackByTeamate(board,
                    Common.SquaresCheckingPath[i].Row, Common.SquaresCheckingPath[i].Col,
                    this.Team))
                {
                    Common.SquaresCheckingPath.Clear();
                    return true;
                }
            }
            return false;
        }
        #endregion
    }

}
