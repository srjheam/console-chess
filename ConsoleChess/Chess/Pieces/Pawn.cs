using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Pawn in chess.
    /// </summary>
    sealed class Pawn : ChessPiece
    {
        protected sealed override Movement Movement => PieceMovement.ForwardMove;
        protected sealed override string Symbol => "P";

        /// <summary>
        /// Constructor for a new <see cref="Pawn"/>.
        /// </summary>
        /// <inheritdoc/>
        public Pawn(Team team, BoardSide side)
            : base(team, side) { }
    }
}
