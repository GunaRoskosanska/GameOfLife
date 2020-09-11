namespace GameOfLife
{
    public class GameInfo
    {
        public CellStatus [,] LifesGenerationGrid { get; set; }
        public int GenerationNumber { get; set; }
        public int AliveCells { get; set; }
    }
}
