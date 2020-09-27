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
            worldGenerator = new WorldGenerator(Id, worldSize);
        }

        /// <summary>
        /// Generates new world's generation
        /// </summary>
        public WorldInfo NextGeneration()
        {
            return worldGenerator.NextGeneration();
        }
    }
}
