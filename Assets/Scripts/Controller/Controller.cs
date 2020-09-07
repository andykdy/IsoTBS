﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine{
    [SerializeField] private Tilemap lookup;
    [SerializeField] private Tilemap map;
    [SerializeField] private Grid grid;
    [SerializeField] private LookUpTile moveTile;
    private Camera cam;
    private List<Vector3> movePositions;
    [HideInInspector] public UnitEntity currUnit;
    

    

    
    // Start is called before the first frame update
    void Start()
    {
        SetState(new Idle(this));
        m_State.Start();
        cam = Camera.main;
        
    }


    private void Update()
    {
        if (m_State == null) return;
        if (Input.GetMouseButtonDown(0)){
            m_State.OnMouseClick();
        }
        m_State.Update();
    }
    
    //#TODO return an enum or sth that lets us know if we clicked a unit or map
    public string Select(){
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = Camera.main.transform.position.z;
        Ray ray = new Ray(mousePos, new Vector3(0, 0, 1));
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Unit")){
                if (currUnit != null)
                    currUnit.SetState(new UnitIdle(currUnit));
                currUnit = hit.transform.gameObject.GetComponent<UnitEntity>();
                currUnit.SetState(new UnitSelect(currUnit));
                Debug.Log("Unit Selected");
                return "unit";
            }
            if (hit.transform.CompareTag("Map")){
                Debug.Log("Map Selected");
                return "map";
            }
            else{
                currUnit.SetState(new UnitIdle(currUnit));
                currUnit = null;
                Debug.Log("You hit nothing");
                return "";
            }
        }

        return "";
    }

    public void MoveUnit()
    {
        Vector3Int start;
        Vector3Int end;
    
        Node startNode;
        Node endNode;
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offsetUnit = currUnit.transform.position;
        offsetUnit.y -= 0.25f;
        start = grid.WorldToCell(offsetUnit);
        startNode = map.GetInstantiatedObject(start).GetComponent<Node>();
        end = grid.WorldToCell(mousePos);
        if (map.HasTile(end)){
            Debug.Log("Searching for path");
            endNode = map.GetInstantiatedObject(end).GetComponent<Node>();
            currUnit.SetPath(Pathfinding.Instance.FindPath(startNode, endNode));
        }
        else{
            Debug.Log("End tile is not within bounds");
        }
        currUnit = null;
    }

    public void RenderLookUp()
    {
        if (currUnit == null){
            Debug.Log("There's no unit selected... Your state system is broken");
        }
        else{
            Vector3 offsetUnit = currUnit.transform.position;
            offsetUnit.y -= 0.25f;
            BFS(offsetUnit, currUnit.TravelPoints);
            movePositions = movePositions.Distinct().ToList();
            foreach (Vector3 p in movePositions){
                lookup.SetTile(grid.WorldToCell(p), moveTile);
            }
        }
    }

    private void  BFS(Vector3 currPos, int movepoints)
    {
        if (movepoints >= 0){
            Vector3Int tilePos = grid.WorldToCell(currPos);
            if (map.HasTile(tilePos)){
                movePositions.Add(currPos);
                Node currNode = map.GetInstantiatedObject(tilePos).GetComponent<Node>();
                BFS(currPos - Vector3.up * 0.25f - Vector3.left * 0.5f, movepoints - currNode.travelCost);
                BFS(currPos - Vector3.down* 0.25f - Vector3.right * 0.5f, movepoints - currNode.travelCost);
                BFS(currPos - Vector3.up * 0.25f - Vector3.right * 0.5f, movepoints - currNode.travelCost);
                BFS(currPos - Vector3.down* 0.25f - Vector3.left * 0.5f, movepoints - currNode.travelCost);
            }
        }
    }

    public void SetMovePos()
    {
        if (movePositions == null){
            movePositions = new List<Vector3>();
        }
        else{
            foreach (Vector3 p in movePositions){
                lookup.SetTile(grid.WorldToCell(p), null);
            }
            movePositions.Clear();
        }
    }
}
