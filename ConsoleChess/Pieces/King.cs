using Board.Enum;

using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the King in chess.
    /// </summary>
    sealed class King : Piece
    {
        sealed protected override string Symbol => "K";
        sealed protected override Movement Movement => PieceMovement.OneSquareAroundMove;

        /// <summary>
        /// Contructor for a new <see cref="King"/>.
        /// </summary>
        /// <inheritdoc/>
        public King(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
