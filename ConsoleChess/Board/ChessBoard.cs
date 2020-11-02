using Pieces;
using Pieces.Enum;

namespace Board
{
    /// <summary>
    /// The <c>Board</c> class contains chess board manipulation related functions.
    /// </summary>
    /// <inheritdoc/>
    sealed class ChessBoard : Board
    {
        /// <summary>
        /// Base constructor for a new standard chess board.
        /// </summary>
        public ChessBoard()
            : base(8, 8)
        {
            PlacePieces();            
        }

        /// <summary>
        /// Place the chess pieces in the board;
        /// </summary>
        sealed protected override void PlacePieces()
        {
            // Start to place black side pieces
            Squares[0, 0] = new Rook(Team.Black);
            Squares[0, 1] = new Knight(Team.Black);
            Squares[0, 2] = new Bishop(Team.Black);
            Squares[0, 3] = new Queen(Team.Black);
            Squares[0, 4] = new King(Team.Black);
            Squares[0, 5] = new Bishop(Team.Black);
            Squares[0, 6] = new Knight(Team.Black);
            Squares[0, 7] = new Rook(Team.Black);
            // Places the black pawns
            for (int column = 0; column < 8; column++)
            {
                Squares[1, column] = new Pawn(Team.Black);
            }

            // Start to place white side pieces
            // Places the white pawns
            for (int column = 0; column < 8; column++)
            {
                Squares[6, column] = new Pawn(Team.White);
            }
            Squares[7, 0] = new Rook(Team.White);
            Squares[7, 1] = new Knight(Team.White);
            Squares[7, 2] = new Bishop(Team.White);
            Squares[7, 3] = new Queen(Team.White);
            Squares[7, 4] = new King(Team.White);
            Squares[7, 5] = new Bishop(Team.White);
            Squares[7, 6] = new Knight(Team.White);
            Squares[7, 7] = new Rook(Team.White);
        }
    }
}
