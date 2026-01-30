using System;
using System.Collections;
using UnityEngine;

public class RayCannon : MonoBehaviour
{
    [SerializeField] private Transform _start, _end;
    Vector2 _originalEndPos;
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private float _cannonSpeed;
    [SerializeField] private float _tolerance;
    private Cannon _cannon;
    [SerializeField] private float _startDelay;
    public Action AttackFinished;

    void Start()
    {
        _cannon = GetComponentInChildren<Cannon>();
        _originalEndPos = _end.position;
        //StartCoroutine(GetOnPosition(true));
    }

    public void Attack(bool fromRight)
    {
        StartCoroutine(GetOnPosition(fromRight));
    }

    private IEnumerator GetOnPosition(bool fromRight)
    {
        if (fromRight)
            _end.position = new Vector3(_originalEndPos.x, _originalEndPos.y);
        else
            _end.position = new Vector3(_originalEndPos.x * -1, _originalEndPos.y);

        //_cannon.transform.position = _end.position;
        _cannon.transform.position = new Vector3(_end.transform.position.x, _cannon.transform.position.y);

        while (_cannon.transform.position.y > _start.position.y)
        {
            _cannon.transform.position = Vector3.MoveTowards(_cannon.transform.position, new Vector2(_cannon.transform.position.x, _start.position.y), _cannonSpeed * Time.deltaTime);
            _lr.SetPosition(0, new Vector2(_start.transform.position.x, _cannon.transform.position.y));
            _lr.SetPosition(1, new Vector2(_end.transform.position.x, _cannon.transform.position.y));
            yield return null;
        }


        StartCoroutine(ToMiddle());
    }

    private IEnumerator ToMiddle()
    {
        _lr.SetPosition(0, _start.position);
        _lr.SetPosition(1, _end.position);

        yield return new WaitForSeconds(_startDelay);
        _cannon.ActivateLaser();


        while (Vector2.Distance(_cannon.transform.position, _start.position) > _tolerance)
        {
            _cannon.transform.position = Vector2.MoveTowards(_cannon.transform.position, _start.position, _cannonSpeed * Time.deltaTime);
            yield return null;
        }

        _cannon.DeactivateLaser();

        StartCoroutine(LeavePosition());
    }

    private IEnumerator LeavePosition()
    {
        while (_cannon.transform.position.y < 15)
        {
            _cannon.transform.position = Vector3.MoveTowards(_cannon.transform.position, new Vector2(_cannon.transform.position.x, 16), _cannonSpeed * Time.deltaTime);
            _lr.SetPosition(0, new Vector2(_start.transform.position.x, _cannon.transform.position.y));
            _lr.SetPosition(1, new Vector2(_end.transform.position.x, _cannon.transform.position.y));
            yield return null;
        }

        AttackFinished?.Invoke();
    }

}
