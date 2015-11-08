using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Game
    {
        private readonly int gameSize;
        public bool[,] Map { get; set; }

        

        public Game(int gameSize)
        {
            if (gameSize <= 0)
                throw new CustomLifeException($"Gamesize has to be positive... was:{gameSize}");
            this.gameSize = gameSize;
            this.Map = GenerateMap(gameSize);
        }

        private bool[,] GenerateMap(int gameSize)
        {
            Random rand = new Random();
            bool[,] map = new bool[gameSize, gameSize];
            for(int x = 0; x < gameSize; x++)
            {
                for(int y = 0; y < gameSize; y++)
                {
                    map[x, y] = rand.NextDouble() > 0.5;
                }
            }
            return map;
        }

        public int GetSize()
        {
            return this.gameSize;
        }

        public bool GetCell(int x, int y)
        {
            if (!InBound(x, y))
                throw new CustomLifeException($"Some Cordinate was not between 0..{gameSize}... x:{x}, y:{y}");
            return Map[x,y];
        }

        public bool InBound(int x, int y)
        {
            return InBound(x) && InBound(y);
        }

        bool InBound(int x)
        {
            return x >= 0 && x < gameSize;
        }

        public void Tick()
        {
            bool[,] nextGen = new bool[gameSize, gameSize];
            Parallel.For(0, gameSize, x =>
            {
                for (int y = 0; y < gameSize; y++)
                {
                    int count = CountNeighbours(x, y);
                    if (GetCell(x, y))
                    {
                        nextGen[x, y] = count == 2 || count == 3;
                    }
                    else
                    {
                        nextGen[x, y] = count == 3;
                    }
                }
            });
            this.Map = nextGen;
        }

        /// <summary>
        /// Will Return the amount of neighbours
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int CountNeighbours(int x, int y)
        {
            if(!InBound(x, y)) throw new CustomLifeException($"Some Cordinate was not between 0..{gameSize}... x:{x}, y:{y}");
            int count = 0;
            for (int nx = -1 + x; nx <= 1 + x; nx++)
            {
                for(int ny = -1 + y; ny <= 1 + y; ny++)
                {
                    if (nx == x && ny == y) continue;
                    if (InBound(nx, ny) && GetCell(nx, ny)) count++;
                }
            }
            return count;
        }
    }
}
