using System.Linq;
using System.Security.Cryptography;

namespace GameOfLife
{
    class CellStatusGenerationManager
    {
        private int rows;
        private int columns;
        private CellStatus[,] currentLifeGenerationGrid;

        public CellStatusGenerationManager(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public int AliveCells { get; private set; }
        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Calculates next lifes generation
        /// </summary>
        /// <returns></returns>
        public CellStatus[,] NextGeneration()
        {
            if (GenerationNumber == 0)
            {
                currentLifeGenerationGrid = FirstGeneration();
            }
            else
            {
                currentLifeGenerationGrid = NextGeneration(currentLifeGenerationGrid);
            }

            GenerationNumber++;
            AliveCells = CalculateAliveCells(currentLifeGenerationGrid);
            return currentLifeGenerationGrid;
        }

        // Counts alive cells
        private int CalculateAliveCells(CellStatus[,] lifeGenerationGrid)
        {
            // As dead = 0, alive = 1, than sum of all cells = alive cells sum
            return lifeGenerationGrid.Cast<int>().Sum();
        }

        // Generate first lifes generation
        private CellStatus[,] FirstGeneration()
        {
            var grid = new CellStatus[rows, columns];

            // Randomly initialize grid
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    grid[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);
                }
            }

            return grid;
        }

        // Calculates next lifes generation based on current generation
        private CellStatus[,] NextGeneration(CellStatus[,] lifeGenerationGrid)
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
                            aliveNeighbors += lifeGenerationGrid[row + i, column + j] == CellStatus.Alive ? 1 : 0;
                        }
                    }
                    var currentCell = lifeGenerationGrid[row, column];

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
