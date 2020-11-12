using Board;

using Extensions;

using Screen;
using static Screen.GraphicEngine;

using System;
using Chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {
            var chessMatch = new ChessMatch(new ChessBoard());

            do
            {
                Console.Clear();
                CapturedPiecesReport(chessMatch.Board.CapturedPieces);
                Console.WriteLine();

                PrintBoard(chessMatch.Board, 4);

                BoardPosition originPosition;
                do
                {
                    Console.Write("Select a piece: ");
                    try
                    {
                        originPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());

                        if (chessMatch.Board.GetPiece(originPosition) is null)
                        {
                            throw new ArgumentNullException("You cannot try to select a empty space.", (Exception)null);
                        }
                        else if (!chessMatch.Board.GetPiece(originPosition).PossibleTargets().HasTrue())
                        {
                            throw new ArgumentException($"The selected a piece {chessMatch.Board.GetPiece(originPosition)} hasn't any avaliable movement.");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        // Clears the line
                        Console.CursorTop -= 1;
                        Console.Write(new string(' ', Console.BufferWidth) + '\r');

                        Console.Write(e.Message);
                        Console.ReadKey(true);

                        // Clears the line

                        Console.CursorTop -= 1;
                        Console.Write(new string(' ', Console.BufferWidth) + '\r');

                        continue;
                    }
                    break;
                } while (true);

                var possibleTargets = chessMatch.Board.GetPiece(originPosition).PossibleTargets();

                Console.Clear();
                CapturedPiecesReport(chessMatch.Board.CapturedPieces);
                Console.WriteLine();

                PrintBoard(chessMatch.Board, possibleTargets, 4);
                Console.WriteLine($"Select a piece: {originPosition}");

                BoardPosition targetPosition;
                do
                {
                    Console.Write("Select the target: ");
                    try
                    {
                        targetPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());

                        if (!UserInput.IsValidBoardPosition(targetPosition, chessMatch.Board))
                        {
                            throw new ArgumentException("The informed position doesn't belong to this board.");
                        }

                        var targetArrayPosition = targetPosition.ToArrayPosition(chessMatch.Board);

                        if (!possibleTargets[targetArrayPosition.Y, targetArrayPosition.X])
                        {
                            throw new ArgumentException("The informed position isn't a possible target for the selected piece.");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        // Clears the line
                        Console.CursorTop -= 1;
                        Console.Write(new string(' ', Console.BufferWidth) + '\r');

                        Console.Write(e.Message);
                        Console.ReadKey(true);

                        // Clears the line
                        Console.CursorTop -= 1;
                        Console.Write(new string(' ', Console.BufferWidth) + '\r');

                        continue;
                    }
                    break;
                } while (true);

                chessMatch.Board.MovePiece(originPosition, targetPosition);
            } while (true);

        }
    }
}
