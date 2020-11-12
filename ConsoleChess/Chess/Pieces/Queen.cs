using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Queen in chess.
    /// </summary>
    sealed class Queen : Piece
    {
        sealed protected override string Symbol => "Q";
        sealed protected override Movement Movement => (Movement)PieceMovement.StraightMove + PieceMovement.DiagonalMove;

        /// <summary>
        /// Contructor for a new <see cref="Queen"/>.
        /// </summary>
        /// <inheritdoc/>
        public Queen(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
