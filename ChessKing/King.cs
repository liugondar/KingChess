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
            KiemTraCacOPhiaTren(board, row, col);
            KiemTraCacOPhiaDuoi(board, row, col);
            KiemTraOBenTrai(board, row, col);
            KiemTraOBenPhai(board, row, col);
        }

        private void KiemTraOBenPhai(ChessSquare[,] board, int row, int col)
        {
            if (col < Common.lastColOfTable)
            {
                // Kiểm tra ô bên phải nếu không ở cột đầu tiên
                if (col < Common.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row, col + 1);
            }
        }

        private void KiemTraOBenTrai(ChessSquare[,] board, int row, int col)
        {
            if (col > Common.firstColOfTable)
            {
                // Kiểm tra cột bên trái nếu không ở cột đầu tiên
                if (col > Common.firstColOfTable)
                    ThayDoiONeuCanThiet(board, row, col - 1);

            }
        }

        private void KiemTraCacOPhiaDuoi(ChessSquare[,] board, int row, int col)
        {
            if (row < Common.lastRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía dưới nếu không ở vị trí cột đầu tiên
                if (col > Common.firstColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col - 1);

                // Kiểm tra ô chéo bên phải phía dưới nếu không ở vị trí cột cuối cùng
                if (col < Common.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col + 1);
                // Kiểm tra ô phía dưới nếu không phải ở vị trí hàng cuối cùng
                if (row < Common.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row + 1, col);
            }
        }
        private void KiemTraCacOPhiaTren(ChessSquare[,] board, int row, int col)
        {
            if (row > Common.firstRowOfTable)
            {
                //Kiểm tra ô chéo bên trái phía trên nếu vị trí không ở cột đầu tiên
                if (col > Common.firstColOfTable)
                    ThayDoiONeuCanThiet(board, row - 1, col - 1);
                // Kiểm tra ô chéo bên phải phía trên nếu vị trí không ở cột cuối cùng
                if (col < Common.lastColOfTable)
                    ThayDoiONeuCanThiet(board, row - 1, col + 1);
                //Kiểm tra ô phía bên trên nếu vị trí không ở hàng đầu tiên
                if (row > Common.firstRowOfTable)
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
