﻿using System;
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

        static public bool Close = false;
        static public ChessSquare[,] Board;
        static public ChessSquare [,] VirtualBoard=new ChessSquare[8,8]; // Hiển thị bàn cờ cũ trong thời gian tính toán nước đi cho AI
        static public List<ChessSquare> CanBeMove = new List<ChessSquare>(); // create list, save position of piece can move
        static public List<ChessSquare> CanBeEat = new List<ChessSquare>();
        static public List<ChessSquare> CanBeProtect = new List<ChessSquare>();

        static public List<ChessSquare> CanBeMoveTemp = new List<ChessSquare>(); // Dùng cho việc xem thử các nước đi quân cờ
        static public int RowSelected = -1; //set value =-1, not in chessboard
        static public int ColSelected = -1; //set value =-1, not in chessboard

        static public Color OldBackGround;//keep back ground before change to violet

        static public bool CheckPromote = false; //phong hau
        static public int RowProQueen = -1;
        static public int ColProQueen = -1;

        static public int Depth = 2;
        // Biến phục vụ mục đích nhập thành
        static public bool isBlackKingSideCastleAvailable = false;
        static public bool isBlackQueenSideCastleAvailable = false;

        static public bool isWhiteKingSideCastleAvailable = false;
        static public bool isWhiteQueenSideCastleAvailable = false;


        // Kiểm tra xem đã di chuyển lần nào chưa, nếu rồi trả về true
        // Chỉ có thể nhập thành  nếu chưa đi
        static public bool isWhiteKingMoved = false;
        static public bool isBlackKingMoved = false;
        static public bool isLeftWhiteCastleMoved = false;
        static public bool isRightWhiteCastleMoved = false;
        static public bool isLeftBlackCastleMoved = false;
        static public bool isRightBlackCastleMoved = false;

        static public bool isWhiteKingChecked = false;
        static public bool isBlackKingChecked = false;


        static public void ResetPropToDefault()
        {
            IsSelectedSquare = false;
            IsTurn = 0;
            IsPlaying = false;
            RowSelected = -1; //set value =-1, not in chessboard
            ColSelected = -1;

            isBlackKingMoved = false;
            isWhiteKingMoved = false;
            isLeftWhiteCastleMoved = false;
            isRightWhiteCastleMoved = false;
            isLeftBlackCastleMoved = false;
            isRightBlackCastleMoved = false;

            isWhiteKingChecked = false;
            isBlackKingChecked = false;

            CanBeMove.Clear();
            CanBeMoveTemp.Clear();
            CanBeEat.Clear();
            CanBeProtect.Clear();

        }
        static public bool IsEmptyChessSquare(ChessSquare[,] board, int row, int col)
        {
            return board[row, col].Chess == null;
        }
        static public void ChangeBackgroundColorToCanEat(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Constants.WhiteTurn || Is2PlayerMode == true)
                board[row, col].BackColor = Color.Red;
            CanBeMove.Add(board[row, col]);
        }
        static public void ChangeBackgroundColorToCanMove(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Constants.WhiteTurn || Is2PlayerMode == true)
                board[row, col].Image = Image.FromFile(linkPoint);
            CanBeMove.Add(board[row, col]);
        }

        static public bool IsDangerSquareToMove(ChessSquare[,] board, int rowCheck, int colCheck, int teamCheck)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j].Chess != null && Board[i, j].Chess.Team != teamCheck)
                    {
                        Board[i, j].Chess.FindWay(Board, Board[i, j].Row, Board[i, j].Col);
                        for (int k = 0; k < CanBeMoveTemp.Count; k++)
                        {
                            if (CanBeMoveTemp[k].Chess == null) CanBeMoveTemp[k].Image = null;
                            if (CanBeMoveTemp[k].Row == rowCheck && CanBeMoveTemp[k].Col == colCheck)
                            {
                                BackChessBoard();
                                CanBeMoveTemp.Clear();
                                return true;
                            }
                        }
                    }
                    BackChessBoard();
                    CanBeMoveTemp.Clear();
                }
            return false;
        }
        static public bool IsProtected(ChessSquare[,] board, int rowCheck, int colCheck, int teamCheck)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j].Chess != null && Board[i, j].Chess.Team == teamCheck)
                    {
                        Board[i, j].Chess.FindSquaresCanProtect(Board, Board[i, j].Row, Board[i, j].Col);
                        for (int k = 0; k < CanBeProtect.Count; k++)
                        {
                            if (CanBeProtect[k].Row == rowCheck && CanBeProtect[k].Col == colCheck)
                            {
                                CanBeProtect.Clear();
                                return true;
                            }
                        }
                    }
                    CanBeProtect.Clear();
                    BackChessBoard();
                }
            return false;
        }

        static public void BackChessBoard()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 0)
                            Board[row, col].BackColor = Color.LavenderBlush;
                        else
                            Board[row, col].BackColor = Color.DarkSlateGray;
                    }
                    else
                    {
                        if (col % 2 == 0)
                            Board[row, col].BackColor = Color.DarkSlateGray;
                        else
                            Board[row, col].BackColor = Color.LavenderBlush;
                    }
                }
        }
        static public void BackChessVirtualBoard()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 0)
                            VirtualBoard[row, col].BackColor = Color.LavenderBlush;
                        else
                            VirtualBoard[row, col].BackColor = Color.DarkSlateGray;
                    }
                    else
                    {
                        if (col % 2 == 0)
                            VirtualBoard[row, col].BackColor = Color.DarkSlateGray;
                        else
                            VirtualBoard[row, col].BackColor = Color.LavenderBlush;
                    }
                }
        }


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
