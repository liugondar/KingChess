namespace ChessKing
{
    public static class Constants
    {
        static public int WhiteTurn = 0;
        static public int BlackTurn = 1;
        static public int firstRowOfTable = 0;
        static public int lastRowOfTable = 7;
        static public int firstColOfTable = 0;
        static public int lastColOfTable = 7;
        // default location white chess
        static public int rowWhiteChessDefault = 7;
        static public int rowWhitePawnDefault = 7;

        static public int colWhiteRightCastleDefault = 7;
        static public int colWhiteRightKnightDefault = 6;
        static public int colWhiteRightBishopDefault = 5;
        static public int colWhiteKingDefault = 4;
        static public int colWhiteQueenDefault = 3;
        static public int colWhiteLeftBishopDefault = 2;
        static public int colWhiteLeftKnightDefault = 1;
        static public int colWhiteLeftCastleDefault = 0;
        //Default black chess
        static public int rowBlackChessDefault = 0;
        static public int rowBlackPawnDefault = 7;

        static public int colBlackRightCastleDefault = 7;
        static public int colBlackRightKnightDefault = 6;
        static public int colBlackRightBishopDefault = 5;
        static public int colBlackKingDefault = 4;
        static public int colBlackQueenDefault = 3;
        static public int colBlackLeftBishopDefault = 2;
        static public int colBlackLeftKnightDefault = 1;
        static public int colBlackLeftCastleDefault = 0;
        //Link image
        static public string linkWhiteCastle = "Image\\Chess_rlt60.png";
        static public string linkWhiteBishop = "Image\\Chess_blt60.png";
        static public string linkWhiteKnight = "Image\\Chess_nlt60.png";
        static public string linkWhiteQueen = "Image\\Chess_qlt60.png";
        static public string linkWhiteKing = "Image\\Chess_klt60.png";
        static public string linkWhitePawn = "Image\\Chess_plt60.png";
        static public string linkBlackCastle = "Image\\Chess_rdt60.png";
        static public string linkBlackBishop = "Image\\Chess_bdt60.png";
        static public string linkBlackKnight = "Image\\Chess_ndt60.png";
        static public string linkBlackQueen = "Image\\Chess_qdt60.png";
        static public string linkBlackKing = "Image\\Chess_kdt60.png";
        static public string linkBlackPawn = "Image\\Chess_pdt60.png";
        static public string linkPoint = "Image\\circle.png";
        public static void SetDefaultChessLocation()
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                // default location white chess
                rowWhiteChessDefault = 7;
                rowWhitePawnDefault = 6;
                //Default black chess
                rowBlackChessDefault = 0;
                rowBlackPawnDefault = 1;
            }
            else
            {
                // default location white chess
                rowWhiteChessDefault = 0;
                rowWhitePawnDefault = 1;
                //Default black chess
                rowBlackChessDefault = 7;
                rowBlackPawnDefault = 6;
            }
        }
    }
}
