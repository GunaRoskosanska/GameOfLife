using GameOfLife.Models;
using System;
using System.Security.Cryptography;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Generates statuses of the cells in the grid (world).
    /// </summary>
    public class WorldGenerator
    {
        /// <summary>
        /// Generates grid of first generation.
        /// </summary>
        public WorldGenerationResult RandomGeneration(WorldSize worldSize)
        {
            if(worldSize.Rows < 1 || worldSize.Columns < 1) // worldSize size is invalid
            {
                throw new System.ArgumentOutOfRangeException("World Size has incorrect value", nameof(worldSize));
            }

            var firstGeneration = new CellStatus[worldSize.Rows, worldSize.Columns];
            int aliveCells = 0;
            // Randomly initialize grid
            for (var row = 0; row < worldSize.Rows; row++)
            {
                for (var column = 0; column < worldSize.Columns; column++)
                {
                    firstGeneration[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);

                    var cell = firstGeneration[row, column];

                    if (cell == CellStatus.Alive)
                    {
                        aliveCells++;
                    }
                }
            }

            return new WorldGenerationResult
            {
                AliveCells = aliveCells,
                IsGenerationAlive = aliveCells > 0,
                Generation = firstGeneration
            };
        }

        /// <summary>
        /// Generates grid for the next generation based on current generation.
        /// </summary>
        /// <param name="lifeGenerationGrid">Used to specify life generation grid.</param>
        public WorldGenerationResult NextGeneration(CellStatus[,] lifeGenerationGrid)
        {
            var isWorldAlive = false;
            int aliveCells = 0;
            var worldSize = new WorldSize
            {
                Rows = lifeGenerationGrid.GetUpperBound(0) + 1,
                Columns = lifeGenerationGrid.GetUpperBound(1) + 1,
            };
            var nextGeneration = new CellStatus[worldSize.Rows, worldSize.Columns];

            // Loop through every cell
            for (var row = 1; row < worldSize.Rows - 1; row++)
            {
                for (var column = 1; column < worldSize.Columns - 1; column++)
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

                    if (currentCell == CellStatus.Alive)
                    {
                        aliveCells++;
                    }

                    if (currentCell != nextGeneration[row, column])
                    {
                        isWorldAlive = true;
                    }
                }
            }

            return new WorldGenerationResult
            {
                AliveCells = aliveCells,
                IsGenerationAlive = isWorldAlive,
                Generation = nextGeneration
            };
        }
    }
}
