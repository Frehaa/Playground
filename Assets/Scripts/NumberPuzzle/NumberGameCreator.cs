using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playground.SlidingPuzzle;
using System.Text;


public class NumberGameCreator : MonoBehaviour {
    [SerializeField][Range(3, 10)]
    private int size;
    [SerializeField][Range(0.001f, 1f)]
    private float swapSpeed;
    [SerializeField][Range(0.1f, 5f)]
    private float repeatTime;
    private PuzzleBoard board;
    private ICollection<SlidingPuzzleVisualizer> visualizers = new List<SlidingPuzzleVisualizer>();

    private Playground.SlidingPuzzle.Environment environment;

    private void Start () {
        board = new PuzzleBoard(size);
        board.Shuffle();
        

        Pair<int> pair = new Pair<int>();

        environment = new Playground.SlidingPuzzle.Environment(board);

        visualizers.Add(new SlidingPuzzleVisualizerInstantSwap(board, transform));
        visualizers.Add(new SlidingPuzzleVisualizerMovingSwap(board, swapSpeed, transform));
        visualizers.Add(new SlidingPuzzleVisualizerMoveRepeater(board, swapSpeed, repeatTime, transform));

        var v = new SlidingPuzzleVisualizerMoveTracer(board, transform)
        {
            RepeatDelay = 3f,
            SwapSpeed = swapSpeed,
            RepeatOnTraceFinished = true,
            SwapDelay = 0.5f
        };

        visualizers.Add(v);

        int counter = 0;
        foreach (SlidingPuzzleVisualizer visualizer in visualizers)
        {
            int x = counter % 3;
            int y = counter / 3;
            Vector2 newPosition = new Vector2(x * 4f, y * -4f);
            visualizer.Container.transform.localPosition = newPosition;
            ++counter;
        }
    }
	
	private void Update ()
    {   
        try
        {
            HandleDebugInput();
            HandleMoveInput();
        }
        catch (ArgumentOutOfRangeException)
        {
            // Do nothing
        }
    }

    private void HandleDebugInput()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            board.Shuffle();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            board.Shuffle(forceSolveable: true);
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log(board.InGoalState);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log(board.CalculateInversions());
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            Debug.Log(board.IsSolveable);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            for (int y = 0; y < board.Size; ++y)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < board.Size; ++x)
                {
                    sb.Append(board.GetNumber(x, y).ToString() + " ");
                }
                Debug.Log(sb.ToString());
            }
        }
    }

    private void HandleMoveInput()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            board.MoveDown();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            board.MoveUp();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            board.MoveRight();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            board.MoveLeft();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            environment.Step();
        }
    }
}
