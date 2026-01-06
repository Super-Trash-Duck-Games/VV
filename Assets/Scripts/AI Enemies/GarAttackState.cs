using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GarAttackState : AttackState
{
    private AIGargEnemy _garg;
    private float _cooldown;
    public GarAttackState(AIGargEnemy aie, float cooldown) : base(aie)
    {
        _garg = aie;
        _cooldown = cooldown;

    }

    public override void OnEnter()
    {
        base.OnEnter();

        //_garg.view.Attack();
        _garg.Punch();

        _garg.StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        float timer = 0;
        while (timer < _cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        fsm.ChangeState(AIEnemiesStates.Patrol);
    }
}
