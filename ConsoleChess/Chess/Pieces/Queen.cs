using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Queen in chess.
    /// </summary>
    sealed class Queen : ChessPiece
    {
        protected sealed override Movement Movement => (Movement)PieceMovement.StraightMove + PieceMovement.DiagonalMove;
        protected sealed override string Symbol => "Q";

        /// <summary>
        /// Constructor for a new <see cref="Queen"/>.
        /// </summary>
        /// <inheritdoc/>
        public Queen(Team team, BoardSide side)
            : base(team, side) { }
    }
}
