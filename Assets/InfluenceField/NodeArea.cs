using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeArea
{
    int _id;
    public int ID { get { return _id; } }
    Node[] _nodes;
    Vector3 _center;
    public Vector3 Center { get { return _center; } }

    public float ArmyStrength;

    NodeArea[] _neigbors;
    public NodeArea[] Neigbors { get { return _neigbors; } }

    public Node[] Nodes { get { return _nodes; } }

    public NodeArea()
    {
    }

    public void SetNodes(Node[] nodes)
    {
        _nodes = nodes;
    }

    public void SetNeigbors(NodeArea[] nodeAreas)
    {
        _neigbors = nodeAreas;
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public void SetCenter(Vector3 center)
    {
        _center = center;
    }
}
