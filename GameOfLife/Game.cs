using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Game
    {
        GameViewer gameViewer;
        CellStatusGenerationManager cellStatusGeneration;
        const int MinValue = 1;
        int Rows;
        int Columns;
        bool IsRuning;

        public void StartNewGame()
        {
            Console.Write("Enter number of rows: ");
            int.TryParse(Console.ReadLine(), out Rows);

            while (Rows < MinValue)
            {
                Console.Write("Please enter positive numbers only. ");
                int.TryParse(Console.ReadLine(), out Rows);
            }

            Console.Write("Enter number of columns: ");
            int.TryParse(Console.ReadLine(), out Columns);

            while (Columns < MinValue)
            {
                Console.Write("Please enter positive numbers only. ");
                int.TryParse(Console.ReadLine(), out Columns);
            }

            cellStatusGeneration = new CellStatusGenerationManager(Rows, Columns);
            gameViewer = new GameViewer();

            var grid = new CellStatus[Rows, Columns];

            // Randomly initialize grid
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    grid[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                IsRuning = false;
                Console.WriteLine("\n Ending game.");
            };

            Console.Clear();

            // Displaying the grid
            IsRuning = true;

            while (IsRuning)
            {
                gameViewer.Print(grid);
                grid = cellStatusGeneration.NextGeneration(grid);
            }
        }
    }
}
