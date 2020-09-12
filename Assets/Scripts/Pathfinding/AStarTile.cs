using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New AStarTile", menuName = "Tiles/AStarTile")]

public class AStarTile : Tile{
	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		go.GetComponent<Node>().Initialize(position, PathFindUtil.tileCost(name));
		go.transform.Translate(0,0.25f,0);
		return base.StartUp(position, tilemap, go);
	}
}
