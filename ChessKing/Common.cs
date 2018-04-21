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
        static public bool Is2PlayerMode = false; //1 player
        static public bool IsSelectedSquare = false; //selected yet
        static public int IsTurn = 0;
        static public int WhiteTurn = 0;
        static public int BlackTurn = 0;
        static public int fisrtRowOfTable = 0;
        static public int lastRowOfTable = 7;
        static public int fisrtColOfTable = 0;
        static public int lastColOfTable = 7;
        static public int whitePawnDefaultRow = 6;
        static public int blackPawnDefaultRow = 1;
        static public bool IsEmptyChessSquare(ChessSquare[,] board, int row, int col)
        {
            return board[row, col].Chess == null;
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

    }
}
