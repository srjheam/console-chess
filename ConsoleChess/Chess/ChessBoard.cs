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
        /// <value>Gets the ChessMatch this ChessBoard belongs to.</value>
        public ChessMatch ChessMatch { get; }
        /// <value>Gets the pawn that might be vuberable to an en passant movement.</value>
        public Pawn EnPassantVulnerable { get; private set; }

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

            var specialMove = false;
            var pawnsDoubleStepForward = false;
            if (origin is Pawn)
            {

                if (Math.Abs(originRow - targetRow) == 2) // First move, Double-step-forward 
                {
                    EnPassantVulnerable = origin as Pawn;
                    pawnsDoubleStepForward = true;
                }
                else if (target is null && Math.Abs(originColumn - targetColumn) == 1)
                {
                    RemovePiece(EnPassantVulnerable);
                    CapturedPieces.Add(EnPassantVulnerable);
                }
            }
            else if (origin is King && originRow == targetRow) // Castling
            {
                specialMove = true;
                if (!(Math.Abs(originColumn - targetColumn) == 1 && !(target is Rook)))
                {
                    // Castling side delta
                    var castlingDelta = targetColumn - originColumn > 0 ? 1 : -1;
                    if (!(target is Rook))
                    {
                        // Search for Rook
                        for (int column = originColumn + castlingDelta; column < Columns && column >= 0; column += castlingDelta)
                        {
                            var columnPiece = GetPiece(originRow, column);
                            if (columnPiece is Rook)
                            {
                                target = columnPiece;
                                Squares[originRow, column] = null;
                                break;
                            }
                        }
                    }

                    target.Move();
                    // Final column position of the Rook relative to the King's final position:
                    //     -1 column if Kingside castling
                    //     +1 column if Queenside castling
                    var rookDelta = targetColumn - originColumn > 0 ? -1 : 1;
                    // Place the Rook
                    PlacePiece(target, targetRow, targetColumn + rookDelta);
                }
            }

            if (!specialMove)
            {                
                if (!(target is null))
                    CapturedPieces.Add(target);
            }

            if (!pawnsDoubleStepForward)
                EnPassantVulnerable = null;

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
