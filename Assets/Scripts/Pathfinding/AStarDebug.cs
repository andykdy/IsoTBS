using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class AStarDebug : Singleton<AStarDebug>{
    [SerializeField] private Grid grid;

    [SerializeField]
    private Tilemap tilemap;
    private Tile tile;

    [SerializeField] private Color openColor, closedColor, pathColor, currentColor, startColor, goalColor;

    [SerializeField] private Canvas canvas;

    [SerializeField] private Text debugText;
    
    private List<GameObject> debugObject = new List<GameObject>();
    // Start is called before the first frame update

    public void CreateTiles(Vector3Int start, Vector3Int goal)
    {
        ColorTile(start, startColor);
        ColorTile(goal, goalColor);
    }

    public void ColorTile(Vector3Int position, Color color)
    {
        tilemap.SetTile(position,tile);
        tilemap.SetTileFlags(position, TileFlags.None);
        tilemap.SetColor(position, color);
    }
}
