using System.Collections;
using UnityEngine;

public class AcidMovement : MonoBehaviour
{
    [SerializeField] private float _acidSpeed;
    [SerializeField] private float _acidStartDelay;

    private bool _startMoving = false;

    private void Start()
    {
        StartCoroutine(MovementStartTimer());
    }

    private void Movement()
    {
        if (_startMoving == true) transform.position += Vector3.up * _acidSpeed * Time.deltaTime;
    }

    private IEnumerator MovementStartTimer()
    {
        yield return new WaitForSeconds(_acidStartDelay);
        _startMoving = true;
    }

    private void Update()
    {
        Movement();
    }
}
