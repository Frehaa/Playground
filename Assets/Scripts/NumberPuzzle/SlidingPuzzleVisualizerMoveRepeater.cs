using Playground.SlidingPuzzle;
using Playground.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class SlidingPuzzleVisualizerMoveRepeater : SlidingPuzzleVisualizerMovingSwap
{
    private Coroutine coroutine;

    public float RepeatTime { get; set; }

    public SlidingPuzzleVisualizerMoveRepeater(PuzzleBoard board, float swapSpeed, float repeatTime, Transform parent) : base(board, swapSpeed, parent)
    {
        RepeatTime = repeatTime;
    }

    public SlidingPuzzleVisualizerMoveRepeater(PuzzleBoard board, float swapSpeed, float repeatTime) : this(board, swapSpeed, repeatTime, null)
    { }

    protected override void OnSwap(int x1, int y1, int x2, int y2)
    {
        if (coroutine != null)
        {
            Singleton<MonoBehaviour>.Instance.StopCoroutine(coroutine);
            RepositionCards();
        }
        
        coroutine = Singleton<MonoBehaviour>.Instance.StartCoroutine(Swap(x1, y1, x2, y2));
    }

    private IEnumerator Swap(int x1, int y1, int x2, int y2)
    {
        int number1 = Board.GetNumber(x1, y1);
        int number2 = Board.GetNumber(x2, y2);

        Vector2 startPosition = GetPositionFromCoordinates(x2, y2);
        Vector2 endPosition = GetPositionFromCoordinates(x1, y1);

        while (true)
        {
            Cards[number1].Position = startPosition;
            Cards[number2].Position = endPosition;

            yield return new WaitForSecondsRealtime(0.35f);

            Cards[number1].MoveTo(endPosition, SwapSpeed);
            Cards[number2].MoveTo(startPosition, SwapSpeed);

            yield return new WaitForSecondsRealtime(RepeatTime);
        }
    }
}