using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Rook in chess.
    /// </summary>
    sealed class Rook : Piece
    {
        sealed protected override string Symbol => "R";
        sealed protected override Movement Movement => PieceMovement.StraightMove;

        /// <summary>
        /// Contructor for a new <see cref="Rook"/>.
        /// </summary>
        /// <inheritdocs/>
        public Rook(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
