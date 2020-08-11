using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New AStarTile", menuName = "Tiles/AStarTile")]

public class AStarTile : Tile{
//	public UnitEntity unit
//	{
//		get {return m_unit; }
//		set {m_unit = value; }
//	}
//
//	private UnitEntity m_unit;
	// private Building building;
	private int sightCost;
	private int travelCost;
	
	public AStarTile cameFromNode;

	public int x;
	public int y;
	
	public int gCost;
	public int fCost;
	public int hCost;
	
	public void CalculateFCost()
	{
		fCost = gCost + hCost;
	}

	public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
	{
		x = position.x;
		y = position.y;
		return base.StartUp(position, tilemap, go);
	}
}
