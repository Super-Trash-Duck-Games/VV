using System;
using UnityEngine;

public class PalmCrush : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get; set; }
    [field: SerializeField] public FinalBossAttackManager manager { get; set; }

    [SerializeField] private Animator _anim;
    void Start()
    {
        if (manager == null)
            manager = FindFirstObjectByType<FinalBossAttackManager>();
        manager.OnAttack += OnAttackCall;
        _anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        _anim.SetTrigger("Slam");
    }

    public void Finished()
    {
        //if (primary) OnFinished?.Invoke();
        manager.UnsubscribeAttack();
    }

    public void OnAttackCall(FinalBossAttacks currentAttack)
    {
        if (currentAttack != attack) return;
        manager.SubscribeAttack();
        Activate();
    }
}
