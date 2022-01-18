using Chess;

using System;
using System.IO;

namespace ConsoleChess
{
    class Program
    {
        static void Main()
        {
            try
            {
                var chessGame = new ChessMatch();
                chessGame.Start();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                Console.WriteLine("Oops! Looks like someone has checkmate the program. This error will be logged. Sorry, but your game had to be stopped.");

                try
                {
                    var now = DateTime.UtcNow;
                    using var sw = new StreamWriter(Path.Combine(Path.GetTempPath(), $"consolechess-{now.Ticks}.log"), true);
                    sw.WriteLine($"{now:O} - consolechess - CRITICAL - {ex.ToString().Replace(Environment.NewLine, " ")}");
                }
                catch (Exception) { }

                Console.ReadKey(true);
            }
        }
    }
}
