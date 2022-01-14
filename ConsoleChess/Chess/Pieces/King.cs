using Board;
using Board.Enums;

using System;

namespace Chess.Pieces
{
    /// <summary>
    /// Represents the King in chess.
    /// </summary>
    sealed class King : ChessPiece
    {
        protected sealed override Movement Movement => PieceMovement.OneSquareAroundMove;
        protected sealed override string Symbol => "K";

        public bool IsInCheck
        {
            get
            {
                var ennemyTargets = Board.GetAllPossibleTargets(Team == Team.Black ? Team.White : Team.Black);
                var kPos = GetPosition();
                return ennemyTargets[kPos.Y, kPos.X];
            }
        }

        /// <summary>
        /// Constructor for a new <see cref="King"/>.
        /// </summary>
        /// <inheritdoc/>
        public King(ChessBoard board, BoardSide side, Team team)
            : base(board, side, team) { }

        public override bool[,] PossibleTargets()
        {
            var possibleTargets = base.PossibleTargets();

            var pos = GetPosition();
            // Checks whether castling is available or not
            if (TimesMoved != 0 || SimulIsUnderAttack(pos.Y, pos.X))
            {
                return possibleTargets;
            }

            // Checks for the kingside castling
            for (int column = pos.X + 1; column < Board.Columns; column++)
            {
                var targetPiece = Board.GetPiece(pos.Y, column);
                // Checks whether the way is clear and secure - without checks.
                if (!IsClearAndSecure(targetPiece, column))
                {
                    break;
                }

                if (targetPiece is Rook)
                {
                    if (targetPiece.TimesMoved != 0)
                    {
                        break;
                    }
                    var tmpRookPos = SimulSet(targetPiece);

                    if (tmpRookPos.X - pos.X == 1)
                    {
                        possibleTargets[tmpRookPos.Y, tmpRookPos.X] = !SimulIsUnderAttack(pos.Y, pos.X);
                    }
                    else
                    {
                        possibleTargets[pos.Y, pos.X + 2] = !SimulIsUnderAttack(pos.Y, pos.X);
                    }

                    SimulUndo(targetPiece, tmpRookPos);
                    break;
                }
            }

            // Checks for the queen side castling
            for (int column = pos.X - 1; column >= 0; column--)
            {
                var targetPiece = Board.GetPiece(pos.Y, column);
                // Checks whether the way is clear and secure - without checks.
                if (!IsClearAndSecure(targetPiece, column))
                {
                    break;
                }

                if (targetPiece is Rook)
                {
                    if (targetPiece.TimesMoved != 0)
                    {
                        break;
                    }
                    var tmpRookPos = SimulSet(targetPiece);

                    if (tmpRookPos.X - pos.X == -1)
                    {
                        possibleTargets[tmpRookPos.Y, tmpRookPos.X] = !SimulIsUnderAttack(pos.Y, pos.X);
                    }
                    else
                    {
                        possibleTargets[pos.Y, pos.X - 2] = !SimulIsUnderAttack(pos.Y, pos.X);
                    }

                    SimulUndo(targetPiece, tmpRookPos);
                    break;
                }
            }

            return possibleTargets;

            bool IsClearAndSecure(Piece targetPiece, int column)
            {
                return !(targetPiece != null && !(targetPiece is Rook) || Math.Abs(pos.X - column) <= 2 && SimulIsUnderAttack(pos.Y, column));
            }
            TwoDimensionPosition SimulSet(Piece rook)
            {
                // Remove King
                Board.Squares[pos.Y, pos.X] = null;
                // Remove Rook
                var tmpRookPos = rook.GetPosition();
                Board.Squares[tmpRookPos.Y, tmpRookPos.X] = null;

                // Cast King and Rook
                // Checks if it's a kingside castling
                if (tmpRookPos.X - pos.X > 1)
                {
                    Board.Squares[pos.Y, pos.X + 2] = this;
                    Board.Squares[pos.Y, pos.X + 1] = rook;
                }
                else if (tmpRookPos.X - pos.X < -1) // Queenside castling
                {
                    Board.Squares[pos.Y, pos.X - 2] = this;
                    Board.Squares[pos.Y, pos.X - 1] = rook;
                }
                else // Side-by-side castling
                {
                    Board.Squares[tmpRookPos.Y, tmpRookPos.X] = this;
                    Board.Squares[pos.Y, pos.X] = rook;
                }

                return tmpRookPos;
            }
            void SimulUndo(Piece rook, TwoDimensionPosition rookBackupPos)
            {
                // Remove King
                var kingSimulPos = GetPosition();
                Board.Squares[kingSimulPos.Y, kingSimulPos.X] = null;
                // Remove Rook
                var rookSimulPos = rook.GetPosition();
                Board.Squares[rookSimulPos.Y, rookSimulPos.X] = null;

                // Replace King
                Board.Squares[pos.Y, pos.X] = this;
                // Replace Rook
                Board.Squares[rookBackupPos.Y, rookBackupPos.X] = rook;
            }
        }
    }
}
