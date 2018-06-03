using System;

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
        //Evaluation
        //White chess
        static public int whitePawnEvaluation = 10;
        static public int whiteKnightEvaluation = 30;
        static public int whiteBishopEvaluation = 30;
        static public int whiteCastleEvaluation = 50;
        static public int whiteQueenEvaluation = 90;
        static public int whiteKingEvaluation = 900;
        //Black chess
        static public int blackPawnEvaluation = -10;
        static public int blackKnightEvaluation = -30;
        static public int blackBishopEvaluation = -30;
        static public int blackCastleEvaluation = -50;
        static public int blackQueenEvaluation = -90;
        static public int blackKingEvaluation = -900;
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

        public static void SetDefualtEvaluation()
        {
            if (Common.Player1ColorTeam == (int)ColorTeam.White)
            {
                //White chess
                whitePawnEvaluation = 10;
                whiteKnightEvaluation = 30;
                whiteBishopEvaluation = 30;
                whiteCastleEvaluation = 50;
                whiteQueenEvaluation = 90;
                whiteKingEvaluation = 900;
                //Black chess
                blackPawnEvaluation = -10;
                blackKnightEvaluation = -30;
                blackBishopEvaluation = -30;
                blackCastleEvaluation = -50;
                blackQueenEvaluation = -90;
                blackKingEvaluation = -900;
            }
            else
            {
                //White chess
                whitePawnEvaluation = -10;
                whiteKnightEvaluation = -30;
                whiteBishopEvaluation = -30;
                whiteCastleEvaluation = -50;
                whiteQueenEvaluation = -90;
                whiteKingEvaluation = -900;
                //Black chess
                blackPawnEvaluation = 10;
                blackKnightEvaluation = 30;
                blackBishopEvaluation = 30;
                blackCastleEvaluation = 50;
                blackQueenEvaluation = 90;
                blackKingEvaluation = 900;
            }
        }
    }
}
