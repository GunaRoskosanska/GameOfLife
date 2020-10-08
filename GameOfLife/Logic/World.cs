using GameOfLife.Extensions;
using GameOfLife.Models;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Represents information about a world.
    /// </summary>
    public class World
    {
        public readonly static WorldSize DefaultSize = new WorldSize(10, 10);

        private readonly IWorldGenerator worldGenerator;

        /// <summary>
        /// Gets or sets a value indicating whether the world is alive.
        /// </summary>
        public bool IsAlive { get; private set; }
        /// <summary>
        /// Gets or sets the world size.
        /// </summary>
        public WorldSize Size { get; private set; }
        /// <summary>
        /// Gets or sets the alive cells count.
        /// </summary>
        public int AliveCells { get; private set; }
        /// <summary>
        /// Gets or sets the generation's number.
        /// </summary>
        public int GenerationNumber { get; private set; }
        /// <summary>
        /// Gets or sets current world`s generation.
        /// </summary>
        public CellStatus[,] Generation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the World.
        /// </summary>
        public World(IWorldGenerator worldGenerator) : this(World.DefaultSize, worldGenerator)
        {
        }

        /// <summary>
        /// Initializes a new instance of the World.
        /// </summary>
        /// <param name="worldSize">Size of World (measured by rows and columns).</param>
        public World(WorldSize worldSize, IWorldGenerator worldGenerator)
        {
            this.worldGenerator = worldGenerator;
            this.Size = worldSize;

            NextGeneration();
        }

        /// <summary>
        /// Returns world's state
        /// </summary>
        public WorldMemento SaveState()
        {
            return new WorldMemento(GenerationNumber, Generation, Size, IsAlive, AliveCells);
        }

        /// <summary>
        /// Restores world's state
        /// </summary>
        public void RestoreState(WorldMemento memento)
        {
            Generation = memento.Generation;
            GenerationNumber = memento.GenerationNumber;
            AliveCells = memento.AliveCells;
            Size = memento.Size;
            IsAlive = memento.IsAlive;
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
