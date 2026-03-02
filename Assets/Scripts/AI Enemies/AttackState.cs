using UnityEngine;
using System.Collections;

public class AttackState : State
{
    protected AIEnemy _aie;
    protected float _cooldown;
    protected bool _cancelPatrol;
    public AttackState(AIEnemy aie, float cooldown)
    {
        _aie = aie;
        _cooldown = cooldown;
    }

    public AttackState(AIGargEnemy aie)
    {
        _aie = aie;
    }

    public override void OnDrawGizmos()
    {
    }

    public override void OnEnter()
    {
        _aie.currentState = AIEnemiesStates.Attack;

        _aie.Attack();
        _cancelPatrol = false;
        _aie.StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown()
    {
        float timer = 0;
        while (timer < _cooldown)
        {
            timer += Time.deltaTime;
            if (_cancelPatrol)
            {
                yield break;
            }
            Debug.Log(timer);
            yield return null;
        }
        fsm.ChangeState(AIEnemiesStates.Patrol);
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            fsm.ChangeState(AIEnemiesStates.Dizzy);
            _cancelPatrol = true;
        }
    }

    public override void OnUpdate()
    {
    }
}
