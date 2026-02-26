using System;
using System.Collections;
using UnityEngine;

public class ZapMachine : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _onContact;
    [SerializeField] private Transform _door;
    [SerializeField] private float _doorLiftSpeed;
    private float _oDoorYPos;
    [SerializeField] private float _doorMaxHeight;
    public Action<bool> doorOpen;
    void Start()
    {
        if (_anim == null) _anim = GetComponent<Animator>();

        _oDoorYPos = _door.transform.position.y;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Zap>(out Zap zap))
        {
            _onContact = true;
            if (zap.electrified)
                StartCoroutine(LiftDoor(zap));
            else
                StartCoroutine(WaitForElectricity(zap));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Zap>(out Zap zap))
        {
            _onContact = false;
        }
    }

    private IEnumerator WaitForElectricity(Zap zap)
    {
        while (_onContact)
        {
            if (zap.electrified)
            {
                StartCoroutine(LiftDoor(zap));
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator LiftDoor(Zap zap)
    {
        _anim.SetBool("Red", true);
        doorOpen?.Invoke(true);
        while (_onContact && zap.electrified)
        {
            if (_door.transform.position.y < _doorMaxHeight)
                _door.transform.position += Vector3.up * Time.deltaTime * _doorLiftSpeed * 5;
            yield return null;

        }


        _anim.SetBool("Red", false);
        while (_door.transform.position.y > _oDoorYPos)
        {
            _door.transform.position -= Vector3.up * Time.deltaTime * _doorLiftSpeed;
            yield return null;
        }
        if (_onContact)
            StartCoroutine(WaitForElectricity(zap));
        doorOpen?.Invoke(false);
    }
}
