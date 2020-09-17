using GameOfLife.Model;
using GameOfLife.View;
using System;
using System.Timers;

namespace GameOfLife.Logic
{
    public class GameOfLife
    {
        private GameSaver gameSaver;
        private GamePresenter gamePresenter;
        private CellStatusGenerationManager cellStatusGeneration;
        private Timer timer;

        public GameOfLife()
        {
            gamePresenter = new GamePresenter();
            gameSaver = new GameSaver("C:\\GameOfLife\\data.json");
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartNewGame()
        {
            GameOption gameOption = gamePresenter.RequestGameOption();

            switch (gameOption)
            {
                case GameOption.NewGame:
                    CreateNewGame();
                    break;
                case GameOption.ContinuePreviousGame:
                    ContinuePreviousGame();
                    break;
                case GameOption.Exit:
                    return;
            }
        }

        /// <summary>
        /// Continues previous game
        /// </summary>
        private void ContinuePreviousGame()
        {
            GameInfo gameInfo = gameSaver.Load();
            if (gameInfo == null)
            {
                CreateNewGame();
            }
            else
            {
                cellStatusGeneration = new CellStatusGenerationManager(gameInfo);
                // To stop the game
                Console.CancelKeyPress += (sender, args) =>
                {
                    timer.Enabled = false;
                    Console.WriteLine("\n Ending game.");
                };

                StartGameTimer();
            }
        }

        /// <summary>
        /// Creates new game
        /// </summary>
        private void CreateNewGame()
        {
            GridSize gridSize = gamePresenter.RequestGridDimensions();
            cellStatusGeneration = new CellStatusGenerationManager(gridSize);
            // To stop the game
            Console.CancelKeyPress += (sender, args) =>
            {
                timer.Enabled = false;
                Console.WriteLine("\n Ending game.");
            };

            StartGameTimer();
        }

        /// <summary>
        /// Enables game timer
        /// </summary>
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
            GameInfo gameInfo = new GameInfo
            {
                LifesGenerationGrid = cellStatusGeneration.NextGeneration(),
                GenerationNumber = cellStatusGeneration.GenerationNumber,
                AliveCells = cellStatusGeneration.AliveCells
            };

            gamePresenter.Print(gameInfo);
            gameSaver.Save(gameInfo);
        }
    }
}
