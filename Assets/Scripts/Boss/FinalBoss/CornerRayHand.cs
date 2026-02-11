using System.Collections;
using UnityEngine;
using System;

public class CornerRayHand : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get ; set ; }
    Action IFinalBossAttack.OnFinished { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    [field: SerializeField] public bool primary { get ; set ; }

    public Action OnFinished;

    [Header("Laser")]
    [SerializeField] private Kumkum _kk;
    [SerializeField] private float _pointSpeed;
    [SerializeField] private Cannon _cannon;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private float _colliderDelay;
    [SerializeField] private float _shootTime;
    [SerializeField] private float _colliderGrowthSpeed;

    [Header("Positioning")]
    [SerializeField] private Transform _positionMarker;
    [SerializeField] private float _positioningSpeed;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Vector2 _oPos;

    void Start()
    {
        if (_kk == null) _kk = FindFirstObjectByType<Kumkum>();
        if (_cannon == null) _cannon = transform.GetComponentInChildren<Cannon>();
        _boxCollider.enabled = false;
        _oPos = transform.position;
    }



    public void Activate()
    {
        StartCoroutine(Position());
    }

    private IEnumerator Position()
    {
        while (transform.position.y > _positionMarker.position.y)
        {
            transform.position -= Vector3.up * Time.deltaTime * _positioningSpeed;
            transform.LookAt(_kk.transform.position);
            yield return null;
        }

        StartCoroutine(ActivateCollider());
    }

    private IEnumerator ActivateCollider()
    {
        float counter = 0;

        while (counter < _colliderDelay)
        {
            transform.LookAt(_kk.transform.position);
            counter += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_shootDelay);

        _boxCollider.enabled = true;

        _cannon.ActivateLaser();

        while (counter < _shootTime)
        {
            _boxCollider.size = new Vector2(_boxCollider.size.x + _colliderGrowthSpeed * Time.deltaTime, _boxCollider.size.y );
            counter += Time.deltaTime;
            yield return null;
        }

        _boxCollider.enabled = false;
        Deactivate();
    }

    public void Deactivate()
    {
        StartCoroutine(PositionBack());
        _cannon.DeactivateLaser();
    }

    private IEnumerator PositionBack()
    {
        while (transform.position.y < _oPos.y)
        {
            transform.position += Vector3.up * Time.deltaTime * _positioningSpeed;
            yield return null;
        }

        Finished();
    }

    public void Finished()
    {
        if (primary) OnFinished?.Invoke();
    }
}
