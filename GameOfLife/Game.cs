using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static GameOfLife.Cell;

namespace GameOfLife
{
    class Game
    {
        int Rows;
        int Columns;
        bool IsRuning;
        Status[,] grid;

        public void StartNewGame()
        {
            Console.Write("Enter number of rows: ");
            Rows = int.Parse(Console.ReadLine());

            Console.Write("Enter number of columns: ");
            Columns = int.Parse(Console.ReadLine());

            var grid = new Status[Rows, Columns];

            // randomly initialize grid
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    grid[row, column] = (Status)RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            // to stop the game
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

        private Status[,] NextGeneration(Status[,] currentGrid)
        {
            var nextGeneration = new Status[Rows, Columns];

            // loop through every cell
            for (var row = 1; row < Rows - 1; row++)
            for (var column = 1; column < Columns - 1; column++)
            {
                //find alive neighbors
                var aliveNeighbors = 0;
                for (var i = -1; i <= 1; i++)
                {
                    for (var j = -1; j <= 1; j++)
                    {
                        aliveNeighbors += currentGrid[row + i, column + j] == Status.Alive ? 1 : 0;
                    }
                }
                var currentCell = currentGrid[row, column];

                // The cell need to be removed from its neighbors
                // as it was counted before
                aliveNeighbors -= currentCell == Status.Alive ? 1 : 0;

                // Implementing the rules of life

                // Cell is lonely and dies
                if (currentCell == Status.Alive && aliveNeighbors < 2)
                {
                    nextGeneration[row, column] = Status.Dead;
                }

                // Cell dies due to over population
                else if (currentCell == Status.Alive && aliveNeighbors > 3)
                {
                    nextGeneration[row, column] = Status.Dead;
                }

                // A new cell is born
                else if (currentCell == Status.Dead && aliveNeighbors == 3)
                {
                    nextGeneration[row, column] = Status.Alive;
                }

                // stays the same
                else
                {
                    nextGeneration[row, column] = currentCell;
                }
            }
            return nextGeneration;
        }

        // The Print method builds a single string then writes to the console by repositioning the cursor
        private void Print(Status[,] future, int timeout = 2000)
        {
            var stringBuilder = new StringBuilder();

            int aliveCellsCount = 0;

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    
                    var cell = future[row, column];
                    stringBuilder.Append(cell == Status.Alive ? "@" : " ");

                    if (cell == Status.Alive)
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
