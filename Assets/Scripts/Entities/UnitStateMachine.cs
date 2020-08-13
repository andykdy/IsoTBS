using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStateMachine : Singleton<UnitStateMachine>{
    protected UnitState m_State;

    public void SetState(UnitState state)
    {
        m_State = state;
        m_State.Start();
    }
}
