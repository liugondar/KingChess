using System.Collections.Generic;
using System.Drawing;

namespace ChessKing
{
    class Common
    {
        //Music
        static public bool IsMusicMuted = false;
        static public bool IsSoundMuted = false;
        // Game mode
        static public bool Is2PlayerMode = false; //1 player
        static public bool IsSelectedSquare = false; //selected yet
        static public int Player1Turn = 0;
        static public int Player2Turn = 1;
        static public int Player1ColorTeam = 1;
        static public int Player2ColorTeam = 2;
        static public bool IsPlaying = false;
        static public int IsTurn = 0;
        static public bool Close = false;
        // for AI
        static public ChessSquare[,] VirtualBoard = new ChessSquare[8, 8]; // Hiển thị bàn cờ cũ trong thời gian tính toán nước đi cho AI
        // For king services
        static public List<ChessSquare> CanBeProtect = new List<ChessSquare>();
        static public List<ChessSquare> CanBeMoveTemp = new List<ChessSquare>(); // Dùng cho việc xem thử các nước đi quân cờ
        static public List<ChessSquare> CanBeEatTemp = new List<ChessSquare>();
            // Check has king moved yet? if not king can castling
        static public bool isWhiteKingMoved = false;
        static public bool isBlackKingMoved = false;
            // Check has Castle moved yet? if not king can castling
        static public bool isLeftWhiteCastleMoved = false;
        static public bool isRightWhiteCastleMoved = false;
        static public bool isLeftBlackCastleMoved = false;
        static public bool isRightBlackCastleMoved = false;
            // Check if king has been in danger
        static public bool isWhiteKingChecked = false;
        static public bool isBlackKingChecked = false;
        static public List<ChessSquare> SquaresCheckingPath = new List<ChessSquare>();
        // Logic
        static public ChessSquare[,] Board;
        static public List<ChessSquare> CanBeMove = new List<ChessSquare>(); // create list, save position of piece can move
        static public List<ChessSquare> CanBeEat = new List<ChessSquare>();
        static public int RowSelected = -1; //set value =-1, not in chessboard
        static public int ColSelected = -1; //set value =-1, not in chessboard

        static public Color OldBackGround;//keep back ground before change to violet

        static public bool CheckPromote = false; //phong hau
   

        static public int Depth = 2;

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
            CanBeEatTemp.Clear();
            CanBeProtect.Clear();
        }
        static public bool IsEmptyChessSquare(ChessSquare[,] board, int row, int col)
        {
            return board[row, col].Chess == null;
        }
        static public void ChangeBackgroundColorToCanEat(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Player1Turn|| Is2PlayerMode == true)
                board[row, col].BackColor = Color.Red;
            CanBeEat.Add(board[row, col]);
        }
        static public void ChangeBackgroundColorToCanMove(ChessSquare[,] board, int row, int col)
        {
            if (IsTurn % 2 == Player1Turn || Is2PlayerMode == true)
                board[row, col].Image = Image.FromFile(Constants.linkPoint);
            CanBeMove.Add(board[row, col]);
        }
        static public bool IsDangerSquareToMove(ChessSquare[,] board, int rowCheck, int colCheck, int teamCheck)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Chess != null && board[i, j].Chess.Team != teamCheck)
                    {
                        board[i, j].Chess.FindSquaresCanEat(board, board[i, j].Row, board[i, j].Col);
                        for (int k = 0; k < CanBeMoveTemp.Count; k++)
                        {
                            if (CanBeMoveTemp[k].Row == rowCheck &&
                                CanBeMoveTemp[k].Col == colCheck)
                            {
                                CanBeEatTemp.Clear();
                                CanBeMoveTemp.Clear();
                                return true;
                            }
                        }
                    }
                    CanBeEatTemp.Clear();
                    CanBeMoveTemp.Clear();
                }
            return false;
        }
        static public bool IsSquareCanBeAttackOrMoveByTeamate(ChessSquare[,] board, int rowCheck, int colCheck, int teamCheck)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Chess != null && board[i, j].Chess.Team == teamCheck)
                    {
                        board[i, j].Chess.FindSquaresCanMove(board, board[i, j].Row, board[i, j].Col);
                        for (int k = 0; k < CanBeMoveTemp.Count; k++)
                        {
                            if (CanBeMoveTemp[k].Chess == null) CanBeMoveTemp[k].Image = null;
                            if (CanBeMoveTemp[k].Row == rowCheck && CanBeMoveTemp[k].Col == colCheck)
                            {
                                CanBeEatTemp.Clear();
                                CanBeMoveTemp.Clear();
                                return true;
                            }
                        }
                        board[i, j].Chess.FindSquaresCanEat(board, board[i, j].Row, board[i, j].Col);
                        for (int k = 0; k < CanBeEatTemp.Count; k++)
                        {
                            
                            if (CanBeEatTemp[k].Row == rowCheck && CanBeEatTemp[k].Col == colCheck)
                            {
                                CanBeEatTemp.Clear();
                                CanBeMoveTemp.Clear();
                                return true;
                            }
                        }
                    }
                    CanBeEatTemp.Clear();
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
                }
            return false;
        }
        // Create chess board image 
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

        static public void ClearMoveSuggestion()
        {
            for (int row = 0; row < 8; row++)
                for (int col = 0; col < 8; col++)
                {
                    if (Board[row, col].Chess == null)
                        Board[row, col].Image = null;
                }
        }
        // Create virtual board for AI 
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
