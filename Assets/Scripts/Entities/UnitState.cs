public abstract class UnitState
{
    protected UnitEntity m_Unit;

    public UnitState(UnitEntity unit)
    {
        m_Unit = unit;
    }

    public virtual void Start() {}
    public virtual void Update() {}
    
}
