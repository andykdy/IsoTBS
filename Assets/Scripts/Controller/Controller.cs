using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Controller : ControllerStateMachine {
    [SerializeField] private Tilemap map;
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
    public void Select(){
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
        {
            if(hit.transform.CompareTag("Unit")){
                currUnit = hit.transform.gameObject.GetComponent<UnitEntity>();
                currUnit.SetSelectHighlight(true);
            }
            else{
                currUnit = null;
            }
        }
    }

    public void MoveUnit()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        currUnit.dest = mousePos;
        currUnit.SetSelectHighlight(false);
    }
}
