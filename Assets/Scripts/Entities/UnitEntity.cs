using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class UnitEntity : UnitStateMachine
{
    public float Health => m_Health;
    public float MaxHealth => m_MaxHealth;

    public event Action m_onDeath = delegate { };

    protected bool m_IsDead;
    protected float m_Health;
    [SerializeField] protected float m_MaxHealth;
    [SerializeField] private Tilemap map;
    [SerializeField] private Material standard;
    [SerializeField] private Material highlight;
    [SerializeField] private List<Node> path;
    public Vector3 dest
    {
        get {return m_dest; }
        set
        {
            m_dest = value;
        }
    }

    private Vector3 m_dest;

    private void Awake()
    {
        SetState(new UnitIdle(this));
        m_State.Start(); //This is... ??? 
        dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_State == null) return;
        m_State.Update();
    }

    public void SetSelectHighlight(bool isSet)
    {
        gameObject.GetComponent<SpriteRenderer>().material = isSet ? highlight : standard;
    }

    public void SetPath(List<Node> givenPath)
    {
        path = givenPath;
        if (path != null){
            path.RemoveAt(0);
            dest = map.layoutGrid.CellToWorld(path[0].nodePos);
            m_State.StartMove();
        }
        else{
            Debug.Log("Returned path was null");
        }
    }

    public void MoveToDest()
    {
        if (Vector3.Distance(transform.position, dest) < 0.01f){
            if (path.Count == 1){
                path[0].cameFromNode = null;
                m_State.StopMove();
            }
            else{
                path.RemoveAt(0);
                dest = map.layoutGrid.CellToWorld(path[0].nodePos);
                path[0].cameFromNode = null;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, dest, 10f * Time.deltaTime);
        }
    }
}
