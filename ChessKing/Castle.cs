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
        string linkPoint = "Image\\circle.png";
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

        private void CheckDownToUp(ChessSquare[,] board, int row, int col)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (Common.IsEmptyChessSquare(board, i, col))
                {
                    ChangeBackgroundColorToCanMove(board, i, col);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        ChangeBackgroundColorToCanEat(board, i, col);
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
                    ChangeBackgroundColorToCanMove(board, i, col);
                }
                else
                {
                    if (this.Team != board[i, col].Chess.Team)
                        ChangeBackgroundColorToCanEat(board, i, col);
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
                    ChangeBackgroundColorToCanMove(board, row, j);
                }
                else
                {
                    if (this.Team != board[row, j].Chess.Team)
                        ChangeBackgroundColorToCanEat(board, row, j);

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
                    ChangeBackgroundColorToCanMove(board, row, j);
                else
                {
                    //square is not empty, check color ,if diffirence about color, change back color
                    if (this.Team != board[row, j].Chess.Team)
                    {
                        ChangeBackgroundColorToCanEat(board, row, j);
                    }
                    break;
                }
            }
        }

        private void ChangeBackgroundColorToCanEat(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsTurn % 2 == Common.WhiteTurn || Common.Is2PlayerMode == true)
                board[row, col].BackColor = Color.Red;
            Common.CanMove.Add(board[row, col]);
        }

        private void ChangeBackgroundColorToCanMove(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsTurn % 2 == Common.WhiteTurn || Common.Is2PlayerMode == true)
                board[row, col].Image = Image.FromFile(linkPoint);
            Common.CanMove.Add(board[row, col]);
        }
    }
}
