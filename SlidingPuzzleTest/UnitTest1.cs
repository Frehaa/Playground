using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Playground.SlidingPuzzle;

namespace SlidingPuzzleTest
{
    [TestClass]
    public class PuzzleboardTest
    {
        private PuzzleBoard board;

        [TestInitialize]
        public void InitializeTest()
        {
            board = new PuzzleBoard(3);
        }

        [TestMethod]
        public void InitialSetupHasExpectedSetup()
        {
            int expected = 0;
            for (int y = 0; y < board.Size; y++)
            {
                for (int x = 0; x < board.Size; x++)
                {
                    Assert.AreEqual(expected, board.GetNumber(x, y));
                    expected++;
                }
            }
        }

        [TestMethod]
        public void MoveDown_NoChange()
        {
            board.MoveDown();

            Assert.AreEqual(3, board.GetNumber(0, 1));
        }

    }
}
