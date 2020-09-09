using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartNewGame();

            Console.ReadKey();

            // Add count of iteration
            // Add possibility to save information to file and restore it on application start
            // Add Possibility to stop application in any moment
        }
    }
}
