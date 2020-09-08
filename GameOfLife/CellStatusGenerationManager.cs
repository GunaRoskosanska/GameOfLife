using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    class CellStatusGenerationManager
    {
        private int rows;
        private int columns;

        public CellStatusGenerationManager(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public int AliveCells { get; private set; }

        public int GenerationNumber { get; private set; }

        public CellStatus[,] NextGeneration(CellStatus[,] currentGrid)
        {
            var nextGeneration = new CellStatus[rows, columns];

            // Loop through every cell
            for (var row = 1; row < rows - 1; row++)
            for (var column = 1; column < columns - 1; column++)
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
    }
}
