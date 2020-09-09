using System;
using System.Text;

namespace GameOfLife
{
    class GameViewer
    {
        // The Print method builds a single string then writes to the console by repositioning the cursor
        public void Print(GameInfo gameInfo)
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

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, 1);
            Console.Write(stringBuilder.ToString());
            
        }
    }
}
