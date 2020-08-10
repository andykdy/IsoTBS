using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class UnitEntity : MonoBehaviour
{
    public float Health => m_Health;
    public float MaxHealth => m_MaxHealth;

    public event Action m_onDeath = delegate { };

    protected bool m_IsDead;
    protected float m_Health;
    [SerializeField] protected float m_MaxHealth;
    [SerializeField] private Tilemap map;
    public Vector3 dest
    {
        get {return m_dest; }
        set {m_dest = value; }
    }

    private Vector3 m_dest;

    private void Awake()
    {
        Vector3Int gridPos = map.WorldToCell(transform.position);
        gridPos.z = 0;
        AStarTile tile = map.GetTile<AStarTile>(gridPos);
        tile.unit = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, 10f * Time.deltaTime);
    }
}
