using GameOfLife.Models;
using GameOfLife.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    class MultipleGames
    {
        private GamePresenter gamePresenter;
        private CellStatusGenerationManager cellStatusGeneration;
        private const int gamesCount = 1000;
        public List<GameInfo> Games = new List<GameInfo>();

        public MultipleGames()
        {

        }

        public void CreateNewGames()
        {
            GridSize gridSize = gamePresenter.RequestGridDimensions();

            foreach (GameInfo game in Games)
            {
                cellStatusGeneration = new CellStatusGenerationManager(gridSize);
            }

            //gamePresenter.CancelKeyPress += GamePresenterCancelKeyPress;

            //StartGameTimer();
        }
        
        public void AddGame(int gameNumber, int aliveCells, int generationNumber, CellStatus[,] lifesGenerationGrid)
        {
            GameInfo game = new GameInfo(gameNumber, aliveCells, generationNumber, lifesGenerationGrid);
            Games.Add(game);
        }
    }
}