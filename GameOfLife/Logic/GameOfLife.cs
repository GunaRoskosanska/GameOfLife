using GameOfLife.Models;
using GameOfLife.View;
using System;
using System.Collections.Generic;
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
        private Timer timer;
        private List<World> worlds;

        public bool IsRunning { get; private set; }

        /// <summary>
        /// Game of life constructor
        /// </summary>
        public GameOfLife()
        {
            worlds = new List<World>();
            gamePresenter = new GamePresenter();
            gameSaver = new GameSaver("C:\\GameOfLife\\data.json");
            IsRunning = true;
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
                    IsRunning = false;
                    return;
            }
        }

        /// <summary>
        /// Continues previous game
        /// </summary>
        public void ContinuePreviousGame()
        {
            WorldInfo gameInfo = gameSaver.Load();

            if (gameInfo == null)
            {
                CreateNewGame();
            }
            else
            {
                //cellStatusGeneration = new WorldGenerator(gameInfo);
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
            var countOfWorlds = gamePresenter.RequestNumberOfWorlds();
            WorldSize worldSize = gamePresenter.RequestWorldSize();

            for(int i = 1; i <= countOfWorlds; i++)
            {
                var world = new World(i, worldSize);

                worlds.Add(world);
            }
            
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
            Console.Clear();
            foreach (var world in worlds)
            {
                var wordlInformation = world.NextGeneration();
                gamePresenter.Print(wordlInformation);
            }

            //gamePresenter.Print(gameInfo);
            //gameSaver.Save(gameInfo);
        }
    }
}
