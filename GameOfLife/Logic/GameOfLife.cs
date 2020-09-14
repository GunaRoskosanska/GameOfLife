using GameOfLife.Model;
using GameOfLife.View;
using System;
using System.Timers;

namespace GameOfLife.Logic
{
    public class GameOfLife
    {
        private GamePresenter gamePresenter;
        private CellStatusGenerationManager cellStatusGeneration;
        private Timer timer;

        public GameOfLife()
        {
            gamePresenter = new GamePresenter();
        }

        public void StartNewGame()
        {
            var gameOption = gamePresenter.RequestGameOption();

            switch (gameOption)
            {
                case GameOption.NewGame:
                    NewGame();
                    break;
                case GameOption.ContinuePreviousGame:
                    ContinuePreviousGame();
                    break;
                case GameOption.Exit:
                    return;
            }
        }

        private void ContinuePreviousGame()
        {
            var gameSaver = new GameSaver();
            var gameInfo = gameSaver.Load();
            cellStatusGeneration = new CellStatusGenerationManager(gameInfo);
            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                timer.Enabled = false;
                Console.WriteLine("\n Ending game.");
            };

            StartGameTimer();
        }

        private void NewGame()
        {
            var gridSize = gamePresenter.RequestGridDimensions();
            cellStatusGeneration = new CellStatusGenerationManager(gridSize);
            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                timer.Enabled = false;
                Console.WriteLine("\n Ending game.");
            };

            StartGameTimer();
        }

        private void StartGameTimer()
        {
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

            gamePresenter.Print(gameInfo);
        }
    }
}
