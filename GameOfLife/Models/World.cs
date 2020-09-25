using GameOfLife.Logic;

namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the World
    /// </summary>
    public class World
    {
        private WorldGenerator worldGenerator;
        
        /// <summary>
        /// Uniqe world identificator
        /// </summary>
        public int Id { get; private set; }

        public World(int id, WorldSize worldSize)
        {
            Id = id;
            worldGenerator = new WorldGenerator(worldSize);
        }

        /// <summary>
        /// Generates new world's generation
        /// </summary>
        public WorldInfo NextGeneration()
        {
            var generation = worldGenerator.NextGeneration();

            var worldInfo = new WorldInfo
            {
                Id = Id,
                AliveCells = worldGenerator.AliveCells,
                GenerationNumber = worldGenerator.GenerationNumber,
                LifesGenerationGrid = generation,
                IsAlive = true //TODO: Ask about PrevGeneration.GetHashCode() != CurrentGeneration.GetHasCode() 
            };

            return worldInfo;
        }
    }
}
