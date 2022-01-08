using Board.Enums;
using Board.Exceptions;

using Extensions;

using System;
using System.Linq;
using System.Collections.Generic;

namespace Board
{
    /// <summary>
    /// The <c>Board</c> class contains all generic board manipulation related functions.
    /// </summary>
    [Serializable]
    abstract class Board
    {
        /// <value>Contains all the board squares with their respective content.</value>
        public Piece[,] Squares { get; protected set; }
        /// <value>Gets the number of columns in the board.</value>
        public int Columns { get; }
        /// <value>Contains all the pieces on the board.</value>
        public HashSet<Piece> Pieces { get; } = new HashSet<Piece>();
        /// <value>Gets the number of rows in the board.</value>
        public int Rows { get; }

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
            Columns = width;
            Rows = height;

            SetupBoard();
        }

        /// <summary>
        /// Moves a piece from a position of the board to another.
        /// </summary>
        /// <param name="originRow">The origin row, in the array <see cref="Squares"/>, where the piece will be removed from.</param>
        /// <param name="originColumn">The origin column, in the array <see cref="Squares"/>, where the piece will be removed from.</param>
        /// <param name="targetRow">The target row, in the array <see cref="Squares"/>, where the removed piece will be placed.</param>
        /// <param name="targetColumn">The target column, in the array <see cref="Squares"/>, where the removed piece will be placed.</param>
        public abstract void MovePiece(int originRow, int originColumn, int targetRow, int targetColumn);
        
        /// <summary>
        /// Moves a piece from a position of the board to another.
        /// </summary>
        /// <param name="origin">The origin piece.</param>
        /// <param name="target">The target position.</param>
        public void MovePiece(in BoardPosition origin, in BoardPosition target)
        {
            var originToArray = origin.ToArrayPosition(this);
            var targetToArray = target.ToArrayPosition(this);

            MovePiece(originToArray.Y, originToArray.X, targetToArray.Y, targetToArray.X);
        }

        /// <summary>
        /// Merges all pieces' possible targets of the <paramref name="t"/>-team.
        /// </summary>
        /// <param name="t">The team to get the possible targets of all its pieces.</param>
        /// <returns>An array of booleans with the same dimensions as the <see cref="Board"/>, where true positions mean a possible target on the board.</returns>
        public bool[,] GetAllPossibleTargets(Team t)
        {
            var allPossibleTargets = new bool[Rows, Columns];

            var queryResult = Pieces.Where(p => p.Team == t);

            foreach (var piece in queryResult)
            {
                allPossibleTargets = allPossibleTargets.Merge(piece.PossibleTargets());
            }

            return allPossibleTargets;
        }

        /// <summary>
        /// Gets whatever is in the position informed by the parameters.
        /// </summary>
        /// <param name="row">Row position in the array.</param>
        /// <param name="column">Column position in the array.</param>
        /// <returns>Whatever is in the position <see cref="Squares"/>[<paramref name="row"/>, <paramref name="column"/>].</returns>
        public Piece GetPiece(int row, int column)
        {
            return Squares[row, column];
        }

        /// <summary>
        /// Gets whatever is in the position informed by the parameters.
        /// </summary>
        /// <param name="orderedPair">The position to get its value in the board.</param>
        /// <returns>Whatever is in the position <see cref="Squares"/>[<paramref name="row"/>, <paramref name="column"/>].</returns>
        public Piece GetPiece(in TwoDimensionPosition orderedPair)
        {
            return GetPiece(orderedPair.Y, orderedPair.X);
        }

        /// <summary>
        /// Gets whatever is in the position informed by the parameter.
        /// </summary>
        /// <param name="boardPosition">The position to get its value in the board.</param>
        /// <returns>Whatever is in <paramref name="boardPosition"/> in the board.</returns>
        public Piece GetPiece(in BoardPosition boardPosition)
        {
            return GetPiece(boardPosition.ToArrayPosition(this));
        }

        /// <summary>
        /// Places a <see cref="Piece"/> in the board.
        /// </summary>
        /// <param name="piece">The piece to be placed.</param>
        /// <param name="row">The row in the array <see cref="Squares"/>, where the piece will be placed.</param>
        /// <param name="column">The column in the array <see cref="Squares"/>, where the piece will be placed.</param>
        public void PlacePiece(Piece piece, int row, int column)
        {
            Squares[row, column] = piece;
            Pieces.Add(piece);
        }

        /// <summary>
        /// Removes a piece from the board and returns it.
        /// </summary>
        /// <param name="row">The row in the array <see cref="Squares"/>, where the piece will be removed from.</param>
        /// <param name="column">The column in the array <see cref="Squares"/>, where the piece will be removed from.</param>
        /// <returns>The piece that have been removed.</returns>
        public Piece RemovePiece(int row, int column)
        {
            var piece = GetPiece(row, column);

            Squares[row, column] = null;
            Pieces.Remove(piece);

            return piece;
        }

        /// <summary>
        /// Place the pieces in their initial positions on the board.
        /// </summary>
        protected abstract void SetupBoard();
    }
}
