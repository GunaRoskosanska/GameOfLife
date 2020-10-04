namespace GameOfLife
{
    /// <summary>
    /// Entry point for starting the Game of Life.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main method (an entry point).
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var game = new Logic.GameOfLife();
            game.Run();
        }
    }
}
