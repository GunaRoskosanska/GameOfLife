using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            StartGameAndLoop game = new StartGameAndLoop();
            game.StartNewGame();

            Console.ReadKey();

            // Add possibility to save information to file and restore it on application start
        }
    }
}
