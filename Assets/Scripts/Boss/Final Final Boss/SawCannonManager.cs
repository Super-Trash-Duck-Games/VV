using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SawCannonManager : MonoBehaviour
{

    [SerializeField] private SawCannon[] _sawCannons;
    [SerializeField] private float _delay;
    [SerializeField] private int[] _attackSequence;
    [SerializeField] private Transform _outPlacer, _inPlacer;
    [SerializeField] private float _speed;
    [SerializeField] private bool _horizontal;

    public void ShootSequence()
    {
        if (_horizontal)
            StartCoroutine(AttackSequenceHorizontal(_attackSequence));
        else
            StartCoroutine(AttackSequenceVertical(_attackSequence));
    }

    private IEnumerator AttackSequenceHorizontal(int[] sequence)
    {
        int counter = 0;

        while (counter < sequence.Length)
        {
            while (_sawCannons[sequence[counter]].transform.position.x < _inPlacer.position.x)
            {
                _sawCannons[sequence[counter]].transform.position += transform.right * Time.deltaTime * _speed;
                yield return null;
            }

            _sawCannons[sequence[counter]].Shoot();

            while (_sawCannons[sequence[counter]].transform.position.x > _outPlacer.position.x)
            {
                _sawCannons[sequence[counter]].transform.position -= transform.right * Time.deltaTime * _speed;
                yield return null;
            }

            yield return new WaitForSeconds(_delay);
            counter++;
        }
    }

    private IEnumerator AttackSequenceVertical(int[] sequence)
    {
        int counter = 0;

        while (counter < sequence.Length)
        {
            while (_sawCannons[sequence[counter]].transform.position.y > _inPlacer.position.y)
            {
                _sawCannons[sequence[counter]].transform.position -= transform.up * Time.deltaTime * _speed;
                yield return null;
            }

            _sawCannons[sequence[counter]].Shoot();

            while (_sawCannons[sequence[counter]].transform.position.y < _outPlacer.position.y)
            {
                _sawCannons[sequence[counter]].transform.position += transform.up * Time.deltaTime * _speed;
                yield return null;
            }

            yield return new WaitForSeconds(_delay);
            counter++;
        }
    }
}
