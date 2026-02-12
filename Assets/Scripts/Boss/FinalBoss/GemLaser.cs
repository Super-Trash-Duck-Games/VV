using UnityEngine;
using System;
using System.Collections;

public class GemLaser : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get; set; }
    [field: SerializeField] public FinalBossAttackManager manager { get; set; }

    [SerializeField] private Animator _anim;
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform[] _laserPoints;
    [SerializeField] private int _attackCounter;
    private bool _attackingInProgress;
    public Gem gem;
    void Start()
    {
        if (manager == null)
            manager = FindFirstObjectByType<FinalBossAttackManager>();

        if (gem == null) gem = GetComponent<Gem>();

        manager.OnAttack += OnAttackCall;

        _lr.positionCount = _laserPoints.Length * 2;
        _lr.enabled = false;
    }
    public void Activate()
    {
        StartCoroutine(LaserShow());
    }

    private IEnumerator LaserShow()
    {
        _attackCounter++;
        _anim.SetInteger("AttackCounter", _attackCounter);
        _lr.enabled = true;

        _attackingInProgress = true;

        while (_attackingInProgress)
        {
            int counter = -1;
            foreach (Transform t in _laserPoints)
            {
                counter++;
                _lr.SetPosition(counter, _origin.position);
                counter++;
                _lr.SetPosition(counter, t.position);
            }

            gem.UpdateDamageAmount(_attackCounter * .25f);

            yield return null;
        }

        _lr.enabled = false;

        manager.UnsubscribeAttack();
        _anim.SetInteger("AttackCounter", 0);

    }

    public void FinishAttack()
    {
        _attackingInProgress = false;
    }

    public void OnAttackCall(FinalBossAttacks currentAttack)
    {
        if (currentAttack != attack) return;
        manager.SubscribeAttack();
        Activate();
    }
}
