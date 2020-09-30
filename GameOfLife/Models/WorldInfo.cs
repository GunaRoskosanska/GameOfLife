namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the game
    /// </summary>
    public class WorldInfo
    {
        /// <summary>
        /// One generation grid of dead and alive cells
        /// </summary>
        public CellStatus[,] LifesGenerationGrid { get; set; }

        /// <summary>
        /// Number of the generation
        /// </summary>
        public int GenerationNumber { get; set; }

        /// <summary>
        /// Count of alive cells in the grid
        /// </summary>
        public int AliveCells { get; set; }

        /// <summary>
        /// Number of the game from 1 to 1000
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Determines whether the world is alive
        /// </summary>
        public bool IsWorldAlive { get; set; }
        /// <summary>
        /// World size
        /// </summary>
        public WorldSize Size { get; internal set; }
    }
}
