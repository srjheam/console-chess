using Board;
using Board.Enums;
using Chess.Pieces;

using System.Collections.Generic;

namespace Chess
{
    /// <summary>
    /// The <c>Board</c> class contains chess board manipulation related functions.
    /// </summary>
    /// <inheritdoc/>
    sealed class ChessBoard : Board.Board
    {
        /// <value>Gets the captured pieces during this match.</value>
        public List<Piece> CapturedPieces { get; } = new List<Piece>();

        /// <summary>
        /// Base constructor for a new standard chess board.
        /// </summary>
        public ChessBoard()
            : base(8, 8)
        { }

        sealed public override void MovePiece(int originRow, int originColumn, int targetRow, int targetColumn)
        {
            var origin = RemovePiece(originRow, originColumn);
            var target = RemovePiece(targetRow, targetColumn);

            if (!(target is null))
                CapturedPieces.Add(target);

            origin.Move();
            PlacePiece(origin, targetRow, targetColumn);
        }

        sealed protected override void SetupBoard()
        {
            // Start to place black side pieces
            PlacePiece(new Rook(Team.Black, this, BoardSide.Top), 0, 0);
            PlacePiece(new Knight(Team.Black, this, BoardSide.Top), 0, 1);
            PlacePiece(new Bishop(Team.Black, this, BoardSide.Top), 0, 2);
            PlacePiece(new Queen(Team.Black, this, BoardSide.Top), 0, 3);
            PlacePiece(new King(Team.Black, this, BoardSide.Top), 0, 4);
            PlacePiece(new Bishop(Team.Black, this, BoardSide.Top), 0, 5);
            PlacePiece(new Knight(Team.Black, this, BoardSide.Top), 0, 6);
            PlacePiece(new Rook(Team.Black, this, BoardSide.Top), 0, 7);
            // Places the black pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.Black, this, BoardSide.Top), 1, column);
            }

            // Start to place white side pieces
            // places the white pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.White, this, BoardSide.Bottom), 6, column);
            }
            PlacePiece(new Rook(Team.White, this, BoardSide.Bottom), 7, 0);
            PlacePiece(new Knight(Team.White, this, BoardSide.Bottom), 7, 1);
            PlacePiece(new Bishop(Team.White, this, BoardSide.Bottom), 7, 2);
            PlacePiece(new Queen(Team.White, this, BoardSide.Bottom), 7, 3);
            PlacePiece(new King(Team.White, this, BoardSide.Bottom), 7, 4);
            PlacePiece(new Bishop(Team.White, this, BoardSide.Bottom), 7, 5);
            PlacePiece(new Knight(Team.White, this, BoardSide.Bottom), 7, 6);
            PlacePiece(new Rook(Team.White, this, BoardSide.Bottom), 7, 7);
        }
    }
}
