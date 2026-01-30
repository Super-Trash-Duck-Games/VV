using UnityEngine;

public class CRayGunState : State
{
    private Cientist _cientist;
    private CData _data;
    private RayCannon _rayCannon;

    public CRayGunState(Cientist cientific, CData data)
    {
        _cientist = cientific;
        _data = data;
        _rayCannon = _data.rayCannon;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientist._currentState = CientistStates.RayGunShoot;

        if (!_cientist.CheckCurrentPosition())
        {
            fsm.ChangeState(CientistStates.Run);
            return;
        }


        if (_cientist.transform.position.x > _data.rightMost.position.x)
            _rayCannon.Attack(true);
        else if (_cientist.transform.position.x < _data.leftMost.position.x)
            _rayCannon.Attack(false);

        _data.ff.ActivateForceField();

        _data.rayCannon.AttackFinished += OnAttackFinished;

    }

    public override void OnExit()
    {
        _data.rayCannon.AttackFinished -= OnAttackFinished;
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

    private void OnAttackFinished()
    {
        fsm.ChangeState(_cientist.GetNextState());
    }
}
