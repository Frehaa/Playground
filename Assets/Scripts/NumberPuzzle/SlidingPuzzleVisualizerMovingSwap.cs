using Playground.SlidingPuzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SlidingPuzzleVisualizerMovingSwap : SlidingPuzzleVisualizer
{
    public float SwapSpeed { get; set; }

    public SlidingPuzzleVisualizerMovingSwap(PuzzleBoard board, float swapSpeed, Transform parent) : base(board, parent)
    {
        SwapSpeed = swapSpeed;
    }

    public SlidingPuzzleVisualizerMovingSwap(PuzzleBoard board, float swapSpeed) : this(board, swapSpeed, null)
    { }
    
    protected override void OnSwap(int x1, int y1, int x2, int y2)
    {
        //Debug.LogFormat("x1={0}, y1={1} || x2={2}, y2={3}", x1, y1, x2, y2);

        int number1 = Board.GetNumber(x1, y1);
        int number2 = Board.GetNumber(x2, y2);

        Vector2 newPosition1 = GetPositionFromCoordinates(x1, y1);
        Vector2 newPosition2 = GetPositionFromCoordinates(x2, y2);

        Cards[number1].MoveTo(newPosition1, SwapSpeed);
        Cards[number2].MoveTo(newPosition2, SwapSpeed);
    }
}
