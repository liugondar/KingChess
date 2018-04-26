using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessKing
{
    class Common
    {
        static public string linkPoint = "Image\\circle.png";
        static public bool Is2PlayerMode = true; //1 player
        static public bool IsSelectedSquare = false; //selected yet
        static public int IsTurn = 0;
        static public int WhiteTurn = 0;
        static public int BlackTurn = 1;
        static public int firstRowOfTable = 0;
        static public int lastRowOfTable = 7;
        static public int firstColOfTable = 0;
        static public int lastColOfTable = 7;
        static public int whitePawnDefaultRow = 6;
        static public int blackPawnDefaultRow = 1;
        static public void ResetPropToDefault()
        {
            IsSelectedSquare = false;
            IsTurn = 0;
            WhiteTurn = 0;
            BlackTurn = 1;
            firstRowOfTable = 0;
            lastRowOfTable = 7;
            firstColOfTable = 0;
            lastColOfTable = 7;
            whitePawnDefaultRow = 6;
            blackPawnDefaultRow = 1;
        }
        static public bool IsEmptyChessSquare(ChessSquare[,] board, int row, int col)
        {
            return board[row, col].Chess == null;
        }
        static public void ChangeBackgroundColorToCanEat(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == WhiteTurn || Is2PlayerMode == true)
                board[row, col].BackColor = Color.Red;
            CanMove.Add(board[row, col]);
        }
        static public void ChangeBackgroundColorToCanMove(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == WhiteTurn || Is2PlayerMode == true)
                board[row, col].Image = Image.FromFile(linkPoint);
            CanMove.Add(board[row, col]);
        }


        static public bool Close = false;
        static public ChessSquare[,] Board;
        static public List<ChessSquare> CanMove = new List<ChessSquare>(); // create list, save position of piece can move
        static public List<ChessSquare> CanEat = new List<ChessSquare>();

        static public int RowSelected = -1; //set value =-1, not in chessboard
        static public int ColSelected = -1; //set value =-1, not in chessboard

        static public Color OldBackGround;//keep back ground before change to violet

        static public bool CheckPromote = false; //phong hau
        static public int RowProQueen = -1;
        static public int ColProQueen = -1;

        static public int Depth = 2;

        static public double[,] PawnWhite =
      {
            {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
            {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
            {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
            {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
            {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
            {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
            {0.5,  1.0,  1.0, -2.0, -2.0,  1.0,  1.0,  0.5},
            {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0}
        };
        static public double[,] PawnBlack =
       {
           {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
           {0.5,  1.0,  1.0, -2.0, -2.0,  1.0,  1.0,  0.5},
           {0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
           {0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
           {0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
           {1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
           {5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
           {0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        };
        static public double[,] BishopWhite =
       {
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
            { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
            { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
            { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
            { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
            { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
            { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
            { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}
        };
        static public double[,] BishopBlack =
         {
             { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
             { -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
             { -1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
             { -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
             { -1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
             { -1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
             { -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
             { -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}
        };
        static public double[,] CastleWhite =
        {
            {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
            {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            {  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0}
        };
        static public double[,] CastleBlack =
       {
            {  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            { -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            {  0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
            {  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
        };
        static public double[,] KingWhite =
        {
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
            {-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
            {2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0},
            {2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0}
        };
        static public double[,] KingBlack =
        {
            {2.0, 3.0, 1.0, 0.0, 0.0, 1.0, 3.0, 2.0},
            {2.0, 2.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0},
            {-1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
            {-2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            {-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0}
        };
        static public double[,] KnightWhite =
       {
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
        {-4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0},
        {-3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0},
        {-3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0},
        {-3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0},
        {-3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0},
        {-4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0},
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
        };
        static public double[,] KnightBlack =
        {
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
        {-4.0, -2.0,  0.0,  0.5,  0.5,  0.0, -2.0, -4.0},
        {-3.0,  0.5,  1.0,  1.5,  1.5,  1.0,  0.5, -3.0},
        {-3.0,  0.0,  1.5,  2.0,  2.0,  1.5,  0.0, -3.0},
        {-3.0,  0.5,  1.5,  2.0,  2.0,  1.5,  0.5, -3.0},
        {-3.0,  0.0,  1.0,  1.5,  1.5,  1.0,  0.0, -3.0},
        {-4.0, -2.0,  0.0,  0.0,  0.0,  0.0, -2.0, -4.0},
        {-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
        };
        static public double[,] QueenWhite =
       {
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
            {-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0},
            {-1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0},
            {-0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5},
            {0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5},
            {-1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0},
            {-1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0},
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0}
        };
        static public double[,] QueenBlack =
         {
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
            {-1.0, 0.0, 0.5, 0.0, 0.0, 0.0, 0.0, -1.0},
            {-1.0, 0.5, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0},
            {0.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5},
            {-0.5, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -0.5},
            {-1.0, 0.0, 0.5, 0.5, 0.5, 0.5, 0.0, -1.0},
            {-1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, -1.0},
            {-2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0}
        };
    }
}
