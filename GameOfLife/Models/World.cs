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

        /// <summary>
        /// Information about world
        /// </summary>
        public WorldInfo Info { get; private set; }

        /// <summary>
        /// World constructor
        /// </summary>
        /// <param name="id">Number of the game from 1 to maxValue</param>
        /// <param name="worldSize">Size of World (measured by rows and columns)</param>
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
            Info = worldGenerator.NextGeneration();
            return Info;
        }
    }
}
