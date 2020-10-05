using GameOfLife.Logic;
using Newtonsoft.Json;

namespace GameOfLife.Models
{
    /// <summary>
    /// Represents information about a world.
    /// </summary>
    public class World
    {
        private readonly WorldGenerator worldGenerator;

        /// <summary>
        /// Gets or sets a uinique world identifier.
        /// </summary>
        [JsonProperty]
        public int Id { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether the world is alive.
        /// </summary>
        [JsonProperty]
        public bool IsAlive { get; private set; }
        /// <summary>
        /// Gets or sets the world size.
        /// </summary>
        [JsonProperty]
        public WorldSize Size { get; private set; }
        /// <summary>
        /// Gets or sets the alive cells count.
        /// </summary>
        [JsonProperty]
        public int AliveCells { get; private set; }
        /// <summary>
        /// Gets or sets the generation's number.
        /// </summary>
        [JsonProperty]
        public int GenerationNumber { get; private set; }
        /// <summary>
        /// Gets or sets current world`s generation.
        /// </summary>
        [JsonProperty]
        public CellStatus[,] Generation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the World.
        /// </summary>
        /// <param name="id">Unique world identifier</param>
        /// <param name="worldSize">Size of World (measured by rows and columns).</param>
        public World(int id, WorldSize worldSize)
        {
            worldGenerator = new WorldGenerator();
            Id = id;
            Size = worldSize;
        }

        /// <summary>
        /// Advances the world to the next generation.
        /// </summary>
        public void NextGeneration()
        {
            var result = GenerationNumber == 0 ?
                worldGenerator.RandomGeneration(Size) :
                worldGenerator.NextGeneration(Generation);

            GenerationNumber++;
            AliveCells = result.AliveCells;
            Generation = result.Generation;
            IsAlive = result.IsGenerationAlive;
        }
    }
}
