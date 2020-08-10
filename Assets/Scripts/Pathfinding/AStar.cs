using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public enum TileType{
    START,
    GOAL,
    WATER,
    GRASS,
    PATH
};

public class AStar : Singleton<AStar>{
    private TileType tileType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Algorithm()
    {
    }
}
