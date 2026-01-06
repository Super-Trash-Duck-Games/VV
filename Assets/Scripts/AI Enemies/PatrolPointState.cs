using UnityEngine;

public class PatrolPointState : State
{
    private AIEnemy _aie;
    private float _waitTime;
    private float _counter;
    public PatrolPointState(AIEnemy aie, float waitTime)
    {
        _aie = aie;
         _waitTime = waitTime;
    }

    public override void OnDrawGizmos()
    {
    }

    private void OnPlayerSeen()
    {
        fsm.ChangeState(AIEnemiesStates.Attack);
        _aie.OnPlayerSeen -= OnPlayerSeen;
        Debug.Log("player seen during PatrolPoint");
    }

    public override void OnEnter()
    {
        _aie.OnPlayerSeen += OnPlayerSeen;
        _aie.currentState = AIEnemiesStates.PatrolPoint;
        _counter = _waitTime;
        _aie.view.Move(false);

    }

    public override void OnExit()
    {
        //_aie.OnPlayerSeen -= OnPlayerSeen;

    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            fsm.ChangeState(AIEnemiesStates.Dizzy);
        }
    }

    public override void OnUpdate()
    {
        _counter -= Time.deltaTime;

        if (_counter < 0)
        {
            fsm.ChangeState(AIEnemiesStates.Patrol);
        }
    }
}
