using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public interface GridData
{
    int Width { get; }
    int Height { get; }
    float GetValue(int x, int y);
}

public struct AreaCheckDataHolder
{
    public Node node;
    public int distanceFromTheStart;

    public AreaCheckDataHolder(Node n, int dist)
    {
        node = n;
        distanceFromTheStart = dist;
    }


}


public class Grid: SingletonNoMonoBehavior<Grid>
{
    private LayerMask obsticalMask = (1 << LayerMask.NameToLayer("Obstical"));
    public Vector3 Center = Vector3.zero;
    public InfluenceMap HighLevelinfluenceMap;
    public InfluenceMap LowLevelinfluenceMap;
    public AreaInfluenceMap AreaInfluenceMap;
    private int gridSizeX, gridSizeY;//the amount of nodes on X and Y of the grid

    float nodeDiameter;
    public float nodeRadius = 1.5f;
    Vector2 gridWorldSize;

    public int Width { get { return gridSizeX; } }
    public int Height { get { return gridSizeY; } }
    Node[,] grid;
    public Node[,] GridArray { get { return grid; } }

    NodeArea[] _areas;

    public NodeArea[] Areas { get { return _areas; } }




    public Grid(int width, int height, int nodeDiameter)
    {
        Instance = this;
        if(Instance != this) { return; }
        gridSizeX = width;
        gridSizeY = height;
        nodeRadius = nodeDiameter / 2;
        this.nodeDiameter = nodeDiameter;

        gridWorldSize = new Vector2(gridSizeX * nodeDiameter, gridSizeY * nodeDiameter);
        

        CreateGrid();
        HighLevelinfluenceMap = new InfluenceMap(width,height, 0.3f, 0.3f);
        LowLevelinfluenceMap = new InfluenceMap(width, height, 0.7f, 1);
        AreaInfluenceMap = new AreaInfluenceMap(_areas, 0.3f, 1);
    }

    //creates a new node for each position of the grid and checks its collisions
    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Center = Vector3.right * gridWorldSize.x / 2 + Vector3.forward * gridWorldSize.y / 2 ;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                
                    Vector3 worldPoint =  Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                //Check if not obsticale
                bool isObstical = Physics.OverlapBox(worldPoint, new Vector3(nodeRadius - 0.02f, nodeRadius - 0.02f, nodeRadius - 0.02f), new Quaternion(), obsticalMask).Length > 0 ? true : false;
                grid[x, y] = new Node(worldPoint, x, y, isObstical);
            }
        }

        foreach(Node n in grid)
        {
            n.FindNeighbors(this);
        }

        CreateNodeAreas();
    }

    //this code is a mess
    void CreateNodeAreas()
    {
        List<NodeArea> nodeAreas = new();

        for (int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                Node n = grid[x, y];
                if (n.NodeArea != null || n.IsObstical) { continue; }



                NodeArea TempNodeArea = new NodeArea();

                List<AreaCheckDataHolder> open = new();
                List<Node> closed = new();

                open.Add(new AreaCheckDataHolder(n, 0));

                NodeAreaAssignmentLoop(ref open, ref closed, TempNodeArea);


                TempNodeArea.SetNodes(closed.ToArray());
                nodeAreas.Add(TempNodeArea);
            }
        }
        NodeAreaValidate(nodeAreas);   
    }

    // potential rewrite, Defide the whole grid in 20X20 areas, Remove disconedted areas from the area and then add those to temp areas.
    // Add those to a temp area and merge that with the first neighboring area.
    void NodeAreaAssignmentLoop(ref List<AreaCheckDataHolder> open, ref List<Node> closed, NodeArea nodeArea, bool StopAtObstical = false)
    {
        //this currently loops through every thing we don't want that.
        while (open.Count > 0)
        {
            AreaCheckDataHolder current = open[0];
            open.RemoveAt(0);
            if (!current.node.IsObstical)
            {
                closed.Add(current.node);
                current.node.SetNodeArea(nodeArea);
            }
            if(current.distanceFromTheStart > 18) { continue; }
            foreach (Vector2I v2 in current.node.Neighbors)
            {
                Node n = grid[v2.x, v2.y];
                if (closed.Contains(n))
                    continue;
                if (n.NodeArea == null)
                {
                    if(StopAtObstical && n.IsObstical) { continue; }
                    if (!OpenListNodeCheck(open,n))
                    {
                        open.Add(new AreaCheckDataHolder(n, current.distanceFromTheStart + 1));
                    }
                }
            }
        }
    }




    void NodeAreaValidate(List<NodeArea> nodeAreas)
    {
        foreach(NodeArea nodeArea in nodeAreas)
        {
            List<Node> open = new();
            List<Node> closed = new();
            open.Add(nodeArea.Nodes[0]);
            while (open.Count > 0)
            {
                Node current = open[0];
                open.RemoveAt(0);
                if(!current.IsObstical)
                 closed.Add(current);
                foreach (Vector2I v2 in current.Neighbors)
                {
                    Node n = grid[v2.x, v2.y];
                    if (closed.Contains(n))
                        continue;
                    if (!n.IsObstical && n.NodeArea == nodeArea)
                    {
                        if (!open.Contains(n))
                        {
                            open.Add(n);
                        }
                    }
                }
            }

            foreach(Node n in nodeArea.Nodes)
            {
                if (!closed.Contains(n))
                {
                    n.SetNodeArea(null);
                }
            }
            nodeArea.SetNodes(closed.ToArray());
        }

        //making new areas for the Deleted nodes
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Node n = grid[x, y];
                if (n.NodeArea != null || n.IsObstical) { continue; }



                NodeArea TempNodeArea = new NodeArea();

                List<AreaCheckDataHolder> open = new();
                List<Node> closed = new();

                open.Add(new AreaCheckDataHolder(n, 0));

                NodeAreaAssignmentLoop(ref open, ref closed, TempNodeArea,true);


                TempNodeArea.SetNodes(closed.ToArray());
                nodeAreas.Add(TempNodeArea);
            }
        }

        MergeAreas(nodeAreas);
    }

    bool OpenListNodeCheck(List<AreaCheckDataHolder> dataHolders, Node node)
    {

        foreach (AreaCheckDataHolder dataHolder in dataHolders)
        {
            if (dataHolder.node == node) return true;
        }
        return false;
    }


    void MergeAreas(List<NodeArea> nodeAreas)
    {
        List<NodeArea> BlackList = new();
        foreach (NodeArea area in nodeAreas)
        {
            if (BlackList.Contains(area)) { continue; }
            List<Node> nodes = area.Nodes.ToList();
            foreach (Node n in area.Nodes)
            {
                foreach (Vector2I v2 in n.Neighbors)
                {
                    if(v2.DecayMod > 1) { continue; } // skip diagonals
                    Node node = grid[v2.x, v2.y];
                    if (node.NodeArea == area) { continue; }
                    if (node.NodeArea == null) { continue; }

                    if (node.NodeArea.Nodes.Length < 150)
                    {
                        NodeArea oldArea = node.NodeArea;
                        foreach (Node newNode in oldArea.Nodes)
                        {
                            nodes.Add(newNode);
                            newNode.SetNodeArea(area);
                        }
                        BlackList.Add(oldArea);
                        continue;
                    }
                }
            }
            area.SetNodes(nodes.ToArray());
        }
        foreach (NodeArea area in BlackList)
        {
            nodeAreas.Remove(area);
        }
        //setting center
        foreach (NodeArea nodeArea in nodeAreas)
        {
            Vector3 closestToZero = Vector3.positiveInfinity;
            Vector3 furthestFromZero = Vector3.zero;

            foreach (Node n in nodeArea.Nodes)
            {
                if (Vector3.Distance(Vector3.zero, n._worldPoint) < Vector3.Distance(Vector3.zero, closestToZero))
                {
                    closestToZero = n._worldPoint;
                }
                if (Vector3.Distance(Vector3.zero, n._worldPoint) > Vector3.Distance(Vector3.zero, furthestFromZero))
                {
                    furthestFromZero = n._worldPoint;
                }
            }
            nodeArea.SetCenter(closestToZero + (furthestFromZero - closestToZero) / 2);
        }
        FindAreaNeighbors(nodeAreas);
    }

    void FindAreaNeighbors(List<NodeArea> nodeAreas)
    {
        foreach (NodeArea area in nodeAreas)
        {
            List<NodeArea> neighbors = new();
            foreach (Node n in area.Nodes)
            {
                foreach(Vector2I v2 in n.Neighbors)
                {
                    Node node = grid[v2.x, v2.y];
                    if(node.NodeArea == area) { continue; }
                    if(node.NodeArea == null) { continue; }
                    if (neighbors.Contains(node.NodeArea)) { continue; }
                    neighbors.Add(node.NodeArea);
                }
            }
            area.SetNeigbors(neighbors.ToArray());
        }

        for(int i = 0; i < nodeAreas.Count(); i++)
        {
            nodeAreas[i].SetID(i);
        }

        _areas = nodeAreas.ToArray();


    }

    public Node GetGridNode(int x, int y)
    {
        return grid[x, y];
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        /*float percentX = (worldPosition.x - Center.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z - Center.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);*/

        /*int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);*/
        int x = Mathf.RoundToInt(worldPosition.x);
        int y = Mathf.RoundToInt(worldPosition.z);
        return grid[x, y];
    }

    //returns the first walkable node form a world positon
    public Node getSafeNodeFromWordlPoint(Vector3 worldPosition)
    {
        int i = 0;
        Node actualNode = NodeFromWorldPoint(worldPosition);
        if (!actualNode.IsObstical)
            return actualNode;

        List<Node> CheckedNodes = new();
        return getSafePointFromNeighbours(actualNode, ref i, worldPosition, ref CheckedNodes);
    }

    public Node getSafePointFromNeighbours(Node node, ref int i, Vector3 worldPosition, ref List<Node> CheckedNodes)
    {
        if (i > 900)
            return null;
        Vector2I[] neighbours = node.Neighbors;
        int index = 0;
        float distance = Mathf.Infinity;
        int closestIndex = 0;

        foreach (Vector2I v2 in neighbours)
        {
            i++;
            float d = Vector3.Distance(grid[v2.x, v2.y]._worldPoint, worldPosition);
            if(d < distance)
            {
                if (!CheckedNodes.Contains(grid[v2.x, v2.y]))
                {
                    distance = d;
                    closestIndex = index;
                }
            }
            index++;
        }
        CheckedNodes.Add(grid[neighbours[closestIndex].x, neighbours[closestIndex].y]);
        if (!grid[neighbours[closestIndex].x, neighbours[closestIndex].y].IsObstical)
        {
            return grid[neighbours[closestIndex].x, neighbours[closestIndex].y];
        }

        
        return getSafePointFromNeighbours(grid[neighbours[closestIndex].x, neighbours[closestIndex].y], ref i, worldPosition, ref CheckedNodes);
    }


    public void AddEntityToALLMaps(ArmyActorBase entity)
    {
        LowLevelinfluenceMap.AddEntityToList(entity);
        HighLevelinfluenceMap.AddEntityToList(entity);
        AreaInfluenceMap.AddEntityToList(entity);
    }
    public void RemoveEntityFromALLMaps(ArmyActorBase entity)
    {
        LowLevelinfluenceMap.RemoveEntityFromList(entity);
        HighLevelinfluenceMap.RemoveEntityFromList(entity);
        AreaInfluenceMap.RemoveEntityFromList(entity);
    }


    public void Update()
    {
        
    }

    IEnumerator InfluenceMapUpdate()
    {
        while (true) {
            HighLevelinfluenceMap.UpdateField(this);
            LowLevelinfluenceMap.UpdateField(this);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator InfluenceAreaMapUpdate()
    {
        while (true)
        {
            AreaInfluenceMap.UpdateField();
            yield return new WaitForSeconds(1);
        }
    }

    public void SetupCorotines(GameMaster gameMaster)
    {
        gameMaster.StartCoroutine(InfluenceMapUpdate());
        gameMaster.StartCoroutine(InfluenceAreaMapUpdate());
    }

}
