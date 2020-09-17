using GameOfLife.Model;
using Newtonsoft.Json;
using System.IO;

namespace GameOfLife.Logic
{
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
        public void Save (GameInfo gameInfo)
        {
            EnsureDirectory();
            string jsonString = JsonConvert.SerializeObject(gameInfo);
            File.WriteAllText(fileName, jsonString);
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
        /// Shows previously saved infromation
        /// </summary>
        public GameInfo Load()
        {
            if (File.Exists(fileName))
            {
                string jsonInformation = File.ReadAllText(fileName);
                var gameInfo = JsonConvert.DeserializeObject<GameInfo>(jsonInformation);
                return gameInfo;
            }

            return null;
        } 
    }
}
