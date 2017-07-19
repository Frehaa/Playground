using Playground.SlidingPuzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SlidingPuzzleVisualizerInstantSwap : SlidingPuzzleVisualizer
{
    public SlidingPuzzleVisualizerInstantSwap(PuzzleBoard board, Transform parent) : base(board, parent) { }
    public SlidingPuzzleVisualizerInstantSwap(PuzzleBoard board) : base(board) { }

    protected override void OnSwap(int x1, int y1, int x2, int y2)
    {
        int number1 = Board.GetNumber(x1, y1);
        int number2 = Board.GetNumber(x2, y2);
        
        Cards[number1].Position = GetPositionFromCoordinates(x1, y1);
        Cards[number2].Position = GetPositionFromCoordinates(x2, y2);
    }
}
