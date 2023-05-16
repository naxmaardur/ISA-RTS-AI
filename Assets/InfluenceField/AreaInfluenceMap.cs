using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaInfluenceMap
{
	//list of entities that can chance the influenceMap
	List<ArmyActorBase> _entities = new();

	NodeArea[] _influences;
	float[] _influencesBuffer;

	public float decay;
	public float momentum;

	public int Width { get { return _influences.GetLength(0); } }
	public int Height { get { return _influences.GetLength(1); } }
	public float GetValue(int x)
	{
		return _influences[x].ArmyStrength;
	}


	//Class Constructors
	public AreaInfluenceMap(NodeArea[] areas, float decay, float momentum)
	{
		_influences = areas;
		_influencesBuffer = new float[_influences.Length];
		this.decay = decay;
		this.momentum = momentum;
	}

	//changes the value of a grit position
	public void SetInfluence(float[] values, bool[] updated)
	{
		for(int i = 0; i< values.Length; i++)
        {
            if (updated[i])
            {
				_influences[i].ArmyStrength = values[i];
				_influencesBuffer[i] = values[i];
			}
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

	//this should happen once per second
	public void UpdateField()
    {
		UpdateEntities();
		UpdatePropagation();
		UpdateInfluenceBuffer();
		UpdatePropagation();
		UpdateInfluenceBuffer();
	}

	void UpdateEntities()
    {

		float[] areas = new float[_influences.Length];
		bool[] updated = new bool[_influences.Length];
		//adds all enities in a area together to one value for the area
		foreach(IPropagator entity in _entities)
        {
			if (Grid.Instance.GridArray[entity.GridPosition.x, entity.GridPosition.y].NodeArea == null) { return; }
			int id = Grid.Instance.GridArray[entity.GridPosition.x, entity.GridPosition.y].NodeArea.ID;

			areas[id] += entity.ArmyInfluenceValue;
			updated[id] = true;
		}
		SetInfluence(areas, updated);
    }

	//Speadring the values over the grid
	void UpdatePropagation()
    {
		//looping through each position in the grid
		for (int x = 0; x < _influences.Length; x++)
		{
				
				float maxInf = 0;
				float minInf = 0;
				//getting the neighbors of the area
				NodeArea nodeArea = _influences[x];
				NodeArea[] neighbors = nodeArea.Neigbors;
				foreach (NodeArea neighbor in neighbors)
				{
					//calculating the influence value of the neighbor using the buffer and decay
					float inf = _influencesBuffer[neighbor.ID] * Mathf.Exp(-decay);
					maxInf = Mathf.Max(inf, maxInf);
					minInf = Mathf.Min(inf, minInf);
				}
				float Inf = maxInf + minInf;
				if(x == 2)
				{
					Debug.Log(Inf);
				}
				_influences[x].ArmyStrength = Mathf.Lerp(_influencesBuffer[x], Inf, momentum);

			//setting the influence to the greater value.
			/*if (Mathf.Abs(minInf) > maxInf)
			_influences[x].ArmyStrength = Mathf.Lerp(_influencesBuffer[x], minInf, momentum);
			else
				_influences[x].ArmyStrength = Mathf.Lerp(_influencesBuffer[x], maxInf, momentum);*/
		}
	}

	//moves values from the influence map into the Buffer.
	void UpdateInfluenceBuffer()
    {
		for (int x = 0; x < _influences.Length; ++x)
		{
				_influencesBuffer[x] = _influences[x].ArmyStrength;
		}
	}
}
