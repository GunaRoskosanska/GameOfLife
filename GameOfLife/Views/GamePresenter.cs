using GameOfLife.Models;
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
        public WorldSize RequestWorldSize()
        {
            Console.Write("Enter number of rows (from 1 to 1000): ");
            int rows = ReadNumber();

            Console.Write("Enter number of columns (from 1 to 1000): ");
            var columns = ReadNumber();

            return new WorldSize
            {
                Columns = columns,
                Rows = rows
            };
        }

        private static int ReadNumber()
        {
            int.TryParse(Console.ReadLine(), out int count);

            while (count < MinValue || count > MaxValue)
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

        public int RequestNumberOfWorlds()
        {
            Console.Write("Enter number of worlds (from 1 to 1000): ");
            int numberOfWorld = ReadNumber();

            return numberOfWorld;
        }

        // Shows game options
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
        /// Shows on screen state of game
        /// </summary>
        /// <param name="gameInfo">Game information</param>
        public void Print(WorldInfo gameInfo)
        {
            var cellStatuses = gameInfo.LifesGenerationGrid;
            int aliveCells = gameInfo.AliveCells;
            int generationNumber = gameInfo.GenerationNumber;

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
