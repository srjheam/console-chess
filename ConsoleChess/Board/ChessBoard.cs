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
            SetupBoard();            
        }

        sealed public override void MovePiece(int originRow, int originColumn, int targetRow, int targetColumn)
        {
            var origin = RemovePiece(originRow, originColumn);

            PlacePiece(origin, targetRow, targetColumn);
        }

        sealed protected override void SetupBoard()
        {
            // Start to place black side pieces
            PlacePiece(new Rook(Team.Black), 0, 0);
            PlacePiece(new Knight(Team.Black), 0, 1);
            PlacePiece(new Bishop(Team.Black), 0, 2);
            PlacePiece(new Queen(Team.Black), 0, 3);
            PlacePiece(new King(Team.Black), 0, 4);
            PlacePiece(new Bishop(Team.Black), 0, 5);
            PlacePiece(new Knight(Team.Black), 0, 6);
            PlacePiece(new Rook(Team.Black), 0, 7);
            // Places the black pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.Black), 1, column);
            }

            // Start to place white side pieces
            // places the white pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.White), 6, column);
            }
            PlacePiece(new Rook(Team.White), 7, 0);
            PlacePiece(new Knight(Team.White), 7, 1);
            PlacePiece(new Bishop(Team.White), 7, 2);
            PlacePiece(new Queen(Team.White), 7, 3);
            PlacePiece(new King(Team.White), 7, 4);
            PlacePiece(new Bishop(Team.White), 7, 5);
            PlacePiece(new Knight(Team.White), 7, 6);
            PlacePiece(new Rook(Team.White), 7, 7);
        }
    }
}
