using Board;
using Board.Enums;

using System;
using System.Collections.Generic;

namespace Chess
{
    /// <summary>
    /// Contains methods that take a <paramref name="p"/>-piece and returns its possible movements on the board according to the method rule.
    /// </summary>
    /// <param name="p">The p-piece to get its possible movements.</param>
    /// <param name="board">The board where the piece is.</param>
    /// <returns>An array of Booleans possible targets for the <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
    delegate bool[,] Movement(Piece p, Board.Board board);

    /// <summary>
    /// Contains all types of movement of the pieces.
    /// </summary>
    static class PieceMovement
    {
        /// <summary>
        /// Returns an array of Booleans with possible targets for a straight-moving <see cref="Piece"/>.
        /// </summary>
        /// <param name="p">The straight-moving piece.</param>
        /// <param name="board">The board where the piece is.</param>
        /// <returns>An array of Booleans with possible targets for the straight-moving <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
        public static bool[,] StraightMove(Piece p, Board.Board board)
        {
            var possibleMovements = new bool[board.Rows, board.Columns];
            var position = p.GetPosition();

            #region Top
            // Checks the top of the piece:
            //  [...]
            //    1
            //    0
            //    P
            for (int row = position.Y - 1; row >= 0; row--)
            {
                var watchedTarget = board.GetPiece(row, position.X);

                if (watchedTarget is null)
                {
                    possibleMovements[row, position.X] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, position.X] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Right
            // Checks the right side of the piece:
            //  P01[...]
            for (int column = position.X + 1; column < board.Columns; column++)
            {
                var watchedTarget = board.GetPiece(position.Y, column);

                if (watchedTarget is null)
                {
                    possibleMovements[position.Y, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[position.Y, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Bottom
            // Checks the bottom side of the piece:
            //    P
            //    0
            //    1
            //  [...]
            for (int row = position.Y + 1; row < board.Rows; row++)
            {
                var watchedTarget = board.GetPiece(row, position.X);

                if (watchedTarget is null)
                {
                    possibleMovements[row, position.X] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, position.X] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Left
            // Checks the left side of the piece:
            //  [...]10P
            for (int column = position.X - 1; column >= 0; column--)
            {
                var watchedTarget = board.GetPiece(position.Y, column);

                if (watchedTarget is null)
                {
                    possibleMovements[position.Y, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[position.Y, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            return possibleMovements;
        }

        /// <summary> 
        /// Returns an array of Booleans with possible targets for a diagonal-moving <see cref="Piece"/>.
        /// </summary>
        /// <param name="p">The diagonal-moving piece.</param>
        /// <param name="board">The board where the piece is.</param>
        /// <returns>An array of Booleans with possible targets for the diagonal-moving <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
        public static bool[,] DiagonalMove(Piece p, Board.Board board)
        {
            var possibleMovements = new bool[board.Rows, board.Columns];
            var position = p.GetPosition();

            #region Top right
            // Checks the top right diagonal of the piece:
            //      [..]
            //      1
            //     0
            //    P
            for (int row = position.Y - 1, column = position.X + 1; row >= 0 && column < board.Columns; row--, column++)
            {
                var watchedTarget = board.GetPiece(row, column);

                if (watchedTarget is null)
                {
                    possibleMovements[row, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Bottom right
            // Checks the bottom right diagonal of the piece:
            //    P
            //     0
            //      1
            //      [..]
            for (int row = position.Y + 1, column = position.X + 1; row < board.Rows && column < board.Columns; row++, column++)
            {
                var watchedTarget = board.GetPiece(row, column);

                if (watchedTarget is null)
                {
                    possibleMovements[row, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Bottom left
            // Checks the bottom left diagonal of the piece:
            //         P
            //        0
            //       1
            //    [..]
            for (int row = position.Y + 1, column = position.X - 1; row < board.Rows && column >= 0; row++, column--)
            {
                var watchedTarget = board.GetPiece(row, column);

                if (watchedTarget is null)
                {
                    possibleMovements[row, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            #region Top left
            // Checks the top left diagonal of the piece:
            //    [..]
            //       1
            //        0
            //         P
            for (int row = position.Y - 1, column = position.X - 1; row >= 0 && column >= 0; row--, column--)
            {
                var watchedTarget = board.GetPiece(row, column);

                if (watchedTarget is null)
                {
                    possibleMovements[row, column] = true;
                }
                else if (watchedTarget.Team != p.Team)
                {
                    possibleMovements[row, column] = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            #endregion

            return possibleMovements;
        }

        /// <summary> 
        /// Returns an array of Booleans with possible targets for a 'one square around moving' <see cref="Piece"/>.
        /// </summary>
        /// <param name="p">The 'one square around moving' piece.</param>
        /// <param name="board">The board where the piece is.</param>
        /// <returns>An array of Booleans with possible targets for the <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
        public static bool[,] OneSquareAroundMove(Piece p, Board.Board board)
        {
            var possibleMovements = new bool[board.Rows, board.Columns];
            var position = p.GetPosition();

            #region One Square Around
            // 012
            // 3P5
            // 678
            for (int row = position.Y - 1; row <= position.Y + 1; row++)
            {
                if (row < 0 || row >= board.Rows)
                {
                    continue;
                }

                for (int column = position.X - 1; column <= position.X + 1; column++)
                {
                    if (column < 0 || column >= board.Columns)
                    {
                        continue;
                    }

                    var watchedTarget = board.GetPiece(row, column);

                    if (watchedTarget is null || watchedTarget.Team != p.Team)
                    {
                        possibleMovements[row, column] = true;
                    }
                }
            }
            #endregion

            return possibleMovements;
        }

        /// <summary> 
        /// Returns an array of Booleans with possible targets for a forward-moving <see cref="Piece"/>.
        /// </summary>
        /// <remarks>
        /// This movement consists of:
        /// <list type="bullet">
        /// <item>Both front diagonals.</item>
        /// <description>When the capture is available.</description>
        /// <item>A step forward.</item>
        /// <description>If the front board square is empty.</description>
        /// <item>Two steps forward.</item>
        /// <description>When both first step and second step are empty and it is the first movement of the piece.</description>
        /// </list>
        /// </remarks>
        /// <param name="p">The forward-moving piece.</param>
        /// <param name="board">The board where the piece is.</param>
        /// <returns>An array of Booleans with possible targets for the forward-moving <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
        public static bool[,] ForwardMove(Piece p, Board.Board board)
        {
            var possibleMovements = new bool[board.Rows, board.Columns];
            var position = p.GetPosition();

            #region Direction Modifier
            /* Set the directionModifier, it'll say which direction to follow
             * in the Piece.Board array, e.g.:
             *  - A forward-moving Piece which is from the top of the board must
             *    always move positively along the Piece.Board array rows.
             *  - On the other hand, a forward-moving piece from the bottom of the
             *    board always will move negatively along the Piece.Board array rows.
             */
            var directionModifier = p.Side switch
            {
                BoardSide.Top => 1,
                BoardSide.Bottom => -1,
                _ => throw new NotImplementedException("The Piece.Side is invalid.")
            };
            #endregion

            #region Front Diagonals
            // 0 1
            //  P
            TwoDimensionPosition watchedPosition;
            for (int columnModifier = 0; columnModifier <= 2; columnModifier += 2)
            {
                watchedPosition = new TwoDimensionPosition(position.X - 1 + columnModifier, position.Y + directionModifier);
                if (watchedPosition.X >= 0
                    && watchedPosition.X < board.Columns
                    && watchedPosition.Y >= 0
                    && watchedPosition.Y < board.Rows)
                {
                    var watchedPiece = board.GetPiece(watchedPosition);
                    if (!(watchedPiece is null) && watchedPiece.Team != p.Team)
                    {
                        possibleMovements[watchedPosition.Y, watchedPosition.X] = true;
                    }
                }

            }
            #endregion

            #region First step and Second Step Forward
            //  2
            //  P
            watchedPosition = new TwoDimensionPosition(position.X, position.Y + directionModifier);
            if (watchedPosition.Y >= 0
                && watchedPosition.Y < board.Rows
                && board.GetPiece(watchedPosition) is null)
            {
                possibleMovements[watchedPosition.Y, watchedPosition.X] = true;

                //  3
                //
                //  P
                watchedPosition = new TwoDimensionPosition(watchedPosition.X, watchedPosition.Y + directionModifier);
                if (watchedPosition.Y >= 0
                    && watchedPosition.Y < board.Rows
                    && p.TimesMoved == 0
                    && board.GetPiece(watchedPosition.Y, watchedPosition.X) is null)
                {
                    possibleMovements[watchedPosition.Y, watchedPosition.X] = true;
                }
            }

            #endregion

            return possibleMovements;
        }

        /// <summary> 
        /// Returns an array of Booleans with possible targets for a "L-shape"-moving <see cref="Piece"/>.
        /// </summary>
        /// <remarks>
        /// This movement consists of moving the piece two squares vertically and then one square horizontally (or vice versa).
        /// </remarks>
        /// <param name="p">The "L-shape"-moving piece.</param>
        /// <param name="board">The board where the piece is.</param>
        /// <returns>An array of Booleans with possible targets for the "L-shape"-moving <paramref name="p"/>-piece, where true means a valid target and false means an invalid target.</returns>
        public static bool[,] LShapeMove(Piece p, Board.Board board)
        {
            var possibleMovements = new bool[board.Rows, board.Columns];
            var position = p.GetPosition();

            #region Gathering Possible Targets
            /*   7 0
             *  6   1
             *    P
             *  5   2
             *   4 3
             */
            var targetsSet = new HashSet<TwoDimensionPosition>
            {
                new TwoDimensionPosition(position.X + 1, position.Y - 2), // 0
                new TwoDimensionPosition(position.X + 2, position.Y - 1), // 1
                new TwoDimensionPosition(position.X + 2, position.Y + 1), // 2
                new TwoDimensionPosition(position.X + 1, position.Y + 2), // 3
                new TwoDimensionPosition(position.X - 1, position.Y + 2), // 4
                new TwoDimensionPosition(position.X - 2, position.Y + 1), // 5
                new TwoDimensionPosition(position.X - 2, position.Y - 1), // 6
                new TwoDimensionPosition(position.X - 1, position.Y - 2) // 7
            };
            #endregion

            #region Filtering Targets
            foreach (var target in targetsSet)
            {
                if (target.Y >= 0 && target.Y < board.Rows && target.X >= 0 && target.X < board.Columns)
                {
                    var watchedPiece = board.GetPiece(target);
                    if (watchedPiece is null || watchedPiece.Team != p.Team)
                    {
                        possibleMovements[target.Y, target.X] = true;
                    }
                }
            }
            #endregion

            return possibleMovements;
        }
    }
}
