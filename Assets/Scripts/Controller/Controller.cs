using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine {
    [SerializeField] private Tilemap map;
    [SerializeField] private Grid grid;
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
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Unit")){
                if (currUnit != null)
                    currUnit.SetSelectHighlight(false);
                currUnit = hit.transform.gameObject.GetComponent<UnitEntity>();
                currUnit.SetSelectHighlight(true);
                Debug.Log("Unit Selected");
                return "unit";
            }
            if (hit.transform.CompareTag("Map")){
                Debug.Log("Map Selected");
                return "map";
            }
            else{
                currUnit = null;
                Debug.Log("You hit nothing");
                currUnit.SetSelectHighlight(true);
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
        start = grid.WorldToCell(currUnit.transform.position);
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

        currUnit.SetSelectHighlight(false);
        currUnit = null;
    }
}
