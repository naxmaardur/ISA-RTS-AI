using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public Vector3 _worldPoint;
	private int _x;
	private int _y;
	private bool _isObstical;
	private NodeArea _nodeArea = null;
	public int X { get { return _x; } }
	public int Y { get { return _y; } }
	public NodeArea NodeArea { get { return _nodeArea; } }

	public bool IsObstical { get { return _isObstical; } }

    private Vector2I[] _neighbors;

    public Vector2I[] Neighbors { get { return _neighbors; } }


	public Node(Vector3 worldPoint, int x, int y, bool isObstical)
    {
		_isObstical = isObstical;
		_worldPoint = worldPoint;
		_x = x;
		_y = y;
	}

    public void SetNeighbors(Vector2I[] neighbors)
    {
        _neighbors = neighbors;
    }

	//should only be run when the grid changes
    public void FindNeighbors(Grid grid)
    {
		List<Vector2I> neighbors = new();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{

				if (x == 0 && y == 0)
					continue;
				int checkX = X + x;
				int checkY = Y + y;
				if (checkX >= 0 && checkX < grid.Width && checkY >= 0 && checkY < grid.Height)
				{
					if (Mathf.Abs(x) + Mathf.Abs(y) == 2)
					{
						// diagonals
						neighbors.Add(new Vector2I(checkX, checkY, 1.4142f));
					}
					else
					{
						neighbors.Add(new Vector2I(checkX, checkY));
					}
				}

			}
		}

		SetNeighbors(neighbors.ToArray());
	}


	public void SetNodeArea(NodeArea area)
    {
		_nodeArea = area;
    }
}
