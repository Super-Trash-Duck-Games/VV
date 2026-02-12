using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAttackManager : MonoBehaviour
{
    [SerializeField] private FinalBossAttacks[] _attackSequence;
    private int _attackCount = 0;

    public Action<FinalBossAttacks> OnAttack;
    public int currentAttackCounter { get; private set; }

    [SerializeField] private float _attackDelay;
    [SerializeField] private Animator _bossAnim;
    private void Start()
    {
        StartCoroutine(StartAttackSequenceOnDelay());
    }

    private IEnumerator StartAttackSequenceOnDelay()
    {
        yield return new WaitForSeconds(_attackDelay);

        Attack(_attackSequence[0]);
    }

    private void Attack(FinalBossAttacks attack)
    {
        //_attacks[attack].Activate();
        OnAttack?.Invoke(attack);
    }

    public void SubscribeAttack()
    {
        currentAttackCounter++;
    }

    public void UnsubscribeAttack()
    {
        currentAttackCounter--;
        if (_attackCount == _attackSequence.Length)
        {
            Debug.Log("DeadBoss");
            return;
        }
        if (currentAttackCounter == 0)
        {
            Attack(GetNextAttack());
            //_bossAnim.SetBool("Attack", false);
        }
    }

    private FinalBossAttacks GetNextAttack()
    {
        _attackCount++;
        
        if (_attackSequence[_attackCount] == FinalBossAttacks.GemLaser)
            _bossAnim.SetBool("Attack", true);
        else
            _bossAnim.SetBool("Attack", false);
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