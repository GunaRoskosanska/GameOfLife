using GameOfLife.Logic;
using GameOfLife.Models;
using System.Linq;
using Xunit;


namespace GameOfLife.Tests
{
    public class WorldGeneratorTest
    {
        [Theory]
        [InlineData(10, 10)] // rows and columns to test
        [InlineData(15, 20)]
        public void RandomGeneration_CorrectInput_ReturnsGeneration(int rows, int columns) // method to test RandomGeneration()
        {
            // Arrange
            WorldGenerator worldGenerator = new WorldGenerator(); // create new object of worldGenerator
            WorldSize worldSize = new WorldSize(); // create object of worldSize
            worldSize.Rows = rows; // sets value for rows property in worldSize object
            worldSize.Columns = columns;

            // Act
            WorldGenerationResult result = worldGenerator.RandomGeneration(worldSize); // execute method RandomGeneration() in worldGenerator object with worldSize parameter and save it to result

            // Assert
            var actualRows = result.Generation.GetUpperBound(0) + 1;
            var actualColumns = result.Generation.GetUpperBound(1) + 1;
            var expectedLifeCells = result.Generation.Cast<int>().Sum();
            var expectedIsGenerationAlive = expectedLifeCells > 0;

            Assert.Equal(rows, actualRows);
            Assert.Equal(columns, actualColumns);
            Assert.Equal(expectedLifeCells, result.AliveCells);
            Assert.Equal(expectedIsGenerationAlive, result.IsGenerationAlive);
        }
    }
}