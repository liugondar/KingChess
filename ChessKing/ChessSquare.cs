using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessKing
{
    class ChessSquare:Button
    {
        enum ColorTeam
        {
            None,
            White,
            Black,
        };
        public Chess Chess { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public ChessSquare(ChessSquare a)
        {
            Chess = a.Chess;
            Image = a.Image;
            Row = a.Row;
            Col = a.Col;
        }

    }
}
