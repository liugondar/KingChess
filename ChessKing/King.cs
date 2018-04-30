using System;

namespace ChessKing
{
    class King : Chess
    {
        public King()
        {
            this.IsKing = true;
        }
        /// <summary>
        /// Check Các ô xung quanh vị trí quân vua:
        /// -Nếu trống hiển thị di chuyển
        /// - Quân địch có thể ăn
        /// - Quân mình để nguyên
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {

            // Kiểm tra các ô phía trên 
            KiemTraONhapThanh(board, row, col);
            KiemTraCacOPhiaTren(board, row, col);
            KiemTraCacOPhiaDuoi(board, row, col);
            KiemTraOBenTrai(board, row, col);
            KiemTraOBenPhai(board, row, col);
        }

        private void KiemTraONhapThanh(ChessSquare[,] board, int row, int col)
        {
            // row= 7 col=4 is default location white king
            // row= 7 col=0 is default location white queen side castle
            // row= 7 col=7 is default location white king side castle
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                if (Common.isWhiteKingMoved) return;

                Common.isWhiteQueenSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, 7, 0)
                    && !Common.isWhiteKingCastled
                    && Common.IsEmptyChessSquare(board, 7, 1)
                    && Common.IsEmptyChessSquare(board, 7, 2)
                    && Common.IsEmptyChessSquare(board, 7, 3);

                Common.isWhiteKingSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, 7, 7)
                    && !Common.isWhiteKingCastled
                    && Common.IsEmptyChessSquare(board, 7, 6)
                    && Common.IsEmptyChessSquare(board, 7, 5);

                if (Common.isWhiteQueenSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, 7, 2);

                if (Common.isWhiteKingSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, 7, 6);
            }
            // row= 0 col=4 is default location black king
            // row= 0 col=0 is default location black queen side castle
            // row= 0 col=7 is default location black king side castle
            else
            {
                if (Common.isBlackKingMoved) return;

                Common.isBlackQueenSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, 0, 0)
                    && !Common.isBlackKingCastled
                    && Common.IsEmptyChessSquare(board, 0, 1)
                    && Common.IsEmptyChessSquare(board, 0, 2)
                    && Common.IsEmptyChessSquare(board, 0, 3);

                Common.isBlackKingSideCastleAvailable =
                    !Common.IsEmptyChessSquare(board, 0, 7)
                    && !Common.isBlackKingCastled
                    && Common.IsEmptyChessSquare(board, 0, 6)
                    && Common.IsEmptyChessSquare(board, 0, 5);

                if (Common.isBlackQueenSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, 0, 2);

                if (Common.isBlackKingSideCastleAvailable)
                    Common.ChangeBackgroundColorToCanMove(board, 0, 6);
            }
        }

        private void KiemTraOBenPhai(ChessSquare[,] board, int row, int col)
        {
            if (col < Constants.lastColOfTable)
            {
                // Kiểm tra ô bên phải nếu không ở cột đầu tiên
                if (Common.IsEmptyChessSquare(board, row, col + 1))
                {
                    Common.ChangeBackgroundColorToCanMove(board, row, col + 1);
                }
                else
                {
                    if (this.Team != board[row, col + 1].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, row, col + 1);
                }
            }
        }

        private void KiemTraOBenTrai(ChessSquare[,] board, int row, int col)
        {
            if (col > Constants.firstColOfTable)
            {
                // Kiểm tra cột bên trái nếu không ở cột đầu tiên

                if (Common.IsEmptyChessSquare(board, row, col - 1))
                {
                    Common.ChangeBackgroundColorToCanMove(board, row, col - 1);
                }
                else
                {
                    if (this.Team != board[row, col - 1].Chess.Team)
                        Common.ChangeBackgroundColorToCanEat(board, row, col - 1);
                }
            }
        }

        private void KiemTraCacOPhiaDuoi(ChessSquare[,] board, int row, int col)
        {
            if (row < Constants.lastRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía dưới nếu không ở vị trí cột đầu tiên
                if (col > Constants.firstColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col - 1);

                // Kiểm tra ô chéo bên phải phía dưới nếu không ở vị trí cột cuối cùng
                if (col < Constants.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col + 1);
                // Kiểm tra ô phía dưới nếu không phải ở vị trí hàng cuối cùng
                if (row < Constants.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col);
            }
        }
        private void KiemTraCacOPhiaTren(ChessSquare[,] board, int row, int col)
        {
            if (row > Constants.firstRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía trên nếu vị trí không ở cột đầu tiên
                if (col > Constants.firstColOfTable)
                    ThayDoiONeuCanThiet(board, row - 1, col - 1);
                // Kiểm tra ô chéo bên phải phía trên nếu vị trí không ở cột cuối cùng
                if (col < Constants.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row - 1, col + 1);
                //Kiểm tra ô phía bên trên nếu vị trí không ở hàng đầu tiên
                if (row > Constants.firstRowOfTable)
                    ThayDoiONeuCanThiet(board, row - 1, col);
            }
        }
        private void ThayDoiONeuCanThiet(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row, col))
            {
                Common.ChangeBackgroundColorToCanMove(board, row, col);
            }
            else
            {
                if (this.Team != board[row, col].Chess.Team)
                    Common.ChangeBackgroundColorToCanEat(board, row, col);
            }
        }
    }
}
