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

        /// <summary>
        /// Initializes a new instance of the WorldSize.
        /// </summary>
        public WorldSize()
        {
        }

        /// <summary>
        /// Initializes a new instance of the WorldSize.
        /// </summary>
        public WorldSize(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }
    }
}