using GameOfLife.Model;
using Newtonsoft.Json;
using System.IO;

namespace GameOfLife.Logic
{
    class GameSaver
    {
        private string fileName;
        public GameSaver(string fileName)
        {
            this.fileName = fileName;
        }

        public void Save (GameInfo gameInfo)
        {
            EnsureDirectory();
            var jsonString = JsonConvert.SerializeObject(gameInfo);
            File.WriteAllText(fileName, jsonString); // file name
        }

        private void EnsureDirectory()
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public GameInfo Load()
        {
            if (File.Exists(fileName))
            {
                var jsonString = File.ReadAllText(fileName);
                var gameInfo = JsonConvert.DeserializeObject<GameInfo>(jsonString);
                return gameInfo;
            }
            else
            {
                return null;
            }
        } 
    }
}
