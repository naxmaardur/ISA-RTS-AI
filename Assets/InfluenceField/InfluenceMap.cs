using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Vector2I
{
	public int x;
	public int y;
	public float DecayMod;

	public Vector2I(int nx, int ny)
	{
		x = nx;
		y = ny;
		DecayMod = 1;
	}

	public Vector2I(int nx, int ny, float nd)
	{
		x = nx;
		y = ny;
		DecayMod = nd;
	}
}

public interface IPropagator
{
	Vector2I GridPosition { get; }
	float InfluenceValue { get; }
	float ArmyInfluenceValue { get; }
}

public class InfluenceMap : GridData
{
	//list of entities that can chance the influenceMap
	List<ArmyActorBase> _entities = new();

	float[,] _influences;
	float[,] _influencesBuffer;

	public float decay;
	public float momentum;

	public int Width { get { return _influences.GetLength(0); } }
	public int Height { get { return _influences.GetLength(1); } }
	public float GetValue(int x, int y)
	{
		return _influences[x, y];
	}


	//Class Constructors
	public InfluenceMap(int size, float decay, float momentum)
	{
		_influences = new float[size, size];
		_influencesBuffer = new float[size, size];
		this.decay = decay;
		this.momentum = momentum;
	}

	public InfluenceMap(int width, int height, float decay, float momentum)
	{
		_influences = new float[width, height];
		_influencesBuffer = new float[width, height];
		this.decay = decay;
		this.momentum = momentum;
	}


	//changes the value of a grit position
	public void SetInfluence(int x, int y, float value)
	{
		if (x < Width && y < Height)
		{
			_influences[x, y] = value;
			_influencesBuffer[x, y] = value;
		}
	}



	public void AddEntityToList(ArmyActorBase p)
	{
		_entities.Add(p);
	}

	public void RemoveEntityFromList(ArmyActorBase p)
	{
		_entities.Remove(p);
	}


	public void UpdateField(Grid grid)
    {
		UpdateEntities();
		UpdatePropagation(grid);
		UpdateInfluenceBuffer();
	}

	void UpdateEntities()
    {
		foreach(IPropagator entity in _entities)
        {
			SetInfluence(entity.GridPosition.x, entity.GridPosition.y, entity.InfluenceValue);
        }
    }

	//Speadring the values over the grid
	void UpdatePropagation(Grid grid)
    {
		//looping through each position in the grid
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				
				float maxInf = 0;
				float minInf = 0;
				//getting the neighbors of the positon
				Node n = grid.GetGridNode(x, y);
                if (n.IsObstical) {continue;}
				Vector2I[] neighbors = n.Neighbors;
				foreach (Vector2I neighbor in neighbors)
				{
					//calculating the influence value of the neighbor using the buffer and decay
					float inf = _influencesBuffer[neighbor.x, neighbor.y] * Mathf.Exp(-decay * neighbor.DecayMod);
					maxInf = Mathf.Max(inf, maxInf);
					minInf = Mathf.Min(inf, minInf);
				}


				float Inf = maxInf + minInf;



				//setting the influence to the greater value.
				//if (Mathf.Abs(minInf) > maxInf)
					_influences[x, y] = Mathf.Lerp(_influencesBuffer[x, y], Inf, momentum);
				//else
				//	_influences[x, y] = Mathf.Lerp(_influencesBuffer[x, y], maxInf, momentum);
			}
		}
	}

	//moves values from the influence map into the Buffer.
	void UpdateInfluenceBuffer()
    {
		for (int x = 0; x < Width; ++x)
		{
			for (int y = 0; y < Height; ++y)
			{
				_influencesBuffer[x, y] = _influences[x, y];
			}
		}
	}
}
