using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class Moving : UnitState
{
    public Moving(UnitEntity unit) : base(unit) {}

    public override void Start()
    {
        m_Unit.SetSelectHighlight(false);
    }

    public override void Update()
    {
        m_Unit.MoveToDest();
    }
}
