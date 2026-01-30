using System;
using UnityEngine;

public class CRunState : State
{
    private Cientist _cientist;
    private CData _data;
    private Action _runDirection;
    public CRunState(Cientist cientific, CData data)
    {
        _cientist = cientific;
        _data = data;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientist._currentState = CientistStates.Run;

        switch (_data.targetPosition)
        {
            case -1:
                _runDirection += RunLeft;
                break;
            case 0:
                _runDirection += RunMiddle;
                break;
            case 1:
                _runDirection += RunRight;
                break;
        }

        _cientist.anim.SetBool("Run", true);
    }

    public override void OnExit()
    {
        _data.rb2d.linearVelocity = Vector2.zero;
        _runDirection = null;
        _cientist.anim.SetBool("Run", false);
    }

    public override void OnFixedUpdate()
    {
        _runDirection();
        if (_data.rb2d.linearVelocity.x > _data.speedCap) _data.rb2d.linearVelocity = new Vector2(_data.speedCap, _data.rb2d.linearVelocity.y);
    }


    private void RunRight()
    {
        if (_cientist.transform.position.x < _data.rightMost.position.x)
            _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime, ForceMode2D.Force);
        else
            fsm.ChangeState(_cientist.GetCurrentState());
    }

    private void RunLeft()
    {
        if (_cientist.transform.position.x > _data.leftMost.position.x)
            _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime * -1, ForceMode2D.Force);
        else
            fsm.ChangeState(_cientist.GetCurrentState());
    }

    private void RunMiddle()
    {
        if (Mathf.Abs(_cientist.transform.position.x - _data.middle.position.x) > _data.targetTolerance)
        {
            if (_cientist.transform.position.x > _data.middle.position.x)
                _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime, ForceMode2D.Force);
            else if (_cientist.transform.position.x < _data.middle.position.x)
                _data.rb2d.AddForce(Vector2.right * _data.runSpeed * 100 * Time.deltaTime, ForceMode2D.Force);
        }
        else
            fsm.ChangeState(_cientist.GetCurrentState());
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
    }
}
