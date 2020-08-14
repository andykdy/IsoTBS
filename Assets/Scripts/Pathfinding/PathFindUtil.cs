using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFindUtil
{
    public static int tileCost(string type)
    {
        switch (type){
            case "Mountain":
                return 5;
            case "Water":
                return 1000;
            case "Dirt":
                return 1;
            case "Beach":
                return 2;
            case "Grass":
                return 2;
            default:
                Debug.LogError("Tile type not recognized. Tile cost set to -1");
                return -1;
        }
    }
}
