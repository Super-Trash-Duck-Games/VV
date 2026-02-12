using System;
using UnityEngine;

public interface IFinalBossAttack
{
    public FinalBossAttacks attack { get; set; }
    public void Activate();

    public FinalBossAttackManager manager {get; set;}
    public void OnAttackCall(FinalBossAttacks currentAttack);
}
