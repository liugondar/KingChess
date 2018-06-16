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

        /// Tìm các ô cờ mà quân cơ có thể di chuyển và ăn được
        /// Thêm vào common.CanBeEat và Common.CanBeMove các ô cờ phù hợp đã tìm
        /// Sau đó đổi màu các ô cờ tương ứng
        public virtual void FindWayAndAutoChangeSquareIfNeeded(ChessSquare[,] board, int row, int col){}

        /// Tìm các ô cờ mà quân cơ có thể ăn được
        /// Thêm vào common.CanBeEatTemp các ô phù hợp
        /// phục vụ cho việc t kiểm tra đường đi cho vua, nhập thành, chiếu,...
        public virtual void FindSquaresCanEat(ChessSquare[,] board, int row, int col) { }

        /// Tìm ô cờ mà quân cờ có thể di chuyển được
        /// Thêm vào common.common.CanBeMoveTemp các ô phù hợp
        /// phục vụ cho việc t kiểm tra đường đi cho vua, nhập thành, chiếu,...
        public virtual void FindSquaresCanMove(ChessSquare[,] board, int row, int col) { }

        /// Thêm vào common.common.CanBeProtect các ô phù hợp
        /// phục vụ cho việc t kiểm tra đường đi cho vua, nhập thành, chiếu,..
        public virtual void FindSquaresCanProtect(ChessSquare[,] board,int row,int col) { }
    }
}
