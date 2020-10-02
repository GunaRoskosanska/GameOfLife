using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Models
{
    /// <summary>
    /// Contains information that will be shown on screen
    /// </summary>
    public class GameSnapshot
    {
        /// <summary>
        /// Game snapshot constructor
        /// </summary>
        public GameSnapshot()
        {
            WorldsToPrint = new List<World>();
        }

        /// <summary>
        /// Count of total lifes in all worlds
        /// </summary>
        public int TotalLifes { get; set; }

        /// <summary>
        /// Count of all worlds with status Alive
        /// </summary>
        public int TotalAliveWorlds { get; set; }

        /// <summary>
        /// Count of all worlds
        /// </summary>
        public int TotalWorlds { get; set; }

        /// <summary>
        /// List of all worlds
        /// </summary>
        public List<World> WorldsToPrint { get; set; }
        public List<World> Worlds { get; internal set; }
    }
}
