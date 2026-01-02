using UnityEngine;

public class DizzyState : State
{
    private AIEnemy _aie;
    private float _dizzyTime;
    private float _dizzyTimer;
    public AIEnemyView _view;
    public DizzyState(AIEnemy aie, float dizzy, AIEnemyView view)
    {
        _aie = aie;
        _dizzyTime = dizzy;
        _view = view;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _dizzyTimer = 0;
        _view.Dizzy(true);
    }

    public override void OnExit()
    {
        _view.Dizzy(false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
    }

    public override void OnUpdate()
    {
        _dizzyTimer += Time.deltaTime;
        if (_dizzyTimer > _dizzyTime)
            fsm.ChangeState(AIEnemiesStates.PatrolPoint);
    }
}
