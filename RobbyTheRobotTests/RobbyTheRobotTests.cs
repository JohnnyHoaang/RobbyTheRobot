using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobbyTheRobot;
using System;
namespace RobbyTheRobotTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenerateGridTest()
        {
            IRobbyTheRobot robby = Robby.CreateRobby(100,243,100,0.05,0.05);
            ContentsOfGrid [,] grid = robby.GenerateRandomTestGrid();
            int canCount = 0;
            int emptyCount = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if(grid[i,j] == ContentsOfGrid.Can)
                    {
                        canCount++;
                    }
                    else if(grid[i,j] == ContentsOfGrid.Empty)
                    {
                        emptyCount++;
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
            int halfGrid = 50;
            Assert.AreEqual(halfGrid, canCount);
            Assert.AreEqual(halfGrid, emptyCount);
        }
    }
}
