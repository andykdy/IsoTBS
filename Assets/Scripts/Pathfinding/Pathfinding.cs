using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Pathfinding: Singleton<Pathfinding>
{
    [SerializeField]
    private Tilemap map;
    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> FindPath(Node startNode, Node endNode)
    {
        foreach (Node n in FindObjectsOfType<Node>()){
            n.Initialize();
        }
        openList = new List<Node> {startNode};
        closedList = new List<Node>();
        startNode.gCost = startNode.travelCost;
        startNode.hCost = CalculateDistCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0){
            Node currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode){
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbour in GetNeighbourList(currentNode)){
                if (closedList.Contains(neighbour)) continue;
                int tentativeGCost = currentNode.gCost + CalculateDistCost(currentNode, neighbour) + currentNode.travelCost;
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

    private List<Node> GetNeighbourList(Node currentNode)
    {   
        List<Node> neighbourList = new List<Node>();
        if (map.HasTile(currentNode.nodePos + Vector3Int.left))
            neighbourList.Add(map.GetInstantiatedObject(currentNode.nodePos + Vector3Int.left).GetComponent<Node>());
        if (map.HasTile(currentNode.nodePos + Vector3Int.right))
            neighbourList.Add(map.GetInstantiatedObject(currentNode.nodePos + Vector3Int.right).GetComponent<Node>());
        if (map.HasTile(currentNode.nodePos + Vector3Int.down))
            neighbourList.Add(map.GetInstantiatedObject(currentNode.nodePos + Vector3Int.down).GetComponent<Node>());
        if (map.HasTile(currentNode.nodePos + Vector3Int.up))
            neighbourList.Add(map.GetInstantiatedObject(currentNode.nodePos + Vector3Int.up).GetComponent<Node>());
        return neighbourList;
    }

    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node curr = endNode;
        while (curr.cameFromNode != null){
            path.Add(curr.cameFromNode);
            curr = curr.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistCost(Node a, Node b)
    {
        int x = Mathf.Abs(a.nodePos.x - b.nodePos.x);
        int y = Mathf.Abs(a.nodePos.y - b.nodePos.y);
        int remaining = Mathf.Abs(x - y);
        return Mathf.Min(x, y) + remaining;
    }

    private Node GetLowestFCostNode(List<Node> pathNodeList)
    {
        Node lowest = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++){
            if (pathNodeList[i].fCost < lowest.fCost)
                lowest = pathNodeList[i];
        }

        return lowest;
    }
}
