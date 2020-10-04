namespace GameOfLife.Models
{
    /// <summary>
    /// Available options of the game.
    /// </summary>
    public enum GameOption
    {
        /// <summary>
        /// Option of the game - start new game.
        /// </summary>
        NewGame = 1,

        /// <summary>
        /// Option of the game - continue current game.
        /// </summary>
        ContinueGame = 2,

        /// <summary>
        /// Option of the game - change what exact worlds will be shown on screen.
        /// </summary>
        ChangeWorlds = 3,

        /// <summary>
        /// Option of the game - save game.
        /// </summary>
        SaveGame = 4,

        /// <summary>
        /// Option of the game - load game.
        /// </summary>
        LoadGame = 5,

        /// <summary>
        /// Option of the game - exit game.
        /// </summary>
        Exit = 6
    }
}
