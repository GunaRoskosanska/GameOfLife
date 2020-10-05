namespace GameOfLife.Models
{
    /// <summary>
    /// Represents world`s generation result.
    /// </summary>
    public class WorldGenerationResult
    {
        /// <summary>
        /// Gets or sets the alive cells count.
        /// </summary>
        public int AliveCells { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the world is alive.
        /// </summary>
        public bool IsGenerationAlive { get; set; }
        /// <summary>
        /// Gets or sets current world generation.
        /// </summary>
        public CellStatus[,] Generation { get; set; }
    }
}
