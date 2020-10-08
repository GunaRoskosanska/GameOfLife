using GameOfLife.Models;
using System;
using System.Security.Cryptography;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Generates statuses of the cells in the grid (world).
    /// </summary>
    public class WorldGenerator : IWorldGenerator
    {
        /// <summary>
        /// Generates grid of first generation.
        /// </summary>
        public WorldGenerationResult RandomGeneration(WorldSize worldSize)
        {
            if (worldSize.Rows < 1 || worldSize.Columns < 1) // worldSize size is invalid
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
        /// <param name="currentGeneration">Used to specify life generation grid.</param>
        public WorldGenerationResult NextGeneration(CellStatus[,] currentGeneration)
        {
            var isWorldAlive = false;
            int aliveCells = 0;
            var worldSize = new WorldSize
            {
                Rows = currentGeneration.GetUpperBound(0) + 1,
                Columns = currentGeneration.GetUpperBound(1) + 1,
            };
            var nextGeneration = new CellStatus[worldSize.Rows, worldSize.Columns];

            // Loop through every cell
            for (var row = 0; row < worldSize.Rows; row++)
            {
                for (var column = 0; column < worldSize.Columns; column++)
                {
                    // Find alive neighbors
                    var aliveNeighbors = CalculateLiveNeighbours(row, column, currentGeneration);
                  
                    var currentCell = currentGeneration[row, column];
                    
                    var judgment = Judge(currentCell, aliveNeighbors);

                    nextGeneration[row, column] = judgment;

                    if (judgment == CellStatus.Alive)
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

        /// <summary>
        /// Makes decision whether a cell remains alive or dead
        /// </summary>
        private CellStatus Judge(CellStatus cell, int aliveNeighbors)
        {
            // Implementing the rules of life
            if (cell == CellStatus.Alive && aliveNeighbors < 2) // Cell is lonely and dies
            {
                return CellStatus.Dead;
            }
            else if (cell == CellStatus.Alive && aliveNeighbors > 3) // Cell dies due to over population
            {
                return CellStatus.Dead;
            }
            else if (cell == CellStatus.Dead && aliveNeighbors == 3) // A new cell is born
            {
                return CellStatus.Alive;
            }
            else // Stays the same
            {
                return cell;
            }
        }

        /// <summary>
        /// Given any cell - calculate live neighbours
        /// </summary>
        /// <param name="x">X coord of Cell</param>
        /// <param name="y">Y coord of Cell</param>
        /// <returns>Number of live neighbours</returns>
        private int CalculateLiveNeighbours(int x, int y, CellStatus[,] currentGeneration)
        {
            var size = new WorldSize
            {
                Rows = currentGeneration.GetUpperBound(0) + 1,
                Columns = currentGeneration.GetUpperBound(1) + 1,
            };

            // Calculate live neighours
            int liveNeighbours = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i < 0 || x + i >= size.Rows)   // Out of bounds
                        continue;
                    if (y + j < 0 || y + j >= size.Columns)   // Out of bounds
                        continue;
                    if (x + i == x && y + j == y)       // Same Cell
                        continue;

                    // Add cells value to current live neighbour count
                    liveNeighbours += (int)currentGeneration[x + i, y + j];
                }
            }

            return liveNeighbours;
        }
    }
}
