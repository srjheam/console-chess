using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Pawn in chess.
    /// </summary>
    sealed class Pawn : Piece
    {
        sealed protected override string Symbol => "P";
        sealed protected override Movement Movement => PieceMovement.ForwardMove;

        /// <summary>
        /// Constructor for a new <see cref="Pawn"/>.
        /// </summary>
        /// <inheritdoc/>
        public Pawn(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
