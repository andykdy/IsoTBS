using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class Selected : ControllerState
{
    public Selected(Controller controller) : base(controller) {}


    public override void Start()
    {
    }

    public override void Update()
    {
        
    }
    
    public override void OnMouseClick()
    {
        if (m_Controller.Select() == "unit"){
            m_Controller.SetState(new Selected(m_Controller));
        }
        else{
            m_Controller.MoveUnit();
            m_Controller.SetState(new Idle(m_Controller));
        }
    }
}
