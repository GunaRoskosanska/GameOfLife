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

        /// <summary>
        /// Number of the game from 1 to 1000
        /// </summary>
        public int GameNumber { get; set; }

        public GameInfo(int gameNumber, int aliveCells, int generationNumber, CellStatus[,] lifesGenerationGrid)
        {
            GameNumber = gameNumber;
            AliveCells = aliveCells;
            GenerationNumber = generationNumber;
            LifesGenerationGrid = lifesGenerationGrid;
        }
    }
}
