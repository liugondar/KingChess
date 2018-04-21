using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessKing
{
    class Pawn:Chess
    {
        string linkPoint = "Image\\circle.png";

        public Pawn()
        {
            this.IsPawn = true;
        }
    }
}
