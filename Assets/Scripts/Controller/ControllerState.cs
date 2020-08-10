public abstract class ControllerState
{
    protected Controller m_Controller;

    public ControllerState(Controller controller)
    {
        m_Controller = controller;
    }

    public virtual void Start() {}

    public virtual void Update() {}
    public virtual void OnMouseClick() {}
}
