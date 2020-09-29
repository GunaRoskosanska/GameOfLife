﻿using GameOfLife.Models;
using System;
using System.Text;

namespace GameOfLife.View
{
    /// <summary>
    /// Information of Game of Life that has to be shown to user
    /// </summary>
    public class GamePresenter
    {
        private const int MinValue = 1;
        private const int MaxValue = 1000;
        private const string IncorrectEnteredNumberAnnouncement = "Please enter positive numbers only from 1 to 1000. ";

        /// <summary>
        /// Occurs when Ctrl+C
        /// </summary>
        public event Action CancelKeyPress = delegate { };

        /// <summary>
        /// Game presenter constructor
        /// </summary>
        public GamePresenter()
        {
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                CancelKeyPress?.Invoke();
            };
        }

        /// <summary>
        /// Requests to enter number of rows and columns for the game
        /// </summary>
        public WorldSize RequestWorldSize(int minValue = 1, int maxValue = 100)
        {
            Console.Write($"Enter number of rows (from {minValue} to {maxValue}): ");
            int rows = ReadNumber(minValue, maxValue);

            Console.Write($"Enter number of columns (from {minValue} to {maxValue}): ");
            var columns = ReadNumber(minValue, maxValue);

            return new WorldSize
            {
                Columns = columns,
                Rows = rows
            };
        }

        /// <summary>
        /// Reviews entered values (if they are numbers from 1 to 1000)
        /// </summary>
        /// <returns></returns>
        private static int ReadNumber(int minValue, int maxValue)
        {
            int.TryParse(Console.ReadLine(), out int count);

            while (count < minValue || count > maxValue)
            {
                Console.Write(IncorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out count);
            }

            return count;
        }

        /// <summary>
        /// Requests to choose game options (new game, continue previous game, exit)
        /// </summary>
        public GameOption RequestGameOption()
        {
            while (true)
            {
                PrintGameMenu();
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        return GameOption.NewGame;
                    case "2":
                        return GameOption.ContinuePreviousGame;
                    case "3":
                        return GameOption.Exit;
                    default:
                        Console.WriteLine("Please choose one of the options above. Enter any key...");
                        Console.ReadKey(false);
                        break;
                }
            }
        }

        /// <summary>
        /// Requests to enter number of worlds (games) to execure in parallel
        /// </summary>
        public int RequestCountOfWorlds(int minValue = 1, int maxValue = 1000)
        {
            Console.Write($"Enter number of worlds (from {minValue} to {maxValue}): ");
            int countOfWorld = ReadNumber(minValue, maxValue);

            return countOfWorld;
        }

        internal void PrintGameSaved()
        {
            Console.WriteLine("Game saved.");
        }

        /// <summary>
        /// Requests to enter numbers of worlds (games) to be shown (up to 8 worlds)
        /// </summary
        public int[] RequestNumbersOfWorldToShow(int numberOfWorlds)
        {
            Console.Write($"How many worlds You want to see (max {numberOfWorlds})? ");
            int countOfWorldsToShow = ReadNumber(1, numberOfWorlds);

            while (countOfWorldsToShow > numberOfWorlds || countOfWorldsToShow < 1)
            {
                Console.Write($"Please enter positive numbers only from 1 to {numberOfWorlds}. ");
                countOfWorldsToShow = ReadNumber(1, numberOfWorlds);
            }

            int[] numbersOfWorlds = new int[countOfWorldsToShow];

            Console.WriteLine("Every world has its own number starting from 1. ");

            for (int i = 0; i < countOfWorldsToShow; i++)
            { 
                Console.Write("Enter number of the world " + $"{i+1}" + ": ");
                numbersOfWorlds[i] = ReadNumber(1, numberOfWorlds);
            }

            return numbersOfWorlds;
        }

        // Shows game`s options
        public void PrintGameMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game menu:");
            Console.WriteLine("1 - Start New Game");
            Console.WriteLine("2 - Continue Previous Game");
            Console.WriteLine("3 - Exit");
        }

        /// <summary>
        /// Shows game`s options after pause
        /// </summary>
        public void PrintGameSecondMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game menu:");
            Console.WriteLine("1 - Start New Game");
            Console.WriteLine("2 - Continue Previous Game");
            Console.WriteLine("3 - Exit");
            Console.WriteLine("4 - Save game");
            Console.WriteLine("5 - Change selected worlds on the screen");
        }

        /// <summary>
        /// Requests game`s options after pause (Ctrl+C)
        /// </summary>
        public GameOption RequestGamePauseOption()
        {
            while (true)
            {
                PrintGameSecondMenu();
                var gameOption = Console.ReadLine();
                switch (gameOption)
                {
                    case "1":
                        return GameOption.NewGame;
                    case "2":
                        return GameOption.ContinuePreviousGame;
                    case "3":
                        return GameOption.Exit;
                    case "4":
                        return GameOption.SaveGame;
                    case "5":
                        return GameOption.ChangeWorldsOnScreen;
                    default:
                        Console.WriteLine("Please choose one of the options above. Enter any key...");
                        Console.ReadKey(false);
                        break;
                }
            }
        }

        /// <summary>
        /// Shows on screen state of game
        /// </summary>
        /// <param name="worldInfo">Game information</param>
        public void Print(WorldInfo worldInfo)
        {
            var worldAliveStatus = worldInfo.IsWorldAlive ? "Alive" : "Dead";
            Console.WriteLine($"World ID: {worldInfo.Id,4} | Generation : {worldInfo.GenerationNumber,4} | Lives: {worldInfo.AliveCells,4} | {worldAliveStatus}");

            return;
            var cellStatuses = worldInfo.LifesGenerationGrid;
            int aliveCells = worldInfo.AliveCells;
            int generationNumber = worldInfo.GenerationNumber;

            int rows = cellStatuses.GetUpperBound(0) + 1;
            int columns = cellStatuses.Length / rows;

            var stringBuilder = new StringBuilder();

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var cell = cellStatuses[row, column];
                    stringBuilder.Append(cell == CellStatus.Alive ? "@" : " ");
                }
                stringBuilder.AppendLine();
            }

            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Generation #{generationNumber} | Count of live cells: {aliveCells}");
            Console.WriteLine($"You can stop the application by pressing Ctrl+C.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(stringBuilder.ToString());
        }
    }
}
