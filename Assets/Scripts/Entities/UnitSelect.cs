using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : UnitState
{
    public UnitSelect(UnitEntity unit) : base(unit) {}

    public override void Start()
    {
        m_Unit.SetSelectHighlight(true);
    }

    public override void Update()
    {
    }
}
