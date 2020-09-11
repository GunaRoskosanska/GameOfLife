using System;
using System.Timers;

namespace GameOfLife
{
    public class GameOfLife
    {
        private const int MinValue = 1;
        private const int MaxValue = 50;
        private const string IncorrectEnteredNumberAnnouncement = "Please enter positive numbers only from 1 to 50. ";

        private GamePresenter gameViewer;
        private CellStatusGenerationManager cellStatusGeneration;
        private Timer timer;
        
        public void StartNewGame()
        {
            Console.Write("Enter number of rows (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out var rows);

            while (rows < MinValue || rows > MaxValue)
            {
                Console.Write(IncorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out rows);
            }

            Console.Write("Enter number of columns (from 1 to 50): ");
            int.TryParse(Console.ReadLine(), out  var columns);
            
            while (columns < MinValue || columns > MaxValue)
            {
                Console.Write(IncorrectEnteredNumberAnnouncement);
                int.TryParse(Console.ReadLine(), out columns);
            }

            cellStatusGeneration = new CellStatusGenerationManager(rows, columns);
            gameViewer = new GamePresenter();

            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                timer.Enabled = false;
                Console.WriteLine("\n Ending game.");
            };

            Console.Clear();

            timer = new Timer(1000);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        /// <summary>
        /// Handler for timer event
        /// </summary>
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var gameInfo = new GameInfo
            {
                LifesGenerationGrid = cellStatusGeneration.NextGeneration(),
                GenerationNumber = cellStatusGeneration.GenerationNumber,
                AliveCells = cellStatusGeneration.AliveCells
            };

            gameViewer.Print(gameInfo);
        }
    }
}
