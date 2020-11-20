using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Rook in chess.
    /// </summary>
    sealed class Rook : Piece
    {
        protected sealed override Movement Movement => PieceMovement.StraightMove;
        protected sealed override string Symbol => "R";

        /// <summary>
        /// Constructor for a new <see cref="Rook"/>.
        /// </summary>
        /// <inheritdocs/>
        public Rook(Team team, BoardSide side)
            : base(team, side) { }
    }
}
