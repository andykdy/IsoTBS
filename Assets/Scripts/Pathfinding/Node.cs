using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    public int gCost;
    public int fCost;
    public int hCost;
    public int travelCost;

    public Node cameFromNode;

    public Vector3Int nodePos;
	
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public void Initialize(Vector3Int pos, int cost)
    {
        hCost = cost;
        nodePos = pos;
        gCost = int.MaxValue;
        CalculateFCost();
        cameFromNode = null;
    }
}
