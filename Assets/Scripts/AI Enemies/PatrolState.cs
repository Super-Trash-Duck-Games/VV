using UnityEngine;

public class PatrolState : State
{
    private AIEnemy _aie;
    private Transform[] _waypoints;
    private int _currentWPIndex;
    private Rigidbody2D _rb2d;
    private EntityPackage _mp;
    private bool _goingRight = true;
    private bool _mirrorOnPatrol;
    public PatrolState(AIEnemy aie, Transform[] waypoints, Rigidbody2D rb2d, EntityPackage mp)
    {
        _aie = aie;
        _waypoints = waypoints;
        _mp = mp;
        _rb2d = rb2d;
        _currentWPIndex = 0;

    }

    public override void OnDrawGizmos()
    {
    }

    private void OnPlayerSeen()
    {
        _aie.OnPlayerSeen -= OnPlayerSeen;
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
        //_aie.OnPlayerSeen -= OnPlayerSeen;
    }

    public override void OnFixedUpdate()
    {
        Vector2 moveDir;

        if (_goingRight)
        {
            moveDir = Vector2.right;
            if (_rb2d.position.x > _waypoints[_currentWPIndex].position.x)
            {
                _mirrorOnPatrol = true;
                _goingRight = false;
                ChangeCurrentWaypoint();
            }
        }
        else
        {
            moveDir = Vector2.right * -1;
            if (_rb2d.position.x < _waypoints[_currentWPIndex].position.x)
            {
                _mirrorOnPatrol = false;
                _goingRight = true;
                ChangeCurrentWaypoint();
            }
        }

        moveDir.y = 0;
        _rb2d.MovePosition(_rb2d.position + moveDir * _mp.speed * Time.deltaTime);



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
