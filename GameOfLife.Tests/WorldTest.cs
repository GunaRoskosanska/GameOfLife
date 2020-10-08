using GameOfLife.Logic;
using GameOfLife.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameOfLife.Tests
{
    public class WorldTest
    {
        [Fact]
        public void CreationDefaultWorldTest()
        {
            //Arrange
            var worldGenerationResult = RandomWorld(World.DefaultSize);
            var worldGeneratorMock = new Mock<IWorldGenerator>();
            worldGeneratorMock.Setup(x => x.RandomGeneration(World.DefaultSize)).Returns(worldGenerationResult);

            //Act
            var world = new World(worldGeneratorMock.Object);

            //Assert
            Assert.NotNull(world);
            Assert.Equal(World.DefaultSize.Rows, world.Size.Rows);
            Assert.Equal(World.DefaultSize.Columns, world.Size.Columns);
            Assert.Equal(worldGenerationResult.AliveCells, world.AliveCells);
            Assert.Equal(worldGenerationResult.IsGenerationAlive, world.IsAlive);
            Assert.Equal(worldGenerationResult.Generation, world.Generation);
            Assert.Equal(1, world.GenerationNumber);
        }

        [Fact]
        public void CreationWorldSize15Test()
        {
            //Arrange
            var wordlSize = new WorldSize(15, 10);
            var worldGenerationResult = RandomWorld(wordlSize);
            var worldGeneratorMock = new Mock<IWorldGenerator>();
            worldGeneratorMock.Setup(x => x.RandomGeneration(wordlSize)).Returns(worldGenerationResult);

            //Act
            var world = new World(wordlSize, worldGeneratorMock.Object);

            //Assert
            Assert.NotNull(world);
            Assert.Equal(wordlSize.Rows, world.Size.Rows);
            Assert.Equal(wordlSize.Columns, world.Size.Columns);
            Assert.Equal(worldGenerationResult.AliveCells, world.AliveCells);
            Assert.Equal(worldGenerationResult.IsGenerationAlive, world.IsAlive);
            Assert.Equal(worldGenerationResult.Generation, world.Generation);
            Assert.Equal(1, world.GenerationNumber);
        }

        [Fact]
        public void AdvanceWorldToNextGenerationTest()
        {
            //Arrange
            var wordlSize = new WorldSize(15, 10);
            var worldFirstGenerationResult = RandomWorld(wordlSize);
            var worldSecondGenerationResult = RandomWorld(wordlSize);
            var worldGeneratorMock = new Mock<IWorldGenerator>();
            worldGeneratorMock.Setup(x => x.RandomGeneration(wordlSize)).Returns(worldFirstGenerationResult);
            worldGeneratorMock.Setup(x => x.NextGeneration(worldFirstGenerationResult.Generation)).Returns(worldSecondGenerationResult);
            var world = new World(wordlSize, worldGeneratorMock.Object);

            //Act
            world.NextGeneration(); // advance world to second generation

            //Assert
            Assert.NotNull(world);
            Assert.Equal(wordlSize.Rows, world.Size.Rows);
            Assert.Equal(wordlSize.Columns, world.Size.Columns);
            Assert.Equal(worldSecondGenerationResult.AliveCells, world.AliveCells);
            Assert.Equal(worldSecondGenerationResult.IsGenerationAlive, world.IsAlive);
            Assert.Equal(worldSecondGenerationResult.Generation, world.Generation);
            Assert.Equal(2, world.GenerationNumber);
        }

        private WorldGenerationResult RandomWorld(WorldSize worldSize)
        {
            return new WorldGenerator().RandomGeneration(worldSize);
        }
    }
}
