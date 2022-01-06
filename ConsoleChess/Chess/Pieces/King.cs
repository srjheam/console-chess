using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the King in chess.
    /// </summary>
    sealed class King : ChessPiece
    {
        protected sealed override Movement Movement => PieceMovement.OneSquareAroundMove;
        protected sealed override string Symbol => "K";

        /// <summary>
        /// Constructor for a new <see cref="King"/>.
        /// </summary>
        /// <inheritdoc/>
        public King(ChessBoard board, BoardSide side, Team team)
            : base(board, side, team) { }
    }
}
