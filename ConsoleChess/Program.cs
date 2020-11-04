using Board;

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

                BoardPosition boardPosition;
                Console.Write("Type a board position: ");
                try
                {
                    boardPosition = UserInput.ConvertToBoardPosition(Console.ReadLine());
                }
                catch (ArgumentException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadKey(true);
                    continue;
                }


                Console.WriteLine();
                if (UserInput.IsValidBoardPosition(boardPosition, gameBoard))
                {
                    Console.WriteLine("You've typed the board position: " + boardPosition);
                    Console.Write("Which represents the position [{0}, {1}] in the game board's based-zero array.",
                        boardPosition.ToArrayPosition(gameBoard).X,
                        boardPosition.ToArrayPosition(gameBoard).Y);
                }
                else
                {
                    Console.WriteLine("You've type an invalid board position.");
                }
                Console.ReadKey(true);
            } while (true);

        }
    }
}
