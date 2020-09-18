namespace GameOfLife.Models
{
    /// <summary>
    /// Cells can have 1 of 2 statuses - dead or alive
    /// </summary>
    public enum CellStatus
    {
        /// <summary>
        /// Status of cell - dead
        /// </summary>
        Dead = 0,
        /// <summary>
        /// Status of cell - alive
        /// </summary>
        Alive = 1,
    }
}