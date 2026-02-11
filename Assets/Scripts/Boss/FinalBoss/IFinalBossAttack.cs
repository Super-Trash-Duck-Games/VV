using System;
using UnityEngine;

public interface IFinalBossAttack
{
    public FinalBossAttacks attack { get; set; }
    public bool primary { get; set; }
    public void Activate();
    public Action OnFinished { get; set; }
    public void Finished();
}
