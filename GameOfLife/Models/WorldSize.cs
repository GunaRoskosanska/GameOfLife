namespace GameOfLife.Models
{
    /// <summary>
    /// Size of the World (measured by rows and columns).
    /// </summary>
    public class WorldSize
    {
        /// <summary>
        /// Count of rows in the grid.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Count of columns in the grid.
        /// </summary>
        public int Columns { get; set; }
    }
}