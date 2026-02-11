using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAttackManager : MonoBehaviour
{
    //[SerializeField] private CornerRayHand[] _cornerHands;
    //[SerializeField] private FistSlam[] _fists;
    [SerializeField] private GameObject[] _finalBossAttacks;
    private Dictionary<FinalBossAttacks[], IFinalBossAttack> _attacks = new Dictionary<FinalBossAttacks[], IFinalBossAttack>();
    [SerializeField] private FinalBossAttacks[] _attackSequence;
    private int _attackCount = 0;

    private void Start()
    {
        foreach (var attack in _finalBossAttacks)
        {
            IFinalBossAttack a = attack.GetComponent<IFinalBossAttack>();
            _attacks.Add(a.attack, a);
            if (a.primary) a.OnFinished += OnAttackFinished;
        }

        for (int i = 0; i < _finalBossAttacks.Length; i++)
        {
            IFinalBossAttack a = _finalBossAttacks[i].GetComponent<IFinalBossAttack>();
            _attacks.Add(a.attack, a);
            if (a.primary) a.OnFinished += OnAttackFinished;
        }

        //_fists[0].OnFinished += OnAttackFinished;
        //_cornerHands[0].OnFinished += OnAttackFinished;
        Attack(_attackSequence[0]);
    }

    private void OnAttackFinished()
    {
        Attack(GetNextAttack());
    }

    private void Attack(FinalBossAttacks attack)
    {
        _attacks[attack].Activate();
    }

    private FinalBossAttacks GetNextAttack()
    {
        _attackCount++;
        return _attackSequence[_attackCount];
    }

}

public enum FinalBossAttacks
{
    FistSlam,
    PalmCrush,
    FingerRay,
    GemLaser
}