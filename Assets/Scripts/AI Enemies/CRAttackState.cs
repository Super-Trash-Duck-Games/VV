using UnityEngine;

public class CRAttackState : AttackState
{
    public CRAttackState(AIEnemy aie) : base(aie)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _aie._view.Attack();
    }
}
