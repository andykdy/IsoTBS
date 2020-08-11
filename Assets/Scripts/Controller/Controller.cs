using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine {
    [SerializeField] private Tilemap map;
    [SerializeField] private Grid grid;
    private Camera cam;
    [HideInInspector] public UnitEntity currUnit;

    private Vector3Int start;
    private Vector3Int end;

    private AStarTile startNode;
    private AStarTile endNode;
    
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

        if (Input.GetKeyDown(KeyCode.Space)){
            Pathfinding.Instance.FindPath(startNode, endNode);
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
                return "unit";
            }
            if (hit.transform.CompareTag("Map")){

                if (start == Vector3Int.zero){
                    start = grid.WorldToCell(mousePos);
                    startNode = map.GetTile<AStarTile>(start);
                    Debug.Log("Start set at... " + start.x + " , " + start.y + " , " + start.z);
                } else if (end == Vector3Int.zero){
                    end = grid.WorldToCell(mousePos);
                    endNode = map.GetTile<AStarTile>(end);
                    Debug.Log("End set at... " + end.x + " , " + end.y + " , " + end.z);
                }

                return "map";
            }
            else{
                currUnit = null;
                currUnit.SetSelectHighlight(true);
                return "";
            }
        }

        return "";
    }

    public void MoveUnit()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        currUnit.dest = mousePos;
        currUnit.SetSelectHighlight(false);
        currUnit = null;
    }
}
