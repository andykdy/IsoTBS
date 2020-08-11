using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Pathfinding: Singleton<Pathfinding>
{
    [SerializeField]
    private Tilemap map;
    private List<AStarTile> openList;
    private List<AStarTile> closedList;

    public List<AStarTile> FindPath(AStarTile startNode, AStarTile endNode)
    {
        openList = new List<AStarTile> {startNode};
        closedList = new List<AStarTile>();
        BoundsInt dims = map.cellBounds;
        
        for (int i = dims.xMin; i < dims.xMax; i++){
            for (int j = dims.yMin; j < dims.yMax; j++){
                AStarTile temp = map.GetTile<AStarTile>(new Vector3Int(i,j,0));
                temp.gCost = int.MaxValue;
                temp.CalculateFCost();
                temp.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0){
            AStarTile currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode){
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (AStarTile neighbour in GetNeighbourList(map, currentNode)){
                if (closedList.Contains(neighbour)) continue;
                int tentativeGCost = currentNode.gCost + CalculateDistCost(currentNode, neighbour);
                if (tentativeGCost < neighbour.gCost){
                    neighbour.cameFromNode = currentNode;
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = CalculateDistCost(neighbour, endNode);
                    neighbour.CalculateFCost();
                    if (!openList.Contains(neighbour)){
                        openList.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    private List<AStarTile> GetNeighbourList(Tilemap tm, AStarTile currentNode)
    {
        BoundsInt dims = tm.cellBounds;
        
        List<AStarTile> neighbourList = new List<AStarTile>();
        if (currentNode.x - 1 >= 0)
            neighbourList.Add(tm.GetTile<AStarTile>(new Vector3Int(currentNode.x - 1, currentNode.y,0)));
        if (currentNode.x + 1 < dims.xMax)
            neighbourList.Add(tm.GetTile<AStarTile>(new Vector3Int(currentNode.x + 1, currentNode.y,0)));
        if (currentNode.y - 1 >= 0)
            neighbourList.Add(tm.GetTile<AStarTile>(new Vector3Int(currentNode.x, currentNode.y - 1,0)));
        if (currentNode.y + 1 < dims.yMax)
            neighbourList.Add(tm.GetTile<AStarTile>(new Vector3Int(currentNode.x, currentNode.y + 1,0)));
        return neighbourList;
    }

    private List<AStarTile> CalculatePath(AStarTile endNode)
    {
        List<AStarTile> path = new List<AStarTile>();
        path.Add(endNode);
        AStarTile curr = endNode;
        while (curr.cameFromNode != null){
            path.Add(curr.cameFromNode);
            curr = curr.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistCost(AStarTile a, AStarTile b)
    {
        int x = Mathf.Abs(a.x - b.x);
        int y = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(x - y);
        return Mathf.Min(x, y) + remaining;
    }

    private AStarTile GetLowestFCostNode(List<AStarTile> pathNodeList)
    {
        AStarTile lowest = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++){
            if (pathNodeList[i].fCost < lowest.fCost)
                lowest = pathNodeList[i];
        }

        return lowest;
    }
}
