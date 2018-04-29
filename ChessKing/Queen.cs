using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessKing
{
    class Queen : Chess
    {
        public Queen()
        {
            this.IsQueen = true;
        }
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            CheckRightToLeft(board, row, col);
            CheckLeftToRight(board, row, col);
            CheckUpToDown(board, row, col);
            CheckDownToUp(board, row, col);

            XetCheoTraiLen(board, row, col);
            XetCheoTraiXuong(board, row, col);
            XetCheoPhaiLen(board, row, col);
            XetCheoPhaiXuong(board, row, col);
        }

        private void CheckDownToUp(ChessSquare[,] board, int row, int col)
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
        /// <summary>
        /// Kiểm tra từ vị trí quân xe về phía bên dưới bàn cờ 
        /// Nếu ô cờ trống thì hiển thị có thể di chuyển
        /// Nếu ô cờ có team địch thì đổi màu ô cờ, dừng việc kiểm tra
        /// Nếu ô cờ có team mình thì dừng việc kiểm tra
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void CheckUpToDown(ChessSquare[,] board, int row, int col)
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
        /// <summary>
        /// Kiểm tra từ vị trí quân xe về phía bên phải bàn cờ 
        /// Nếu ô cờ trống thì hiển thị có thể di chuyển
        /// Nếu ô cờ có team địch thì đổi màu ô cờ, dừng việc kiểm tra
        /// Nếu ô cờ có team mình thì dừng việc kiểm tra
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void CheckLeftToRight(ChessSquare[,] board, int row, int col)
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
        /// <summary>
        /// Kiểm tra từ vị trí quân xe về phía bên trái bàn cờ 
        /// Nếu ô cờ trống thì hiển thị có thể di chuyển
        /// Nếu ô cờ có team địch thì đổi màu ô cờ, dừng việc kiểm tra
        /// Nếu ô cờ có team mình thì dừng việc kiểm tra
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void CheckRightToLeft(ChessSquare[,] board, int row, int col)
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
            for (int i = row - 1; i >= Constants.firstRowOfTable; i--)
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
