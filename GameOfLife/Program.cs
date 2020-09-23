using GameOfLife.Models;
using System;

namespace GameOfLife
{
    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Logic.GameOfLife();
            game.StartNewGame();

            //while (true)
            //{
            //    Console.Write("Would You like to Start New Game (1), Continue Previous Game (2) or Exit (3)?");
            //    var gameNextOption = Console.ReadLine();

            //    switch (gameNextOption)
            //    {
            //        case "1":
            //            game.StartNewGame();
            //            break;
            //        case "2":
            //            game.ContinuePreviousGame();
            //            break;
            //        case "3":
            //            return;
            //        default:
            //            Console.WriteLine("Please choose one of the options above. Enter any key...");
            //            //Console.ReadKey(false);
            //            break;
            //    }
            //}
        }
    }
}
