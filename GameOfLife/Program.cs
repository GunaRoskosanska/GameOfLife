using System;

namespace GameOfLife
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameOfLife game = new GameOfLife();
            game.StartNewGame();

            Console.ReadKey();

            // TODO: Add possibility to save information to file and restore it on application start
        }
    }
}
