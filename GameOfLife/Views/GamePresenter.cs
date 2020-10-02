using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameOfLife.View
{
    /// <summary>
    /// Information of Game of Life that has to be shown to user
    /// </summary>
    public class GamePresenter
    {
        /// <summary>
        /// Shows this message when entered values are incorrect
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        private string InvalidInputValidationMessage(int minValue, int maxValue)
        {
            return $"Please enter positive numbers only from {minValue} to {maxValue}. ";
        }

        /// <summary>
        /// Occurs when press Ctrl+C
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
        /// Reviews entered values (if they are numbers from minimal value to maximum value)
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        private int ReadNumber(int minValue, int maxValue)
        {
            int.TryParse(Console.ReadLine(), out int count);

            while (count < minValue || count > maxValue)
            {
                Console.Write(InvalidInputValidationMessage(minValue, maxValue));
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
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        public int RequestCountOfWorlds(int minValue = 1, int maxValue = 1000)
        {
            Console.Write($"Enter number of worlds (from {minValue} to {maxValue}): ");
            int countOfWorlds = ReadNumber(minValue, maxValue);

            return countOfWorlds;
        }

        /// <summary>
        /// Shows message that game has saved
        /// </summary>
        internal void PrintGameSaved()
        {
            Console.WriteLine("Game saved.");
        }

        /// <summary>
        /// Shows message if there are no previously saved games
        /// </summary>
        internal void PrintNoSavedGame()
        {
            Console.WriteLine("There are no saved games. Please start new game.");
        }

        /// <summary>
        /// Requests to enter numbers of worlds (games) to be shown (up to 8 worlds)
        /// </summary>
        /// <param name="numberOfWorldsToShow">8 or less (if total number of worlds is less than 8)</param>
        /// <param name="countOfWorlds">Count of all worlds in the game</param>
        public int[] RequestNumbersOfWorldToShow(int numberOfWorldsToShow, int countOfWorlds)
        {
            Console.Write($"How many worlds You want to see (max {numberOfWorldsToShow})? ");
            int countOfWorldsToShow = ReadNumber(1, numberOfWorldsToShow);

            while (countOfWorldsToShow > numberOfWorldsToShow || countOfWorldsToShow < 1)
            {
                Console.Write($"Please enter positive numbers only from 1 to {numberOfWorldsToShow}. ");
                countOfWorldsToShow = ReadNumber(1, numberOfWorldsToShow);
            }

            List<int> numbersOfWorlds = new List<int>();

            Console.WriteLine("Every world has its own number starting from 1. ");

            for (int i = 0; i < countOfWorldsToShow; i++)
            {
                Console.Write("Enter number of the world " + $"{i + 1}" + ": ");
                int numberOfWorld = ReadNumber(1, countOfWorlds);

                while (numbersOfWorlds.Contains(numberOfWorld))
                {
                    Console.Write("This world is already selected. Please choose another one. ");
                    numberOfWorld = ReadNumber(1, countOfWorlds);
                }

                numbersOfWorlds.Add(numberOfWorld);
            }

            return numbersOfWorlds.ToArray();
        }

        /// <summary>
        /// Shows game`s options at the beginning
        /// </summary>
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
        /// Shows on screen information about the game
        /// </summary>
        /// <param name="snapshot">Game information that has to be shown on screen</param>
        public void Print(GameSnapshot snapshot)
        {
            Console.Clear();
            
            var worldSize = snapshot.WorldsToPrint.First().Info.Size;

            var strBuilder = new StringBuilder();
            foreach (var world in snapshot.WorldsToPrint)
            {
                var deadOrAlive = world.Info.IsWorldAlive ? "Alive" : "Dead";
                strBuilder.Append($"ID:{world.Info.Id} G:{world.Info.GenerationNumber} L:{world.Info.AliveCells} {deadOrAlive}".PadRight(30));
            }
            PrintLine(strBuilder.ToString(), ConsoleColor.White);

            strBuilder = new StringBuilder();
            for (int i = 0; i < worldSize.Rows; i++)
            {
                foreach (var world in snapshot.WorldsToPrint)
                {
                    var lineBuilder = new StringBuilder();
                    for (int j = 0; j < worldSize.Columns; j++)
                    {
                        if (world.Info.LifesGenerationGrid[i, j] == CellStatus.Alive)
                            lineBuilder.Append("·");
                        else
                            lineBuilder.Append(" ");
                    }
                    strBuilder.Append(lineBuilder.ToString().PadRight(30));
                }
                strBuilder.AppendLine();
            }
            PrintLine(strBuilder.ToString(), ConsoleColor.Green);

            PrintGameStatus(snapshot);
        }

        private void PrintGameStatus(GameSnapshot snapshot)
        {
            PrintLine($"Total Worlds: {snapshot.TotalWorlds, 4} Alive Worlds: {snapshot.TotalAliveWorlds,4} Lifes: {snapshot.TotalLifes,3}", ConsoleColor.White);
            Console.WriteLine($"You can stop the application by pressing Ctrl+C.");
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
        }

        public void PrintLine(string text, ConsoleColor color)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prevColor;
        }
    }
}
