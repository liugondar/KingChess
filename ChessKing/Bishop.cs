using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessKing
{
    class Bishop : Chess
    {
        public Bishop()
        {
            this.IsBishop = true;
        }
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            XetCheoTraiLen(board, row, col);
            XetCheoTraiXuong(board, row, col);
            XetCheoPhaiLen(board, row, col);
            XetCheoPhaiXuong(board, row, col);

        }
        /// <summary>
        /// Xét từ vị trí quân cờ bishop
        /// Xét lên phía dưới bên phải : => col tăng, row giảm
        /// Cho chạy vòng lặp xét từng ô cờ
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void XetCheoPhaiXuong(ChessSquare[,] board, int row, int col)
        {
            int j;
            j = col + 1;
            for (int i = row + 1; i <= Common.lastRowOfTable; i++)
            {
                if (j > Common.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                    {
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                j++;
            }
        }
        /// <summary>
        /// Xét từ vị trí quân cờ bishop
        /// Xét lên phía trên bên phải : => col lên, row giảm
        /// Cho chạy vòng lặp xét từng ô cờ
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void XetCheoPhaiLen(ChessSquare[,] board, int row, int col)
        {
            int j = col + 1;
            for (int i = row - 1; i >= Common.firstRowOfTable; i--)
            {
                if (j > Common.lastColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                    {
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                j++;
            }
        }

        /// <summary>
        /// Xét từ vị trí quân cờ bishop
        /// Xét lên phía dưới bên trái : => col giảm, row tăng
        /// Cho chạy vòng lặp xét từng ô cờ
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name = "col" ></ param >
        private void XetCheoTraiXuong(ChessSquare[,] board, int row, int col)
        {
            int j = col - 1;
            for (int i = row + 1; i <= Common.lastRowOfTable; i++)
            {
                if (j < Common.firstColOfTable) break;
                if (Common.IsEmptyChessSquare(board, i, j))
                {
                    //load blue poin on button, in the way of piece
                    Common.ChangeBackgroundColorToCanMove(board, i, j);
                }
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[i, j].Chess.Team)
                    {
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                j--;
            }
        }
        /// <summary>
        /// Xét từ vị trí quân cờ bishop
        /// Xét lên phía trên bên trái : => col giảm, row giảm
        /// Cho chạy vòng lặp xét từng ô cờ
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void XetCheoTraiLen(ChessSquare[,] board, int row, int col)
        {
            // Khởi đầu bỏ qua vị trí bishop hiện tại, xét ô trái trên đầu tiên
            int j = col - 1;
            for (int i = row - 1; i >= Common.firstRowOfTable; i--)
            {
                // Kiểm tra điều kiện, nếu ngoài bàn cờ ( col <0) thì xong việc xét chéo trái lên
                if (j < Common.firstColOfTable) break;

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
                    {
                        Common.ChangeBackgroundColorToCanEat(board, i, j);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                j--;
            }
        }
    }
}
