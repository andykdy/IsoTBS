using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerStateMachine : Singleton<ControllerStateMachine>{
    protected ControllerState m_State;

    public void SetState(ControllerState state)
    {
        m_State = state;
        m_State.Start();
    }
}
