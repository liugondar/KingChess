using System.Drawing;

namespace ChessKing
{
    class Pawn : Chess
    {
        private int nextRow;
        public Pawn()
        {
            this.IsPawn = true;
        }

        #region Find ways and display in chess board
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
                XetCoTrang(board, row, col);
            else
                XetCoDen(board, row, col);
        }
        #region Xét tốt cờ trắng
        private void XetCoTrang(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                if (row >= Constants.lastRowOfTable) return;
            }
            else
            {
                if (row <= Constants.firstColOfTable)
                    return;
            }

            if (col < Constants.lastColOfTable)
                CoTrangKiemTraOCheoPhai(board, row, col);
            if (col > Constants.firstColOfTable)
                CoTrangKiemTraOCheoBenTrai(board, row, col);
            if (row == Constants.rowWhitePawnDefault)
                CoTrangCoTheTienHaiBuoc(board, row, col);
            else
                CoTrangCoTheTienMotBuoc(board, row, col);
        }
        private void CoTrangKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow == 8 || nextRow < 0) return;
            bool oCheoBenPhaiCoQuanCo = board[nextRow, col + 1].Chess != null;
            if (oCheoBenPhaiCoQuanCo)
            {
                bool quanCoKhacTeam = this.Team != board[nextRow, col + 1].Chess.Team;

                if (quanCoKhacTeam)
                {
                    // Báo hiệu có thể ăn quân cờ
                    if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode)
                        board[nextRow, col + 1].BackColor = Color.Red;
                    // Báo hiểu có thể ăn và tiến hóa
                    if (Common.Player1ColorTeam == (int)ColorTeam.White)
                    {
                        if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.White)
                            Common.CheckPromote = true;
                    }
                    else if (Common.Player2ColorTeam == (int)ColorTeam.White)
                    {
                        if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.White)
                            Common.CheckPromote = true;
                    }
                    // Add ô chéo phải vào danh sách ô quân cờ có thể di chuyênr
                    Common.CanBeEat.Add(board[nextRow, col + 1]);
                }
            }
        }
        private void CoTrangKiemTraOCheoBenTrai(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow == 8 || nextRow < 0) return;
            bool oCheoBenTraiCoQuanCo = board[nextRow, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                bool quanCoKhacTeam = this.Team != board[nextRow, col - 1].Chess.Team;
                if (quanCoKhacTeam)
                {
                    if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                        board[nextRow, col - 1].BackColor = Color.Red;

                    if (Common.Player1ColorTeam == (int)ColorTeam.White)
                    {
                        if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.White)
                            Common.CheckPromote = true;
                    }
                    else if (Common.Player2ColorTeam == (int)ColorTeam.White)
                    {
                        if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.White)
                            Common.CheckPromote = true;
                    }
                    Common.CanBeEat.Add(board[nextRow, col - 1]);
                }
            }
        }
        private void CoTrangCoTheTienMotBuoc(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;

            if (Common.IsEmptyChessSquare(board, nextRow, col))
            {
                if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                    board[nextRow, col].Image = Image.FromFile(Constants.linkPoint);
                if (Common.Player1ColorTeam == (int)ColorTeam.White)
                {
                    if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.White)
                        Common.CheckPromote = true;
                }
                else if (Common.Player2ColorTeam == (int)ColorTeam.White)
                {
                    if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.White)
                        Common.CheckPromote = true;
                }
                Common.CanBeMove.Add(board[nextRow, col]);
            }
        }
        private void CoTrangCoTheTienHaiBuoc(ChessSquare[,] board, int row, int col)
        {
            nextRow = row + 1;
            int next2Row = row + 2;
            if (nextRow >7 || nextRow < 0) return;
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                nextRow = row - 1;
                next2Row = row - 2;
                // Vòng lặp chạy từ vị trí default=6 tới 2 vị trí tiếp theo 
                //Kiểm tra 2 ô cờ tiếp theo còn trống không
                for (int i = nextRow; i >= next2Row; i--)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                            board[i, col].Image = Image.FromFile(Constants.linkPoint);
                        Common.CanBeMove.Add(board[i, col]);
                    }
                    else break;
                }
            }
            else
            {
                for (int i = nextRow; i <= next2Row; i++)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                            board[i, col].Image = Image.FromFile(Constants.linkPoint);
                        Common.CanBeMove.Add(board[i, col]);
                    }
                    else break;
                }
            }

        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDen(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                if (row <= Constants.firstRowOfTable) return;
            }
            else
            {
                if (row >= Constants.lastRowOfTable)
                    return;
            }
            if (col > 0)
                CoDenKiemTraOCheoTrai(board, row, col);

            if (col < 7)
                CoDenKiemTraOCheoPhai(board, row, col);
            if (row == Constants.rowBlackPawnDefault)
                CoDenCoTheTienhaiBuoc(board, row, col);
            else
                CoDenCoTheTienMotBuoc(board, row, col);
        }
        private void CoDenCoTheTienMotBuoc(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            if (Common.IsEmptyChessSquare(board, nextRow, col))
            {
                if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode)
                    board[nextRow, col].Image = Image.FromFile(Constants.linkPoint);

                if (Common.Player2ColorTeam == (int)ColorTeam.Black)
                {
                    if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                        Common.CheckPromote = true;
                }
                else if (Common.Player1ColorTeam == (int)ColorTeam.Black)
                {
                    if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                        Common.CheckPromote = true;
                }
                Common.CanBeMove.Add(board[nextRow, col]);
            }
        }
        private void CoDenCoTheTienhaiBuoc(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                for (int i = row + 1; i <= 3; i++)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                            board[i, col].Image = Image.FromFile(Constants.linkPoint);
                        Common.CanBeMove.Add(board[i, col]);
                    }
                    else
                        break;
                }
            }
            else
            {
                for (int i = row - 1; i >= 4; i--)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                            board[i, col].Image = Image.FromFile(Constants.linkPoint);
                        Common.CanBeMove.Add(board[i, col]);
                    }
                    else
                        break;
                }
            }

        }
        private void CoDenKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[nextRow, col + 1].Chess != null)
            {
                if (this.Team != board[nextRow, col + 1].Chess.Team)
                {
                    if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                        board[nextRow, col + 1].BackColor = Color.Red;


                    if (Common.Player2ColorTeam == (int)ColorTeam.Black)
                    {
                        if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                            Common.CheckPromote = true;
                    }
                    else if (Common.Player1ColorTeam == (int)ColorTeam.Black)
                    {
                        if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                            Common.CheckPromote = true;
                    }
                    Common.CanBeEat.Add(board[nextRow, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTrai(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            if (board[nextRow, col - 1].Chess != null)
            {
                if (this.Team != board[nextRow, col - 1].Chess.Team)
                {
                    if (Common.IsTurn % 2 == Common.Player1Turn || Common.Is2PlayerMode == true)
                        board[nextRow, col - 1].BackColor = Color.Red;


                    if (Common.Player2ColorTeam == (int)ColorTeam.Black)
                    {
                        if (nextRow == 7 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                            Common.CheckPromote = true;
                    }
                    else if (Common.Player1ColorTeam == (int)ColorTeam.Black)
                    {
                        if (nextRow == 0 && board[row, col].Chess.Team == (int)ColorTeam.Black)
                            Common.CheckPromote = true;
                    }
                    Common.CanBeEat.Add(board[nextRow, col - 1]);
                }
            }
        }
        #endregion
        #endregion

        #region tìm nước ăn và không hiển thị
        public override void FindSquaresCanEat(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
                XetCoTrangTimNuocAn(board, row, col);
            else
                XetCoDenTimNuocAn(board, row, col);
        }
        #region Xét tốt cờ trắng
        private void XetCoTrangTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                if (row >= Constants.lastRowOfTable) return;
            }
            else
            {
                if (row <= Constants.firstColOfTable)
                    return;
            }
            if (col > Constants.firstColOfTable)
                CoTrangKiemTraOCheoBenTraiTimNuocAn(board, row, col);
            if (col < Constants.lastColOfTable)
                CoTrangKiemTraOCheoPhaiTimNuocAn(board, row, col);
        }
        private void CoTrangKiemTraOCheoPhaiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;

            bool oCheoBenPhaiCoQuanCo = board[nextRow, col + 1].Chess != null;
            if (oCheoBenPhaiCoQuanCo)
            {
                if (board[nextRow, col + 1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[nextRow, col + 1]);
                }
            }
            else
            {
                Common.CanBeMoveTemp.Add(board[nextRow, col + 1]);
                
            }
        }
        private void CoTrangKiemTraOCheoBenTraiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[nextRow, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                if (board[nextRow, col - 1].Chess.Team != Team)
                    Common.CanBeEatTemp.Add(board[nextRow, col - 1]);
            }
            else
            {
                Common.CanBeMoveTemp.Add(board[nextRow, col - 1]);
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDenTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                if (row >= Constants.lastRowOfTable) return;
            }
            else
            {
                if (row <= Constants.firstColOfTable)
                    return;
            }
            CoDenKiemTraOCheoTraiTimNuocAn(board, row, col);
            CoDenKiemTraOCheoPhaiTimNuocAn(board, row, col);
        }
        private void CoDenKiemTraOCheoPhaiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[nextRow, col + 1].Chess == null)
            {
                Common.CanBeMoveTemp.Add(board[nextRow, col + 1]);
            }
            else
            {
                if (board[nextRow, col + 1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[nextRow, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTraiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            if (board[nextRow, col - 1].Chess == null)
            {
                Common.CanBeMoveTemp.Add(board[nextRow, col - 1]);
            }
            else
            {
                if (board[nextRow, col - 1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[nextRow, col - 1]);
                }
            }
        }

        #endregion

        #endregion

        #region Tìm nước đi được và không hiển thị
        public override void FindSquaresCanMove(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                if (row >= Constants.lastRowOfTable) return;
            }
            else
            {
                if (row <= Constants.firstColOfTable)
                    return;
            }
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
                FindSquareCanBeMoveByWhitePawn(board, row, col);
            else
                FindSquareCanBeMoveByBlackPawn(board, row, col);
        }


        #region Xét tốt cờ trắng
        private void FindSquareCanBeMoveByWhitePawn(ChessSquare[,] board, int row, int col)
        {

            if (row == Constants.rowWhitePawnDefault)
                CheckIfWhitePawnCanJump2Step(board, row, col);
            else
                CheckIfWhitePawnCanJump1Step(board, row, col);
        }

        private void CheckIfWhitePawnCanJump1Step(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (Common.IsEmptyChessSquare(board, nextRow, col))
            {
                Common.CanBeMoveTemp.Add(board[nextRow, col]);
            }
        }
        private void CheckIfWhitePawnCanJump2Step(ChessSquare[,] board, int row, int col)
        {
            // Vòng lặp chạy từ vị trí default=6 tới 2 vị trí tiếp theo 
            //Kiểm tra 2 ô cờ tiếp theo còn trống không
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                for (int i = row - 1; i >= 4; i--)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        Common.CanBeMoveTemp.Add(board[i, col]);
                    }
                    else break;
                }
            }
            else
            {
                for (int i = row + 1; i <= 3; i++)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                    {
                        Common.CanBeMoveTemp.Add(board[i, col]);
                    }
                    else break;
                }
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void FindSquareCanBeMoveByBlackPawn(ChessSquare[,] board, int row, int col)
        {
            if (row == Constants.rowBlackPawnDefault)
                CheckIfBlackPawnCanJump2Step(board, row, col);
            else
                CheckIfBlackPawnCanJump1Step(board, row, col);
        }
        private void CheckIfBlackPawnCanJump1Step(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            if (Common.IsEmptyChessSquare(board, nextRow, col))
                Common.CanBeMoveTemp.Add(board[nextRow, col]);
        }
        private void CheckIfBlackPawnCanJump2Step(ChessSquare[,] board, int row, int col)
        {
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
            {
                for (int i = row + 1; i <= 3; i++)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                        Common.CanBeMoveTemp.Add(board[i, col]);
                    else break;
                }
            }
            else if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                for (int i = row - 1; i >= 4; i--)
                {
                    if (Common.IsEmptyChessSquare(board, i, col))
                        Common.CanBeMoveTemp.Add(board[i, col]);
                    else break;
                }
            }
        }

        #endregion

        #endregion

        #region Tìm các ô có thể bảo vị được và không hiển thị
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
                FindSquaresCanBeProtectedByWhitePawn(board, row, col);
            else
                FindSquaresCanBeProtectedByBlackPawn(board, row, col);
        }
        #region Xét tốt cờ trắng
        private void FindSquaresCanBeProtectedByWhitePawn(ChessSquare[,] board, int row, int col)
        {

            bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
            if (isInTable)
            {
                if (col > Constants.firstColOfTable)
                    CheckLeftSquareCanBeProtectedByWhitePawn(board, row, col);
                if (col < Constants.lastColOfTable)
                    CheckRightSquareCanBeProtectedByWhitePawn(board, row, col);
            }
        }
        private void CheckRightSquareCanBeProtectedByWhitePawn(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
            {
                nextRow = row + 1;
            }
            if (nextRow >7 || nextRow < 0) return;
            bool oCheoBenPhaiCoQuanCo = board[nextRow, col + 1].Chess != null;
            if (oCheoBenPhaiCoQuanCo)
            {
                bool quanCoCungTeam = this.Team == board[nextRow, col + 1].Chess.Team;

                if (quanCoCungTeam)
                    Common.CanBeProtect.Add(board[nextRow, col + 1]);
            }
        }
        private void CheckLeftSquareCanBeProtectedByWhitePawn(ChessSquare[,] board, int row, int col)
        {

            nextRow = row - 1;
            if (Common.Player1ColorTeam == (int)ColorTeam.Black)
                nextRow = row + 1;

            if (nextRow >7 || nextRow < 0) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[nextRow, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                bool quanCoCungTeam = this.Team == board[nextRow, col - 1].Chess.Team;
                if (quanCoCungTeam)
                    Common.CanBeProtect.Add(board[nextRow, col - 1]);
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void FindSquaresCanBeProtectedByBlackPawn(ChessSquare[,] board, int row, int col)
        {
            bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
            if (isInTable)
            {
                if (col > Constants.firstColOfTable)
                    CoDenKiemTraOCheoTraiTimOBaoVe(board, row, col);
                if (col < Constants.lastColOfTable)
                    CoDenKiemTraOCheoPhaiTimOBaoVe(board, row, col);
            }
        }
        private void CoDenKiemTraOCheoPhaiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
                nextRow = row + 1;
            if (nextRow >7 || nextRow < 0) return;
            if (board[nextRow, col + 1].Chess != null)
            {
                if (this.Team == board[nextRow, col + 1].Chess.Team)
                {
                    Common.CanBeProtect.Add(board[nextRow, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTraiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {
            nextRow = row - 1;
            if (Common.Player2ColorTeam == (int)ColorTeam.Black)
                nextRow = row + 1;

            if (nextRow >7 || nextRow < 0) return;
            if (board[nextRow, col - 1].Chess != null)
            {
                if (this.Team == board[nextRow, col - 1].Chess.Team)
                    Common.CanBeProtect.Add(board[nextRow, col - 1]);
            }
        }
        #endregion
        #endregion


    }
}