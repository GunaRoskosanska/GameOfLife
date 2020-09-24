using GameOfLife.Models;
using GameOfLife.View;
using System;
using System.Timers;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Game of life main logic part
    /// </summary>
    public class GameOfLife
    {
        private GameSaver gameSaver;
        private GamePresenter gamePresenter;
        private CellStatusGenerationManager cellStatusGeneration;
        private Timer timer;

        public bool IsRunning { get; private set; }

        /// <summary>
        /// Game of life
        /// </summary>
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
            IsRunning = true;
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
                    IsRunning = false;
                    return;
            }
        }

        /// <summary>
        /// Continues previous game
        /// </summary>
        public void ContinuePreviousGame()
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
                gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

                StartGameTimer();
            }
        }

        /// <summary>
        /// Creates new game
        /// </summary>
        public void CreateNewGame()
        {
            GridSize gridSize = gamePresenter.RequestGridDimensions();
            cellStatusGeneration = new CellStatusGenerationManager(gridSize);
            // To stop the game
            gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

            StartGameTimer();
        }
        /// <summary>
        /// Handles cancel key press
        /// </summary>
        private void GamePresenterCancelKeyPress()
        {
            timer.Enabled = false;
            timer.Elapsed -= OnTimerElapsed;
            gamePresenter.CancelKeyPress -= GamePresenterCancelKeyPress;
            StartNewGame();
        }

        /// <summary>
        /// Enables game timer
        /// </summary>
        public void StartGameTimer()
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
                AliveCells = cellStatusGeneration.AliveCells,
            };

            gamePresenter.Print(gameInfo);
            gameSaver.Save(gameInfo);
        }
    }
}
