using System;
using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private ParticleSystem _ps;
    [SerializeField] private float _growthSpeed;
    private bool _shooting;
    [SerializeField] private BoxCollider2D _collider;
    void Start()
    {
        _collider ??= GetComponent<BoxCollider2D>();
        _collider.enabled = false;
    }

    public void ActivateLaser()
    {
        _shooting = true;
        StartCoroutine(Laser(true));
        StartCoroutine(FollowCannon());
        _collider.enabled = true;
    }

    public void DeactivateLaser()
    {
        _shooting = false;
        _collider.enabled = false;

        StartCoroutine(Laser(false));
    }

    private IEnumerator Laser(bool onOff)
    {

        if (onOff)
        {
            _ps.Play();
            while (_lr.endWidth < 1)
            {
                _lr.startWidth += Time.deltaTime * _growthSpeed;
                yield return null;
            }
        }
        else
        {
            _ps.Stop();
            while (_lr.endWidth > 0)
            {
                _lr.startWidth -= Time.deltaTime * _growthSpeed;
                yield return null;
            }
        }
    }

    private IEnumerator FollowCannon()
    {
        while (_shooting)
        {
            _lr.SetPosition(0, transform.position);
            _lr.SetPosition(1, transform.position + Vector3.down * 20);
            yield return null;
        }
    }
}
