using Board;

using Extensions;

using Screen;
using static Screen.GraphicEngine;

using System;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {
            var gameBoard = new ChessBoard();

            do
            {
                Console.Clear();
                PrintBoard(gameBoard, 4);

                BoardPosition originPosition;
                do
                {
                    Console.Write("Select a piece: ");
                    try
                    {
                        originPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());

                        if (gameBoard.GetPiece(originPosition) is null)
                        {
                            throw new ArgumentNullException("You cannot try to select a empty space.", (Exception)null);
                        }
                        else if (!gameBoard.GetPiece(originPosition).PossibleTargets().HasTrue())
                        {
                            throw new ArgumentException($"The selected a piece {gameBoard.GetPiece(originPosition)} hasn't any avaliable movement.");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        // Clears the line
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.Write(e.Message);
                        Console.ReadKey(true);

                        // Clears the line
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        continue;
                    }
                    break;
                } while (true);

                var possibleTargets = gameBoard.GetPiece(originPosition).PossibleTargets();

                Console.Clear();
                PrintBoard(gameBoard, possibleTargets, 4);
                Console.WriteLine($"Select a piece: {originPosition}");
                BoardPosition targetPosition;
                do
                {
                    Console.Write("Select the target: ");
                    try
                    {
                        targetPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());

                        if (!UserInput.IsValidBoardPosition(targetPosition, gameBoard))
                        {
                            throw new ArgumentException("The informed position doesn't belong to this board.");
                        }

                        var targetArrayPosition = targetPosition.ToArrayPosition(gameBoard);

                        if (!possibleTargets[targetArrayPosition.Y, targetArrayPosition.X])
                        {
                            throw new ArgumentException("The informed position isn't a possible target for the selected piece.");
                        }
                    }
                    catch (ArgumentException e)
                    {
                        // Clears the line
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        Console.Write(e.Message);
                        Console.ReadKey(true);

                        // Clears the line
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.Write(new string(' ', Console.BufferWidth));
                        Console.SetCursorPosition(0, Console.CursorTop);

                        continue;
                    }
                    break;
                } while (true);

                gameBoard.MovePiece(originPosition, targetPosition);
            } while (true);

        }
    }
}
