using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// The ChessPiece class represents a generic chess piece.
    /// </summary>
    abstract class ChessPiece : Piece
    {
        public ChessPiece(Team team, BoardSide boardSide)
            : base(team, boardSide) { }
    }
}
