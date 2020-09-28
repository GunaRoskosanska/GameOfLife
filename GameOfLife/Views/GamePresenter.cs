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
        private int countOfWorld;
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

        /// <summary>
        /// Reviews entered values (if they are numbers from 1 to 1000)
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Requests to enter number of worlds (games) to execure in parallel
        /// </summary>
        public int RequestCountOfWorlds()
        {
            Console.Write("Enter number of worlds (from 1 to 1000): ");
            int countOfWorld = ReadNumber();

            return countOfWorld;
        }

        /// <summary>
        /// Requests to enter numbers of worlds (games) to show on console (up to 8 worlds)
        /// </summary>
        public int RequestNumberOfWorldToShow()
        {
            //Console.Write("Enter the numbers of the worlds you want to see (max 8 worlds, numbers sepatare by space): ");
            //int[] numberOfWorld = Array.ConvertAll(Console.ReadLine().Split(' '), (item) => Convert.ToInt32(item));

            Console.Write("How many worlds You want to see (max 8)? ");
            int countOfWorldsToShow = int.Parse(Console.ReadLine());

            int[] numbersOfWorlds = new int[countOfWorldsToShow];

            if(countOfWorldsToShow > 8 || countOfWorldsToShow < 1)
            {
                Console.Write("Please enter positive numbers only from 1 to 8.");
                RequestNumberOfWorldToShow();
            }            
            if(countOfWorldsToShow <= 8 && countOfWorldsToShow <= countOfWorld)
            {
                for(int i = 0; i < countOfWorldsToShow; i++)
                {
                    Console.Write("Enter number of the world " + i+1 + ": ");
                    numbersOfWorlds[i] = int.Parse(Console.ReadLine());
                }
            }

            return numbersOfWorlds; // ????
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
        /// <param name="worldInfo">Game information</param>
        public void Print(WorldInfo worldInfo)
        {
            var worldAliveStatus = worldInfo.IsWorldAlive ? "Alive" : "Dead";

            foreach(int numberOfWorlds in numbersOfWorlds)
            {
                if(numberOfWorlds == worldInfo.Id) // ???
                {
                    Console.WriteLine($"World ID: {worldInfo.Id,4} | Generation : {worldInfo.GenerationNumber,4} | Lives: {worldInfo.AliveCells,4} | {worldAliveStatus}");
                }
            }

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
