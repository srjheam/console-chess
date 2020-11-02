using System;
using System.Linq;

namespace Screen
{
    /// <summary>
    /// Contains all methods related to the graphics generator for the console.
    /// </summary>
    static class GraphicEngine
    {
        /// <summary>
        /// Prints an board to the console with all letter and row indicators.
        /// </summary>
        /// <param name="board">Board to be printed.</param>
        /// <param name="spacesAtLeft">Distance between <see cref="System.Console.CursorLeft"/> and the column of the buffer area where each <paramref name="board"/>'s row begins to be written.</param>
        public static void PrintBoard(Board.Board board, int spacesAtLeft)
        {
            /* This function output (example for a 10x10 board and spacesAtLeft = 3, on the board '-' means board.Square without piece in position):
             *       ABCDEFGH
             *      +========+
             *    10|--------|10
             *    9 |--------| 9
             *    8 |--------| 8
             *    7 |--------| 7
             *    6 |--------| 6
             *    5 |--------| 5
             *    4 |--------| 4
             *    3 |--------| 3
             *    2 |--------| 2
             *    1 |--------| 1
             *      +========+
             *       ABCDEFGH
             */

            // Column of the buffer area where board begins to be written. 
            var columnWhereBoardBegins = Console.CursorLeft;
            // Length of the greatest row used for figure out how many spaces to insert amid row indicator and the board's edge.
            var greatestRowNumberLength = board.Rows.ToString().Length;

            // UPPER COLUMNS indicators (Generates the left spaces. Generates the column indicators, then converts them to an char[], then a string. Concats both.)
            // Regarding left spaces part: spacesAtLeft (this function parameter) + greatestRowNumberLength + 1 (left edge '|' width)
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength + 1)
                + String.Concat(
                    Enumerable.Range('\u0041', board.Columns)
                    .Select(c => (char)c)));
            Console.SetCursorPosition(columnWhereBoardBegins, Console.CursorTop);

            // Left spaces and TOP EDGE of the board
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength)
                + $"+{new String('=', board.Columns)}+");
            Console.SetCursorPosition(columnWhereBoardBegins, Console.CursorTop);

            // Writing the board rows
            for (int row = 0; row < board.Rows; row++)
            {
                int rowIndicator = board.Rows - row;

                // Left spaces, row indicator, pad amid row indicator and board edge to greatestRowNumberLength and then the LEFT EDGE of the board
                Console.Write(
                    new string(' ', spacesAtLeft)
                    + rowIndicator.ToString().PadLeft(greatestRowNumberLength)
                    + "|");
                
                // Pieces in this line
                for (int column = 0; column < board.Columns; column++)
                {
                    Console.Write(board.Squares[row, column] == null ? "-" : board.Squares[row, column].ToString());
                }

                // RIGHT EDGE with row indicator
                Console.WriteLine(
                    "|"
                    + (rowIndicator).ToString().PadRight(greatestRowNumberLength));
                Console.SetCursorPosition(columnWhereBoardBegins, Console.CursorTop);
            }

            // Left spaces and LOWER EDGE of the board
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength)
                + $"+{new String('=', board.Columns)}+");
            Console.SetCursorPosition(columnWhereBoardBegins, Console.CursorTop);

            // Left spaces and BOTTOM COLUMN indicators
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength + 1)
                + String.Concat(
                    Enumerable.Range('\u0041', board.Columns)
                    .Select(c => (char)c)));
        }
    }
}
