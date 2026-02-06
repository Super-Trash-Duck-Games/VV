using UnityEngine;

public class CSpawnEnemyState : State
{
    private Cientist _cientist;
    private CData _data;

    public CSpawnEnemyState(Cientist cientist, CData data)
    {
        _cientist = cientist;
        _data = data;
    }
    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _cientist._currentState = CientistStates.SpawnEnemy;

        if (_data.targetPosition == 0) _cientist.SelectRandomPosition();

        if (!_cientist.CheckCurrentPosition())
        {
            fsm.ChangeState(CientistStates.Run);
            return;
        }

        switch (_data.targetPosition)
        {
            case -1:
                if (_cientist.transform.position.x < _data.leftMost.position.x)
                {
                    _data.bringer.Bring(false);
                    _data.boxSpawner.CreateBoxWall(false);
                }

                break;
            case 1:
                if (_cientist.transform.position.x > _data.rightMost.position.x)
                {
                    _data.bringer.Bring(true);
                    _data.boxSpawner.CreateBoxWall(true);
                }

                break;
        }
        _cientist.anim.SetBool("ButtonActive", true);

        _data.vulnerableTime = 90;

        fsm.ChangeState(_cientist.GetNextState());
    }

    public override void OnExit()
    {
        _cientist.anim.SetBool("ButtonActive", false);
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
