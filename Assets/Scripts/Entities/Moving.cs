using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class Moving : UnitState
{
    public Moving(UnitEntity unit) : base(unit) {}

    public Vector3 target;

    public override void Start()
    {
    }

    public override void Update()
    {
        m_Unit.MoveToDest();
    }

    public override void StopMove()
    {
        m_Unit.SetState(new UnitIdle(m_Unit));
    }
}
