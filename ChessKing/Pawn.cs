using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessKing
{
    class Pawn : Chess
    {
        string linkPoint = "Image\\circle.png";

        public Pawn()
        {
            this.IsPawn = true;
        }

        #region Tìm nước đi và hiển thị 
        public override void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                XetCoTrangDiTuDuoiLen(board, row, col);
            }
            else
            {
                XetCoDenDiTuTrenXuong(board, row, col);
            }
        }
        #region Xét tốt cờ trắng
        private void XetCoTrangDiTuDuoiLen(ChessSquare[,] board, int row, int col)
        {
            if (row < Constants.lastRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoTrangKiemTraOCheoBenTrai(board, row, col);
                    CoTrangKiemTraOCheoPhai(board, row, col);
                }

                if (row == Constants.whitePawnDefaultRow)
                    CoTrangCoTheTienHaiBuoc(board, row, col);
                else
                    CoTrangCoTheTienMotBuoc(board, row, col);
            }
        }
        private void CoTrangKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;

            bool oCheoBenPhaiCoQuanCo = board[row - 1, col + 1].Chess != null;
            if (oCheoBenPhaiCoQuanCo)
            {
                bool quanCoKhacTeam = this.Team != board[row - 1, col + 1].Chess.Team;

                if (quanCoKhacTeam)
                {

                    // Báo hiệu có thể ăn quân cờ
                    if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                        board[row - 1, col + 1].BackColor = Color.Red;

                    // Báo hiểu có thể ăn và tiến hóa
                    if (row - 1 == 0 && board[row, col].Chess.Team == 1)
                    {
                        Common.CheckPromote = true;
                    }
                    // Add ô chéo phải vào danh sách ô quân cờ có thể di chuyênr
                    Common.CanBeEat.Add(board[row - 1, col + 1]);
                }
            }
        }
        private void CoTrangKiemTraOCheoBenTrai(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[row - 1, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                bool quanCoKhacTeam = this.Team != board[row - 1, col - 1].Chess.Team;
                if (quanCoKhacTeam)
                {
                    // Báo hiệu có thể ăn quân cờ
                    if (Common.IsTurn % 2 == Constants.WhiteTurn || Common.Is2PlayerMode == true)
                        board[row - 1, col - 1].BackColor = Color.Red;
                    // Báo hiểu có thể ăn và tiến hóa
                    if (row - 1 == 0 && board[row, col].Chess.Team == 1)
                    {
                        Common.CheckPromote = true;
                    }
                    // Add ô trái chéo vào danh sách ô quân cờ có thể di chuyênr
                    Common.CanBeEat.Add(board[row - 1, col - 1]);
                }
            }
        }
        private void CoTrangCoTheTienMotBuoc(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row - 1, col))
            {
                if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                    board[row - 1, col].Image = Image.FromFile(linkPoint);
                if (row - 1 == 0 && board[row, col].Chess.Team == 1)
                {
                    Common.CheckPromote = true;
                }
                Common.CanBeMove.Add(board[row - 1, col]);
            }
        }
        private void CoTrangCoTheTienHaiBuoc(ChessSquare[,] board, int row, int col)
        {
            // Vòng lặp chạy từ vị trí default=6 tới 2 vị trí tiếp theo 
            //Kiểm tra 2 ô cờ tiếp theo còn trống không
            for (int i = row - 1; i >= 4; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    if (Common.IsTurn % 2 == Constants.WhiteTurn || Common.Is2PlayerMode == true)
                        board[i, col].Image = Image.FromFile(linkPoint);
                    Common.CanBeMove.Add(board[i, col]);
                }
                else break;
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDenDiTuTrenXuong(ChessSquare[,] board, int row, int col)
        {
            if (row > Constants.firstRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoDenKiemTraOCheoTrai(board, row, col);
                    CoDenKiemTraOCheoPhai(board, row, col);
                }

                if (row == Constants.blackPawnDefaultRow)
                    CoDenCoTheTienhaiBuoc(board, row, col);
                else
                    CoDenCoTheTienMotBuoc(board, row, col);
            }
        }
        private void CoDenCoTheTienMotBuoc(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row + 1, col))
            {
                if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                    board[row + 1, col].Image = Image.FromFile(linkPoint);
                if (row + 1 == 7 && board[row, col].Chess.Team == 2)
                {
                    Common.CheckPromote = true;
                }
                Common.CanBeMove.Add(board[row + 1, col]);
                //dk phong hau

            }
        }
        private void CoDenCoTheTienhaiBuoc(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i <= 3; i++)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                        board[i, col].Image = Image.FromFile(linkPoint);
                    Common.CanBeMove.Add(board[i, col]);
                }
                else
                    break;
            }
        }
        private void CoDenKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[row + 1, col + 1].Chess != null)
            {
                if (this.Team != board[row + 1, col + 1].Chess.Team)
                {
                    if (Common.IsTurn % 2 == Constants.WhiteTurn || Common.Is2PlayerMode == true)
                        board[row + 1, col + 1].BackColor = Color.Red;

                    if (row + 1 == 0 && board[row, col].Chess.Team == 2)
                    {
                        Common.CheckPromote = true;
                    }
                    Common.CanBeEat.Add(board[row + 1, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTrai(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            if (board[row + 1, col - 1].Chess != null)
            {
                if (this.Team != board[row + 1, col - 1].Chess.Team)
                {
                    if (Common.IsTurn % 2 == 0 || Common.Is2PlayerMode == true)
                        board[row + 1, col - 1].BackColor = Color.Red;

                    if (row + 1 == 0 && board[row, col].Chess.Team == 2)
                    {
                        Common.CheckPromote = true;
                    }
                    Common.CanBeEat.Add(board[row + 1, col - 1]);
                }
            }
        }
        #endregion
        #endregion

        #region tìm nước ăn và không hiển thị
        public override void FindSquareCanBeEat(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                XetCoTrangDiTuDuoiLenTimNuocAn(board, row, col);
            }
            else
            {
                XetCoDenDiTuTrenXuongTimNuocAn(board, row, col);
            }
        }
        #region Xét tốt cờ trắng
        private void XetCoTrangDiTuDuoiLenTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            if (row < Constants.lastRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoTrangKiemTraOCheoBenTraiTimNuocAn(board, row, col);
                    CoTrangKiemTraOCheoPhaiTimNuocAn(board, row, col);
                }
            }
        }
        private void CoTrangKiemTraOCheoPhaiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;

            bool oCheoBenPhaiCoQuanCo = board[row - 1, col + 1].Chess == null;
            if (oCheoBenPhaiCoQuanCo)
            {
                    Common.CanBeMoveTemp.Add(board[row - 1, col + 1]);
            }
            else
            {
                if (board[row - 1, col +1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[row - 1, col - 1]);
                }
            }
        }
        private void CoTrangKiemTraOCheoBenTraiTimNuocAn(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[row - 1, col - 1].Chess == null;
            if (oCheoBenTraiCoQuanCo)
            {
                    Common.CanBeMoveTemp.Add(board[row - 1, col - 1]);
            }
            else
            {
                if (board[row - 1, col - 1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[row - 1, col - 1]);
                }
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDenDiTuTrenXuongTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            if (row > Constants.firstRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoDenKiemTraOCheoTraiTimNuocAn(board, row, col);
                    CoDenKiemTraOCheoPhaiTimNuocAn(board, row, col);
                }
            }
        }
        private void CoDenKiemTraOCheoPhaiTimNuocAn(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[row + 1, col + 1].Chess == null)
            {
                    Common.CanBeMoveTemp.Add(board[row + 1, col + 1]);
            }
            else
            {
                if (board[row + 1, col +1 ].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[row - 1, col - 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTraiTimNuocAn(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            if (board[row + 1, col - 1].Chess == null)
            {
                    Common.CanBeMoveTemp.Add(board[row + 1, col - 1]);
            }
            else
            {
                if (board[row+ 1, col - 1].Chess.Team != Team)
                {
                    Common.CanBeEatTemp.Add(board[row - 1, col - 1]);
                }
            }
        }

        #endregion

        #endregion

        #region Tìm nước đi được và không hiển thị
        public override void FindSquareCanBeMove(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                FindSquareCanBeMoveFromBottom(board, row, col);
            }
            else
            {
                FindSquareCanBeMoveFromTop(board, row, col);
            }
        }


        #region Xét tốt cờ trắng
        private void FindSquareCanBeMoveFromBottom(ChessSquare[,] board, int row, int col)
        {
                if (row == Constants.whitePawnDefaultRow)
                    CheckIfWhitePawnCanJump2Step(board, row, col);
                else
                    CheckIfWhitePawnCanJump1Step(board, row, col);
        }

        private void CheckIfWhitePawnCanJump1Step(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row - 1, col))
            {
                Common.CanBeMoveTemp.Add(board[row - 1, col]);
            }
        }
        private void CheckIfWhitePawnCanJump2Step(ChessSquare[,] board, int row, int col)
        {
            // Vòng lặp chạy từ vị trí default=6 tới 2 vị trí tiếp theo 
            //Kiểm tra 2 ô cờ tiếp theo còn trống không
            for (int i = row - 1; i >= 4; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanBeMoveTemp.Add(board[i, col]);
                }
                else break;
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void FindSquareCanBeMoveFromTop(ChessSquare[,] board, int row, int col)
        {
                if (row == Constants.blackPawnDefaultRow)
                    CheckIfBlackPawnCanJump2Step(board, row, col);
                else
                    CheckIfBlackPawnCanJump1Step(board, row, col);
        }
        private void CheckIfBlackPawnCanJump1Step(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row + 1, col))
            {
                Common.CanBeMoveTemp.Add(board[row + 1, col]);
            }
        }
        private void CheckIfBlackPawnCanJump2Step(ChessSquare[,] board, int row, int col)
        {
            for (int i = row + 1; i <= 3; i++)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    Common.CanBeMoveTemp.Add(board[i, col]);
                }
                    break;
            }
        }

        #endregion

        #endregion

        #region Tìm các ô có thể bảo vị được và không hiển thị
        public override void FindSquaresCanProtect(ChessSquare[,] board, int row, int col)
        {
            if (board[row, col].Chess.Team == (int)ColorTeam.White)
            {
                XetCoTrangDiTuDuoiLenTimOBaove(board, row, col);
            }
            else
            {
                XetCoDenDiTuTrenXuongTimOBaoVe(board, row, col);
            }
        }
        #region Xét tốt cờ trắng
        private void XetCoTrangDiTuDuoiLenTimOBaove(ChessSquare[,] board, int row, int col)
        {
            if (row < Constants.lastRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoTrangKiemTraOCheoBenTraiTimOBaoVe(board, row, col);
                    CoTrangKiemTraOCheoPhaiTimOBaoVe(board, row, col);
                }
            }
        }
        private void CoTrangKiemTraOCheoPhaiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;

            bool oCheoBenPhaiCoQuanCo = board[row - 1, col + 1].Chess != null;
            if (oCheoBenPhaiCoQuanCo)
            {
                bool quanCoCungTeam = this.Team == board[row - 1, col + 1].Chess.Team;

                if (quanCoCungTeam)
                {
                    // Báo hiểu có thể ăn và tiến hóa
                    // Add ô chéo phải vào danh sách ô quân cờ có thể di chuyênr
                    Common.CanBeProtect.Add(board[row - 1, col + 1]);
                }
            }
        }
        private void CoTrangKiemTraOCheoBenTraiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[row - 1, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                bool quanCoCungTeam = this.Team == board[row - 1, col - 1].Chess.Team;
                if (quanCoCungTeam)
                {
                    Common.CanBeProtect.Add(board[row - 1, col - 1]);
                }
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDenDiTuTrenXuongTimOBaoVe(ChessSquare[,] board, int row, int col)
        {
            if (row > Constants.firstRowOfTable)
            {
                bool isInTable = row >= Constants.firstRowOfTable && row <= Constants.lastRowOfTable;
                if (isInTable)
                {
                    CoDenKiemTraOCheoTraiTimOBaoVe(board, row, col);
                    CoDenKiemTraOCheoPhaiTimOBaoVe(board, row, col);
                }
            }
        }
        private void CoDenKiemTraOCheoPhaiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Constants.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[row + 1, col + 1].Chess != null)
            {
                if (this.Team == board[row + 1, col + 1].Chess.Team)
                {
                    Common.CanBeProtect.Add(board[row + 1, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTraiTimOBaoVe(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Constants.firstColOfTable;
            if (laConTotDauTien) return;
            if (board[row + 1, col - 1].Chess != null)
            {
                if (this.Team == board[row + 1, col - 1].Chess.Team)
                {
                    Common.CanBeProtect.Add(board[row + 1, col - 1]);
                }
            }
        }
        #endregion
        #endregion
    }
}