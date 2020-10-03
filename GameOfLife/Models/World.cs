using GameOfLife.Logic;
using Newtonsoft.Json;

namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the World
    /// </summary>
    public class World
    {
        private WorldGenerator worldGenerator;


        /// <summary>
        /// Number of the game from 1 to 1000
        /// </summary>
        [JsonProperty]
        public int Id { get; private set; }
        /// <summary>
        /// Indicates whether the world is alive
        /// </summary>
        [JsonProperty]
        public bool IsAlive { get; private set; }
        /// <summary>
        /// World size
        /// </summary>
        [JsonProperty]
        public WorldSize Size { get; private set; }
        /// <summary>
        /// Count of alive cells in the grid
        /// </summary>
        [JsonProperty]
        public int AliveCells { get; private set; }
        /// <summary>
        /// Number of the generation
        /// </summary>
        [JsonProperty]
        public int GenerationNumber { get; private set; }
        /// <summary>
        /// One generation grid of dead and alive cells
        /// </summary>
        [JsonProperty]
        public CellStatus[,] Generation { get; private set; }


        /// <summary>
        /// World constructor
        /// </summary>
        /// <param name="id">Number of the game from 1 to maxValue</param>
        /// <param name="worldSize">Size of World (measured by rows and columns)</param>
        public World(int id, WorldSize worldSize)
        {
            worldGenerator = new WorldGenerator();
            Id = id;
            Size = worldSize;
        }

        /// <summary>
        /// Generates next generation of the world
        /// </summary>
        public void NextGeneration()
        {
            var result = GenerationNumber == 0 ?
                worldGenerator.RandomGeneration(Size) :
                worldGenerator.NextGeneration(Generation);

            GenerationNumber++;
            AliveCells = result.AliveCells;
            Generation = result.Generation;
            IsAlive = result.IsWorldAlive;
        }
    }
}
