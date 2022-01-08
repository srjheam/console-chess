using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Rook in chess.
    /// </summary>
    sealed class Rook : ChessPiece
    {
        protected sealed override Movement Movement => PieceMovement.StraightMove;
        protected sealed override string Symbol => "R";

        /// <summary>
        /// Constructor for a new <see cref="Rook"/>.
        /// </summary>
        /// <inheritdocs/>
        public Rook(ChessBoard board, BoardSide side, Team team)
            : base(board, side, team) { }
    }
}
