using System.Collections;
using UnityEngine;

public class CornerRayHand : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private Kumkum _kk;
    [SerializeField] private float _pointSpeed;
    [SerializeField] private Cannon _cannon;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private float _colliderDelay;
    [SerializeField] private float _shootTime;
    private bool _activated;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Activate();
        }
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
            counter += Time.deltaTime;
            yield return null;
        }
        _activated = false;

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
    }
}
