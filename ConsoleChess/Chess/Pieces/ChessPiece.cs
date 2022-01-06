using Board;
using Board.Enums;

namespace Chess.Pieces
{
    /// <summary>
    /// The ChessPiece class represents a generic chess piece.
    /// </summary>
    abstract class ChessPiece : Piece
    {
        /// <summary>
        /// Base contructor for a new ChessPiece.
        /// </summary>
        public ChessPiece(ChessBoard chessBoard, BoardSide boardSide, Team team)
            : base(chessBoard, boardSide, team) { }
    }
}
