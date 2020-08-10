using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : ControllerState
{
    public Idle(Controller controller) : base(controller) {}


    public override void Start()
    {

    }

    public override void Update()
    {
        // If click on character, move to selected
//        if (m_Controller)
//        {
//            m_Controller.SetState(new Charging(m_Turret));
//        }
    }

    public override void OnMouseClick()
    {
        m_Controller.SelectUnit();
        if (m_Controller.currUnit != null){
            m_Controller.SetState(new Selected(m_Controller));
        }
    }
}
