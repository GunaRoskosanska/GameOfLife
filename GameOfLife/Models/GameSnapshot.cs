using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Models
{
    public class GameSnapshot
    {
        public GameSnapshot()
        {
            Worlds = new List<World>();
        }

        public int TotalLifes { get; set; }
        public int TotalAliveWorlds { get; set; }
        public int TotalWorlds { get; set; }
        public List<World> Worlds { get; set; }
    }
}
