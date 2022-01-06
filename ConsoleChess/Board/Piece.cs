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
        /// <summary>
        /// Contains the board of the piece.
        /// </summary>
        /// <value>Gets the origin side of the piece on the board.</value>
        public Board Board { get; }
        /// <summary>
        /// <para>Contains the origin board side of the piece.</para>
        /// This data is used to identify the direction to follow when the piece is of the ForwardMove type.
        /// </summary>
        /// <value>Gets the origin side of the piece on the board.</value>
        public BoardSide Side { get; }
        /// <value>Gets the team of the piece.</value>
        public Team Team { get; }
        /// <value>Gets the number of times that this piece has been moved.</value>
        public int TimesMoved { get; private set; } = 0;
        /// <value>Contains the movement rule of the piece.</value>
        protected abstract Movement Movement { get; }
        /// <value>Gets the symbol of the piece.</value>
        protected abstract string Symbol { get; }

        /// <summary>
        /// Base constructor for a new generic piece.
        /// </summary>
        /// <param name="board">Board of the piece.</param>
        /// <param name="side">Origin side on the board.</param>
        /// <param name="team">Team of the piece.</param>
        protected Piece(Board board, BoardSide side, Team team)
        {
            Board = board;
            Team = team;
            Side = side;
        }

        /// <summary>
        /// Gets the position of the piece in the <see cref="Board"/>.
        /// </summary>
        /// <returns>An <see cref="TwoDimensionPosition"/> revealing the position of the piece.</returns>
        /// <exception cref="NullReferenceException">Thrown when the piece is not in the <see cref="Board"/>.</exception>
        public TwoDimensionPosition GetPosition()
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
            throw new NullReferenceException("This piece does not belongs to the board.");
        }

        /// <summary>
        /// This must be called every time the piece is moved on the <see cref="Board"/>.
        /// </summary>
        public void Move()
        {
            TimesMoved++;
        }

        /// <summary>
        /// Returns all the possible targets for this piece.
        /// </summary>
        /// <returns>An array of booleans with the same dimensions as the <see cref="Board"/>, where true positions mean a possible target on the board.</returns>
        public virtual bool[,] PossibleTargets()
        {
            var possibleTargets = new bool[Board.Rows, Board.Columns];
            var movementSet = Movement.GetInvocationList();

            foreach (var movement in movementSet)
            {
                possibleTargets = possibleTargets.Merge(movement.DynamicInvoke(this, Board) as bool[,]);
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
