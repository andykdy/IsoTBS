﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdle : UnitState
{
    public UnitIdle(UnitEntity unit) : base(unit) {}


    public override void Start()
    {
        m_Unit.SetSelectHighlight(false);
    }

    public override void Update()
    {
    }
}
