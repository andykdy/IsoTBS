using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFindUtil
{
    public static int tileCost(string type)
    {
        switch (type){
            case "Mountain":
                return 3;
            case "Water":
                return int.MaxValue;
            case "Dirt":
                return 1;
            case "Beach":
                return 2;
            case "Grass":
                return 2;
            default:
                return 0;
        }
    }
}
