using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New AStarTile", menuName = "Tiles/AStarTile")]

public class AStarTile : Tile{
	public UnitEntity unit
	{
		get {return m_unit; }
		set {m_unit = value; }
	}

	private UnitEntity m_unit;
	// private Building building;
	private int sightCost;
	private int travelCost;
}
