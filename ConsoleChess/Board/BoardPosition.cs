using Screen;

using System;

namespace Board
{
    /// <summary>
    /// Represents a position on a <see cref="Board"./>.
    /// </summary>
    readonly struct BoardPosition
    {
        /// <value>An integer that represents the row of the board.</value>
        public readonly int Row;
        /// <value>A character that represents the column of the board.</value>
        public readonly char Column;

        /// <summary>
        /// Basic contructor for a position on the board.
        /// </summary>
        /// <param name="row">Row position on the board.</param>
        /// <param name="column">The Column position on the board.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throwed if the <paramref name="row"/> doesn't represents a board position.</exception>
        /// <exception cref="ArgumentException">Throwed if the <paramref name="column"/> doesn't represents a board position. </exception>
        public BoardPosition(int row, char column)
        {
            column = Char.ToLower(column);

            // Checks whether the parameters are valid.
            if (row <= 0)
            {
                throw new ArgumentOutOfRangeException("A Board.BoardPosition.Row cannot be less than or equal to zero.", innerException: null);
            }
            else if (!Char.IsLetter(column))
            {
                throw new ArgumentException("A Board.BoardPosition.Column can only be a letter.");
            }

            Row = row;
            Column = column;
        }

        /// <summary>
        /// Constructor for a position on the <paramref name="board"/> that is based on an existing <see cref="TwoDimensionPosition"/>.
        /// </summary>
        /// <param name="position">The existing position to be converted.</param>
        /// <param name="board">The board the <paramref name="position"/> refers to.</param>
        public BoardPosition(TwoDimensionPosition position, Board board)
        {
            if (position.X >= board.Columns || position.Y >= board.Rows || position.X < 0 || position.Y < 0)
                throw new InvalidCastException("The informed TwoDimensionPosition cannot be converted to BoardPosition.");
            
            Row = board.Rows - position.Y;
            Column = (char)(97 + position.X);
        }

        /// <summary>
        /// Converts this instance to its equivalent array position.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>A <see cref="TwoDimensionPosition"/> that represents a position in a based-zero array, where the y-axis is for the array row and x-axis is for the array column.</returns>
        /// <exception cref="ArgumentException">Thrown when this instance of <see cref="BoardPosition"/> does note belong to the <paramref name="board"/> parameter.</exception>
        public TwoDimensionPosition ToArrayPosition(Board board)
        {
            if (!UserInput.IsValidBoardPosition(this, board))
            {
                throw new ArgumentException("This BoardPosition does not belong to the Board informed in the parameters.");
            }

            int arrayRow = board.Rows - Row;
            int arrayColumn = (int)Column - 97;

            return new TwoDimensionPosition(arrayColumn, arrayRow);
        }

        /// <summary>
        /// Converts this instance to string.
        /// </summary>
        /// <returns>A string that represents this Board.BoardPosition.</returns>
        public override string ToString()
        {
            return $"{Column}{Row}";
        }
    }
}
