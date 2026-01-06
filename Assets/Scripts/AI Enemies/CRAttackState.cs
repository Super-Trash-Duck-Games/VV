using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CRAttackState : AttackState
{
    AICrocoEnemy _croco;
    private float _coolDown;
    private Coroutine _cooldown;
    public CRAttackState(AICrocoEnemy aie, float cooldown) : base(aie)
    {
        _croco = aie;
        _coolDown = cooldown;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _croco.view.Attack();

        Debug.Log("Enter attack state");

        var bullet = _croco.Shoot();
        bullet.TargetPlayer();

        _cooldown = _croco.StartCoroutine(Cooldown());
    }


    private IEnumerator Cooldown()
    {
        float timer = 0;
        while (timer < _coolDown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        fsm.ChangeState(AIEnemiesStates.Patrol);
    }
}
