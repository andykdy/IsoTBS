using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdle : UnitState
{
    public UnitIdle(UnitEntity unit) : base(unit) {}


    public override void Start()
    {
        
    }

    public override void Update()
    {
    }

    public override void StartMove()
    {
        m_Unit.SetState(new Moving(m_Unit));
    }
}
