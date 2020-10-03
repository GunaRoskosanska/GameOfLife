namespace GameOfLife.Models
{
    public class WorldGenerationResult
    {
        public int AliveCells { get; set; }
        public bool IsWorldAlive { get; set; }
        public CellStatus[,] Generation { get; set; }
    }
}
