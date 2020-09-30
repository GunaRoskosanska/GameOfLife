using GameOfLife.Models;
using GameOfLife.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Timers;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Game of life main logic part
    /// </summary>
    public class GameOfLife
    {
        private const int CountOfWorldsToShow = 8;
        private GameSaver gameSaver;
        private GamePresenter gamePresenter;
        private Timer timer;
        private List<World> worlds;
        private int[] worldsToPrint;

        public bool IsRunning { get; private set; }

        /// <summary>
        /// Returns total worlds count
        /// </summary>
        public int WorldsCount { get { return worlds.Count; } }

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
        /// Creates new game
        /// </summary>
        public void CreateNewGame()
        {
            int countOfWorlds = gamePresenter.RequestCountOfWorlds();
            WorldSize worldSize = gamePresenter.RequestWorldSize();
            
            int countToRequest = countOfWorlds > CountOfWorldsToShow ? CountOfWorldsToShow : countOfWorlds;
            worldsToPrint = gamePresenter.RequestNumbersOfWorldToShow(countToRequest, countOfWorlds);

            worlds = new List<World>();

            for (int i = 1; i <= countOfWorlds; i++)
            {
                var world = new World(i, worldSize);

                worlds.Add(world);
            }
            
            // To stop the game
            gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

            StartGameTimer();
        }

        /// <summary>
        /// Continues previous game after pause
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
        /// Continue game after pause (Ctrl + C) depending of the choice made
        /// </summary>
        public void ContinueGameAfterPause()
        {
            GameOption gameOption = gamePresenter.RequestGamePauseOption();

            switch (gameOption)
            {
                case GameOption.NewGame:
                    CreateNewGame();
                    break;
                case GameOption.ContinuePreviousGame:
                    ContinuePreviousGame();
                    break;
                case GameOption.SaveGame:
                    gamePresenter.PrintGameSaved();
                    gameSaver.Save(worlds.Select(world=>world.Info).ToList());
                    break;
                case GameOption.ChangeWorldsOnScreen:
                    ChangeWorldsOnScreen();
                    break;
                case GameOption.Exit:
                    IsRunning = false;
                    return;
            }
        }
        /// <summary>
        /// Changes worlds on the screen
        /// </summary>
        public void ChangeWorldsOnScreen()
        {
            int countToRequest = WorldsCount > CountOfWorldsToShow ? CountOfWorldsToShow : WorldsCount;
            worldsToPrint = gamePresenter.RequestNumbersOfWorldToShow(countToRequest, WorldsCount);
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
            ContinueGameAfterPause();
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

                if (worldsToPrint.Contains(world.Id))
                {
                    gamePresenter.Print(wordlInformation);
                }
            }

            //gamePresenter.Print(gameInfo);
        }
    }
}
