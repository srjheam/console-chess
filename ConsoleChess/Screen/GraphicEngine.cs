using Board;
using Board.Enums;

using Chess;

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
        /// Cleans just one row of the console.
        /// </summary>
        /// <param name="consoleRow">The console row to be cleaned.</param>
        public static void CleanConsoleRow(int consoleRow)
        {
            (int consoleLeft, int consoleTop) tmp = (Console.CursorLeft, Console.CursorTop);

            Console.CursorLeft = 0;
            Console.CursorTop = consoleRow;

            Console.Write(new string(' ', Console.BufferWidth));

            Console.CursorLeft = tmp.consoleLeft;
            Console.CursorTop = tmp.consoleTop;
        }

        /// <summary>
        /// Requests an input from the user on the console.
        /// </summary>
        /// <remarks>If the message isn't null, it concatenates the <paramref name="message"/> with a ": " at the message's end, writes the result and then reads a line of the user input.</remarks>
        /// <param name="message">An informative message for the user before the input</param>
        /// <returns>The user input.</returns>
        public static string RequestUserInput(string message = null)
        {
            var tmp = Console.CursorTop;

            Console.Write(message is null ? null : message + ": ");
            var input = Console.ReadLine();

            Console.CursorTop = tmp;

            return input;
        }

        /// <summary>
        /// Cleans the actual cursor row and prints an error message just in the row which the cursor is.
        /// </summary>
        /// <param name="ex">The message to be printed.</param>
        public static void ShowOneLineError(string message)
        {
            CleanConsoleRow(Console.CursorTop);

            Console.Write(message);
            Console.ReadKey(true);

            CleanConsoleRow(Console.CursorTop);

            Console.CursorLeft = 0;
        }

        /// <summary>
        /// Cleans the actual cursor row and prints an error just in the row which the cursor is.
        /// </summary>
        /// <param name="ex">The error as an exception to be printed.</param>
        public static void ShowOneLineError(Exception ex)
        {
            ShowOneLineError(ex.Message);
        }

        /// <summary>
        /// Prints on the console the summary of the <paramref name="match"/>.
        /// </summary>
        /// <param name="match">The match to print its summary.</param>
        public static void PrintMatchSummary(ChessMatch match)
        {
            CapturedPiecesReport(match.Board.CapturedPieces);

            if (match.SelectedPiece is null)
                PrintBoard(match.Board, 4);
            else
                PrintBoard(match.Board, match.SelectedPiece.PossibleTargets(), 4);
            Console.WriteLine();

            Console.WriteLine($"Turn: {match.Turn}");
            Console.Write("Team: ");
            PrintTeam(match.TeamPlaying);
            if (match.Board.GetKing(match.TeamPlaying).IsInCheck)
            {
                Console.WriteLine();
                Console.Write("      ");
                PrintCheck(match.TeamPlaying);
            }
            Console.WriteLine();
            Console.WriteLine();

            if (!(match.SelectedPiece is null))
            {
                // Example:
                // Selected piece: P (e2)
                Console.Write($"Selected piece: ");
                PrintPiece(match.SelectedPiece);
                Console.WriteLine($" ({ new BoardPosition(match.SelectedPiece.GetPosition(), match.Board)})");
            }
            if (!(match.SelectedTarget is null))
            {
                // Example:
                // Selected target: P (e2)
                Console.Write($"Selected target: ");
                PrintPiece(match.Board.GetPiece(match.SelectedTarget.Value));
                Console.WriteLine($" ({ new BoardPosition(match.SelectedPiece.GetPosition(), match.Board)})");
            }
        }

        /// <summary>
        /// Prints on the console all the valid options for a pawn promotion special move.
        /// </summary>
        public static void PrintPawnPromotionOptions()
        {
            Console.WriteLine("Pawn promotion options");
            Console.WriteLine("Q - Queen");
            Console.WriteLine("N - Knight");
            Console.WriteLine("R - Rook");
            Console.WriteLine("B - Bishop");
        }
        
        /// <summary>
        /// Prints on the console the result of the <paramref name="match"/>.
        /// </summary>
        /// <param name="match">The match to print its result.</param>
        public static void PrintMatchResults(ChessMatch match)
        {
            var titleLength = match.GameStatus.ToString().Length;
            var winner = match.TeamPlaying == Team.Black ? Team.White : Team.Black;
            var colors = GetTeamColors(winner);

            if (match.GameStatus is Chess.Enums.GameStatus.Stalemate)
            {
                colors = (ConsoleColor.DarkBlue, ConsoleColor.DarkCyan);
            }

            var tmp = (Console.BackgroundColor, Console.ForegroundColor);
            Console.BackgroundColor = colors.BackgroundColor;
            Console.ForegroundColor = colors.ForegroundColor;

            var spaces = new string(' ', (int)Math.Floor((18 - titleLength) / 2d));
            Console.WriteLine();
            Console.CursorLeft = 1;
            Console.WriteLine(new string(' ', 18));
            Console.CursorLeft = 1;
            Console.WriteLine($"{new string(' ', (18 - titleLength) % 2)}{spaces}{match.GameStatus.ToString().ToUpper()}{spaces}");
            Console.CursorLeft = 1;
            Console.WriteLine(new string(' ', 18));
            Console.WriteLine();

            Console.BackgroundColor = tmp.BackgroundColor;
            Console.ForegroundColor = tmp.ForegroundColor;

            PrintBoard(match.Board, 4);

            Console.BackgroundColor = colors.BackgroundColor;
            Console.ForegroundColor = colors.ForegroundColor;

            string message = String.Empty;
            switch (match.GameStatus)
            {
                case Chess.Enums.GameStatus.Checkmate:
                    message = $"{winner} wins!";
                    break;
                case Chess.Enums.GameStatus.Stalemate:
                    message = "Game ends in a tie";
                    break;
            }
            var messageLength = message.Length;
            spaces = new string(' ', (int)Math.Floor((18 - messageLength) / 2d));
            Console.WriteLine();
            Console.CursorLeft = 1;
            Console.WriteLine($"{new string(' ', (18 - messageLength) % 2)}{spaces}{message}{spaces}");

            Console.BackgroundColor = tmp.BackgroundColor;
            Console.ForegroundColor = tmp.ForegroundColor;
        }

        /// <summary>
        /// Prints IN CHECK using the repective team colors.
        /// </summary>
        /// <param name="teamPlaying">The team that is in check.</param>
        private static void PrintCheck(Team teamPlaying)
        {
            var tmp = (Console.BackgroundColor, Console.ForegroundColor);

            var teamColors = GetTeamColors(teamPlaying);
            Console.BackgroundColor = teamColors.BackgroundColor;
            Console.ForegroundColor = teamColors.ForegroundColor;
            Console.Write($" I N   C H E C K ");

            Console.BackgroundColor = tmp.BackgroundColor;
            Console.ForegroundColor = tmp.ForegroundColor;
        }

        /// <summary>
        /// Prints a report regarding the captured pieces by each team of the board.
        /// </summary>
        /// <param name="board">The board which has the captured pieces.</param>
        public static void CapturedPiecesReport(List<Piece> capturedPieces)
        {
            var firstColumn = Console.CursorLeft + 1;
            var tmpColor = Console.ForegroundColor;

            var teams = capturedPieces.GroupBy(x => x.Team).OrderBy(x => x.Key);

            if (teams.Count() > 0)
            {
                int greaterLength = 0;
                foreach (var team in teams)
                {
                    var header = $"Captured {team.Key.ToString().ToLower()} pieces:";
                    var captured = $"{String.Join(", ", team)}";

                    if (header.Length > greaterLength)
                        greaterLength = header.Length;
                    else if (captured.Length > greaterLength)
                        greaterLength = captured.Length;
                }

                foreach (var team in teams)
                {
                    Console.ForegroundColor = GetTeamColors(team.Key).ForegroundColor;

                    var header = $"Captured {team.Key.ToString().ToLower()} pieces:";
                    var captured = $"{String.Join(", ", team)}";

                    Console.CursorLeft = firstColumn;
                    WriteCentralized(header, greaterLength);
                    Console.WriteLine();

                    Console.CursorLeft = firstColumn;
                    WriteCentralized(captured, greaterLength);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = tmpColor;
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
        /// Prints the team on the console with its specific colors.
        /// </summary>
        /// <param name="t">The team to print.</param>
        private static void PrintTeam(Team t)
        {
            var tmp = (Console.BackgroundColor, Console.ForegroundColor);

            var teamColors = GetTeamColors(t);
            Console.BackgroundColor = teamColors.BackgroundColor;
            Console.ForegroundColor = teamColors.ForegroundColor;
            Console.Write($" {t.ToString().ToUpper()} ");

            Console.BackgroundColor = tmp.BackgroundColor;
            Console.ForegroundColor = tmp.ForegroundColor;
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
                Team.Black => (ConsoleColor.DarkYellow, ConsoleColor.Yellow),
                Team.White => (ConsoleColor.DarkGray, ConsoleColor.White),
                _ => throw new NotImplementedException("Unexpected Piece.Team received. Color not implemented yet.")
            };
        }

        /// <summary>
        /// Writes the specified string value centralized at the middle of the next totalLength-characters, to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="totalLength">The total final length of the value to write.</param>
        private static void WriteCentralized(string value, int totalLength)
        {
            if (value.Length > totalLength)
                throw new ArgumentException("The value must be equal to or less than totalLength.");

            var spacesAtEdges = (totalLength - value.Length) / 2;

            Console.CursorLeft += spacesAtEdges;

            Console.Write(value);

            Console.CursorLeft += spacesAtEdges;
        }
    }
}
