using UnityEngine;

public class CSpawnEnemyState : State
{
    private Cientist _cientific;
    private CData _data;

    public CSpawnEnemyState(Cientist cientific, CData data)
    {
        _cientific = cientific;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientific._currentState = CientistStates.SpawnEnemy;
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
