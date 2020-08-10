using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    [SerializeField] private Material standard;
    [SerializeField] private Material highlight;
    public Vector3 dest
    {
        get {return m_dest; }
        set
        {
            Vector3 isoMatch = value;
            isoMatch.z = -1.5f;
            m_dest = isoMatch;
        }
    }

    private Vector3 m_dest;

    private void Awake()
    {
        m_dest = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, dest, 10f * Time.deltaTime);
    }

    public void SetSelectHighlight(bool isSet)
    {
        gameObject.GetComponent<SpriteRenderer>().material = isSet ? highlight : standard;
    }
}
