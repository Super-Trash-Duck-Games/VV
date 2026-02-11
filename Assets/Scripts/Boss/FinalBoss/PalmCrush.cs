using System;
using UnityEngine;

public class PalmCrush : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get; set; }
    public Action OnFinished { get ; set ; }
    [field: SerializeField] public bool primary { get; set; }

    [SerializeField] private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Activate()
    {
        _anim.SetTrigger("Slam");
    }

    public void Finished()
    {
        if (primary) OnFinished?.Invoke();
    }
}
