using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [Header("Movement")]
    [field: SerializeField] public float Speed { get; private set; }
    [SerializeField] float _levitateFrequency;
    float _xOffset;
    float _xpos;
    float _yOriginPos;

    [Header("Fall Attack")]
    [SerializeField] private float _fallAttackDelay;
    [SerializeField] private float _fallAttackSpeed;
    [SerializeField] private float _fallHangTime;
    [SerializeField] private float _yMin;
    [SerializeField] private float _yMax;
    private float _originalY;
    private Coroutine _currentAttack;

    [Header("Fall Attack")]
    [SerializeField] private float _openHandRayDelay;
    [SerializeField] private ParticleSystem _ray;


    void Start()
    {
        _anim ??= GetComponentInChildren<Animator>();
        _currentAttack = StartCoroutine(FallAttack());
        _originalY = transform.position.y;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _xpos += Speed * Time.deltaTime;
        transform.position = new Vector3(_xpos + Mathf.PingPong(_xOffset, _levitateFrequency), transform.position.y, transform.position.z);
        _xOffset += Time.deltaTime * _levitateFrequency;
    }

    private IEnumerator FallAttack()
    {
        float timer = 0;
        while (timer < _fallAttackDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //Go up
        while (transform.position.y < _yMax - .5f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _yMax), Time.deltaTime * _fallAttackSpeed / 3);
            yield return null;
        }

        //Hang
        yield return new WaitForSeconds(_fallHangTime);

        //Fall
        while (transform.position.y > _yMin + .5f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _yMin), Time.deltaTime * _fallAttackSpeed);
            yield return null;
        }

        EventManager.Trigger("BossHandFall");

        //Go to original position
        while (transform.position.y < _originalY - .1f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _originalY), Time.deltaTime * _fallAttackSpeed / 3);
            yield return null;
        }

        _currentAttack = StartCoroutine(FallAttack());
    }

    private IEnumerator RayAttack()
    {
        _anim.SetTrigger("Open");

        yield return new WaitForSeconds(_openHandRayDelay);

        _ray.Play();

        yield return new WaitForSeconds(_openHandRayDelay);

        _anim.SetTrigger("Close");
        StopCoroutine(_currentAttack);
        _currentAttack = StartCoroutine(FallAttack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            StopCoroutine(_currentAttack);
            _currentAttack = StartCoroutine(RayAttack());
        }
    }
}