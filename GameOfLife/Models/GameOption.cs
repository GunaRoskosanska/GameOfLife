namespace GameOfLife.Models
{
    /// <summary>
    /// There are 5 game options
    /// </summary>
    public enum GameOption
    {
        /// <summary>
        /// Option of the game - start new game
        /// </summary>
        NewGame = 1,

        /// <summary>
        /// Option of the game - continue previous game
        /// </summary>
        ContinuePreviousGame = 2,

        /// <summary>
        /// Option of the game - exit game
        /// </summary>
        Exit = 3,

        /// <summary>
        /// Option of the game - save game
        /// </summary>
        SaveGame = 4,

        /// <summary>
        /// Option of the game - change what exact games will be iterating on screen
        /// </summary>
        ChangeWorldsOnScreen = 5
    }
}
