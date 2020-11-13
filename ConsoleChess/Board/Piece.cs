using Board.Enums;
using Chess;
using Extensions;

using System;

namespace Board
{
    /// <summary>
    /// The <c>Piece</c> class represents a generic piece.
    /// </summary>
    abstract class Piece
    {
        /// <value>Gets the team of the piece.</value>
        public readonly Team Team;

        /// <value>Gets the board where the piece belongs.</value>
        public readonly Board Board;

        /// <summary>
        /// <para>Contains the origin board side of the piece.</para>
        /// This data is used to identify the direction to follow when the piece is of the ForwardMove type.
        /// </summary>
        /// <value>Gets the origin side of the piece on the board.</value>
        public readonly BoardSide Side;

        /// <value>Gets the number of times that this piece has been moved.</value>
        public int TimesMoved { get; private set; } = 0;

        /// <value>Gets the symbol of the piece.</value>
        protected abstract string Symbol { get; }
        /// <value>Contains the movement rule of the piece.</value>
        protected abstract Movement Movement { get; }

        /// <value>
        /// Gets the position of the piece in the <see cref="Board"/>.
        /// </value>
        /// <exception cref="NullReferenceException">Thrown when the piece is not in the <see cref="Board"/>.</exception>
        public TwoDimensionPosition Position
        {
            get
            {
                for (int row = 0; row < Board.Rows; row++)
                {
                    for (int column = 0; column < Board.Columns; column++)
                    {
                        if (Board.GetPiece(row, column) == this)
                        {
                            return new TwoDimensionPosition(column, row);
                        }
                    }
                }
                throw new NullReferenceException("This piece is not in the Board.");
            }
        }

        /// <summary>
        /// Base constructor for a new generic piece.
        /// </summary>
        /// <param name="team">Team of the piece.</param>
        /// <param name="board">Board where the piece belongs.</param>
        /// <param name="side">Origin side on the board.</param>
        protected Piece(Team team, Board board, BoardSide side)
        {
            Team = team;
            Board = board;
            Side = side;
        }

        /// <summary>
        /// It must be called every time the piece is moved on the <see cref="Board"/>.
        /// </summary>
        public void Move()
        {
            TimesMoved++;
        }

        /// <summary>
        /// Returns the possible targets for the piece.
        /// </summary>
        /// <returns>An array of booleans with the same dimensions as the <see cref="Board"/>, where true positions mean a possible target on the board.</returns>
        public bool[,] PossibleTargets()
        {
            var movementSet = Movement.GetInvocationList();
            var possibleTargets = new bool[Board.Rows, Board.Columns];

            foreach (var movement in movementSet)
            {
                possibleTargets = possibleTargets.Merge(movement.DynamicInvoke(this) as bool[,]);
            }

            return possibleTargets;
        }

        /// <summary>
        /// Converts the <c>Piece</c> of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance, consisting of the <see cref="Symbol"/>.</returns>
        sealed public override string ToString()
        {
            return Symbol;
        }
    }
}
