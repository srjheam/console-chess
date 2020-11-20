using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Knight in chess.
    /// </summary>
    sealed class Knight : Piece
    {
        protected sealed override Movement Movement => PieceMovement.LShapeMove;
        protected sealed override string Symbol => "N";

        /// <summary>
        /// Constructor for a new <see cref="Knight"/>.
        /// </summary>
        /// <inheritdoc/>
        public Knight(Team team, BoardSide side)
            : base(team, side) { }
    }
}
