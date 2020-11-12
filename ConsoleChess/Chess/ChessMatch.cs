using Board;

namespace Chess
{
    /// <summary>
    /// Handles a chess match.
    /// </summary>
    class ChessMatch
    {
        /// <value>Gets this match's chess board.</value>
        public ChessBoard Board { get; }

        /// <summary>
        /// Basic constructor for a <see cref="ChessMatch"/>.
        /// </summary>
        /// <param name="board">The board that <see cref="ChessMatch"/> handles.</param>
        public ChessMatch(ChessBoard board)
        {
            Board = board;
        }
    }
}
