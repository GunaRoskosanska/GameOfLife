﻿using GameOfLife.Logic;

namespace GameOfLife.Models
{
    /// <summary>
    /// Information about the World
    /// </summary>
    public class World
    {
        private WorldGenerator worldGenerator;

        /// <summary>
        /// Information about world
        /// </summary>
        public WorldInfo Info { get; private set; }

        /// <summary>
        /// World constructor
        /// </summary>
        /// <param name="id">Number of the game from 1 to maxValue</param>
        /// <param name="worldSize">Size of World (measured by rows and columns)</param>
        public World(int id, WorldSize worldSize)
        {
            worldGenerator = new WorldGenerator(id, worldSize);
            Info = new WorldInfo
            {
                Id = id
            };
        }

        /// <summary>
        /// World constructor
        /// </summary>
        /// <param name="worldInfo">Information about the world</param>
        public World(WorldInfo worldInfo)
        {
            worldGenerator = new WorldGenerator(worldInfo);
            Info = worldInfo;
        }

        /// <summary>
        /// Generates next generation of the world
        /// </summary>
        public WorldInfo NextGeneration()
        {
            Info = worldGenerator.NextGeneration();
            return Info;
        }
    }
}
