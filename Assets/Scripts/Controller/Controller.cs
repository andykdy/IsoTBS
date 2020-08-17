using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine{
    [SerializeField] private Tilemap lookup;
    [SerializeField] private Tilemap map;
    [SerializeField] private Grid grid;
    [SerializeField] private LookUpTile luTile;
    private Camera cam;
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
        else{Vector3 offsetUnit = currUnit.transform.position;
            offsetUnit.y -= 0.25f;
            lookup.SetTile(grid.WorldToCell(offsetUnit), luTile);
        }
    }
}
