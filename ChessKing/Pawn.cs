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

        public override void FindWay(ChessSquare[,] board, int row, int col)
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
            if (row < Common.lastRowOfTable)
            {
                bool isInTable = row >= Common.fisrtRowOfTable && row <= Common.lastRowOfTable;
                if (isInTable)
                {
                    CoTrangKiemTraOCheoBenTrai(board, row, col);
                    CoTrangKiemTraOCheoPhai(board, row, col);
                }

                if (row == Common.whitePawnDefaultRow)
                    CoTrangCoTheTienHaiBuoc(board, row, col);
                else
                    CoTrangCoTheTienMotBuoc(board, row, col);
            }
        }
        private void CoTrangKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Common.lastColOfTable;
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
                    Common.CanMove.Add(board[row - 1, col + 1]);
                }
            }
        }
        private void CoTrangKiemTraOCheoBenTrai(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Common.fisrtColOfTable;
            if (laConTotDauTien) return;
            //check duong cheo
            bool oCheoBenTraiCoQuanCo = board[row - 1, col - 1].Chess != null;
            if (oCheoBenTraiCoQuanCo)
            {
                bool quanCoKhacTeam = this.Team != board[row - 1, col - 1].Chess.Team;
                if (quanCoKhacTeam)
                {
                    // Báo hiệu có thể ăn quân cờ
                    if (Common.IsTurn % 2 == Common.WhiteTurn || Common.Is2PlayerMode == true)
                        board[row - 1, col - 1].BackColor = Color.Red;
                    // Báo hiểu có thể ăn và tiến hóa
                    if (row - 1 == 0 && board[row, col].Chess.Team == 1)
                    {
                        Common.CheckPromote = true;
                    }
                    // Add ô trái chéo vào danh sách ô quân cờ có thể di chuyênr
                    Common.CanMove.Add(board[row - 1, col - 1]);
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
                Common.CanMove.Add(board[row - 1, col]);
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
                    if (Common.IsTurn % 2 == Common.WhiteTurn || Common.Is2PlayerMode == true)
                        board[i, col].Image = Image.FromFile(linkPoint);
                    Common.CanMove.Add(board[i, col]);
                }
                else break;
            }
        }
        #endregion

        #region Xét tốt cờ đen
        private void XetCoDenDiTuTrenXuong(ChessSquare[,] board, int row, int col)
        {
            if (row > Common.fisrtRowOfTable)
            {
                bool isInTable = row >= Common.fisrtRowOfTable && row <= Common.lastRowOfTable;
                if (isInTable)
                {
                    CoDenKiemTraOCheoTrai(board, row, col);
                    CoDenKiemTraOCheoPhai(board, row, col);
                }

                if (row == Common.blackPawnDefaultRow)
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
                Common.CanMove.Add(board[row + 1, col]);
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
                    Common.CanMove.Add(board[i, col]);
                }
                else
                    break;
            }
        }
        private void CoDenKiemTraOCheoPhai(ChessSquare[,] board, int row, int col)
        {
            bool laConTotCuoiCung = col >= Common.lastColOfTable;
            if (laConTotCuoiCung) return;
            if (board[row + 1, col + 1].Chess != null)
            {
                if (this.Team != board[row + 1, col + 1].Chess.Team)
                {
                    if (Common.IsTurn % 2 == Common.WhiteTurn || Common.Is2PlayerMode == true)
                        board[row + 1, col + 1].BackColor = Color.Red;

                    if (row + 1 == 0 && board[row, col].Chess.Team == 2)
                    {
                        Common.CheckPromote = true;
                    }
                    Common.CanMove.Add(board[row + 1, col + 1]);
                }
            }
        }
        private void CoDenKiemTraOCheoTrai(ChessSquare[,] board, int row, int col)
        {

            bool laConTotDauTien = col <= Common.fisrtColOfTable;
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
                    Common.CanMove.Add(board[row + 1, col - 1]);
                }
            }
        }
        #endregion
    }
}