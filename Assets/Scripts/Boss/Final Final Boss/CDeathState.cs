using UnityEngine;

public class CDeathState : State
{
    private Cientist _cientific;
    private CData _data;

    public CDeathState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.Death;
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
    }
}
