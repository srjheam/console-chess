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
        public ChessMatch ChessMatch { get; }

        /// <summary>
        /// Base constructor for a new standard chess board.
        /// </summary>
        public ChessBoard(ChessMatch match)
            : base(8, 8)
        {
            ChessMatch = match;
        }

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
            PlacePiece(new Rook(this, BoardSide.Top, Team.Black), 0, 0);
            PlacePiece(new Knight(this, BoardSide.Top, Team.Black), 0, 1);
            PlacePiece(new Bishop(this, BoardSide.Top, Team.Black), 0, 2);
            PlacePiece(new Queen(this, BoardSide.Top, Team.Black), 0, 3);
            PlacePiece(new King(this, BoardSide.Top, Team.Black), 0, 4);
            PlacePiece(new Bishop(this, BoardSide.Top, Team.Black), 0, 5);
            PlacePiece(new Knight(this, BoardSide.Top, Team.Black), 0, 6);
            PlacePiece(new Rook(this, BoardSide.Top, Team.Black), 0, 7);
            
            // Places black pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(this, BoardSide.Top, Team.Black), 1, column);
            }
            #endregion

            #region White Pieces
            // Places white pawns
            for (int column = 0; column < 8; column++)
            {
                PlacePiece(new Pawn(this, BoardSide.Bottom, Team.White), 6, column);
            }
            PlacePiece(new Rook(this, BoardSide.Bottom, Team.White), 7, 0);
            PlacePiece(new Knight(this, BoardSide.Bottom, Team.White), 7, 1);
            PlacePiece(new Bishop(this, BoardSide.Bottom, Team.White), 7, 2);
            PlacePiece(new Queen(this, BoardSide.Bottom, Team.White), 7, 3);
            PlacePiece(new King(this, BoardSide.Bottom, Team.White), 7, 4);
            PlacePiece(new Bishop(this, BoardSide.Bottom, Team.White), 7, 5);
            PlacePiece(new Knight(this, BoardSide.Bottom, Team.White), 7, 6);
            PlacePiece(new Rook(this, BoardSide.Bottom, Team.White), 7, 7);
            #endregion
        }
    }
}
