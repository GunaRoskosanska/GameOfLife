using System;
using System.Text;

namespace GameOfLife
{
    class GameBuilding
    {
        // The Print method builds a single string then writes to the console by repositioning the cursor
        public void DrawGeneration(GameInfoShownInConsole gameInfo)
        {
            var cellStatuses = gameInfo.LifesGenerationGrid;
            var aliveCells = gameInfo.AliveCells;
            var generationNumber = gameInfo.GenerationNumber;

            var rows = cellStatuses.GetUpperBound(0) + 1;
            var columns = cellStatuses.Length / rows;

            var stringBuilder = new StringBuilder();

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    var cell = cellStatuses[row, column];
                    stringBuilder.Append(cell == CellStatus.Alive ? "@" : " ");
                }
                stringBuilder.Append("\n");
            }

            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Generation #{generationNumber} | Count of live cells: {aliveCells}");
            Console.WriteLine($"You can stop the application by pressing Ctrl+C.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 2);
            Console.Write(stringBuilder.ToString());
        }
    }
}
