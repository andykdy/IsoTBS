using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class Idle : ControllerState
{
    public Idle(Controller controller) : base(controller) {}


    public override void Start()
    {
        m_Controller.InitMovePos();
    }

    public override void Update()
    {
        
    }

    public override void OnMouseClick()
    {
        m_Controller.Select();
        if (m_Controller.currUnit != null){
            m_Controller.SetState(new Selected(m_Controller));
        }
    }
}
