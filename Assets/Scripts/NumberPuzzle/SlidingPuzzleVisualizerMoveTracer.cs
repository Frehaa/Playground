using Playground.SlidingPuzzle;
using Playground.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class SlidingPuzzleVisualizerMoveTracer : SlidingPuzzleVisualizer
{
    public static readonly bool REPEAT_DEFAULT = true;
    public static readonly float REPEAT_DELAY_DEFAULT = 2f;
    public static readonly float DEFAULT_SWAP_SPEED = 0.1f;
    public static readonly float DEFAULT_SWAP_DELAY = 0.1f;

    private IDictionary<int, Point> savedPositions = new Dictionary<int, Point>();
    private IList<Pair<Point, Point>> swapPoints = new List<Pair<Point, Point>>();
    private IList<Pair<int, int>> swapNumbers = new List<Pair<int, int>>();
    private int currentSwapIndex = 0;

    public bool RepeatOnTraceFinished { get; set; }
    public float RepeatDelay { get; set; }
    public float SwapSpeed { get; set; }
    public float SwapDelay { get; set; }

    public SlidingPuzzleVisualizerMoveTracer(PuzzleBoard board) : this(board, null) { }
    public SlidingPuzzleVisualizerMoveTracer(PuzzleBoard board, Transform parent) : base(board, parent)
    {
        RepeatOnTraceFinished = REPEAT_DEFAULT;
        RepeatDelay = REPEAT_DELAY_DEFAULT;
        SwapSpeed = DEFAULT_SWAP_SPEED;
        SwapDelay = DEFAULT_SWAP_DELAY;

        SaveBoardState();
        Singleton<MonoBehaviour>.Instance.StartCoroutine(Swap());
    }

    private void SaveBoardState()
    {
        for (int y = 0; y < Board.Size; ++y)
        {
            for (int x = 0; x < Board.Size; ++x)
            {
                int number = Board.GetNumber(x, y);
                savedPositions.Add(number, new Point(x, y));
            }
        }

        swapPoints.Clear();
        currentSwapIndex = 0;
    }

    public void Reset()
    {
        for (int i = 0; i < Board.Size * Board.Size; ++i)
        {
            Point point = savedPositions[i];
            Cards[i].Position = GetPositionFromCoordinates(point.x, point.y);
        }
        currentSwapIndex = 0;
    }

    protected override void OnSwap(int x1, int y1, int x2, int y2)
    {
        Point point1 = new Point(x1, y1);
        Point point2 = new Point(x2, y2);

        int number1 = Board.GetNumber(x1, y1);
        int number2 = Board.GetNumber(x2, y2);

        swapPoints.Add(new Pair<Point, Point>(point1, point2));
        swapNumbers.Add(new Pair<int, int>(number1, number2));
    }

    private IEnumerator Swap()
    {
        Stopwatch stopWatch = new Stopwatch();

        while (true)
        {
            if (currentSwapIndex >= swapPoints.Count)
            {
                if (!stopWatch.IsRunning)
                {
                    stopWatch.Start();
                }

                if (stopWatch.Elapsed.Seconds >= RepeatDelay)
                {
                    Reset();
                    yield return new WaitForSecondsRealtime(SwapDelay);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
            else
            {
                if (stopWatch.IsRunning)
                {
                    stopWatch.Stop();
                    stopWatch.Reset();
                }

                Point point1 = swapPoints[currentSwapIndex].item1;
                Point point2 = swapPoints[currentSwapIndex].item2;
                
                int number1 = swapNumbers[currentSwapIndex].item1;
                int number2 = swapNumbers[currentSwapIndex].item2;
                
                Vector2 newPosition1 = GetPositionFromCoordinates(point1.x, point1.y);
                Vector2 newPosition2 = GetPositionFromCoordinates(point2.x, point2.y);

                Cards[number1].MoveTo(newPosition1, SwapSpeed);
                Cards[number2].MoveTo(newPosition2, SwapSpeed);

                currentSwapIndex++;

                yield return new WaitForSecondsRealtime(SwapDelay);
            }
        }

    }
}
