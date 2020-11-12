using Board;
using Board.Enums;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Screen
{
    /// <summary>
    /// Contains all methods related to the graphics generator for the console.
    /// </summary>
    static class GraphicEngine
    {
        /// <value>Gets the highlight background color.</value>
        private const ConsoleColor highlightBackground = ConsoleColor.DarkGray;

        /// <summary>
        /// Prints a report regarding the captured pieces by each team of the board.
        /// </summary>
        /// <param name="board">The board which has the captured pieces.</param>
        public static void CapturedPiecesReport(List<Piece> capturedPieces)
        {
            var tmp = Console.ForegroundColor;

            var teams = capturedPieces.GroupBy(x => x.Team).OrderBy(x => x.Key);

            foreach (var team in teams)
            {
                Console.ForegroundColor = GetTeamColors(team.Key).ForegroundColor;
                Console.WriteLine($"Captured {team.Key.ToString().ToLower()} pieces: {{{String.Join(", ", team)}}}");
            }

            Console.ForegroundColor = tmp;
        }

        /// <summary>
        /// Prints an board to the console with all letter and row indicators.
        /// </summary>
        /// <param name="board">Board to be printed.</param>
        /// <param name="highlightedSquares">An array of booleans where true positions means a board square to be highlighted.</param>
        /// <param name="spacesAtLeft">Distance between <see cref="System.Console.CursorLeft"/> and the column of the buffer area where each <paramref name="board"/>'s row begins to be written.</param>
        public static void PrintBoard(Board.Board board, int spacesAtLeft)
        {
            PrintBoard(board, new bool[board.Rows, board.Columns], spacesAtLeft);
        }
        
        /// <summary>
        /// Prints an board to the console with all letter, row indicators and highlighting selected board squares.
        /// </summary>
        /// <param name="board">Board to be printed.</param>
        /// <param name="highlightedSquares">An array of booleans where true positions means a board square to be highlighted.</param>
        /// <param name="spacesAtLeft">Distance between <see cref="System.Console.CursorLeft"/> and the column of the buffer area where each <paramref name="board"/>'s row begins to be written.</param>
        public static void PrintBoard(Board.Board board, in bool[,] highlightedSquares, int spacesAtLeft)
        {
            /* This function output (example for a 10x10 board and spacesAtLeft = 3, on the board '-' means a null board square):
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
            Console.CursorLeft = columnWhereBoardBegins;

            // Left spaces and TOP EDGE of the board
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength)
                + $"+{new String('=', board.Columns)}+");
            Console.CursorLeft = columnWhereBoardBegins;

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
                    PrintBoardSquare(board.GetPiece(row, column), highlightedSquares[row, column]);
                }

                // RIGHT EDGE with row indicator
                Console.WriteLine(
                    "|"
                    + (rowIndicator).ToString().PadRight(greatestRowNumberLength));
                Console.CursorLeft = columnWhereBoardBegins;
            }

            // Left spaces and LOWER EDGE of the board
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength)
                + $"+{new String('=', board.Columns)}+");
            Console.CursorLeft = columnWhereBoardBegins;

            // Left spaces and BOTTOM COLUMN indicators
            Console.WriteLine(
                new string(' ', spacesAtLeft + greatestRowNumberLength + 1)
                + String.Concat(
                    Enumerable.Range('\u0041', board.Columns)
                    .Select(c => (char)c)));
        }

        /// <summary>
        /// Prints a board square and can also highlight it when needed.
        /// </summary>
        /// <param name="p">The content of the board square to be printed.</param>
        /// <param name="highlight">If true it colors the background of the board square with a highlight color; otherwise, no color changes.</param>
        private static void PrintBoardSquare(Piece p, bool highlight = false)
        {
            ConsoleColor tmp = Console.BackgroundColor;

            if (highlight)
                Console.BackgroundColor = highlightBackground;

            PrintPiece(p);

            Console.BackgroundColor = tmp;
        }

        /// <summary>
        /// Prints a piece.
        /// </summary>
        /// <remarks>
        /// This method can color piece's foreground with different colors for different Piece.Team.
        /// </remarks>
        /// <param name="p">The piece to be printed.</param>
        private static void PrintPiece(Piece p)
        {
            if (p is null)
            {
                Console.Write('-');
            }
            else
            {
                var tmp = Console.ForegroundColor;

                Console.ForegroundColor = GetTeamColors(p.Team).ForegroundColor;

                Console.Write(p);

                Console.ForegroundColor = tmp;
            }
        }

        /// <summary>
        /// Gets the BackgroundColor and ForegroundColor that represents the <paramref name="team"/>.
        /// </summary>
        /// <param name="team">The <see cref="Team"/> to get the colors that represents it.</param>
        /// <returns>Returns an <see cref="Tuple{ConsoleColor, ConsoleColor}"/> where the first item is the BackgroundColor and the second item is the ForegroundColor, they're the visual representation of the <paramref name="team"/>.
        /// </returns>
        private static (ConsoleColor BackgroundColor, ConsoleColor ForegroundColor) GetTeamColors(Team team)
        {
            return team switch
            {
                Team.Black => (ConsoleColor.DarkYellow , ConsoleColor.Yellow),
                Team.White => (ConsoleColor.Gray, ConsoleColor.White),
                _ => throw new NotImplementedException("Unexpected Piece.Team received. Color not implemented yet.")
            };
        }
    }
}
