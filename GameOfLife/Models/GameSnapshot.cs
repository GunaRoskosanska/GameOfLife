using System.Collections.Generic;

namespace GameOfLife.Models
{
    /// <summary>
    /// Represents the state of the game at a particular point in time.
    /// </summary>
    public class GameSnapshot
    {
        /// <summary>
        /// Gets or sets total lifes in all worlds.
        /// </summary>
        public int TotalLifes { get; set; }

        /// <summary>
        /// Gets or sets total alive worlds.
        /// </summary>
        public int TotalAliveWorlds { get; set; }

        /// <summary>
        /// Gets total worlds count.
        /// </summary>
        public int TotalWorlds => Worlds?.Count ?? 0;

        /// <summary>
        /// Gets or sets the worlds to display.
        /// </summary>
        public int[] DisplayWorlds { get; set; }

        /// <summary>
        /// Gets or sets all worlds.
        /// </summary>
        public List<WorldMemento> Worlds { get; set; }
    }
}
