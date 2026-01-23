using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour, IHazard
{
    [SerializeField] private float _delay;
    [SerializeField] private float _openTime;
    [SerializeField] private Animator _anim;
    [SerializeField] private BoxCollider2D _hurtCollider;

    void Start()
    {
        if(_anim == null) _anim = GetComponent<Animator>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            StartCoroutine(OnActivate());
        }
    }

    private IEnumerator OnActivate()
    {
        float timer = 0;
        while (timer < _delay)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _hurtCollider.enabled = true;
        _anim.SetTrigger("Activate");
        timer = 0;
        while (timer < _openTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _hurtCollider.enabled = false;
        _anim.SetTrigger("Deactivate");
    }

    public void Activate()
    {
            StartCoroutine(OnActivate());
    }

    public void DeActivate()
    {
    }
}
