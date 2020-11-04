using Board;

using System;

namespace Screen
{
    /// <summary>
    /// This class handles any user input to the console.
    /// </summary>
    /// <remarks>This class can convert the user input.</remarks>
    static class UserInput
    {
        /// <summary>
        /// Converts the <paramref name="input"/> to its equivalent <see cref="BoardPosition"/>.
        /// </summary>
        /// <param name="input">The string to be converted.</param>
        /// <returns>The equivalent <see cref="BoardPosition"/> to this parameter <paramref name="input"/>; otherwise, it throws an exception.</returns>
        /// <exception cref="ArgumentException">Thrown when the user input is not a valid <see cref="BoardPosition"/>.</exception>
        public static BoardPosition ConvertToBoardPosition(string input)
        {
            // Checks whether the user input is a valid BoardPosition by having a
            // character as the first input and then a number.
            if (!IsBoardPosition(input))
            {
                throw new ArgumentException($"The user input \"{input}\" is not a board postion.");
            }
            
            input = input.ToLower();

            return new BoardPosition(int.Parse(input[1..]), input[0]);
        }

        /// <summary>
        /// Checks if the string is a <see cref="BoardPosition"/>.
        /// </summary>
        /// <param name="input">The string to be checked.</param>
        /// <returns>True if <paramref name="input"/> is a <see cref="BoardPosition"/>; otherwise, returns false.</returns>
        public static bool IsBoardPosition(string input)
        {
            return Char.IsLetter(input[0]) && int.TryParse(input[1..], out _);
        }

        /// <summary>
        /// Checks whether the <paramref name="boardPosition"/> might belong to the <paramref name="board"/>.
        /// </summary>
        /// <param name="boardPosition">The board position to be checked whether it can belong to the <paramref name="board"/>.</param>
        /// <param name="board">The board in question.</param>
        /// <returns>True if the <paramref name="boardPosition"/> can belong to the <paramref name="board"/>; otherwise, returns false.</returns>
        public static bool IsValidBoardPosition(BoardPosition boardPosition, Board.Board board)
        {
            return !(boardPosition.Row > board.Rows || (boardPosition.Column - 97) > board.Columns);
        }

        /// <summary>
        /// Checks whether the <paramref name="boardPosition"/> might belong to the <paramref name="board"/>.
        /// </summary>
        /// <param name="boardPosition">The board position to be checked whether it can belong to the <paramref name="board"/>.</param>
        /// <param name="board">The board in question.</param>
        /// <returns>True if the <paramref name="boardPosition"/> can belong to the <paramref name="board"/>; otherwise, returns false.</returns>
        public static bool IsValidBoardPosition(string userInput, Board.Board board)
        {
            BoardPosition boardPosition;
            try
            {
                // Throw an exception if it's not a BoardPosition
                boardPosition = ConvertToBoardPosition(userInput);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return IsValidBoardPosition(boardPosition, board);
        }
    }
}
