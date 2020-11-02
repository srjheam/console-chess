using Board.Exceptions;
using Pieces;

namespace Board
{
    /// <summary>
    /// The <c>Board</c> class contains all generic board manipulation related functions.
    /// </summary>
    abstract class Board
    {
        /// <value>Contains the pieces in the board.</value>
        public readonly Piece[,] Squares;
        /// <value>Gets the number of columns in the board.</value>
        public int Columns
        {
            get => Squares.GetLength(1);
        }
        /// <value>Gets the number of rows in the board.</value>
        public int Rows
        {
            get => Squares.GetLength(0);
        }

        /// <summary>
        /// Base constructor for a new generic board.
        /// </summary>
        /// <param name="width">Width of the board.</param>
        /// <param name="height">Height of the board.</param>
        /// <exception cref="InvalidSizeBoardException">Thrown when either <paramref name="width"/> or <paramref name="height"/> is equal to or shorter than 0.</exception>
        public Board(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new InvalidSizeBoardException("The board does not support width or height equal to or less than 0.");
            }

            Squares = new Piece[height, width];
        }

        /// <summary>
        /// Place the game pieces in the board.
        /// </summary>
        protected abstract void PlacePieces();
    }
}
