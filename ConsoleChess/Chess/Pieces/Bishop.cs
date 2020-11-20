using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Bishop in chess.
    /// </summary>
    sealed class Bishop : Piece
    {
        protected sealed override Movement Movement => PieceMovement.DiagonalMove;
        protected sealed override string Symbol => "B";

        /// <summary>
        /// Constructor for a new <see cref="Bishop"/>.
        /// </summary>
        /// <inheritdoc/>
        public Bishop(Team team, BoardSide side)
            : base(team, side) { }
    }
}
