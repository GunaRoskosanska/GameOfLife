using GameOfLife.Extensions;
using GameOfLife.Logic;
using GameOfLife.Models;
using System;
using System.Collections.Generic;
using Xunit;


namespace GameOfLife.Tests
{
    public class WorldGeneratorTest
    {
        private const CellStatus Dead = CellStatus.Dead;
        private const CellStatus Alive = CellStatus.Alive;

        private readonly static CellStatus[,] WorldThree = new CellStatus[5, 6]
        {
            { Dead, Dead,  Dead,  Dead,  Dead,  Dead },
            { Dead, Dead,  Alive, Alive, Dead,  Dead },
            { Dead, Alive, Dead,  Dead,  Alive, Dead },
            { Dead, Dead,  Alive, Alive, Dead,  Dead },
            { Dead, Dead,  Dead,  Dead,  Dead,  Dead }
        };

        private readonly static CellStatus[,] WorldOneFirstGeneration = new CellStatus[5, 5]
        {
            { Alive, Dead,  Dead,  Dead,  Dead  },
            { Dead,  Alive, Dead,  Dead,  Dead  },
            { Dead,  Dead,  Alive, Dead,  Dead  },
            { Dead,  Dead,  Dead,  Alive, Dead  },
            { Dead,  Dead,  Dead,  Dead,  Alive }
        };

        private readonly static CellStatus[,] WorldOneSecondGeneration = new CellStatus[5, 5]
        {
            { Dead, Dead,  Dead,  Dead,  Dead },
            { Dead, Alive, Dead,  Dead,  Dead },
            { Dead, Dead,  Alive, Dead,  Dead },
            { Dead, Dead,  Dead,  Alive, Dead },
            { Dead, Dead,  Dead,  Dead,  Dead }
        };

        public static IEnumerable<object[]> GenerationsTestData =>
        new List<object[]>
        {
            new object[] { WorldThree, WorldThree, false },
            new object[] { WorldOneFirstGeneration, WorldOneSecondGeneration, true },
        };

        [Theory]
        [InlineData(10, 10)] // rows and columns to test
        [InlineData(15, 20)]
        public void RandomGeneration_CorrectInput_ReturnsGeneration(int rows, int columns) // method to test RandomGeneration()
        {
            // Arrange
            WorldGenerator worldGenerator = new WorldGenerator(); // create new object of worldGenerator
            WorldSize worldSize = new WorldSize
            {
                Rows = rows, // sets value for rows property in worldSize object
                Columns = columns // sets value for columns property in worldSize object
            }; // create object of worldSize

            // Act
            WorldGenerationResult result = worldGenerator.RandomGeneration(worldSize); // execute method RandomGeneration() in worldGenerator object with worldSize parameter and save it to result

            // Assert
            var actualWorldSize = result.Generation.WorldSize();
            var expectedLifeCells = result.Generation.LifesCount();
            var expectedIsGenerationAlive = expectedLifeCells > 0;

            Assert.Equal(rows, actualWorldSize.Rows);
            Assert.Equal(columns, actualWorldSize.Columns);
            Assert.Equal(expectedLifeCells, result.AliveCells);
            Assert.Equal(expectedIsGenerationAlive, result.IsGenerationAlive);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 0)]
        [InlineData(-10, 15)]
        [InlineData(15, -20)]
        [InlineData(-10, -15)]
        public void RandomGeneration_IncorrectInput_ShouldThrowArgumentException(int rows, int columns)
        {
            // Arrange
            WorldGenerator worldGenerator = new WorldGenerator();
            WorldSize worldSize = new WorldSize
            {
                Rows = rows,
                Columns = columns
            };

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => worldGenerator.RandomGeneration(worldSize));
        }

        [Theory]
        [MemberData(nameof(GenerationsTestData))]
        public void NextGenerationTest(CellStatus[,] actual, CellStatus[,] expected, bool expectedIsAlive)
        {
            var expectedAliveCells = expected.LifesCount();
            WorldGenerator worldGenerator = new WorldGenerator();

            var nextGenerationResult = worldGenerator.NextGeneration(actual);

            Assert.Equal(expected, nextGenerationResult.Generation);
            Assert.Equal(expectedAliveCells, nextGenerationResult.AliveCells);
            Assert.Equal(expectedIsAlive, nextGenerationResult.IsGenerationAlive);
        }
    }
}