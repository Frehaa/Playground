using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawnerScript : MonoBehaviour {
    [SerializeField]
    private GameObject nodePrefab;
    private GameObject[,] nodes;

    public int rows;
    public int cols;

    private Vector2 nodeDistance = new Vector2(0.2f , 0.2f);

    // Use this for initialization
    private void Start () {
        nodes = new GameObject[rows, cols];

        for (int y = 0; y < rows; ++y)
        {
            for (int x = 0; x < cols; ++x)
            {
                nodes[y,x]= Instantiate<GameObject>(nodePrefab, new Vector2(nodeDistance.x * x, nodeDistance.y * y), Quaternion.identity, transform);
            }
        }
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            var random = new System.Random();
            int x1 = random.Next(0, cols);
            int y1 = random.Next(0, rows);
            int x2 = random.Next(0, cols);
            int y2 = random.Next(0, rows);

            GameObject node1 = nodes[y1, x1];
            GameObject node2 = nodes[y2, x2];

            node1.transform.position = Vector3.zero;
            node2.transform.position = Vector3.zero;
            Debug.DrawLine(Vector2.zero, Vector2.right, Color.red, 1f);

        }
    }

    public ICollection<GameObject> Adjacent(int x, int y)
    {
        if (IsOutOfBounds(x, y))
            throw new IndexOutOfRangeException();

        int[][] adjacentDirections =
        {
            new int[]{ -1, 1}, new int[]{ 0, 1}, new int[]{ 1, 1},
            new int[]{ -1, 0}, new int[]{ 0, 0}, new int[]{ 1, 0},
            new int[]{ -1, -1}, new int[]{ 0, -1}, new int[]{ 1, -1}
        };


        List<GameObject> adjacentNodes = new List<GameObject>();

        foreach (int[] direction in adjacentDirections)
        {
            int dx = direction[0];
            int dy = direction[1];
            int directionX = x + dx;
            int directionY = y + dy;

            if (!IsOutOfBounds(directionX, directionY))
                adjacentNodes.Add(nodes[directionY, directionX]);
        }

        return adjacentNodes;
    }

    private bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || cols <= x || y < 0 || rows <= y;
    }
}
