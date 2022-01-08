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

        public bool IsInCheck
        {
            get
            {
                var ennemyTargets = Board.GetAllPossibleTargets(Team == Team.Black ? Team.White : Team.Black);
                var kPos = GetPosition();
                return ennemyTargets[kPos.Y, kPos.X];
            }
        }

        /// <summary>
        /// Constructor for a new <see cref="King"/>.
        /// </summary>
        /// <inheritdoc/>
        public King(ChessBoard board, BoardSide side, Team team)
            : base(board, side, team) { }
    }
}
