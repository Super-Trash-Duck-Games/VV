using System;
using System.Collections;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    [SerializeField] protected Transform _eyePivot;
    [SerializeField] protected SpriteRenderer _fov;
    [SerializeField] protected Color _passiveFovColor;
    [SerializeField] protected Color _agressiveFovColor;
    protected Coroutine _watchingKK;
    [SerializeField] protected float _followTime;
    protected bool _isOnFov;
    protected bool _isFollowing;

   
    [SerializeField] protected GameObject _staticEye;
    [SerializeField] protected GameObject _watchingEye;

    public Action<bool> OnActivation;

    void Start()
    {
    }

    protected virtual IEnumerator WatchKumKum()
    {
        var kk = FindFirstObjectByType<Kumkum>();
        while (_isFollowing)
        {
            _eyePivot.LookAt(kk.transform.position);
            yield return null;
        }
    }

    public virtual void Enter()
    {
        Activate();
    }

    public virtual void Exit()
    {
        _isOnFov = false;
        StartCoroutine(KeepFollowing());
    }

    protected virtual void Activate()
    {
        _staticEye.SetActive(false);
        _watchingEye.SetActive(true);
        _isOnFov = true;
        _isFollowing = true;
        _fov.color = _agressiveFovColor;
        _watchingKK = StartCoroutine(WatchKumKum());
        OnActivation?.Invoke(true);


    }

    protected virtual void Deactivate()
    {
        _fov.color = _passiveFovColor;
        _isFollowing = false;
        _staticEye.SetActive(true);
        _watchingEye.SetActive(false);
        OnActivation?.Invoke(false);
    }

    private IEnumerator KeepFollowing()
    {
        var timer = 0f;
        while (timer < _followTime)
        {
            timer += Time.deltaTime;

            if (_isOnFov) yield break;

            yield return null;
        }
        Deactivate();
    }
}
