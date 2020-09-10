using System;
using System.Timers;

namespace GameOfLife
{
    public class StartGameAndLoop
    {
        GameBuilding gameViewer;
        CellStatusGenerationManager cellStatusGeneration;
        const int MinValue = 1;
        const int MaxValue = 50;
        int Rows;
        int Columns;
        private Timer timer;
        const string incorrectEnteredNumberAnnouncement = "Please enter positive numbers only from 1 to 50. ";

        public void StartNewGame()
        {
            Console.Write("Enter number of rows (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out Rows);

            while (Rows < MinValue || Rows > MaxValue)
            {
                Console.Write(incorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out Rows);
            }

            Console.Write("Enter number of columns (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out Columns);
            
            while (Columns < MinValue || Columns > MaxValue)
            {
                Console.Write(incorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out Columns);
            }

            cellStatusGeneration = new CellStatusGenerationManager(Rows, Columns);
            gameViewer = new GameBuilding();

            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                timer.Enabled = false;
                Console.WriteLine("\n Ending game.");
            };

            Console.Clear();

            timer = new Timer(1000);
            timer.Elapsed += Loop;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Loop(object sender, ElapsedEventArgs e)
        {
            var gameInfo = new GameInfoShownInConsole
            {
                LifesGenerationGrid = cellStatusGeneration.NextGeneration(),
                GenerationNumber = cellStatusGeneration.GenerationNumber,
                AliveCells = cellStatusGeneration.AliveCells
            };

            gameViewer.DrawGeneration(gameInfo);
        }
    }
}
