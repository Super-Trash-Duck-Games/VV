using System.Collections;
using Unity.Mathematics;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CrocoBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private LayerMask _collisionLM;
    private Rigidbody2D _rb2d;
    [SerializeField] private float _lifetime;
    [SerializeField] private ParticleSystem _ps;
    private int dir = 1;

    private Coroutine _control;

    private void Awake()
    {
        _control = StartCoroutine(Control());
    }

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        if (transform.localScale.x < 0)
        {
            dir = -1;
            _ps.transform.localScale = new Vector3(-1, 1, 1);
        }

        StartCoroutine(Move());
    }

    public void TargetPlayer()
    {
        Transform target;
        if (_control != null)
            StopCoroutine(_control);
        target = FindFirstObjectByType<Kumkum>().transform;

        if (transform.localScale.x < 0)
            transform.right = transform.position - target.position;
        else
            transform.right = target.position - transform.position;
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0)
        {
            Die();
        }
    }

    private IEnumerator Control()
    {
        //float vert = Input.GetAxisRaw("Vertical");
        while (gameObject.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                Debug.Log("Fuckititty");

                transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime * _rotateSpeed * dir));
            }

            yield return null;
        }
    }

    private IEnumerator Move()
    {
        while (gameObject.activeSelf)
        {
            _rb2d.AddForce(transform.right * Time.deltaTime * _speed * dir, ForceMode2D.Impulse);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_collisionLM & (1 << collision.gameObject.layer)) != 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_collisionLM & (1 << collision.gameObject.layer)) != 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        _rb2d.linearVelocity = Vector3.zero;
        _ps.Play();
        Destroy(gameObject, .2f);
    }
}
