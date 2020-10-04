using GameOfLife.Models;
using Newtonsoft.Json;
using System.IO;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Saves and loads game.
    /// </summary>
    public class GameSaver
    {
        private string fileName;

        /// <summary>
        /// Initializes a new instance of the GameSaver.
        /// </summary>
        /// <param name="fileName">Name of the file where to save the game.</param>
        public GameSaver(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Saves the game snapshot to file.
        /// </summary>
        /// <param name="gameInfo">Information about the game that has to be saved.</param>
        public void Save(GameSnapshot snapshot)
        {
            EnsureDirectory();
            string json = JsonConvert.SerializeObject(snapshot, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        /// <summary>
        /// Loads game snapshot from file.
        /// </summary>
        public GameSnapshot Load()
        {
            try
            {
                string json = File.ReadAllText(fileName);
                var snapshot = JsonConvert.DeserializeObject<GameSnapshot>(json);
                return snapshot;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Ensures that game`s directory exists.
        /// </summary>
        private void EnsureDirectory()
        {
            string directory = Path.GetDirectoryName(Path.GetFullPath(fileName));
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
