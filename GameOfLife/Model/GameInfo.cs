namespace GameOfLife.Model
{
    public class GameInfo
    {
        public CellStatus[,] LifesGenerationGrid { get; set; }
        public int GenerationNumber { get; set; }
        public int AliveCells { get; set; }
        public GridSize GridSize {get; set;}
    }
}
