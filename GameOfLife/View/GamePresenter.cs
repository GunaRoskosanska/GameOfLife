using GameOfLife.Model;
using System;
using System.Text;

namespace GameOfLife.View
{
    public class GamePresenter
    {
        private const int MinValue = 1;
        private const int MaxValue = 50;
        private const string IncorrectEnteredNumberAnnouncement = "Please enter positive numbers only from 1 to 50. ";

        /// <summary>
        /// Requests to enter number of rows and columns for the game
        /// </summary>
        public GridSize RequestGridDimensions()
        {
            Console.Write("Enter number of rows (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out var rows);

            while (rows < MinValue || rows > MaxValue)
            {
                Console.Write(IncorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out rows);
            }

            Console.Write("Enter number of columns (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out var columns);

            while (columns < MinValue || columns > MaxValue)
            {
                Console.Write(IncorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out columns);
            }

            return new GridSize
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

        // Shows game options
        private void PrintGameMenu()
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
        public void Print(GameInfo gameInfo)
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
