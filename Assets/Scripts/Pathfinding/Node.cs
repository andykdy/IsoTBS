using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Node : MonoBehaviour
{
    public int gCost;
    public int fCost;
    public int hCost;
    public int travelCost;
    public bool isTraversable;

    public Node cameFromNode;

    public Vector3Int nodePos;
	
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    
    // TODO: Fix this Russian Doll initialization 
    public void Initialize(Vector3Int pos, string type)
    {
        isTraversable = type != "Water";
        int cost = PathFindUtil.tileCost(type);
        nodePos = pos;
        Initialize(cost);
    }
    public void Initialize(int cost)
    {
        travelCost = cost;
        Initialize();
    }
    public void Initialize()
    {
        gCost = int.MaxValue;
        CalculateFCost();
        cameFromNode = null;
    }
}
