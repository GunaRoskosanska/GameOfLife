using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.StartNewGame();

            Console.Write("Stop the game (Y / N)?");
            string stopTheGame = Console.ReadLine().ToUpper();

            if (stopTheGame == "Y")
            {
                return;
            }
        }
    }
}
