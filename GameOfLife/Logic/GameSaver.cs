using GameOfLife.Model;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameOfLife.Logic
{
    class GameSaver
    {
        public void Save (GameInfo gameInfo)
        {
            var jsonString = JsonSerializer.Serialize(gameInfo);
            File.WriteAllText("C:\\GameOfLife.json", jsonString);
        }

        public GameInfo Load()
        {
            if (File.Exists("C:\\GameOfLife.json"))
            {
                var jsonString = File.ReadAllText("C:\\GameOfLife.json");
                var gameInfo = JsonSerializer.Deserialize<GameInfo>(jsonString);
                return gameInfo;
            }
            else
            {
                return null;
            }
        } 
    }
}
