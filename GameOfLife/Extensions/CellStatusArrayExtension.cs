using GameOfLife.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Extensions
{
    public static class CellStatusArrayExtension
    {
        /// <summary>
        /// Returns lifes count of generation
        /// </summary>
        public static int LifesCount(this CellStatus[,] generation)
        {
            return generation.Cast<int>().Sum();
        }

        /// <summary>
        /// Returns size of generation
        /// </summary>
        public static WorldSize WorldSize(this CellStatus[,] generation)
        {
            new List<int>().Count();

            return new WorldSize
            {
                Rows = generation.GetUpperBound(0) + 1,
                Columns = generation.GetUpperBound(1) + 1,
            };
        }
    }
}
