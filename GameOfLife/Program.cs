using System;
using System.Threading;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Logic.GameOfLife();
            game.StartNewGame();

            while (game.IsRunning)
            {
                Thread.Sleep(millisecondsTimeout: 1500);
            }
        }
    }
}
