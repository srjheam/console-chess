using Screen;

using System;
using System.Drawing;

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
                throw new ArgumentOutOfRangeException("A Board.BoardPosition.Row cannot be less than or equal to zero.", (Exception)null);
            }
            else if (!Char.IsLetter(column))
            {
                throw new ArgumentException("A Board.BoardPosition.Column can only be a letter.");
            }
            
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Converts this instance to its equivalent array position.
        /// </summary>
        /// <param name="board"></param>
        /// <returns>An <see cref="System.Drawing.Point"/> that represents an position in a based-zero array</returns>
        /// <exception cref="ArgumentException">Thrown when this instance of <see cref="BoardPosition"/> does note belong to the <paramref name="board"/> parameter.</exception>
        public Point ToArrayPosition(Board board)
        {
            int arrayRow = board.Rows - Row;
            int arrayColumn = (int)Column - 97;

            if (!UserInput.IsValidBoardPosition(this, board))
            {
                throw new ArgumentException("This BoardPosition does not belong to the Board informed in the parameters.");
            }
            
            return new Point(arrayRow, arrayColumn);
        }

        /// <summary>
        /// Converts this instance to a string.
        /// </summary>
        /// <returns>A string that represents this Board.BoardPosition.</returns>
        public override string ToString()
        {
            return $"{Column}{Row}";
        }
    }
}
