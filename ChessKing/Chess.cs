using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessKing
{
    class Chess
    {
        enum ColorTeam
        {
            None,
            White,
            Black,
        };
        public bool IsDie { get; set; }
        public int Team { get => team; set => team = value; }
        public bool IsKing { get => isKing; set => isKing = value; }
        public bool IsPawn { get => isPawn; set => isPawn = value; }
        public bool IsBishop { get => isBishop; set => isBishop = value; }
        public bool IsCastle { get => isCastle; set => isCastle = value; }
        public bool IsKnight { get => isKnight; set => isKnight = value; }
        public bool IsQueen { get => isQueen; set => isQueen = value; }
        public int Evaluation { get => evaluation; set => evaluation = value; }

        private int team = (int)ColorTeam.None;
        private bool isKing = false;
        private bool isPawn = false;
        private bool isBishop = false;
        private bool isCastle = false;
        private bool isKnight = false;
        private bool isQueen = false;
        private int evaluation;

        public virtual void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col){}
        public virtual void FindWay(ChessSquare[,] board, int row, int col) { }
        public virtual void FindSquaresCanProtect(ChessSquare[,] board,int row,int col) { }
    }
}
