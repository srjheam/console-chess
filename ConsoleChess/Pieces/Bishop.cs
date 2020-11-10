﻿using Board.Enum;

using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Bishop in chess.
    /// </summary>
    sealed class Bishop : Piece
    {
        sealed protected override string Symbol => "B";
        sealed protected override Movement Movement => PieceMovement.DiagonalMove;

        /// <summary>
        /// Contructor for a new <see cref="Bishop"/>.
        /// </summary>
        /// <inheritdoc/>
        public Bishop(Team team, Board.Board board, BoardSide side)
            : base(team, board, side) { }
    }
}
