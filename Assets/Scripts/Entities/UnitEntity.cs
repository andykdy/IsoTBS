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
    public int TravelPoints;

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
        m_State.Start(); //This is... ??? calling start on an awake??
        dest = transform.position;
        TravelPoints = 9; // Placeholder value... Should be different for each unit
    }

    // Update is called once per frame
    void Update()
    {
        m_State?.Update(); // If m_State isn't null, update
    }

    public void SetSelectHighlight(bool isSet)
    {
        gameObject.GetComponent<SpriteRenderer>().material = isSet ? highlight : standard;
    }

    public void SetPath(List<Node> givenPath)
    {
        path = givenPath;
        if (path != null){
            Debug.Log("Path was found");
            dest = path[0].transform.position;
            SetState(new Moving(this));
        }
        else{
            SetState(new UnitIdle(this));
            Debug.Log("Returned path was null");
        }
    }

    public void MoveToDest()
    {
        if (Vector3.Distance(transform.position, dest) < 0.01f){
            if (path.Count == 1){
                Debug.Log("Moving complete. Unit set to idle");
                SetState(new UnitIdle(this));
            }
            else{
                path.RemoveAt(0);
                dest = path[0].transform.position;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, dest, 10f * Time.deltaTime);
        }
    }
}
