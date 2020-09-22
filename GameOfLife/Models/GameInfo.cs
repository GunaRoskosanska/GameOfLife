namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the game
    /// </summary>
    public class GameInfo
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
    }
}
