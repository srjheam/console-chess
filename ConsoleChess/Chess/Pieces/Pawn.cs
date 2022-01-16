using Board;
using Board.Enums;

using System;
using System.Collections.Generic;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the Pawn in chess.
    /// </summary>
    sealed class Pawn : ChessPiece
    {
        protected sealed override Movement Movement => PieceMovement.ForwardMove;
        protected sealed override string Symbol => "P";

        /// <summary>
        /// Constructor for a new <see cref="Pawn"/>.
        /// </summary>
        /// <inheritdoc/>
        public Pawn(ChessBoard board, BoardSide side, Team team)
            : base(board, side, team) { }

        public override bool[,] PossibleTargets()
        {
            var possibleTargets = base.PossibleTargets();

            var enPassantRow = new Dictionary<BoardSide, int> { { BoardSide.Bottom, 3 }, { BoardSide.Top, 4 } };
            var pos = GetPosition();
            var vulnerable = Board.EnPassantVulnerable;

            if (enPassantRow[Side] == pos.Y && IsEnPassantAvailable())
            {
                // Vulnerable's position BEFORE 
                var vulnerablesPos = vulnerable.GetPosition();

                SimulSet(vulnerablesPos);

                var kPos = Board.GetKing(Team).GetPosition();
                possibleTargets[vulnerablesPos.Y + (Side == BoardSide.Bottom ? -1 : 1), vulnerablesPos.X] = !SimulIsUnderAttack(kPos.Y, kPos.X);

                SimulUndo(vulnerablesPos);
            }

            return possibleTargets;

            bool IsEnPassantAvailable()
            {
                if (vulnerable == null)
                {
                    return false;
                }

                return Math.Abs(pos.X - vulnerable.GetPosition().X) == 1;
            }
            void SimulSet(TwoDimensionPosition vulnerablesPos)
            {
                // Remove Pawn
                Board.Squares[pos.Y, pos.X] = null;
                // Remove vulnerable
                Board.Squares[vulnerablesPos.Y, vulnerablesPos.X] = null;
                // Place Pawn
                Board.Squares[vulnerablesPos.Y + (Side == BoardSide.Bottom ? -1 : 1), vulnerablesPos.X] = this;
            }
            void SimulUndo(TwoDimensionPosition vulnerablesPos)
            {
                // Remove Pawn
                Board.Squares[vulnerablesPos.Y + (Side == BoardSide.Bottom ? -1 : 1), vulnerablesPos.X] = null;
                // Replace vulnerable
                Board.Squares[vulnerablesPos.Y, vulnerablesPos.X] = vulnerable;
                // Replace Pawn
                Board.Squares[pos.Y, pos.X] = this;
            }
        }
    }
}
