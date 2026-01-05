using UnityEngine;

public class AttackState : State
{
    protected AIEnemy _aie;
    public AttackState(AIEnemy aie)
    {
        _aie = aie;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _aie.currentState = AIEnemiesStates.Attack;
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
