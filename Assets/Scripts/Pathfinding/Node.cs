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

    public Node cameFromNode;

    public Vector3Int nodePos;
	
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    
    // TODO: Fix this Russian Doll initialization 
    public void Initialize(Vector3Int pos, int cost)
    {
        nodePos = pos;
        Initialize(cost);
    }
    public void Initialize( int cost)
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
