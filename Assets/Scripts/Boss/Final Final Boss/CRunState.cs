using System;
using UnityEngine;

public class CRunState : State
{
    private Cientist _cientific;
    private CData _data;
    private Action _runDirection;
    public CRunState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.Run;

        var rand = UnityEngine.Random.Range(0, 2);
        if (rand == 0) _runDirection += RunRight;
        else _runDirection += RunLeft;
    }

    public override void OnExit()
    {
        _data.rb2d.linearVelocity = Vector2.zero;
        _runDirection = null;
    }

    public override void OnFixedUpdate()
    {
        _runDirection();
        if (_data.rb2d.linearVelocity.x > _data.speedCap) _data.rb2d.linearVelocity = new Vector2(_data.speedCap, _data.rb2d.linearVelocity.y);
    }


    private void RunRight()
    {
        if (_cientific.transform.position.x < _data.rightMost.position.x)
            _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime, ForceMode2D.Force);
        else
            fsm.ChangeState(_cientific.GetCurrentState());
    }

    private void RunLeft()
    {
        if (_cientific.transform.position.x > _data.leftMost.position.x)
            _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime * -1, ForceMode2D.Force);
        else
            fsm.ChangeState(_cientific.GetCurrentState());
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
    }
}
