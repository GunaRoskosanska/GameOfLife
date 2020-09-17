namespace GameOfLife.Model
{
    /// <summary>
    /// Information about the game
    /// </summary>
    public class GameInfo
    {
        public CellStatus[,] LifesGenerationGrid { get; set; }
        public int GenerationNumber { get; set; }
        public int AliveCells { get; set; }
    }
}
