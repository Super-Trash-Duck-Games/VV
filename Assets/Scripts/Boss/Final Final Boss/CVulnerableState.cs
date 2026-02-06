using UnityEngine;

public class CVulnerableState : State
{
    private Cientist _cientific;
    private CData _data;
    float _vulnerableTime;

    public CVulnerableState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.Vulnerable;

        _vulnerableTime = _data.vulnerableTime;


        _data.ff.DeactivateForceField();
    }

    public override void OnExit()
    {
        _data.ff.ActivateForceField();
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //_vulnerableTime = 0;
            fsm.ChangeState(_cientific.GetNextState());
            _data.lives--;
            _cientific.anim.SetTrigger("GetHit");
        }
    }

    public override void OnUpdate()
    {
        _vulnerableTime -= Time.deltaTime;
        if (_vulnerableTime < 0)
            fsm.ChangeState(_cientific.GetLastAttackState());
    }
}
