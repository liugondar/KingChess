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

        //logic
        static public string linkPoint = "Image\\circle.png";
        // Game mode
        static public bool Is2PlayerMode = false; //1 player
        static public bool IsSelectedSquare = false; //selected yet
        static public bool IsPlaying = false;
        static public int IsTurn = 0;
       

        // Biến phục vụ mục đích nhập thành
        static public bool isBlackKingSideCastleAvailable = false;
        static public bool isBlackQueenSideCastleAvailable = false;

        static public bool isWhiteKingSideCastleAvailable = false;
        static public bool isWhiteQueenSideCastleAvailable = false;

        static public bool isWhiteKingMoved = false;
        static public bool isBlackKingMoved = false;

        static public bool isBlackKingSideCastleMoved= false;
        static public bool isBlackQueenSideCastleMoved= false;

        static public bool isWhiteKingSideCastleMoved= false;
        static public bool isWhiteQueenSideCastleMoved= false;

        // Kiểm tra xem đã nhập thành lần nào chưa, nếu rồi trả về false
        // Chỉ có thể nhập thành 1 lần trong 1 game
        static public bool isBlackKingCastled = false;
        static public bool isWhiteKingCastled = false;

       
        static public void ResetPropToDefault()
        {
            IsSelectedSquare = false;
            IsTurn = 0;
            IsPlaying = false;
            

            isBlackKingSideCastleAvailable = false;
            isBlackQueenSideCastleAvailable = false;
            isWhiteKingSideCastleAvailable = false;
            isWhiteQueenSideCastleAvailable = false;

            isBlackKingMoved = false;
            isWhiteKingMoved = false;

            isBlackKingSideCastleMoved= false;
            isBlackQueenSideCastleMoved= false;
            isWhiteKingSideCastleMoved= false;
            isWhiteQueenSideCastleMoved= false;

            isBlackKingCastled = false;
            isWhiteKingCastled = false;
        }
        static public bool IsEmptyChessSquare(ChessSquare[,] board, int row, int col)
        {
            return board[row, col].Chess == null;
        }
        static public void ChangeBackgroundColorToCanEat(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Constants.WhiteTurn || Is2PlayerMode == true)
                board[row, col].BackColor = Color.Red;
            CanMove.Add(board[row, col]);
        }
        static public void ChangeBackgroundColorToCanMove(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Constants.WhiteTurn || Is2PlayerMode == true)
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

        // Ma trận Piece-Square Table phục vụ cho AI tìm được value tốt nhất
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
