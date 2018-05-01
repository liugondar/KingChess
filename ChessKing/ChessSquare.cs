using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessKing
{
    class ChessSquare : Button
    {
        string linkBlackQueen = "Image\\Chess_qdt60.png";
        string linkWhiteQueen = "Image\\Chess_qlt60.png";
        WMPLib.WindowsMediaPlayer moveSound = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer checkSound = new WMPLib.WindowsMediaPlayer();
        #region init fields
        enum ColorTeam
        {
            None,
            White,
            Black,
        };
        public Chess Chess { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public FindWayAction findWayAction;
        public ChessSquare(ChessSquare a)
        {
            Chess = a.Chess;
            Image = a.Image;
            Row = a.Row;
            Col = a.Col;
        }
        public ChessSquare()
        {
            //change properties of button
            this.Size = new System.Drawing.Size(60, 60);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
        #endregion
        /// <summary>
        /// Tạo bàng cờ 8x8 Trống
        /// </summary>
       

        protected override void OnClick(EventArgs e)
        {
            // Khóa bàn cờ khi hết cờ
            if (Common.Close) return;
            //Không làm gì hết nếu ô clicked không có quân cờ
            if (this.Chess == null && !Common.IsSelectedSquare) return;

            moveSound.URL = "Sounds/Move.wav";
            moveSound.settings.autoStart = true;
            ThayDoiOCoKhiClickVaoOCoQuanCo();

            if (Common.Is2PlayerMode)
            {
                Task.Run(() =>
                {
                    Thread.Sleep(200); // delay
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        this.findWayAction();
                    });
                });
            }
        }

        private void ThayDoiOCoKhiClickVaoOCoQuanCo()
        {
            // Quân trắng luôn do người chơi dùng nên không cần xét đến AI
            if (Common.IsTurn % 2 == Constants.WhiteTurn)
            {
                if (Common.IsSelectedSquare == false) // chua click 
                {
                    if (this.Chess.Team == (int)ColorTeam.White)
                    {
                        this.ChangeTurn();
                    }
                    else
                        return;
                }
                else //da click
                {
                    this.ChangeTurn();
                }
            }
            else // black
            {
                if (Common.Is2PlayerMode)
                {
                    if (Common.IsSelectedSquare == false)//chua click
                    {
                        if (this.Chess.Team == (int)ColorTeam.Black) // check xem co dung team dang duoc di hay khong
                        {
                            this.ChangeTurn();
                        }
                        else return;
                    }
                    else this.ChangeTurn();
                }
            }
        }

        private void PhongHau(ref ChessSquare temp)
        {
            Chess newQueen = new Queen();
            if (temp.Chess.Team == 1) //white
            {
                newQueen.Team = (int)ColorTeam.White;
                temp.Chess = newQueen;
                temp.Image = Image.FromFile(linkWhiteQueen);
                temp.Chess.Evaluation = 90;
            }
            else //black
            {
                newQueen.Team = (int)ColorTeam.Black;
                temp.Chess = newQueen;
                temp.Image = Image.FromFile(linkBlackQueen);
                temp.Chess.Evaluation = -90;
            }
        }

        private void ChangeTurn()
        {
            if (Common.IsSelectedSquare == false) RenderWhenNotSelectedYet();
            //selected
            else RenderWhenSelected();
        }


        /// <summary>
        /// Khi click chọn quân cờ lân đầu
        /// </summary>
        private void RenderWhenNotSelectedYet()
        {
            //check square is not Empty 
            if (this.Chess != null)
            {
                Common.IsSelectedSquare = true;
                Common.OldBackGround = Common.Board[this.Row, this.Col].BackColor; //keep background color of chess square 
                this.Chess.FindWayAndAutoChangeSquareIfNeeded(Common.Board, this.Row, this.Col); //findway can move and eat
                this.findWayAction();
                this.BackColor = System.Drawing.Color.Violet; //change background to violet
                Common.RowSelected = this.Row; //keep the row
                Common.ColSelected = this.Col; //keep the col

            }
        }
        /// <summary>
        /// Khi click nước đi cho quân cờ hoặc click hủy bỏ chọn quân cờ
        /// </summary>
        private void RenderWhenSelected()
        {
            Common.IsSelectedSquare = false;//gan lai bang false de lan sau con thuc hien

            if (Common.CanBeMove.Contains(this))//inside list Can Move
            {
                Common.RowProQueen = this.Row;
                Common.ColProQueen = this.Col;
                for (int i = 0; i < Common.CanBeMove.Count; i++)
                {
                    if (Common.CanBeMove[i].Chess == null)
                        Common.CanBeMove[i].Image = null;
                }
                KiemTraNhapThanh(colorTeam: (int)ColorTeam.White);
                KiemTraNhapThanh(colorTeam: (int)ColorTeam.Black);
                ThayDoiHinhAnh();
                Common.IsTurn++; //change turn
                Common.CanBeMove.Clear();
                Common.CanBeMoveTemp.Clear();
                
                KiemTraPhongHau();
                KiemTraQuanVuaConTrenBanCoKhong();
                KiemTraChieuVua();

                KiemTraVuaDaDiChuyen();
                KiemTraCastleDaDiChuyen();
                if (this.Chess.Team == (int)ColorTeam.White)
                    Common.isWhiteKingChecked = false;
                if (this.Chess.Team == (int)ColorTeam.Black)
                    Common.isBlackKingChecked = false;

                // Khi đánh với AI 
                // Đợi 100 mili s
                // Để xử lí hình ảnh xong rồi thực hiện AI tính toán
                if (!Common.Is2PlayerMode)
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(100); // delay
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            XuLiKhiDanhVoiAI();
                        });
                    });
                }
            }
            else //not inside can move list
            {
                HuyBoChonQuan();
            }

        }

        private void KiemTraCastleDaDiChuyen()
        {
            if (!Common.isRightBlackCastleMoved)
            {
                if (IsRightCastleMoved((int)ColorTeam.Black))
                    Common.isRightBlackCastleMoved = true;
            }

            if (!Common.isLeftBlackCastleMoved)
            {
                if (IsLeftCastleMoved((int)ColorTeam.Black))
                    Common.isLeftBlackCastleMoved = true;
            }
            if (!Common.isRightWhiteCastleMoved)
            {
                if (IsRightCastleMoved((int)ColorTeam.Black))
                    Common.isRightWhiteCastleMoved = true;
            }

            if (!Common.isLeftWhiteCastleMoved)
            {
                if (IsLeftCastleMoved((int)ColorTeam.Black))
                    Common.isLeftWhiteCastleMoved = true;
            }
        }

        private void KiemTraVuaDaDiChuyen()
        {
            if (!Common.isWhiteKingMoved)
            {
                if (IsKingMoved((int)ColorTeam.White))
                    Common.isWhiteKingMoved = true;
            }
            if (!Common.isBlackKingMoved)
            {
                if (IsKingMoved((int)ColorTeam.Black))
                Common.isBlackKingMoved = true;
            }
        }

        private bool IsRightCastleMoved(int colorTeam)
        {
            int colCastle;
            int rowCastle;
            if (colorTeam == (int)ColorTeam.White)
            {
                colCastle = Constants.colWhiteRightCastleDefault;
                rowCastle = Constants.rowWhiteRightCastleDefault;

            }
            else
            {
                colCastle = Constants.colBlackRightCastleDefault;
                rowCastle = Constants.rowBlackRightCastleDefault;
            }

            if (Common.Board[rowCastle, colCastle].Chess == null) return true;
            else
            {
                if (!Common.Board[rowCastle, colCastle].Chess.IsCastle) return true;
            }

            return false;
        }

        private bool IsLeftCastleMoved(int colorTeam)
        {
            int colCastle;
            int rowCastle;
            if (colorTeam == (int)ColorTeam.White)
            {
                colCastle = Constants.colWhiteLeftCastleDefault;
                rowCastle = Constants.rowWhiteLeftCastleDefault;

            }
            else
            {
                colCastle = Constants.colBlackLeftCastleDefault;
                rowCastle = Constants.rowBlackLeftCastleDefault;
            }

            if (Common.Board[rowCastle, colCastle].Chess == null) return true;
            else
            {
                if (!Common.Board[rowCastle, colCastle].Chess.IsCastle) return true;
            }

            return false;
        }

        private bool IsKingMoved(int colorTeam)
        {
            int colKing;
            int rowKing;
            if (colorTeam == (int)ColorTeam.White)
            {
                colKing = Constants.colWhiteKingDefault;
                rowKing = Constants.rowWhiteKingDefault;

            }
            else
            {
                colKing = Constants.colBlackKingDefault;
                rowKing = Constants.rowBlackKingDefault;
            }

            if (Common.Board[rowKing, colKing].Chess == null) return true;
            else
            {
                if (!Common.Board[rowKing, colKing].Chess.IsKing) return true;
            }

            return false;
        }

        private void KiemTraNhapThanh(int colorTeam)
        {
            //Check if not king chess => do nothing
            var selectedChessSquare = Common.Board[Common.RowSelected, Common.ColSelected];
            if (!selectedChessSquare.Chess.IsKing) return;

            //Variable for queen side castle
            int rowLeftBishop = -1, colLeftBishop = -1;
            int rowLeftCastle = -1, colLeftCastle = -1;
            int rowQueen = -1, colQueen = -1;
            //Varibale for king side castle
            int rowRightCastle = -1, colRightCastle = -1;
            int rowRightKnight = -1, colRightKnight = -1;
            int rowRightBishop = -1, colRightBishop = -1;

            //  Thêm giá trị các biến ở trên là 
            //các vị trí default các quân cờ bên trắng nếu team trắng
            if (colorTeam == (int)ColorTeam.White)
            {
                SetDefaultLocationForTeamWhite(out rowLeftBishop, out colLeftBishop, out rowLeftCastle, out colLeftCastle, out rowQueen, out colQueen, out rowRightCastle, out colRightCastle, out rowRightKnight, out colRightKnight, out rowRightBishop, out colRightBishop);
            }
            else// Thêm giá trị các biên ở trên là vị trí các quân cờ bên đen
            {
                SetDefaultLocationForTeamBlack(out rowLeftBishop, out colLeftBishop, out rowLeftCastle, out colLeftCastle, out rowQueen, out colQueen, out rowRightCastle, out colRightCastle, out rowRightKnight, out colRightKnight, out rowRightBishop, out colRightBishop);
            }

            // Click to default bishop to compile queen side castle
            // Move castle to queen default square
            bool isClickDefaultLeftBishopSquare =
                this.Row == rowLeftBishop
                && this.Col == colLeftBishop;
            if (isClickDefaultLeftBishopSquare)
            {

                Common.Board[rowQueen, colQueen].Image = Common.Board[rowLeftCastle, colLeftCastle].Image;
                Common.Board[rowLeftCastle, colLeftCastle].Image = null;
                //tra ve background cu
                Common.Board[rowLeftCastle, colLeftCastle].BackColor = Common.OldBackGround;
                Common.BackChessBoard();
                //thay doi quan co
                Common.Board[rowQueen, colQueen].Chess = Common.Board[rowLeftCastle, colLeftCastle].Chess;
                Common.Board[rowLeftCastle, colLeftCastle].Chess = null;

                if (colorTeam == (int)ColorTeam.White)
                {
                    //Thêm trạng thái đã castle để không thể castle lần 2
                    Common.isWhiteKingMoved = true;
                }
                else
                {//Thêm trạng thái đã castle để không thể castle lần 2
                    Common.isBlackKingMoved = true;
                }
            }

            // Click to default Knight to compile king side castle
            // Move castle to bishop default square
            bool isClickDefaultRightKnightSquare =
                this.Row == rowRightKnight
                && this.Col == colRightKnight;
            if (isClickDefaultRightKnightSquare)
            {
                Common.Board[rowRightBishop, colRightBishop].Image = Common.Board[rowRightCastle, colRightCastle].Image;
                Common.Board[rowRightCastle, colRightCastle].Image = null;
                //tra ve background cu
                Common.Board[rowRightCastle, colRightCastle].BackColor = Common.OldBackGround;
                Common.BackChessBoard();
                //thay doi quan co
                Common.Board[rowRightBishop, colRightBishop].Chess = Common.Board[rowRightCastle, colRightCastle].Chess;
                Common.Board[rowRightCastle, colRightCastle].Chess = null;

                if (colorTeam == (int)ColorTeam.White)
                {
                    //Thêm trạng thái đã castle để không thể castle lần 2
                    Common.isWhiteKingMoved = true;
                }
                else
                {//Thêm trạng thái đã castle để không thể castle lần 2
                    Common.isBlackKingMoved = true;
                }
            }
        }

        private static void SetDefaultLocationForTeamBlack(out int rowLeftBishop, out int colLeftBishop, out int rowLeftCastle, out int colLeftCastle, out int rowQueen, out int colQueen, out int rowRightCastle, out int colRightCastle, out int rowRightKnight, out int colRightKnight, out int rowRightBishop, out int colRightBishop)
        {
            rowLeftBishop = Constants.rowBlackLeftBishopDefault;
            colLeftBishop = Constants.colBlackLeftBishopDefault;
            rowLeftCastle = Constants.rowBlackLeftCastleDefault;
            colLeftCastle = Constants.colBlackLeftCastleDefault;
            rowQueen = Constants.rowBlackQueenDefault;
            colQueen = Constants.colBlackQueenDefault;

            rowRightBishop = Constants.rowBlackRightBishopDefault;
            colRightBishop = Constants.colBlackRightBishopDefault;
            rowRightCastle = Constants.rowBlackRightCastleDefault;
            colRightCastle = Constants.colBlackRightCastleDefault;
            rowRightKnight = Constants.rowBlackRightKnightDefault;
            colRightKnight = Constants.colBlackRightKnightDefault;
        }

        private static void SetDefaultLocationForTeamWhite(out int rowLeftBishop, out int colLeftBishop, out int rowLeftCastle, out int colLeftCastle, out int rowQueen, out int colQueen, out int rowRightCastle, out int colRightCastle, out int rowRightKnight, out int colRightKnight, out int rowRightBishop, out int colRightBishop)
        {
            rowLeftBishop = Constants.rowWhiteLeftBishopDefault;
            colLeftBishop = Constants.colWhiteLeftBishopDefault;
            rowLeftCastle = Constants.rowWhiteLeftCastleDefault;
            colLeftCastle = Constants.colWhiteLeftCastleDefault;
            rowQueen = Constants.rowWhiteQueenDefault;
            colQueen = Constants.colWhiteQueenDefault;

            rowRightBishop = Constants.rowWhiteRightBishopDefault;
            colRightBishop = Constants.colWhiteRightBishopDefault;
            rowRightCastle = Constants.rowWhiteRightCastleDefault;
            colRightCastle = Constants.colWhiteRightCastleDefault;
            rowRightKnight = Constants.rowWhiteRightKnightDefault;
            colRightKnight = Constants.colWhiteRightKnightDefault;
        }

        private void HuyBoChonQuan()
        {
            Common.Board[Common.RowSelected, Common.ColSelected].BackColor = Common.OldBackGround;
            for (int i = 0; i < Common.CanBeMove.Count; i++)
            {
                if (Common.CanBeMove[i].Chess == null) Common.CanBeMove[i].Image = null;
            }
            Common.BackChessBoard();
            Common.CanBeMove.Clear();
            Common.CanBeMoveTemp.Clear();
        }

        private void KiemTraChieuVua()
        {
            bool isCheck = false;
            ChessSquare Kingtemp = new ChessSquare();

            this.Check(ref isCheck, ref Kingtemp);
            if (isCheck)
            {
                Common.CanBeMove.Clear();
                Checkmate(Kingtemp);
            }

            Common.CanBeEat.Clear();
            Common.CanBeMove.Clear();

            Common.BackChessBoard();
            if (Kingtemp.Chess != null && Kingtemp.Chess.IsKing == true)
                Common.Board[Kingtemp.Row, Kingtemp.Col].BackColor = Color.BlueViolet;
        }
        private void Check(ref bool isCheck, ref ChessSquare KingTemp)
        {
            // mặc định chưa bị chiếu tướng isCheck=false
            isCheck = false;
            //check if King is danger
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess != null)
                    {
                        Common.Board[i, j].Chess.FindWayAndAutoChangeSquareIfNeeded(Common.Board, Common.Board[i, j].Row, Common.Board[i, j].Col);
                        for (int k = 0; k < Common.CanBeMove.Count; k++)
                        {
                            if (Common.CanBeMove[k].Chess == null) Common.CanBeMove[k].Image = null;
                            else
                            {
                                if (Common.CanBeMove[k].Chess.IsKing)
                                {
                                    isCheck = true;
                                    KingTemp = new ChessSquare(Common.CanBeMove[k]);
                                    if (isCheck)
                                    {
                                        // Set trạng thái đang bị check khiến không thể nhập thành
                                        if (KingTemp.Chess.Team == (int)ColorTeam.White)
                                            Common.isWhiteKingChecked = true;
                                        if (KingTemp.Chess.Team == (int)ColorTeam.Black)
                                            Common.isBlackKingChecked = true;
                                    }
                                }
                            }
                        }
                    }
                    Common.CanBeMove.Clear();
                }
        }
        /// <summary>
        /// - Check if king is available to move => check mate is false
        /// - Check if king is available to eat the attacker=> check mate is false
        /// - If king not avalable to move or eat-> check if teamate can eat the checker
        /// - Finally check if teamate can protect the checking path
        /// </summary>
        /// <param name="temp"></param>
        private void Checkmate(ChessSquare temp)
        {
            Common.CanBeEat.Clear();
            Common.CanBeMove.Clear();
            temp.Chess.FindWayAndAutoChangeSquareIfNeeded(Common.Board,temp.Row,temp.Col);
            bool isCheckmate = true;

            if (Common.CanBeMove.Count>0) 
                isCheckmate = false;
            if (Common.CanBeEat.Count > 0)
                isCheckmate = false;

            if (isCheckmate)
            {
                isCheckmate = !(temp.Chess as King).
                    IsSquareArroundProtected(Common.Board,
                    temp.Row, temp.Col);
            }

            if (isCheckmate)
            {
                isCheckmate = !(temp.Chess as King).
                    IsKingCanBeProtect(Common.Board, temp,
                    Common.Board[this.Row,this.Col]);
            }

            if (isCheckmate)
            {
                if (temp.Chess.Team == (int)ColorTeam.White) MessageBox.Show("The Black Wins");
                else MessageBox.Show("The White Wins");

                Common.IsPlaying = false;
                Common.Close = true;
            }
            else
            {
                checkSound.URL = "Sounds/Check.mp3";
                checkSound.settings.autoStart = true;
            }
        }

        private void KiemTraQuanVuaConTrenBanCoKhong()
        {
            int soLuongVua = 0;
            int colorTeam = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Common.Board[i, j].Chess == null) Common.Board[i, j].Image = null;
                    else if (Common.Board[i, j].Chess.IsKing) { soLuongVua++; colorTeam = Common.Board[i, j].Chess.Team; }
                }
            }

            if (soLuongVua == 1)
            {
                if (colorTeam == (int)ColorTeam.White) MessageBox.Show("The White Wins!");
                else MessageBox.Show("The Black Wins!");
                //TODO: add sound checkMate

                Common.IsPlaying = false;
                Common.Close = true;
            }
        }

        private void KiemTraPhongHau()
        {
            for (int j = 0; j < 8; j++)
            {
                if (Common.Board[0, j].Chess != null && Common.Board[0, j].Chess.IsPawn) PhongHau(ref Common.Board[0, j]);
                if (Common.Board[7, j].Chess != null && Common.Board[7, j].Chess.IsPawn) PhongHau(ref Common.Board[7, j]);
            }
        }

        private void ThayDoiHinhAnh()
        {
            //thay doi hinh anh
            this.Image = Common.Board[Common.RowSelected, Common.ColSelected].Image;
            Common.Board[Common.RowSelected, Common.ColSelected].Image = null;
            //tra ve background cu
            Common.Board[Common.RowSelected, Common.ColSelected].BackColor = Common.OldBackGround;
            Common.BackChessBoard();
            //thay doi quan co
            this.Chess = Common.Board[Common.RowSelected, Common.ColSelected].Chess;
            Common.Board[Common.RowSelected, Common.ColSelected].Chess = null;
        }

        // AI part
        private void XuLiKhiDanhVoiAI()
        {
            if (Common.Is2PlayerMode == false && Common.IsTurn % 2 == 1)
            {
                //TODO: add mini root de xu li ai
                this.minimaxRoot();
                Common.BackChessBoard();
            }
        }
        protected void minimaxRoot()
        {
            Common.BackChessVirtualBoard();
            //Clone Bàn cờ
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                       Common.VirtualBoard[i, j].Chess = Common.Board[i, j].Chess;
                       Common.VirtualBoard[i, j].Image= Common.Board[i, j].Image;
                }
            }
            int depth = Common.Depth;
            double bestValue = -9999;
            double value = 0;
            double alpha = -10000, beta = 10000;
            bool isMax = true;
            ChessSquare[,] board = new ChessSquare[8, 8];
            ChessSquare[,] bestMove = new ChessSquare[8, 8];
            board = Common.VirtualBoard;

            bool isBlackKingInDefault = false;
            // Kiểm tra xem ô vua đen mặc định lúc thực hiện thuật toán có vua đen không?
            if (board[0, 4].Chess != null)
            {
                isBlackKingInDefault = board[0, 4].Chess.IsKing
                    && board[0, 4].Chess.Team == (int)ColorTeam.Black;
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Chess != null && board[i, j].Chess.Team == (int)ColorTeam.Black)
                    {
                        List<ChessSquare> RootTemp = new List<ChessSquare>();

                        int befRow = i;
                        int befCol = j;
                        board[befRow, befCol].Chess.FindWayAndAutoChangeSquareIfNeeded(board, befRow, befCol);

                        for (int k = 0; k < Common.CanBeMove.Count; k++)
                        {
                            RootTemp.Add(Common.CanBeMove[k]);
                        }
                        Common.CanBeMove.Clear();

                        Chess tempChess = new Chess();
                        Image tempImage = null;

                        // Giả định lần lượt các nước đi quân cờ nếu nó có thể di chuyển
                        //được để tìm được giá trị tốt nhất cho nước đi
                        for (int k = 0; k < RootTemp.Count; k++)
                        {
                            tempChess = board[RootTemp[k].Row, RootTemp[k].Col].Chess;
                            tempImage = board[RootTemp[k].Row, RootTemp[k].Col].Image;

                            board[RootTemp[k].Row, RootTemp[k].Col].Chess = board[befRow, befCol].Chess;
                            board[RootTemp[k].Row, RootTemp[k].Col].Image = board[befRow, befCol].Image;

                            board[befRow, befCol].Chess = null;
                            board[befRow, befCol].Image = null;


                            value = minimax(depth - 1, ref board, alpha, beta, !isMax);
                            if (value >= bestValue)
                            {
                                for (int m = 0; m < 8; m++)
                                {
                                    for (int n = 0; n < 8; n++)
                                    {
                                        bestMove[m, n] = new ChessSquare(board[m, n]);
                                    }
                                }
                                bestValue = value;
                            }

                            //phục hồi lại trạng thái ban đầu quân cờ sau khi giả định
                            board[RootTemp[k].Row, RootTemp[k].Col].Undo(ref board, befRow, befCol, tempChess, tempImage);
                        }
                        RootTemp.Clear();
                    }
                }
            }

            for (int k = 0; k < 8; k++)
            {
                for (int l = 0; l < 8; l++)
                {
                    Common.VirtualBoard[k, l].Row = bestMove[k, l].Row;
                    Common.VirtualBoard[k, l].Col = bestMove[k, l].Col;
                    Common.VirtualBoard[k, l].Chess = bestMove[k, l].Chess;
                    Common.VirtualBoard[k, l].Image = bestMove[k, l].Image;
                }
            }


            // Nếu ban đầu vị trí mặc định có vua đen, Kiểm tra xem 
            // Vua đen có đi vào 2 vị trí nhập thành ngắn và dài không
            if (isBlackKingInDefault)
            {
                // Vua đã đi khỏi vị trí default => vị trí default không có cờ
                if (board[0, 4].Chess == null)
                {
                    //colRightBishop =5
                    //colRightKnight =6 
                    //colRightCastle = 7

                    // Kiểm tra king side castle
                    if (board[0, 6].Chess != null)
                    {
                        if (board[0, 6].Chess.IsKing)
                        {
                            Common.VirtualBoard[0, 5].Image = Common.VirtualBoard[0, 7].Image;
                            Common.VirtualBoard[0, 7].Image = null;
                            Common.VirtualBoard[0, 7].BackColor = Common.OldBackGround;
                            Common.BackChessVirtualBoard();
                            Common.VirtualBoard[0, 5].Chess = Common.VirtualBoard[0, 7].Chess;
                            Common.VirtualBoard[0, 7].Chess = null;
                            //Thêm trạng thái đã castle để không thể castle lần 2
                            Common.isBlackKingMoved = true;

                        }
                    }
                    // Kiểm tra queen side castle
                    //colQueen = 3;
                    //colLeftBishop = 2;
                    //colLeftCastle = 0;
                    if (board[0, 2].Chess != null)
                    {
                        if (board[0, 2].Chess.IsKing)
                        {
                            Common.VirtualBoard[0, 3].Image = Common.VirtualBoard[0, 0].Image;
                            Common.VirtualBoard[0, 0].Image = null;
                            Common.VirtualBoard[0, 0].BackColor = Common.OldBackGround;
                            Common.BackChessBoard();
                            Common.VirtualBoard[0, 3].Chess = Common.VirtualBoard[0, 0].Chess;
                            Common.VirtualBoard[0, 0].Chess = null;

                            //Thêm trạng thái đã castle để không thể castle lần 2
                            Common.isBlackKingMoved = true;
                        }
                    }
                }
            }

            //Kiểm tra xem máy có di chuyển vua chưa, phục vụ cho mục đích xét nhập thành
            KiemTraVuaDaDiChuyen();
            KiemTraCastleDaDiChuyen();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                        Common.Board[i, j].Chess = Common.VirtualBoard[i, j].Chess;
                        Common.Board[i, j].Image = Common.VirtualBoard[i, j].Image;
                    Common.VirtualBoard[i, j].Chess = null;
                    Common.VirtualBoard[i, j].Image = null;
                }
            }
            Common.IsTurn++;
        }

        private void Undo(ref ChessSquare[,] board, int befRow, int befCol, Chess tempChess, Image tempImage)
        {
            board[befRow, befCol].Chess = board[this.Row, this.Col].Chess;
            board[befRow, befCol].Image = board[this.Row, this.Col].Image;
            board[this.Row, this.Col].Chess = tempChess;
            board[this.Row, this.Col].Image = tempImage;
        }

        protected double minimax(int depth, ref ChessSquare[,] root, double alpha, double beta, bool isMax)
        {
            if (depth == 0)
                return -this.BestValue(root);
            ChessSquare[,] a = new ChessSquare[8, 8];

            double bestValue;

            int team = 0;
            if (isMax == true)
            {
                team = 2;
                bestValue = -9999;
            } //black
            else
            {
                team = 1;
                bestValue = 9999;
            } //white

            //ke list can move from root
            //List<ChessSquare[,]> tempList = new List<ChessSquare[,]>();
            //createList(root, team, tempList);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int befRow = i;
                    int befCol = j;
                    if (!Common.IsEmptyChessSquare(root, befRow, befCol)
                        && root[befRow, befCol].Chess.Team == team)
                    {
                        List<ChessSquare> RootTemp = new List<ChessSquare>();

                        root[befRow, befCol].Chess.FindWayAndAutoChangeSquareIfNeeded(root, befRow, befCol);

                        for (int k = 0; k < Common.CanBeMove.Count; k++)
                        {
                            RootTemp.Add(Common.CanBeMove[k]);
                        }
                        Common.CanBeMove.Clear();

                        Chess tempChess = new Chess();
                        Image tempImage = null;

                        for (int k = 0; k < RootTemp.Count; k++)
                        {
                            tempChess = root[RootTemp[k].Row, RootTemp[k].Col].Chess;
                            tempImage = root[RootTemp[k].Row, RootTemp[k].Col].Image;

                            root[RootTemp[k].Row, RootTemp[k].Col].Chess = root[befRow, befCol].Chess;
                            root[RootTemp[k].Row, RootTemp[k].Col].Image = root[befRow, befCol].Image;
                            root[befRow, befCol].Chess = null;
                            root[befRow, befCol].Image = null;

                            if (team == 2)
                            {
                                bestValue = Math.Max(bestValue, minimax(depth - 1, ref root, alpha, beta, !isMax));
                                alpha = Math.Max(alpha, bestValue);
                                if (beta <= alpha)
                                {
                                    root[RootTemp[k].Row, RootTemp[k].Col].Undo(ref root, befRow, befCol, tempChess, tempImage);
                                    return bestValue;
                                }
                            }
                            else
                            {
                                bestValue = Math.Min(bestValue, minimax(depth - 1, ref root, alpha, beta, !isMax));
                                beta = Math.Min(beta, bestValue);
                                if (beta <= alpha)
                                {
                                    root[RootTemp[k].Row, RootTemp[k].Col].Undo(ref root, befRow, befCol, tempChess, tempImage);
                                    return bestValue;
                                }
                            }
                            root[RootTemp[k].Row, RootTemp[k].Col].Undo(ref root, befRow, befCol, tempChess, tempImage);
                        }
                        RootTemp.Clear();
                    }
                }
            }
            return bestValue;
        }

        private double BestValue(ChessSquare[,] board)
        {
            double Val = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j].Chess != null)
                    {
                        double pieceEvaluation = 0;
                        //Team trắng evalution >0
                        if (board[i, j].Chess.Evaluation > 0)
                        {
                            pieceEvaluation = getPieceEvaluation(board, i, j);
                        }
                        else //Team đen evalution <0
                        {
                            pieceEvaluation = -getPieceEvaluation(board, i, j);
                        }
                        Val = Val + board[i, j].Chess.Evaluation + pieceEvaluation;
                    }
                }
            return Val;
        }

        /// <summary>
        /// Evaluation được khởi tạo tại form frmChessKing khi khởi tạo các
        /// quân cờ trên bàn cờ
        /// - Quân trắng: pawn:10 ; castle: 50; knight: 30; bishop: 30; queen: 90; king: 900
        /// - Quân đen: pawn:-10 ; castle:-50; knight:-30; bishop: -30; queen: -90; king: -900
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private double getPieceEvaluation(ChessSquare[,] board, int row, int col)
        {
            if (Common.IsEmptyChessSquare(board, row, col)) return 0;
            if (board[row, col].Chess.IsPawn == true) //pawn
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.PawnWhite, row, col)
                    : PieceEvaluation(Common.PawnBlack, row, col);
            }
            else if (board[row, col].Chess.IsCastle == true) //Castle
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.CastleWhite, row, col)
                    : PieceEvaluation(Common.CastleBlack, row, col);
            }
            else if (board[row, col].Chess.IsKnight == true) //Knight
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.KnightWhite, row, col)
                    : PieceEvaluation(Common.KnightBlack, row, col);
            }
            else if (board[row, col].Chess.IsBishop == true) //Bishop
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.BishopWhite, row, col)
                    : PieceEvaluation(Common.BishopBlack, row, col);
            }
            else if (board[row, col].Chess.IsQueen == true) //Queen
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.QueenWhite, row, col)
                    : PieceEvaluation(Common.QueenBlack, row, col);
            }
            else //if (board[row, col].Chess.IsKing == true) //King
            {
                return (board[row, col].Chess.Evaluation > 0)
                    ? PieceEvaluation(Common.KingWhite, row, col)
                    : PieceEvaluation(Common.KingBlack, row, col);
            }
        }

        public double PieceEvaluation(double[,] a, int row, int col)
        {
            return a[row, col];
        }
    }

}
