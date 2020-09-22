using GameOfLife.Models;
using System.Security.Cryptography;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Generates cells statuses in the grid
    /// </summary>
    public class CellStatusGenerationManager
    {
        private CellStatus[,] currentLifeGenerationGrid;
        private GridSize gridSize;

        public int AliveCells { get; private set; }
        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Generates cells statuses in the grid
        /// </summary>
        /// <param name="gridSize">Grid size</param>
        public CellStatusGenerationManager(GridSize gridSize)
        {
            this.gridSize = gridSize;
        }

        /// <summary>
        /// Generates status of each cell in the grid
        /// </summary>
        /// <param name="gameInfo">Game information</param>
        public CellStatusGenerationManager(GameInfo gameInfo)
        {
            this.GenerationNumber = gameInfo.GenerationNumber;
            this.AliveCells = gameInfo.AliveCells;
            this.currentLifeGenerationGrid = gameInfo.LifesGenerationGrid;

            int rows = this.currentLifeGenerationGrid.GetUpperBound(0) + 1;
            int columns = this.currentLifeGenerationGrid.Length / rows;
            this.gridSize = new GridSize
            {
                Rows = rows,
                Columns = columns
            };
        }

        /// <summary>
        /// Calculates next life generation
        /// </summary>
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
            return currentLifeGenerationGrid;
        }

        /// <summary>
        /// Generates grid of first generation
        /// </summary>
        private CellStatus[,] FirstGeneration()
        {
            var grid = new CellStatus[gridSize.Rows, gridSize.Columns];

            // Randomly initialize grid
            for (var row = 0; row < gridSize.Rows; row++)
            {
                for (var column = 0; column < gridSize.Columns; column++)
                {
                    grid[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);
                }
            }
            return grid;
        }

        /// <summary>
        /// Generates grid of next generation based on current generation
        /// </summary>
        /// <param name="lifeGenerationGrid">Used to specify life generation grid</param>
        private CellStatus[,] NextGeneration(CellStatus[,] lifeGenerationGrid)
        {
            var nextGeneration = new CellStatus[gridSize.Rows, gridSize.Columns];

            // Loop through every cell
            for (var row = 1; row < gridSize.Rows - 1; row++)
            {
                for (var column = 1; column < gridSize.Columns - 1; column++)
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

                    // The cell needs to be removed from its neighbors as it was counted before
                    aliveNeighbors -= currentCell == CellStatus.Alive ? 1 : 0;

                    // Implementing the rules of life
                    if (currentCell == CellStatus.Alive && aliveNeighbors < 2) // Cell is lonely and dies
                    {
                        nextGeneration[row, column] = CellStatus.Dead;
                    }
                    else if (currentCell == CellStatus.Alive && aliveNeighbors > 3) // Cell dies due to over population
                    {
                        nextGeneration[row, column] = CellStatus.Dead;
                    }
                    else if (currentCell == CellStatus.Dead && aliveNeighbors == 3) // A new cell is born
                    {
                        nextGeneration[row, column] = CellStatus.Alive;
                    }
                    else // Stays the same
                    {
                        nextGeneration[row, column] = currentCell;
                    }
                }
            }
            return nextGeneration;
        }
    }
}
