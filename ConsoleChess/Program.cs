using Board;

using Pieces;

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

                        if (gameBoard.GetPiece(originPosition) == null)
                        {
                            throw new ArgumentNullException("You cannot try to select a empty space.", (Exception)null);
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

                BoardPosition targetPosition;
                do
                {
                    Console.Write("Select the target: ");
                    try
                    {
                        targetPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());

                        if (!UserInput.IsValidBoardPosition(targetPosition, gameBoard))
                        {
                            throw new ArgumentException("The position informed doesn't belong to this board.");
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
