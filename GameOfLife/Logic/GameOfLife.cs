using GameOfLife.Models;
using GameOfLife.View;
using System;
using System.Collections.Generic;
using System.Timers;

namespace GameOfLife.Logic
{
    /// <summary>
    /// Game of life main logic part.
    /// </summary>
    public class GameOfLife
    {
        private const int OneSecond = 1000;
        private const int CountOfWorldsToShow = 8;
        private const int MinWorldSize = 10;
        private const int MaxWorldSize = 20;
        private GameSaver gameSaver;
        private GamePresenter gamePresenter;
        private Timer timer;
        private List<World> worlds;
        private int[] displayWorlds;

        /// <summary>
        /// Gets or sets a value indicating whether the game is running.
        /// </summary>
        public bool Running { get; private set; }

        /// <summary>
        /// Gets total worlds count.
        /// </summary>
        public int WorldsCount { get { return worlds.Count; } }

        /// <summary>
        /// Gets or sets total alive worlds.
        /// </summary>
        public int TotalAliveWorlds { get; private set; }

        /// <summary>
        /// Gets or sets total lifes in all worlds.
        /// </summary>
        public int TotalLifes { get; private set; }


        /// <summary>
        /// Initializes a new instance of the Game Of Life.
        /// </summary>
        public GameOfLife()
        {
            worlds = new List<World>();
            displayWorlds = new int[0];
            gamePresenter = new GamePresenter();
            gameSaver = new GameSaver("game.json");
        }

        /// <summary>
        /// Run game.
        /// </summary>
        public void Run()
        {
            Running = true;

            OpenMenu();

            while (Running)
            {
                System.Threading.Thread.Sleep(OneSecond);
            }
        }

        /// <summary>
        /// Displays the menu and processes selected game option.
        /// </summary>
        private void OpenMenu()
        {
            GameOption gameOption = gamePresenter.RequestGameOption();

            ProcessGameOption(gameOption);
        }

        /// <summary>
        /// Displays the pause menu and processes selected option of the game.
        /// </summary>
        private void OpenPauseMenu()
        {
            GameOption gameOption = gamePresenter.RequestGamePauseOption();

            ProcessGameOption(gameOption);
        }

        /// <summary>
        /// Processes options of the game.
        /// </summary>
        /// <param name="option">Game option</param>
        private void ProcessGameOption(GameOption option)
        {
            switch (option)
            {
                case GameOption.NewGame:
                    CreateGame();
                    break;
                case GameOption.ContinueGame:
                    ContinueGame();
                    break;
                case GameOption.ChangeWorlds:
                    RequestDisplayWorlds();
                    ContinueGame();
                    break;
                case GameOption.SaveGame:
                    SaveGame();
                    OpenPauseMenu();
                    break;
                case GameOption.LoadGame:
                    ContinueSavedGame();
                    break;
                case GameOption.Exit:
                    Running = false;
                    return;
            }
        }

        /// <summary>
        /// Creates new game.
        /// </summary>
        private void CreateGame()
        {
            int worldsCount = gamePresenter.RequestCountOfWorlds();
            WorldSize worldSize = gamePresenter.RequestWorldSize(MinWorldSize, MaxWorldSize);

            worlds = CreateWorlds(worldsCount, worldSize);

            RequestDisplayWorlds();

            gamePresenter.PauseRequested += Pause;
            StartGameTimer();
        }

        /// <summary>
        /// Continues previous game after pause.
        /// </summary>
        private void ContinueGame()
        {
            gamePresenter.PauseRequested += Pause;
            StartGameTimer();
        }

        /// <summary>
        /// Changes worlds displayed on the screen.
        /// </summary>
        public void RequestDisplayWorlds()
        {
            int countToRequest = WorldsCount > CountOfWorldsToShow ? CountOfWorldsToShow : WorldsCount;
            displayWorlds = gamePresenter.RequestNumbersOfWorldToShow(countToRequest, WorldsCount);
        }

        /// <summary>
        /// Saves current game.
        /// </summary>
        private void SaveGame()
        {
            try
            {
                gameSaver.Save(Snapshot());
                gamePresenter.PrintGameSaved();
            }
            catch (Exception e)
            {
                gamePresenter.PrintLine("Can not save the game. Reason: " + e.Message);
            }
        }

        /// <summary>
        /// Loads and continues saved game.
        /// </summary>
        private void ContinueSavedGame()
        {
            GameSnapshot snapshot = gameSaver.Load();

            if (snapshot == null)
            {
                gamePresenter.PrintNoSavedGame();
                CreateGame();
            }
            else
            {
                worlds = snapshot.Worlds; 
                displayWorlds = snapshot.DisplayWorlds;

                ContinueGame();
            }
        }

        /// <summary>
        /// Pauses current game.
        /// </summary>
        private void Pause()
        {
            timer.Elapsed -= OnTimerElapsed;
            gamePresenter.PauseRequested -= Pause;
            timer.Enabled = false;

            OpenPauseMenu();
        }

        /// <summary>
        /// Enables game timer.
        /// </summary>
        private void StartGameTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Gets a snapshot of the game.
        /// </summary>
        /// <returns></returns>
        private GameSnapshot Snapshot()
        {
            return new GameSnapshot
            {
                Worlds = worlds,
                DisplayWorlds = displayWorlds,
                TotalAliveWorlds = TotalAliveWorlds,
                TotalLifes = TotalLifes,
            };
        }

        /// <summary>
        /// Advances all worlds to a next generation.
        /// </summary>
        private void NextGeneration()
        {
            var totalAliveWorlds = 0;
            var totalLifes = 0;

            foreach (var world in worlds)
            {
                world.NextGeneration();

                totalAliveWorlds += world.IsAlive ? 1 : 0;
                totalLifes += world.AliveCells;
            }

            TotalAliveWorlds = totalAliveWorlds;
            TotalLifes = totalLifes;
        }

        /// <summary>
        /// Creates list of worlds.
        /// </summary>
        /// <param name="worldsCount">Count of worlds to create</param>
        /// <param name="worldSize">World size</param>
        /// <returns></returns>
        private List<World> CreateWorlds(int worldsCount, WorldSize worldSize)
        {
            var newWorlds = new List<World>();

            for (int i = 1; i <= worldsCount; i++)
            {
                var world = new World(i, worldSize);

                newWorlds.Add(world);
            }

            return newWorlds;
        }

        /// <summary>
        /// Occurs when the timer elapses.
        /// </summary>
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            NextGeneration();
            gamePresenter.Print(Snapshot());
        }
    }
}
