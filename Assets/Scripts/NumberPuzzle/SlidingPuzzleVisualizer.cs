using Playground.SlidingPuzzle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SlidingPuzzleVisualizer
{
    private static readonly float CARD_SPACING = 1.05f;
    
    protected PuzzleBoard Board { get; private set; }
    protected IDictionary<int, NumberCard> Cards { get; private set; } 
    public GameObject Container { get; private set; }

    public SlidingPuzzleVisualizer(PuzzleBoard board) : this(board, null)
    {}

    public SlidingPuzzleVisualizer(PuzzleBoard board, Transform parent)
    {
        Cards = new Dictionary<int, NumberCard>();
        Container = new GameObject("SlidingPuzzleVisualizer");
        Container.transform.parent = parent;
        
        this.Board = board;
        board.OnSwap += OnSwap;

        for (int i = 0; i < board.Size * board.Size; ++i)
        {
            NumberCard card = new NumberCard(i, Container.transform);

            Cards.Add(i, card);
        }
        RepositionCards();
    }

    protected void RepositionCards()
    {
        for (int y = 0; y < Board.Size; ++y)
        {
            for (int x = 0; x < Board.Size; ++x)
            {
                int number = Board.GetNumber(x, y);
                
                Cards[number].Position = GetPositionFromCoordinates(x, y);
            }
        }
    }

    protected Vector2 GetPositionFromCoordinates(int x, int y)
    {
        return new Vector2(x * CARD_SPACING, y * -CARD_SPACING);
    }

    protected abstract void OnSwap(int x1, int y1, int x2, int y2);

}