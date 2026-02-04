using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private AIEnemy _aie;
    //private List<Vector2> _waypoints;
    private int _currentWPIndex;
    private Rigidbody2D _rb2d;
    private EntityPackage _mp;
    private bool _goingRight = true;
    private bool _mirrorOnPatrol;
    public PatrolState(AIEnemy aie, Rigidbody2D rb2d, EntityPackage mp)
    {
        _aie = aie;
        //_waypoints = waypoints;
        _mp = mp;
        _rb2d = rb2d;
        _currentWPIndex = 0;

    }

    public override void OnDrawGizmos()
    {
    }

    private void OnPlayerSeen()
    {
        _aie.OnPlayerSeen = null;
        Debug.Log("player seen during Patrol");
        fsm.ChangeState(AIEnemiesStates.Attack);
    }

    public override void OnEnter()
    {
        _aie.OnPlayerSeen += OnPlayerSeen;
        _aie.currentState = AIEnemiesStates.Patrol;
        _aie.view.Move(true);
        _aie.Mirror(_mirrorOnPatrol);
        _aie.view.SkipMorph();
    }

    public override void OnExit()
    {
        _aie.OnPlayerSeen = null;
        _aie.view.Move(false);

    }

    public override void OnFixedUpdate()
    {
        GoToPosition(_aie.waypoints[_currentWPIndex]);
    }

    private void GoToPosition(Vector3 endPos) 
    {
        Vector2 dir = endPos - _aie.transform.position;
        dir.Normalize();
        //dir.y = 0;

        _goingRight = dir.x < 0;
        _mirrorOnPatrol = !_goingRight;

        if(Vector3.Distance(new Vector2( _aie.transform.position.x, _aie.transform.position.y), new Vector2(endPos.x, _aie.transform.position.y)) < .5f) 
        {
            _currentWPIndex++;
            if (_currentWPIndex >= _aie.waypoints.Count)
                _currentWPIndex = 0;

            fsm.ChangeState(AIEnemiesStates.PatrolPoint);
        }

        _rb2d.MovePosition(_rb2d.position += dir * _mp.speed * Time.deltaTime);
        //_rb2d.AddForce(_rb2d.position -= dir * _mp.speed * Time.deltaTime, ForceMode2D.Force);
    }

    private void ChangeCurrentWaypoint()
    {
        if (_currentWPIndex == 1) _currentWPIndex = 0;
        else if (_currentWPIndex == 0) _currentWPIndex = 1;

        fsm.ChangeState(AIEnemiesStates.PatrolPoint);
    }

    public override void OnUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            fsm.ChangeState(AIEnemiesStates.Dizzy);
        }
    }
}
