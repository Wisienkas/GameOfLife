using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameOfLife;
using System.Diagnostics;

namespace GameOfLifeTest
{
    [TestClass]
    public class GameOfLifeTest
    {
        private Game game;
        /// <summary>
        ///  Game is described as being square in the specification
        ///  So only one length is needed
        /// </summary>
        private readonly int GAME_SIZE = 3;

        [TestInitialize]
        public void init()
        {
            game = new Game(GAME_SIZE);
            game.Map = InitTestMap(GAME_SIZE);
        }
        
        /// <summary>
        /// The Board
        /// 
        ///     - * *
        ///     - * *
        ///     * - -
        /// 
        /// After one tick
        /// Evaluates to
        /// 
        ///     - * *
        ///     * - *
        ///     - * - 
        /// 
        /// </summary>
        /// <param name="gameSize"></param>
        /// <returns></returns>
        private bool[,] InitTestMap(int gameSize)
        {
            return new bool[,]
            {
                {false, true, true},
                {false, true, true},
                {true, false, false},
            };
        }

        [TestMethod]
        public void TestMapSize()
        {
            Assert.AreEqual(GAME_SIZE, game.GetSize());
        }

        /// <summary>
        /// According to the requirement
        /// Game Shall throw CustomLifeException for any 
        /// illigal thing in the game.
        /// 
        /// Example given as Nullpointers, and non-existing neighbours 
        /// at the edge of the game
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CustomLifeException))]
        public void TestInvalidGameSize()
        {
            game = new Game(-1);
        }

        [TestMethod]
        public void TestInBound()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            Assert.IsTrue(game.InBound(0, 0));
            Assert.IsTrue(game.InBound(1, 1));
            Assert.IsFalse(game.InBound(2, 2));
            Assert.IsFalse(game.InBound(-1, -1));
        }

        [TestMethod]
        public void TestGetCellValid()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            Assert.IsTrue(game.GetCell(0, 0));
            Assert.IsTrue(game.GetCell(0, 1));
            Assert.IsFalse(game.GetCell(1, 0));
            Assert.IsFalse(game.GetCell(1, 1));
        }

        /// <summary>
        /// Checks if it throws the Custom Life Exception
        /// when the y value is below 0
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CustomLifeException))]
        public void TestGetCellinvalidUnderflow1()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            game.GetCell(0, -1);
        }

        /// <summary>
        /// Checks if it throws the Custom Life Exception
        /// when the x value is below 0
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CustomLifeException))]
        public void TestGetCellinvalidUnderflow2()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            game.GetCell(-1, 0);
        }

        /// <summary>
        /// Checks if it throws the Custom Life Exception
        /// when the y value is above gamesize
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CustomLifeException))]
        public void TestGetCellinvalidOverflow1()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            game.GetCell(0, 2);
        }

        /// <summary>
        /// Checks if it throws the Custom Life Exception
        /// when the x value is above gamesize
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CustomLifeException))]
        public void TestGetCellinvalidOverflow2()
        {
            game = new Game(2);
            game.Map = new bool[,] {
                { true, true },
                { false,false}
            };

            game.GetCell(2, 0);
        }

        /// <summary>
        /// The resulting array for counting should be
        /// 
        ///     2 3 3
        ///     3 4 3
        ///     1 3 2
        /// 
        /// </summary>
        [TestMethod]
        public void TestCountNeighbours()
        {
            Assert.AreEqual(2, game.CountNeighbours(0, 0), "At {0, 0}");
            Assert.AreEqual(3, game.CountNeighbours(0, 1), "At {0, 1}");
            Assert.AreEqual(3, game.CountNeighbours(0, 2), "At {0, 2}");

            Assert.AreEqual(3, game.CountNeighbours(1, 0), "At {1, 0}");
            Assert.AreEqual(4, game.CountNeighbours(1, 1), "At {1, 1}");
            Assert.AreEqual(3, game.CountNeighbours(1, 2), "At {1, 2}");

            Assert.AreEqual(1, game.CountNeighbours(2, 0), "At {2, 0}");
            Assert.AreEqual(3, game.CountNeighbours(2, 1), "At {2, 1}");
            Assert.AreEqual(2, game.CountNeighbours(2, 2), "At {2, 2}");
        }

        /// <summary>
        /// Rule 1: Any Live Cell with fewer than two 
        ///         live neighbours dies.(How about hermits?)
        /// 
        /// As seen in the documentation for "InitTestMap"
        /// the cell (2, 0) should die in first tick
        /// </summary>
        [TestMethod]
        public void TestRule1()
        {
            game.Tick();
            Assert.IsFalse(game.GetCell(2, 0));
        }

        /// <summary>
        /// Rule 2: Any Live cell with two or three live neighbors lives
        /// 
        /// This Rule Applies to:
        ///  - (0, 1)
        ///  - (0, 2)
        ///  - (1, 2)
        /// 
        /// </summary>
        [TestMethod]
        public void TestRule2()
        {
            game.Tick();
            Assert.IsTrue(game.GetCell(0, 1), "At: {0, 1}");
            Assert.IsTrue(game.GetCell(0, 2), "At: {0, 2}");
            Assert.IsTrue(game.GetCell(1, 2), "At: {1, 2}");
        }

        /// <summary>
        /// Rule 3: Any Live cell with more than three live neighbors dies
        /// 
        /// This Rule Applies to:
        ///  - (1, 1)
        /// </summary>
        [TestMethod]
        public void TestRule3()
        {
            game.Tick();
            Assert.IsFalse(game.GetCell(1, 1), "At: {1, 1}");
        }

        /// <summary>
        /// Rule 4: Any dead cell with exactly three live neightbors becomes alive
        /// 
        /// This Rule Applies to:
        ///  - (1, 0)
        ///  - (2, 1)
        /// 
        /// This Rule Doesn't Applies to:
        ///  - (0, 0)
        ///  - (2, 2)
        /// </summary>
        [TestMethod]
        public void TestRule4()
        {
            game.Tick();
            Assert.IsTrue(game.GetCell(1, 0), "At: {1, 0}");
            Assert.IsTrue(game.GetCell(2, 1), "At: {2, 1}");
            Assert.IsFalse(game.GetCell(0, 0), "At: {0, 0}");
            Assert.IsFalse(game.GetCell(2, 2), "At: {2, 2}");
        }


    }
}
