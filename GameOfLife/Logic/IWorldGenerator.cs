using GameOfLife.Models;

namespace GameOfLife.Logic
{
    public interface IWorldGenerator
    {
        WorldGenerationResult NextGeneration(CellStatus[,] lifeGenerationGrid);
        WorldGenerationResult RandomGeneration(WorldSize worldSize);
    }
}