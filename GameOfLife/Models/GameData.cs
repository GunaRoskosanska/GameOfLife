using System.Collections.Generic;

namespace GameOfLife.Models
{
    public class GameData
    {
        public List<WorldInfo> Worlds { get; set;  }
        public int[] WorldsToPrint { get; set; }
    }
}
