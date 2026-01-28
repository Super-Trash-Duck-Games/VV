using UnityEngine;

public class CRayGunState : State
{
    private Cientist _cientific;
    private CData _data;
    private RayCannon _rayCannon;

    public CRayGunState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
        _rayCannon = _data.rayCannon;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.RayGunShoot;

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
