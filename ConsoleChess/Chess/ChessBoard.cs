using Board;
using Board.Enums;

using Chess.Pieces;

using System;
using System.Linq;
using System.Collections.Generic;

namespace Chess
{
    /// <summary>
    /// The <c>Board</c> class contains chess board manipulation related functions.
    /// </summary>
    /// <inheritdoc/>
    [Serializable]
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

        /// <summary>
        /// Given a team, this method returns that team's king.
        /// </summary>
        /// <param name="t">The team to get its king.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown when there is no <paramref name="t"/>-team king on the board.</exception>
        public King GetKing(Team t) => Pieces.First(p => p is King && p.Team == t) as King;

        public sealed override void MovePiece(int originRow, int originColumn, int targetRow, int targetColumn)
        {
            var origin = RemovePiece(originRow, originColumn);
            var target = RemovePiece(targetRow, targetColumn);

            if (!(target is null))
                CapturedPieces.Add(target);

            origin.Move();
            PlacePiece(origin, targetRow, targetColumn);
        }

        protected sealed override void SetupBoard()
        {
            #region Black Pieces
            PlacePiece(new Rook(Team.Black, BoardSide.Top), 0, 0);
            PlacePiece(new Knight(Team.Black, BoardSide.Top), 0, 1);
            PlacePiece(new Bishop(Team.Black, BoardSide.Top), 0, 2);
            PlacePiece(new Queen(Team.Black, BoardSide.Top), 0, 3);
            PlacePiece(new King(Team.Black, BoardSide.Top), 0, 4);
            PlacePiece(new Bishop(Team.Black, BoardSide.Top), 0, 5);
            PlacePiece(new Knight(Team.Black,  BoardSide.Top), 0, 6);
            PlacePiece(new Rook(Team.Black, BoardSide.Top), 0, 7);
            
            // Places black pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.Black, BoardSide.Top), 1, column);
            }
            #endregion

            #region White Pieces
            // Places white pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(Team.White, BoardSide.Bottom), 6, column);
            }
            PlacePiece(new Rook(Team.White, BoardSide.Bottom), 7, 0);
            PlacePiece(new Knight(Team.White, BoardSide.Bottom), 7, 1);
            PlacePiece(new Bishop(Team.White, BoardSide.Bottom), 7, 2);
            PlacePiece(new Queen(Team.White, BoardSide.Bottom), 7, 3);
            PlacePiece(new King(Team.White, BoardSide.Bottom), 7, 4);
            PlacePiece(new Bishop(Team.White, BoardSide.Bottom), 7, 5);
            PlacePiece(new Knight(Team.White, BoardSide.Bottom), 7, 6);
            PlacePiece(new Rook(Team.White, BoardSide.Bottom), 7, 7);
            #endregion
        }
    }
}
