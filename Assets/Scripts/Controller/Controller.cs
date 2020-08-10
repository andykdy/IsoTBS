using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine {
    [SerializeField] private Material m_selected_mat;
    [SerializeField] private Material m_default_mat;
    [SerializeField] private Tilemap map;
    private Camera cam;
    public UnitEntity currUnit;
    
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
    public void SelectUnit(){
        Vector2 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Vector3Int gridPos = map.WorldToCell(mousePos);
        AStarTile tile = map.GetTile<AStarTile>(gridPos);
        if (tile != null){
    //            if (currUnit != null)
    //                currUnit.GetComponent<SpriteRenderer>().material = default_mat;
            currUnit = tile.unit;
            currUnit.dest = new Vector3(mousePos.x, mousePos.y, 2);
            //currUnit.GetComponent<SpriteRenderer>().material = selected_mat;
        }
    }
}
