using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class Game
    {
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
                Print(grid);
                grid = NextGeneration(grid);
            }
        }

        private CellStatus[,] NextGeneration(CellStatus[,] currentGrid)
        {
            var nextGeneration = new CellStatus[Rows, Columns];

            // Loop through every cell
            for (var row = 1; row < Rows - 1; row++)
            for (var column = 1; column < Columns - 1; column++)
            {
                // Find alive neighbors
                var aliveNeighbors = 0;
                for (var i = -1; i <= 1; i++)
                {
                    for (var j = -1; j <= 1; j++)
                    {
                        aliveNeighbors += currentGrid[row + i, column + j] == CellStatus.Alive ? 1 : 0;
                    }
                }
                var currentCell = currentGrid[row, column];

                // The cell need to be removed from its neighbors
                // as it was counted before
                aliveNeighbors -= currentCell == CellStatus.Alive ? 1 : 0;

                // Implementing the rules of life

                // Cell is lonely and dies
                if (currentCell == CellStatus.Alive && aliveNeighbors < 2)
                {
                    nextGeneration[row, column] = CellStatus.Dead;
                }

                // Cell dies due to over population
                else if (currentCell == CellStatus.Alive && aliveNeighbors > 3)
                {
                    nextGeneration[row, column] = CellStatus.Dead;
                }

                // A new cell is born
                else if (currentCell == CellStatus.Dead && aliveNeighbors == 3)
                {
                    nextGeneration[row, column] = CellStatus.Alive;
                }

                // Stays the same
                else
                {
                    nextGeneration[row, column] = currentCell;
                }
            }

            return nextGeneration;
        }

        // The Print method builds a single string then writes to the console by repositioning the cursor
        private void Print(CellStatus[,] future, int timeout = 1000)
        {
            var stringBuilder = new StringBuilder();

            int aliveCellsCount = 0;

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    
                    var cell = future[row, column];
                    stringBuilder.Append(cell == CellStatus.Alive ? "@" : " ");

                    if (cell == CellStatus.Alive)
                    {
                        aliveCellsCount++;
                    }
                }
                stringBuilder.Append("\n");
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Console.WriteLine($"Count of live cells: {aliveCellsCount}");
            Thread.Sleep(timeout);
        }
    }
}
