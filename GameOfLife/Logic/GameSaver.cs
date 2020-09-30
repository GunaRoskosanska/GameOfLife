using GameOfLife.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Saves the game
    /// </summary>
    public class GameSaver
    {
        private string fileName;

        /// <summary>
        /// Shows where to save the game
        /// </summary>
        /// <param name="fileName">Name of the file where to save the game</param>
        public GameSaver(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Saves the game
        /// </summary>
        /// <param name="gameInfo">Information about the game that has to be saved</param>
        public void Save (List<WorldInfo> gameInfo)
        {
            try
            {
                EnsureDirectory();
                string gameInfoJsonData = JsonConvert.SerializeObject(gameInfo);
                File.WriteAllText(fileName, gameInfoJsonData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Can not save the game. Reason: " + e.Message);
            }

        }

        /// <summary>
        /// Finds where to save the file with game information
        /// </summary>
        private void EnsureDirectory()
        {
            string directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Shows previously saved information
        /// </summary>
        public WorldInfo Load()
        {
            try
            {
                string jsonInformation = File.ReadAllText(fileName);
                var gameInfo = JsonConvert.DeserializeObject<WorldInfo>(jsonInformation);
                return gameInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " Please start a new game.");
                return null;
            }
        } 
    }
}
