using Board.Enum;

using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Knight in chess.
    /// </summary>
    sealed class Knight : Piece
    {
        sealed protected override string Symbol => "N";
        sealed protected override Movement Movement => PieceMovement.LShapeMove;

        /// <summary>
        /// Contructor for a new <see cref="Knight"/>.
        /// </summary>
        /// <inheritdoc/>
        public Knight(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
