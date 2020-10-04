namespace GameOfLife.Models
{
    /// <summary>
    /// Available game options
    /// </summary>
    public enum GameOption
    {
        /// <summary>
        /// Start new game.
        /// </summary>
        NewGame = 1,

        /// <summary>
        /// Continue current game.
        /// </summary>
        ContinueGame = 2,

        /// <summary>
        /// Сhange what exact games will be iterating on screen.
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
