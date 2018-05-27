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
        public int Team { get ; set ; }
        public bool IsKing { get ; set ; }
        public bool IsPawn { get ; set ; }
        public bool IsBishop { get ; set ; }
        public bool IsCastle { get; set; }
        public bool IsKnight { get ; set ; }
        public bool IsQueen { get ; set ; }
        public int Evaluation { get ; set ; }

        public virtual void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col){}
        public virtual void FindSquareCanBeEat(ChessSquare[,] board, int row, int col) { }
        public virtual void FindSquaresCanProtect(ChessSquare[,] board,int row,int col) { }
        public virtual void FindSquareCanBeMove(ChessSquare[,] board, int row, int col) { }
    }
}
