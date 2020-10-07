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
        public void WorldCreationTest()
        {
            var world = new World(1, new WorldSize {  Rows = 10, Columns = 15});

            Assert.NotNull(world);
        }
    }
}
