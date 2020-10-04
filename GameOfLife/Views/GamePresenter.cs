using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace GameOfLife.View
{
    /// <summary>
    /// Information of Game of Life that has to be shown to user
    /// </summary>
    public class GamePresenter
    {
        private const char AliveCell = '·';
        private const char DeadCell = ' ';
        private const int Shift = 25;

        /// <summary>
        /// Method to be invoked when press Ctrl+C
        /// </summary>
        public event Action PauseRequested = delegate { };

        /// <summary>
        /// Initializes a new instance of the GamePresenter.
        /// </summary>
        public GamePresenter()
        {
            //Subscribes on Ctrl+C event
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                PauseRequested?.Invoke();
            };
        }

        /// <summary>
        /// Requests to enter number of rows and columns for the game
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        public WorldSize RequestWorldSize(int minValue = 10, int maxValue = 20)
        {
            Print($"Enter number of rows (from {minValue} to {maxValue}): ");
            int rows = ReadNumber(minValue, maxValue);

            Print($"Enter number of columns (from {minValue} to {maxValue}): ");
            var columns = ReadNumber(minValue, maxValue);

            return new WorldSize
            {
                Columns = columns,
                Rows = rows
            };
        }

        /// <summary>
        /// Requests to choose game options (new game, continue previous game, exit)
        /// </summary>
        public GameOption RequestGameOption()
        {
            while (true)
            {
                PrintMenu();
                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        return GameOption.NewGame;
                    case "2":
                        return GameOption.LoadGame;
                    case "3":
                        return GameOption.Exit;
                    default:
                        PrintLine("Please choose one of the options above. Enter any key...");
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
        public int RequestCountOfWorlds(int minValue = 1, int maxValue = 1000)
        {
            Print($"Enter number of worlds (from {minValue} to {maxValue}): ");
            int countOfWorlds = ReadNumber(minValue, maxValue);

            return countOfWorlds;
        }

        /// <summary>
        /// Shows message that game has saved
        /// </summary>
        public void PrintGameSaved()
        {
            PrintLine("Game saved successfully.");
            System.Threading.Thread.Sleep(1000); //short delay for displaying information
        }

        /// <summary>
        /// Shows message if there are no previously saved games
        /// </summary>
        internal void PrintNoSavedGame()
        {
            PrintLine("There are no saved games. Please start new game.");
        }

        /// <summary>
        /// Requests to enter numbers of worlds (games) to be shown (up to 8 worlds)
        /// </summary>
        /// <param name="numberOfWorldsToShow">8 or less (if total number of worlds is less than 8)</param>
        /// <param name="countOfWorlds">Count of all worlds in the game</param>
        public int[] RequestNumbersOfWorldToShow(int numberOfWorldsToShow, int countOfWorlds)
        {
            Print($"How many worlds You want to see (max {numberOfWorldsToShow})? ");
            int countOfWorldsToShow = ReadNumber(1, numberOfWorldsToShow);

            while (countOfWorldsToShow > numberOfWorldsToShow || countOfWorldsToShow < 1)
            {
                Print($"Please enter positive numbers only from 1 to {numberOfWorldsToShow}. ");
                countOfWorldsToShow = ReadNumber(1, numberOfWorldsToShow);
            }

            List<int> numbersOfWorlds = new List<int>();

            PrintLine("Every world has its own number starting from 1. ");

            for (int i = 0; i < countOfWorldsToShow; i++)
            {
                Print("Enter number of the world " + $"{i + 1}" + ": ");
                int numberOfWorld = ReadNumber(1, countOfWorlds);

                while (numbersOfWorlds.Contains(numberOfWorld))
                {
                    Print("This world is already selected. Please choose another one. ");
                    numberOfWorld = ReadNumber(1, countOfWorlds);
                }

                numbersOfWorlds.Add(numberOfWorld);
            }

            return numbersOfWorlds.ToArray();
        }

        /// <summary>
        /// Displays on console information about the game.
        /// </summary>
        /// <param name="snapshot">Game information that has to be shown on screen</param>
        public void Print(GameSnapshot snapshot)
        {
            Console.Clear();

            var displayWorlds = snapshot.DisplayWorlds.Select(x => snapshot.Worlds[x - 1]).ToArray();

            var left = 0;
            var top = 0;

            foreach (var world in displayWorlds)
            {
                Print(world, new Point(left, top));
                left += Shift;
            }

            Console.SetCursorPosition(0, Shift);
            PrintLine($"Total Worlds: {snapshot.TotalWorlds,4} Alive Worlds: {snapshot.TotalAliveWorlds,4} Lifes: {snapshot.TotalLifes,3}", ConsoleColor.White);
            PrintLine($"You can pause the game by pressing Ctrl+C.");
        }
        /// <summary>
        /// Displays on the console information about world at the specified position.
        /// </summary>
        /// <param name="world">World information about which we want to display.</param>
        /// <param name="position">Position.</param>
        private void Print(World world, Point position)
        {
            var left = position.X;
            var top = position.Y;
            
            Console.SetCursorPosition(left, top);
            var deadOrAlive = world.IsAlive ? "Alive" : "Dead";
            Print($"ID:{world.Id} G:{world.GenerationNumber} L:{world.AliveCells} {deadOrAlive}".PadRight(Shift));
            top++;

            for (int i = 0; i < world.Size.Rows; i++)
            {
                var stringBuilder = new StringBuilder();
                for (int j = 0; j < world.Size.Columns; j++)
                {
                    if (world.Generation[i, j] == CellStatus.Alive)
                    {
                        stringBuilder.Append(AliveCell);
                    }
                    else
                    {
                        stringBuilder.Append(DeadCell);
                    }
                }

                Console.SetCursorPosition(left, top);
                Print(stringBuilder.ToString().PadRight(Shift), world.IsAlive ? ConsoleColor.Green : ConsoleColor.Red);
                top++;
            }
        }
        /// <summary>
        /// Requests game`s options after pause (Ctrl+C)
        /// </summary>
        public GameOption RequestGamePauseOption()
        {
            while (true)
            {
                PrintPauseMenu();
                var gameOption = Console.ReadLine();
                switch (gameOption)
                {
                    case "1":
                        return GameOption.NewGame;
                    case "2":
                        return GameOption.ContinueGame;
                    case "3":
                        return GameOption.ChangeWorlds;
                    case "4":
                        return GameOption.SaveGame;
                    case "5":
                        return GameOption.Exit;
                    default:
                        PrintLine("Please choose one of the options above. Enter any key...");
                        Console.ReadKey(false);
                        break;
                }
            }
        }
        /// <summary>
        /// Prints specific string value with defined color, followed by the current line terminator
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="color">Color</param>
        public void PrintLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prevColor;
        }

        /// <summary>
        /// Prints specific string value with defined color
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="color">Color</param>
        public void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = prevColor;
        }

        /// <summary>
        /// Displays game menu.
        /// </summary>
        private void PrintMenu()
        {
            Console.Clear();
            PrintLine("Game menu:");
            PrintLine("1 - Start new game");
            PrintLine("2 - Load Game");
            PrintLine("3 - Exit");
        }

        /// <summary>
        /// Displays game pause menu.
        /// </summary>
        private void PrintPauseMenu()
        {
            Console.Clear();
            PrintLine("Game menu:");
            PrintLine("1 - Start new game");
            PrintLine("2 - Continue game");
            PrintLine("3 - Change displayed worlds");
            PrintLine("4 - Save game");
            PrintLine("5 - Exit");
        }

        /// <summary>
        /// Reviews entered values (if they are numbers from minimal value to maximum value)
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        private int ReadNumber(int minValue, int maxValue)
        {
            int.TryParse(Console.ReadLine(), out int count);

            while (count < minValue || count > maxValue)
            {
                Print(InvalidInputValidationMessage(minValue, maxValue));
                int.TryParse(Console.ReadLine(), out count);
            }

            return count;
        }

        /// <summary>
        /// Shows this message when entered values are incorrect
        /// </summary>
        /// <param name="minValue">Minimal value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns></returns>
        private string InvalidInputValidationMessage(int minValue, int maxValue)
        {
            return $"Please enter numbers only from {minValue} to {maxValue}. ";
        }
    }
}
