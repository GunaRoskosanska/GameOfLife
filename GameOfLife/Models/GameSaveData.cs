using System.Collections.Generic;

namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the game (for save)
    /// </summary>
    public class GameSaveData
    {
        /// <summary>
        /// Information about all the worlds
        /// </summary>
        public List<World> Worlds { get; set;  }

        /// <summary>
        /// Numbers of words that were chosen to show on screen
        /// </summary>
        public int[] WorldsToPrint { get; set; }
    }
}
