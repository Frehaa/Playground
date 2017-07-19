using System;
using System.Collections.Generic;
/*
 * 3 x 3 = 8-puzzle
 * [] [1] [2] 
 * [3] [4] [5] 
 * [6] [7] [8] 
 * 
 * 4 x 4 = 15-puzzle
 * []   [1]  [2]  [3]
 * [4]  [5]  [6]  [7]
 * [8]  [9]  [10] [11]
 * [12] [13] [14] [15]
 * 
 * 5 x 5 = 24-puzzle
 * []   [1]  [2]  [3]  [4]
 * [5]  [6]  [7]  [8]  [9]
 * [10] [11] [12] [13] [14]
 * [15] [16] [17] [18] [19]
 * [20] [21] [22] [23] [24]
 * 
 * Stay away
 * 6 x 6 = 35-puzzle
 * []   [1]  [2]  [3]  [4]  [5]
 * [6]  [7]  [8]  [9]  [10] [11]
 * [12] [13] [14] [15] [16] [17]
 * [18] [19] [20] [21] [22] [23]
 * [24] [25] [26] [27] [28] [29]
 * [30] [31] [32] [33] [34] [35]
 */

/* Coordinates as follows
 * [0, 0] [1, 0] [2, 0]
 * [0, 1] [1, 1] [2, 1]
 * [0, 2] [1, 2] [2, 2]
 */
namespace Playground.SlidingPuzzle
{
    public class PuzzleBoard 
    {
        public delegate void Test(int x1, int y1, int x2, int y2);

        public event Test OnSwap;

        private int[,] numbers; // The empty piece is represented by 0
        private Point zero;
        
        public bool IsSolveable { get; private set; }
        public bool InGoalState { get { return CheckForGoalState(); } }
        public int Size { get; private set; }

        public PuzzleBoard(int size)
        {
            if (size <= 0)
                throw new ArgumentException("board size has to be a positive number.");

            this.Size = size;
            
            numbers = new int[Size, Size];
            InitializeBoard();
        }

        public PuzzleBoard(PuzzleBoard board)
        {
            zero = board.zero;
            Size = board.Size;
            IsSolveable = board.IsSolveable;

            numbers = new int[Size, Size];
            for (int y = 0; y < Size; ++y)
            {
                for (int x = 0; x < Size; ++x)
                {
                    numbers[x, y] = board.numbers[x, y];
                }
            }
        }

        private void InitializeBoard()
        {
            int counter = 0;
            for (int y = 0; y < Size; ++y)
            {
                for (int x = 0; x < Size; ++x)
                {
                    numbers[x, y] = counter;
                    ++counter;
                }
            }
        }

        public int GetNumber(int x, int y)
        {
            if (OutOfBounds(x, y))
                throw new ArgumentOutOfRangeException("x, y", "x: " + x + ", y: " + y + " >= Size: " + Size);
            return numbers[x, y];
        }

        private bool OutOfBounds(int x, int y)
        {
            return (x < 0 || x >= Size ||
                    y < 0 || y >= Size);
        }
        
        public int CalculateInversions()
        {
            int inversionCount = 0;

            var previousNumbers = new List<int>();
            for (int y = 0; y < Size; ++y)
            {
                for (int x = 0; x < Size; ++x)
                { 
                    int currentNumber = numbers[x, y];

                    if (currentNumber == 0)
                        continue;
                
                    foreach (int previousNumber in previousNumbers)
                    {
                        if (previousNumber < currentNumber)
                            ++inversionCount;
                    }
                    previousNumbers.Add(currentNumber);
                }
            }

            return inversionCount;
        }
        
        /* Fisher-Yates Shuffle */
        public void Shuffle(bool forceSolveable)
        {
            for (int i = Size * Size - 1; i > 0; --i)
            {
                int j = UnityEngine.Random.Range(0, i + 1);

                int x1 = j % Size;
                int y1 = j / Size;
                int x2 = i % Size;
                int y2 = i / Size;

                Swap(x1, y1, x2, y2);
            }

            IsSolveable = CalculateSolvability();

            if (forceSolveable && !IsSolveable)
                Shuffle(true);
        }

        /** If the grid width is odd, then the number of inversions in a solvable situation is even.
          * If the grid width is even, and the blank is on an even row counting from the bottom(second-last, fourth - last etc), 
          *       then the number of inversions in a solvable situation is odd.
          * If the grid width is even, and the blank is on an odd row counting from the bottom(last, third-last, fifth - last etc) 
          *       then the number of inversions in a solvable situation is even.
        */
        private bool CalculateSolvability()
        {
            int inversions = CalculateInversions();
            if (Util.IsOdd(Size))
            {
                return Util.IsEven(inversions);
            }
            else
            {
                if (Util.IsEven(zero.y))
                {
                    return Util.IsOdd(inversions);
                }
                else
                {
                    return Util.IsEven(inversions);
                }
            }
        }

        public void Shuffle()
        {
            Shuffle(false);
        }

        public void MoveUp()
        {
            try
            {
                Swap(zero.x, zero.y, zero.x, zero.y - 1);
            }
            catch (Exception) {}
        }

        public void MoveDown()
        {
            try
            {
                Swap(zero.x, zero.y, zero.x, zero.y + 1);
            }
            catch (Exception) { }

        }

        public void MoveLeft()
        {
            try
            {
                Swap(zero.x, zero.y, zero.x - 1, zero.y);
            }
            catch (Exception) {}

        }

        public void MoveRight()
        {
            try
            {
                Swap(zero.x, zero.y, zero.x + 1, zero.y);
            }
            catch (Exception) { }

        }

        private void Swap(int x1, int y1, int x2, int y2)
        {
            int tmp = GetNumber(x1, y1);
            numbers[x1, y1] = GetNumber(x2, y2);
            numbers[x2, y2] = tmp;
            UpdateZeroCoordinatesIfNeeded(x1, y1, x2, y2);
            
            if (OnSwap != null)
                OnSwap(x1, y1, x2, y2);
        }

        private void UpdateZeroCoordinatesIfNeeded(int x1, int y1, int x2, int y2)
        {
            if (x1 == zero.x && y1 == zero.y)
            {
                zero.x = x2;
                zero.y = y2;
            }
            else if (x2 == zero.x && y2 == zero.y)
            {
                zero.x = x1;
                zero.y = y1;
            }
        }

        private bool CheckForGoalState()
        {
            int counter = 1;
            for (int i = 0; i < Size * Size; ++i)
            {
                int x = i % Size;
                int y = i / Size;
                int card = GetNumber(x, y);

                if (card == counter)
                {
                    ++counter;
                }
                else if (card != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
