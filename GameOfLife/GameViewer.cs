using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    class GameViewer
    {


        // The Print method builds a single string then writes to the console by repositioning the cursor
        public void Print(CellStatus[,] cellStatuses, int timeout = 1000)
        {
            var rows = cellStatuses.GetUpperBound(0) + 1;
            var columns = cellStatuses.Length / rows;

            var stringBuilder = new StringBuilder();

            int aliveCellsCount = 0;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {

                    var cell = cellStatuses[row, column];
                    stringBuilder.Append(cell == CellStatus.Alive ? "@" : " ");

                    if (cell == CellStatus.Alive)
                    {
                        aliveCellsCount++;
                    }
                }
                stringBuilder.Append("\n");
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(stringBuilder.ToString());
            Console.WriteLine($"Count of live cells: {aliveCellsCount}");
            Thread.Sleep(timeout);
        }
    }
}
