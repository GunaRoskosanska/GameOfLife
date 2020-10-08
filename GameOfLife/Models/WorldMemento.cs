using Newtonsoft.Json;

namespace GameOfLife.Models
{
    public class WorldMemento
    {
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
        /// Initializes a new instance of the WorldMemento.
        /// </summary>
        public WorldMemento(int generationNumber, CellStatus[,] generation, WorldSize size, bool isAlive, int aliveCells)
        {
            GenerationNumber = generationNumber;
            Generation = generation;
            IsAlive = isAlive;
            AliveCells = aliveCells;
            Size = size;
        }
    }
}
