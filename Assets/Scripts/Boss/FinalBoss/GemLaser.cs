using UnityEngine;
using System;
using System.Collections;

public class GemLaser : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get; set; }
    [field: SerializeField] public bool primary { get ; set ; }
    public Action OnFinished { get; set; }

    [SerializeField] private Animator _anim;
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform[] _laserPoints;
    [SerializeField] private int _attackCounter;

    void Start()
    {
        _lr.positionCount = _laserPoints.Length * 2;

        int counter = -1;
        foreach (Transform t in _laserPoints)
        {
            counter++;
            _lr.SetPosition(counter, _origin.position);
            counter++;
            _lr.SetPosition(counter, t.position);
        }
    }

    void Update()
    {
        int counter = -1;
        foreach (Transform t in _laserPoints)
        {
            counter++;
            _lr.SetPosition(counter, _origin.position);
            counter++;
            _lr.SetPosition(counter, t.position);
        }
    }

    void IFinalBossAttack.Activate()
    {
        StartCoroutine(LaserShow());
    }

    private IEnumerator LaserShow()
    {
        AnimatorClipInfo clipInfo = _anim.GetCurrentAnimatorClipInfo(0)[0];
        yield return null;
        _attackCounter++;

        Finished();

    }

    public void Finished()
    {
        if (primary) OnFinished?.Invoke();
    }
}
