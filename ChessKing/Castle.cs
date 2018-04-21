using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChessKing
{
    class Castle : Chess
    {
        public Castle()
        {
            this.IsCastle = true;
        }
        public override void FindWay(ChessSquare[,] board, int row, int col)
        {
            CheckRightToLeft(board, row, col);
            CheckLeftToRight(board, row, col);
            CheckUpToDown(board, row, col);
            CheckDownToUp(board, row, col);
        }

        /// <summary>
        /// Kiểm tra từ vị trí quân xe về phía bên trên bàn cờ 
        /// Nếu ô cờ trống thì hiển thị có thể di chuyển
        /// Nếu ô cờ có team địch thì đổi màu ô cờ, dừng việc kiểm tra
        /// Nếu ô cờ có team mình thì dừng việc kiểm tra
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
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
            for (int j = col + 1; j <= Common.lastColOfTable; j++)
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
            for (int j = col - 1; j >= Common.firstColOfTable; j--)
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


    }
}
