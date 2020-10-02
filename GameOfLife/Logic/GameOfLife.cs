using GameOfLife.Models;
using GameOfLife.View;
using System.Collections.Generic;
using System.Linq;
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
        private List<World> worldsToPrint;

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
            worldsToPrint = new List<World>();
            gamePresenter = new GamePresenter();
            gameSaver = new GameSaver("C:\\GameOfLife\\data.json");
            IsRunning = true;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartNewGame()
        {
            OpenMenu();
        }

        /// <summary>
        /// Creates new game
        /// </summary>
        public void CreateNewGame()
        {
            int countOfWorlds = gamePresenter.RequestCountOfWorlds();
            WorldSize worldSize = gamePresenter.RequestWorldSize(10, 20);
            worlds = new List<World>();
            for (int i = 1; i <= countOfWorlds; i++)
            {
                var world = new World(i, worldSize);

                worlds.Add(world);
            }

            int countToRequest = WorldsCount > CountOfWorldsToShow ? CountOfWorldsToShow : WorldsCount;
            var numbersOfworldsToPrint = gamePresenter.RequestNumbersOfWorldToShow(countToRequest, WorldsCount);
            worldsToPrint = numbersOfworldsToPrint.Select(x => worlds[x-1]).ToList();

            // To stop the game
            gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

            StartGameTimer();
        }

        /// <summary>
        /// Continues previous game after pause
        /// </summary>
        public void ContinueGame()
        {
            gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;
            StartGameTimer();
        }

        /// <summary>
        /// Continues previous saved game after pause
        /// </summary>
        public void LoadSavedGame()
        {
            GameData gameData = gameSaver.Load();

            if (gameData == null)
            {
                gamePresenter.PrintNoSavedGame();
                CreateNewGame();
            }
            else
            {

                worlds = gameData.Worlds.Select(x => new World(x)).ToList();
                worldsToPrint = gameData.WorldsToPrint.Select(x => worlds[x-1]).ToList();

                // To stop the game
                gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

                StartGameTimer();
            }
        }

        /// <summary>
        /// Shows on screen Game menu (options)
        /// </summary>
        public void OpenMenu()
        {
            GameOption gameOption = gamePresenter.RequestGameOption();

            switch (gameOption)
            {
                case GameOption.NewGame:
                    CreateNewGame();
                    break;
                case GameOption.ContinuePreviousGame:
                    LoadSavedGame();
                    break;
                case GameOption.Exit:
                    IsRunning = false;
                    return;
            }
        }

        /// <summary>
        /// Continues game after pause (Ctrl + C) depending of the choice made
        /// </summary>
        public void OpenExtendedMenu()
        {
            GameOption gameOption = gamePresenter.RequestGamePauseOption();

            switch (gameOption)
            {
                case GameOption.NewGame:
                    CreateNewGame();
                    break;
                case GameOption.ContinuePreviousGame:
                    ContinueGame();
                    break;
                case GameOption.SaveGame:
                    SaveGame();
                    OpenExtendedMenu();
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
        /// Saves game to file
        /// </summary>
        private void SaveGame()
        {
            var gameData = new GameData
            {
                Worlds = worlds.Select(x => x.Info).ToList(),
                WorldsToPrint = worldsToPrint.Select(x => x.Info.Id).ToArray()
            };

            gameSaver.Save(gameData);
            gamePresenter.PrintGameSaved();
            System.Threading.Thread.Sleep(1000); //short delay for displaying information
        }

        /// <summary>
        /// Changes worlds on the screen
        /// </summary>
        public void ChangeWorldsOnScreen()
        {
            int countToRequest = WorldsCount > CountOfWorldsToShow ? CountOfWorldsToShow : WorldsCount;
            var numbersOfworldsToPrint = gamePresenter.RequestNumbersOfWorldToShow(countToRequest, WorldsCount);
            worldsToPrint = numbersOfworldsToPrint.Select(x => worlds[x-1]).ToList();
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
            OpenExtendedMenu();
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
            var snapshot = new GameSnapshot
            {
                TotalWorlds = worlds.Count,
                WorldsToPrint = worldsToPrint,
                Worlds = worlds
            };

            foreach (var world in worlds)
            {
                var wordlInformation = world.NextGeneration();

                snapshot.TotalAliveWorlds += wordlInformation.IsWorldAlive ? 1 : 0;
                snapshot.TotalLifes += wordlInformation.AliveCells;
            }

            gamePresenter.Print(snapshot);
        }
    }
}
