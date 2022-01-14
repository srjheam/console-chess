using Board;
using Board.Enums;

using Extensions;

using System.Collections.Generic;
using System.Linq;

namespace Chess.Pieces
{
    /// <summary>
    /// The ChessPiece class represents a generic chess piece.
    /// </summary>
    abstract class ChessPiece : Piece
    {
        public new ChessBoard Board { get; }
        /// <summary>
        /// Base contructor for a new ChessPiece.
        /// </summary>
        public ChessPiece(ChessBoard chessBoard, BoardSide boardSide, Team team)
            : base(chessBoard, boardSide, team)
        {
            Board = chessBoard;
        }

        public override bool[,] PossibleTargets()
        {
            var possibleTargets = base.PossibleTargets();

            for (int i = 0; i < possibleTargets.GetLength(0); i++)
            {
                for (int j = 0; j < possibleTargets.GetLength(1); j++)
                {
                    if (possibleTargets[i, j])
                    {
                        var tmpPos = GetPosition();
                        var tmpTarget = SimulSet(i, j);

                        var kPos = Board.GetKing(Team).GetPosition();
                        possibleTargets[i,j] = !SimulIsUnderAttack(kPos.Y, kPos.X);

                        SimulUndo(tmpPos, tmpTarget);
                    }
                }
            }

            return possibleTargets;

            Piece SimulSet(int tRow, int tColumn)
            {
                // Remove this Piece
                var pos = GetPosition();
                Board.Squares[pos.Y, pos.X] = null;

                // Stores and replace target
                var tmpTarget = Board.Squares[tRow, tColumn];
                Board.Squares[tRow,tColumn] = this;

                return tmpTarget;
            }
            void SimulUndo(TwoDimensionPosition origin, Piece target)
            {
                // Replace target
                var pos = GetPosition();
                Board.Squares[pos.Y, pos.X] = target;

                // Replace this Piece
                Board.Squares[origin.Y, origin.X] = this;
            }
        }

        /// <summary>
        /// Checks if the given position is target of a ennemy piece. Under simulation conditions.
        /// </summary>
        /// <param name="tRow">Ennemy target row to check.</param>
        /// <param name="tColumn">Ennemy target column to check.</param>
        /// <returns>True if the position is under attack; otherwise, it returns false.</returns>
        protected bool SimulIsUnderAttack(int tRow, int tColumn)
        {
            var pieces = new HashSet<Piece>();
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    var piece = Board.Squares[i, j];
                    if (piece != null)
                    {
                        pieces.Add(piece);
                    }
                }
            }
            var queryResult = pieces.Where(p => p.Team == (Team == Team.Black ? Team.White : Team.Black));

            var ennemyTargets = new bool[Board.Rows, Board.Columns];
            foreach (ChessPiece piece in queryResult)
            {
                var targets = new bool[Board.Rows, Board.Columns];
                var movementSet = piece.Movement.GetInvocationList();

                foreach (var movement in movementSet)
                {
                    targets = targets.Merge(movement.DynamicInvoke(piece, Board) as bool[,]);
                }

                ennemyTargets = ennemyTargets.Merge(targets);
            }

            return ennemyTargets[tRow, tColumn];
        }
    }
}
