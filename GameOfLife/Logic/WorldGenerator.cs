using GameOfLife.Models;
using System.Security.Cryptography;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Generates cells statuses in the grid (world)
    /// </summary>
    public class WorldGenerator
    {
        private CellStatus[,] currentLifeGenerationGrid;
        private readonly int id;
        private WorldSize gridSize;

        public int GenerationNumber { get; private set; }

        /// <summary>
        /// Generates cells statuses in the grid
        /// </summary>
        /// <param name="gridSize">Grid size</param>
        public WorldGenerator(int id, WorldSize gridSize)
        {
            this.id = id;
            this.gridSize = gridSize;
        }

        /// <summary>
        /// Generates status of each cell in the grid
        /// </summary>
        /// <param name="gameInfo">Game information</param>
        public WorldGenerator(WorldInfo gameInfo)
        {
            this.GenerationNumber = gameInfo.GenerationNumber;
            this.currentLifeGenerationGrid = gameInfo.LifesGenerationGrid;

            int rows = this.currentLifeGenerationGrid.GetUpperBound(0) + 1;
            int columns = this.currentLifeGenerationGrid.Length / rows;
            this.gridSize = new WorldSize
            {
                Rows = rows,
                Columns = columns
            };
        }

        /// <summary>
        /// Calculates next life generation
        /// </summary>
        public WorldInfo NextGeneration()
        {
            WorldInfo worldInfo;
            if (GenerationNumber == 0)
            {
                worldInfo = FirstGeneration();
                currentLifeGenerationGrid = worldInfo.LifesGenerationGrid;
            }
            else
            {
                worldInfo = NextGeneration(currentLifeGenerationGrid);
                currentLifeGenerationGrid = worldInfo.LifesGenerationGrid;
            }

            GenerationNumber++;
            worldInfo.GenerationNumber = GenerationNumber;
            worldInfo.Size = gridSize;
            return worldInfo;
        }

        /// <summary>
        /// Generates grid of first generation
        /// </summary>
        private WorldInfo FirstGeneration()
        {
            var firstGeneration = new CellStatus[gridSize.Rows, gridSize.Columns];
            int aliveCells = 0;

            // Randomly initialize grid
            for (var row = 0; row < gridSize.Rows; row++)
            {
                for (var column = 0; column < gridSize.Columns; column++)
                {
                    firstGeneration[row, column] = (CellStatus)RandomNumberGenerator.GetInt32(0, 2);

                    var cell = firstGeneration[row, column];

                    if (cell == CellStatus.Alive)
                    {
                        aliveCells++;
                    }
                }
            }
            return new WorldInfo
            {
                Id = id,
                AliveCells = aliveCells,
                IsWorldAlive = aliveCells > 0,
                LifesGenerationGrid = firstGeneration
            };
        }

        /// <summary>
        /// Generates grid of next generation based on current generation
        /// </summary>
        /// <param name="lifeGenerationGrid">Used to specify life generation grid</param>
        private WorldInfo NextGeneration(CellStatus[,] lifeGenerationGrid)
        {
            var isWorldAlive = false;
            int aliveCells = 0;
            int aliveWorlds = 0;
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

                    if(currentCell == CellStatus.Alive)
                    {
                        aliveCells++;
                    }

                    if(currentCell!=nextGeneration[row, column])
                    {
                        isWorldAlive = true;
                        aliveWorlds++;
                    }
                }
            }

            return new WorldInfo
            {
                Id = id,
                AliveCells = aliveCells,
                IsWorldAlive = isWorldAlive,
                LifesGenerationGrid = nextGeneration
            };
        }
    }
}
