using Board;
using static Screen.GraphicEngine;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {
            var gameBoard = new ChessBoard();

            PrintBoard(gameBoard, 4);
        }
    }
}
