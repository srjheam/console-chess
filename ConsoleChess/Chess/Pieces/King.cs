using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the King in chess.
    /// </summary>
    sealed class King : Piece
    {
        protected sealed override Movement Movement => PieceMovement.OneSquareAroundMove;
        protected sealed override string Symbol => "K";

        /// <summary>
        /// Constructor for a new <see cref="King"/>.
        /// </summary>
        /// <inheritdoc/>
        public King(Team team, BoardSide side)
            : base(team, side) { }
    }
}
