using System.Collections;
using UnityEngine;
using System;

public class FistSlam : MonoBehaviour, IFinalBossAttack
{
    [field: SerializeField] public FinalBossAttacks attack { get; set; }
    [field: SerializeField] public bool primary { get ; set ; }
    public Action OnFinished { get; set; }


    [SerializeField] private Transform _left, _right;
    [SerializeField] private float _speed;
    private Vector2 _oPos;
    [SerializeField] private Kumkum _kk;
    [SerializeField] private float _homingTime;
    [SerializeField] private float _beforeSlamDelay, _afterSlamDelay;

    void Start()
    {
        _oPos = transform.position;

        if (_kk == null) _kk = FindFirstObjectByType<Kumkum>();
    }

    public void Activate()
    {
        StartCoroutine(InitiateAttack());
    }

    private IEnumerator InitiateAttack()
    {
        while (transform.position.y > _left.position.y)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - _speed * Time.deltaTime);
            yield return null;
        }

        float counter = 0;

        while (counter < _homingTime)
        {
            counter += Time.deltaTime;
            if (_kk.transform.position.x > _left.position.x && _kk.transform.position.x < _right.position.x)
            {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, _kk.transform.position.x, _speed * Time.deltaTime), transform.position.y);
            }
            yield return null;
        }

        while (transform.position.y < _left.position.y + 3)
        {
            transform.position += Vector3.up * _speed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_beforeSlamDelay);

        while (transform.position.y > 0)
        {
            transform.position -= Vector3.up * _speed * 15 * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_afterSlamDelay);

        while (Vector2.Distance(transform.position, _oPos) > .1f)
        {
            transform.position = Vector2.Lerp(transform.position, _oPos, _speed * Time.deltaTime);
            yield return null;
        }

        OnFinished?.Invoke();

    }

    public void Finished()
    {
        if (primary) OnFinished?.Invoke();
    }
}
